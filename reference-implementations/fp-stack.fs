\ example implementation

\ public domain

\ This is an ANS Forth program
\ The program uses the following words
\ from CORE :
\  Constant here allot Variable ! : @ +! ; dup IF >r r> THEN POSTPONE 
\  immediate 
\ from CORE-EXT :
\  2>r 2r> 
\ from BLOCK-EXT :
\  \ 
\ from FILE :
\  ( 
\ from FLOAT :
\  falign floats f! fswap frot f@ >float d>f f* f+ f- f/ f0< f0= f< f>d 
\  fconstant fdrop fdup FLiteral floor fmax fmin fnegate fover fround 
\  represent 
\ from FLOAT-EXT :
\  df! df@ f** f. fabs facos facosh falog fasin fasinh fatan fatan2 fatanh 
\  fcos fcosh fe. fexp fexpm1 fln flnp1 flog fs. fsin fsincos fsinh fsqrt 
\  ftan ftanh f~ sf! sf@ 

\ After loading this program, a system is an ANS Forth system with a
\ separate floating-point stack.

\ Well, not quite: The following things are missing:

\ 1) The text interpreter does not put FP numbers on the new FP stack.

\ 2) The environmental query FLOAT-STACK is not answered correctly.

\ This has hardly been tested.

\ 12.3.3 The size of a floating-point stack shall be at least 6 items.
100 constant fp-stack-size

falign here fp-stack-size floats allot constant fp-stack

variable fp \ FP stack pointer
fp-stack fp !

: >fs ( r -- ) ( F: -- r )
    fp @ f!
    1 floats fp +! ;

: 2>fs ( r1 r2 -- ) ( F: -- r1 r2 )
    fswap >fs >fs ;

: 3>fs ( r1 r2 r3 -- ) ( F: -- r1 r2 r3 )
    frot >fs 2>fs ;

: fs> ( F: r -- r )
    -1 floats fp +!
    fp @ f@ ;

: 2fs> ( -- r1 r2 ) ( F: r1 r2 -- )
    fs> fs> fswap ;

: 3fs> ( -- r1 r2 r3 ) ( F: r1 r2 r3 -- )
    2fs> fs> frot frot ;

: >float ( c-addr u -- true | false ) ( F: -- r |  )
    >float dup if
        >r >fs r>
    then ;

: d>f ( d -- ) ( F: -- r )
    d>f >fs ;

: f! ( f-addr -- ) ( F: r -- )
    >r fs> r> f! ;

: f* ( F: r1 r2 -- r3 )
    2fs> f* >fs ;

: f+ ( F: r1 r2 -- r3 )
    2fs> f+ >fs ;

: f- ( F: r1 r2 -- r3 )
    2fs> f- >fs ;

: f/ ( F: r1 r2 -- r3 )
    2fs> f/ >fs ;

: f0< ( -- flag ) ( F: r -- )
    fs> f0< ;

: f0= ( -- flag ) ( F: r -- )
    fs> f0= ;

: f< ( -- flag ) ( F: r1 r2 -- )
    2fs> f< ;

: f>d ( -- d ) ( F: r -- )
    fs> f>d ;

: f@ ( f-addr -- ) ( F: -- r )
    f@ >fs ;

: fconstant ( "<spaces>name" -- ) ( F: r -- )
    fs> fconstant ;

: fdrop ( F: r -- )
    fs> fdrop ; \ not very efficient

: fdup ( F: r -- r r )
    fs> fdup 2>fs ;

: fliteral ( compilation: F: r -- ) ( run-time: F: -- r )
    fs> postpone fliteral postpone >fs ; immediate

: floor ( F: r1 -- r2 )
    fs> floor >fs ;

: fmax ( F: r1 r2 -- r3 )
    2fs> fmax >fs ;

: fmin ( F: r1 r2 -- r3 )
    2fs> fmin >fs ;

: fnegate ( F: r1 -- r2 )
    fs> fnegate >fs ;

: fover ( F: r1 r2 -- r1 r2 r1 )
    2fs> fover 3>fs ;

: frot ( F: r1 r2 r3 -- r2 r3 r1 )
    3fs> frot 3>fs ;

: fround ( F: r1 -- r2 )
    fs> fround >fs ;

: fswap ( F: r1 r2 -- r2 r1 )
    2fs> fswap 2>fs ;

: represent ( c-addr u -- n flag1 flag2 ) ( F: r -- )
    2>r fs> 2r> represent ;

: df! ( df-addr -- ) ( F: r -- )
    >r fs> r> df! ;

: df@ ( df-addr -- ) ( F: -- r )
    df@ >fs ;

: f** ( F: r1 r2 -- r3 )
    2fs> f** >fs ;

: f. ( F: r -- )
    fs> f. ;

: fabs ( F: r1 -- r2 )
    fs> fabs >fs ;

: facos ( F: r1 -- r2 )
    fs> facos >fs ;

: facosh ( F: r1 -- r2 )
    fs> facosh >fs ;

: falog ( F: r1 -- r2 )
    fs> falog >fs ;

: fasin ( F: r1 -- r2 )
    fs> fasin >fs ;

: fasinh ( F: r1 -- r2 )
    fs> fasinh >fs ;

: fatan ( F: r1 -- r2 )
    fs> fatan >fs ;

: fatan2 ( F: r1 r2 -- r3 )
    2fs> fatan2 >fs ;

: fatanh ( F: r1 -- r2 )
    fs> fatanh >fs ;

: fcos ( F: r1 -- r2 )
    fs> fcos >fs ;

: fcosh ( F: r1 -- r2 )
    fs> fcosh >fs ;

: fe. ( F: r -- )
    fs> fe. ;

: fexp ( F: r1 -- r2 )
    fs> fexp >fs ;

: fexpm1 ( F: r1 -- r2 )
    fs> fexpm1 >fs ;

: fln ( F: r1 -- r2 )
    fs> fln >fs ;

: flnp1 ( F: r1 -- r2 )
    fs> flnp1 >fs ;

: flog ( F: r1 -- r2 )
    fs> flog >fs ;

: fs. ( F: r -- )
    fs> fs. ;

: fsin ( F: r1 -- r2 )
    fs> fsin >fs ;

: fsincos ( F: r1 -- r2 r3 )
    fs> fsincos 2>fs ;

: fsinh ( F: r1 -- r2 )
    fs> fsinh >fs ;

: fsqrt ( F: r1 -- r2 )
    fs> fsqrt >fs ;

: ftan ( F: r1 -- r2 )
    fs> ftan >fs ;

: ftanh ( F: r1 -- r2 )
    fs> ftanh >fs ;

: f~ ( -- flag ) ( F: r1 r2 r3 -- )
    3fs> f~ ;

: sf! ( sf-addr -- ) ( F: r -- )
    >r fs> r> sf! ;

: sf@ ( sf-addr -- ) ( F: -- r )
    sf@ >fs ;

\ code for FP number input
s" gforth" environment? [if]
    s" 0.6.2" compare 0> [if]
:noname ( c-addr u -- ... xt )
    2dup sfnumber
    IF
	>fs 2drop [comp'] FLiteral
    ELSE
	defers compiler-notfound1
    ENDIF ;
IS compiler-notfound1

:noname ( c-addr u -- ... xt )
    2dup sfnumber
    IF
	>fs 2drop ['] noop
    ELSE
	defers interpreter-notfound1
    ENDIF ;
IS interpreter-notfound1
    [else]
      .( Please insert adapted FP number input code for your Gforth here ) abort
    [then]
[else]
    \ for other systems than Gforth
    .( Please insert adapted FP number input code for your system here ) abort
[then]