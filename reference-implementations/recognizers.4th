\ This file is in the public domain. NO WARRANTY.

: translate: ( xt-int xt-comp xt-post "name" -- )
    create , , , ; \ postponing has offset 0, compiling cell+, interpreting 2 cells +

: interpret2 ( ... -- ... )
    begin
        parse-name dup while
            rec-forth state @ 2 + cells + @ execute
    repeat
    2drop ;

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
        dup @ swap cell- loop
    drop ;

: recs ( -- )
    action-of rec-forth get-recs 0 ?do
        >name name>string type space
    loop ;

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

: postpone ( "name" -- )
    parse-name dup 0= #-16 and throw
    rec-forth postponing ; immediate

\ ' interpret2 ' interpret replace-word
