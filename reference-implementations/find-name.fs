\ find-name and find-name-in

\ mostly by Bernd Paysan
\ <http://forth-standard.org/standard/core/FIND#reply-146>

\ this file is in the public domain

: >lower ( c1 -- c2 )
    dup 'A' 'Z' 1+ within bl and or ;
: istr= ( addr1 u1 addr2 u2 -- flag )
   rot over <> IF  2drop drop false  EXIT  THEN
    bounds ?DO
        dup c@ >lower I c@ >lower <> IF  drop false  unloop  EXIT  THEN
        1+
    LOOP  drop true ;

: find-name-in-helper  ( addr u wid -- nt / 0 )
    dup >r name>string 2over istr= IF
        rot drop r> -rot false
    ELSE  r> drop true  THEN ;

: find-name-in ( addr u wid -- nt / 0 )
    >r 0 -rot r> 
    ['] find-name-in-helper
    swap traverse-wordlist 2drop ;
    
: find-name {: c-addr u -- nt | 0 :}
    get-order
    [defined] gforth [if]
        [ ' locals >body ] literal swap 1+
    [else] [defined] vfxforth [if]
            [ ' localvars >body 3 cells + ] literal swap 1+
        [else]
            cr .( warning: FIND-NAME does not find locals )
        [then]
    [then]
    0 swap 0 ?do ( widn...widi nt|0 )
        dup 0= if
            drop c-addr u rot find-name-in
        else
            nip
        then
    loop ;
