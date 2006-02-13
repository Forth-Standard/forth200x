\ required

\ This file is in the public domain. NO WARRANTY.

\ This implementation does not implement the requirements wrt MARKER
\ and FORGET (REQUIRED only includes each file once, whether a marker
\ was executed or not), so it is not a correct implementation on
\ systems that support these words.

\ The program uses the following words
\ from CORE :
\  environment? 0= : swap >r dup 2dup r> rot move ; cells r@ @ over ! cell+ 
\  2! BEGIN WHILE 2@ IF drop 2drop EXIT THEN REPEAT ELSE Variable 
\ from CORE-EXT :
\  2>r 2r@ 2r> true 
\ from BLOCK-EXT :
\  \ 
\ from EXCEPTION :
\  throw 
\ from FILE :
\  S" ( included 
\ from MEMORY :
\  allocate 
\ from STRING :
\  compare 
\ from TOOLS-EXT :
\  [IF] [THEN]

\ we use a linked list of names

s" X:parse-name" environment? 0= [if]
    s" parse-name.fs" included
[then]

: save-mem	( addr1 u -- addr2 u ) \ gforth
    \ copy a memory block into a newly allocated region in the heap
    swap >r
    dup allocate throw
    swap 2dup r> rot rot move ;

: name-add ( addr u listp -- )
    >r save-mem ( addr1 u )
    3 cells allocate throw \ allocate list node
    r@ @ over ! \ set next pointer
    dup r> ! \ store current node in list var
    cell+ 2! ;
    
: name-present? ( addr u list -- f )
    rot rot 2>r begin ( list R: addr u )
	dup
    while
	dup cell+ 2@ 2r@ compare 0= if
	    drop 2r> 2drop true EXIT
	then
	@
    repeat
    ( drop 0 ) 2r> 2drop ;

: name-join ( addr u list -- )
    >r 2dup r@ @ name-present? if
	r> drop 2drop
    else
	r> name-add
    then ;

variable included-names 0 included-names !

: included ( i*x addr u -- j*x )
    2dup included-names name-join
    included ;

: required ( i*x addr u -- i*x )
    2dup included-names @ name-present? 0= if
	included
    else
	2drop
    then ;

: require ( i*x "name" -- i*x )
    parse-name required ;

: include ( i*x "name" -- j*x )
    parse-name included ;
