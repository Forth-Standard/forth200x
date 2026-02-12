\ This file is in the public domain. NO WARRANTY.

\ The program uses the following words
\ from CORE :
\  : Create , ; @ execute ' dup postpone Literal 2drop IF ELSE drop THEN 
\  swap over and >body 1+ ! cell+ LOOP >r cells + r> allot does> 2dup i rot 
\  unloop EXIT +LOOP type space BEGIN WHILE REPEAT 0= immediate 
\ from CORE-EXT :
\  case of endof endcase u> ?DO tuck 2>r <> 2r> 
\ from CORE-EXT-2012 :
\  Defer IS action-of parse-name 
\ from BLOCK-EXT :
\  \ 
\ from DOUBLE :
\  2Literal 
\ from EXCEPTION :
\  throw 
\ from FILE :
\  ( 
\ from FLOATING :
\  FLiteral >float 
\ from TOOLS-EXT :
\  state 
\ from TOOLS-EXT-2012 :
\  name>interpret name>compile name>string 
\ from non-ANS :
\  find-name snumber? latestxt >name interpret replace-word 

\ The non-standard words are implemented on Gforth (development):

\ 'find-name' ( c-addr u - nt | 0  ) gforth-0.2
\   Find the name c-addr u in the current search order.  Return its nt,
\   if found, otherwise 0.

\   find-name has been accepted into Forth 200x in 2018

\ 'snumber?' ( c-addr u -- 0 / n -1 / d 0> )
\       Convert string to an integer number, single or double.

\ 'latestxt' ( - xt  ) gforth-0.6
\    xt is the execution token of the most recent word defined in the
\    current section.

\ '>name' ( xt - nt|0  ) gforth-0.2 "to-name"
\    For most words (all words with the default implementation of
\    'name>interpret'), '>name' is the inverse of 'name>interpret': for these
\    words 'nt name>interpret' produces xt.  [...]

\ The following two words are used for plugging 'interpret1' into the
\ existing text-interpreter setup instead of 'interpret'.  For other
\ Forth systems you probably need to do it differently, and maybe even
\ replace INTERPRET1 with something else.

\ : interpret ( ... -- ... ) \ gforth-internal
\     \ interpret/compile the (rest of the) input buffer

\ 'replace-word' ( xt1 xt2 -  ) gforth-1.0
\     make xt2 do xt1, both need to be colon definitions

    
: translate: ( xt-int xt-comp xt-post "name" -- )
    create , , , ; \ postponing has offset 0, compiling cell+, interpreting 2 cells +

: postponing ( ... translation-token -- )
    @ execute ;


: undefined-word ( -- )
    #-13 throw ;

' undefined-word dup dup translate: translate-none

: nop ;

: lit, ( n -- )
    postpone literal ;

: litlit, ( n -- )
    lit, postpone lit, ;

' nop ' lit, ' litlit, translate: translate-cell
    
: 2lit, ( n1 n2 -- )
    postpone 2literal ;

: 2lit2lit, ( n1 n2 -- )
    2lit, postpone 2lit, ;

' nop ' 2lit, ' 2lit2lit, translate: translate-dcell

: flit, ( r -- )
    postpone fliteral ;

: flitflit, ( r -- )
    flit, postpone flit, ;

' nop ' flit, ' flitflit, translate: translate-float

: name-int ( ... nt -- ... )
    name>interpret execute ;

: name-comp ( ... nt -- ... )
    name>compile execute ;

: name-post ( nt -- )
    lit, postpone name-comp ;

' name-int ' name-comp ' name-post translate: translate-name

: rec-none ( c-addr u -- translation )
    2drop translate-none ;

: rec-name ( c-addr u -- translation )
    find-name dup if
        translate-name
    else
        drop translate-none
    then ;

: rec-number ( c-addr u -- translation )
    snumber? case
        0  of translate-none endof
        -1 of translate-cell endof
        translate-dcell swap
    endcase ;

: rec-float ( c-addr u -- translation )
    \ for production use you probably do not want to accept everything
    \ that >FLOAT accepts.
    >float if translate-float else translate-none then ;

: set-recs ( xt_u ... xt_1 u xt -- )
    \ stored in the sequence in the order u xt_1 ... xt_u
    over #16 u> #-80 and throw
    >body over 1+ 0 ?do
        tuck ! cell+ loop
    drop ;

: get-recs ( xt -- xt_u ... xt_1 u )
    >body dup @ dup >r cells + r> 1+ 0 ?do
        dup @ swap 1 cells - loop
    drop ;

: rec-sequence: ( xtu .. xt1 u "name" -- )
    create #17 cells allot latestxt set-recs
  does> ( c-addr u -- translation )
    dup @ cells swap cell+ tuck + swap ?do
        2dup i rot rot 2>r @ execute dup translate-none <> if
            2r> 2drop unloop exit then
        drop 2r>
    1 cells +loop
    rec-none ;

' rec-float ' rec-number ' rec-name 3 rec-sequence: rec-default

defer rec-forth ' rec-default is rec-forth

: recs ( -- )
    action-of rec-forth get-recs 0 ?do
        >name name>string type space
    loop ;

: interpret1 ( ... -- ... )
    begin
        parse-name dup while
            rec-forth state @ 2 + cells + @ execute
    repeat
    2drop ;

: postpone ( "name" -- )
    parse-name dup 0= #-16 and throw
    rec-forth postponing ; immediate

' interpret1 ' interpret replace-word
