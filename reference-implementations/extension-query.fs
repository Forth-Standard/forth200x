\ reference implementation for extension queries

\ public domain

\ This implementation can be used both by systems and programs to
\ implement extension queries; it loads the implementation of the
\ extension from an appropriately named file, if the system does not
\ have the extension already.

\ This implementation can be stacked: the system uses it to load its
\ implementation of the extension (if the extension is not already
\ present); and the program uses another instance of this
\ implementation to check what the system has, and load the reference
\ implementation (where available) if necessary.

\ the main downside of this implementation is the location of the
\ extension-dir: If you use an absolute directory name, you have to
\ set it when you install the directory on each system; if you use a
\ relative name, you can only run the program from a specific working
\ directory.

\ here are parameters that that you may have to change:

: extension-dir ( -- c-addr u )
    \ directory containing the files implementing the extension;
    \ this directory must only contain files for official extensions,
    \ otherwise this will not be a correct implementation of the proposal
    \ you can find a directory with reference implementations at
    \ http://www.forth200x.org/extensions/
    \ or http://www.forth200x.org/forth200x-code.zip
    s" forth200x-code/extensions/" ;

: extension-prefix ( -- c-addr u )
    \ string at the start of every extension query
    s" x:" ;

: extension-file-extension ( -- c-addr u )
    \ the file extension of the implementation files
    s" .fs" ;

\ end of parameters

31 constant max-word-length

create eq-path
max-word-length extension-dir nip + extension-file-extension nip + chars allot

extension-dir eq-path swap chars move

eq-path extension-dir nip chars + constant eq-filename

: make-eq-filename ( c-addr1 u1 -- c-addr2 u2 )
    dup >r
    eq-filename swap chars move
    extension-file-extension eq-filename r@ chars + swap chars move
    eq-path r> extension-dir nip + extension-file-extension nip + ;

: string-prefix? ( c-addr1 u1 c-addr2 u2 -- f ) \ gforth
    tuck 2>r min 2r> compare 0= ;

variable extension-list 0 extension-list !
\ linked list of extension names; each node: link, then counted string

\ I would have liked to do "['] defnoop execute-parsing" here, but
\ that's not stadard (yet:-).

: insert-extension ( addr u -- )
    dup cell+ char+ allocate throw
    extension-list @ over ! dup extension-list !
    cell+ 2dup c! \ store count
    char+ swap chars move ;

: lookup-extension ( c-addr u -- f )
    \ true if extension is in list
    2>r extension-list @ begin ( list r: c-addr u )
	dup while
	    dup cell+ count 2r@ compare 0= if
		drop 2r> 2drop true exit then
	    @ repeat
    2r> 2drop ;

s" extension-query" insert-extension \ this is already loading

: to-lower ( c1 -- c2 )
    dup [char] A [char] Z 1+ within if
	[char] A - [char] a +
    then ;

: save-lower ( c-addr1 u -- c-addr2 u )
    \ c-addr1 u is a string; c-addr2 u is the lower-case variant of
    \ the string in newly ALLOCATEd memory.
    dup allocate throw swap 2dup 2>r ( c-addr1 c-addr2 u r: c-addr2 u )
    over chars + swap ?do
	dup c@ to-lower i c!
    char+ 1 chars +loop
    drop 2r> ;

: environment? ( c-addr u -- false | ... true )
    2dup 2>r environment? dup if ( f ) \ system answers query in another way
	2r> 2drop exit then
    2r> save-lower 2>r
    2r@ extension-prefix string-prefix? 0= if \ not an extension query
	2r> 2drop exit then
    drop 2r> extension-prefix nip /string ( c-addr1 u1 )
    dup max-word-length u> if \ the name is too long
	2drop false exit then
    2dup lookup-extension if \ the extension is already (being) loaded
	2drop true exit then
    2dup make-eq-filename r/o open-file if ( c-addr1 u1 fid ) \ doesn't exist
	drop 2drop false exit then
    >r insert-extension ( r:fid ) \ break cycle before including
    r> include-file
    true ;


\ tests for this implementation

0 [if]
require test/tester.fs

\ { s" core" environment? -> true true } \ !! could also be false
{ s" Y:blabla" environment? -> false }
{ s" X:abcdefghijabcdefghijabcdefghijabcdefghij" environment? -> false }
{ s" X:extension-query" environment? -> true }
{ s" X:non-existant file" environment? -> false }
{ s" X:deferred" environment? -> true }
{ s" X:deferred" environment? -> true } \ try again
{ s" X:extension-query" environment? -> true } \ test lookup again
[then]
