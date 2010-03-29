\ DEESCAPE
\ if you pass a string through DEESCAPE and then SUBSTITUTE, you get
\ the original string

\ helper words
2variable deescape-buffer

: bounds over + swap ;

: count% ( c-addr len -- u )
    \ u is the number of % in the string
    0 rot rot bounds ?do
	i c@ [char] % = 1 and +
    loop ;

: deescape1 ( c-addr1 len1 c-addr2 len2 -- )
    \ copy c-addr1 len1 to c-addr2 len2, replacing % with %%
    drop rot rot bounds ?do
	i c@ dup [char] % = if
	    over c! char+ [char] % then
	over c! char+ loop
    drop ;

\ the word for the user
: deescape ( c-addr1 len1 -- c-addr2 len2 )
    \ replace string c-addr1 len1 with string c-addr2 len2, with all
    \ occurences of % replaced with %%.  c-addr2 len2 is valid until
    \ the next invocation of DEESCAPE.
    2dup count% over + deescape-buffer 2@ drop over resize throw swap
    2dup deescape-buffer 2!
    deescape1 deescape-buffer 2@ ;
