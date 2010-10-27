\ This implements the extended-locals proposal in Forth200x

\ This file is in the public domain. NO WARRANTY.

\ The program uses the following words
\ from CORE :
\  environment? drop : 0= ; Constant >r r> or POSTPONE IF EXIT THEN 
\  immediate BEGIN 2dup rot 1+ 2drop DO LOOP 
\ from CORE-EXT :
\  pick AGAIN 
\ from BLOCK-EXT :
\  \ 
\ from FILE :
\  S" ( 
\ from LOCAL :
\  (local) 
\ from STRING :
\  compare 
\ from X:parse-name :
\  parse-name 

s" X:parse-name" environment? drop

: str= ( c-addr1 u1 c-addr2 u2 -- f ) \ gforth
    compare 0= ;

12345 constant undefined-value

: match-or-end? ( c-addr1 u1 c-addr2 u2 -- f )
    2 pick 0= >r str= r> or ;

: scan-args ( 0 c-addr1 u1 -- c-addr1 u1 ... c-addrn un n c-addrn+1 un+1 )
    begin
	2dup s" |"  match-or-end? 0= while
	2dup s" --" match-or-end? 0= while
	2dup s" :}" match-or-end? 0= while
	rot 1+ parse-name
    again then then then ;

: scan-undefined ( n c-addr1 u1 -- c-addr1 u1 ... c-addrn un n c-addrn+1 un+1 )
    2dup s" |" str= 0= if
	exit
    then
    2drop parse-name
    begin
	2dup s" --" match-or-end? 0= while
	2dup s" :}" match-or-end? 0= while
	rot 1+ parse-name
	postpone undefined-value
    again then then ;

: scan-end ( c-addr1 u1 -- c-addr2 u2 )
    begin
	2dup s" :}" match-or-end? 0= while
	2drop parse-name
    repeat ;

: define-locals ( c-addr1 u1 ... c-addrn un n -- )
    0 ?do
	(local)
    loop
    0 0 (local) ;

: {: ( -- )
    0 parse-name scan-args scan-undefined scan-end 2drop define-locals
; immediate



