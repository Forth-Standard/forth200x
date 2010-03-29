\ rough version, does not free memory

: s+ { addr1 u1 addr2 u2 -- addr u }
    u1 u2 + allocate throw { addr }
    addr1 addr u1 move
    addr2 addr u1 + u2 move
    addr u1 u2 +
;

: dir-part ( c-addr1 u1 -- c-addr1 u2 )
    \ the prefix of c-addr1 u1 up to and including the last "/"
    begin
	dup while
	    1- 2dup + c@ '/' =
	until
	1+
    then ;

2variable including-dir
0 0 including-dir 2!

: included ( ... c-addr u -- ... )
    including-dir 2@ 2>r
    2dup dir-part including-dir 2!
    r/o open-file throw include-file
    2r> including-dir 2! ;

: include-name-abs ( c-addr u1 -- c-addr2 u2 )
    including-dir 2@ 2swap s+ ;

: f" ( "name" -- c-addr u )
    \ interpreter only for now
    '" parse include-name-abs ;

: include parse-name include-name-abs included ;

