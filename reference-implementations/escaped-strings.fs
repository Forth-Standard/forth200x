decimal

\ prelude

: c+! ( c c-addr1 -- )
    tuck c@ + swap c! ;

: endif postpone then ; immediate

\ from RfD v4

: PLACE         \ c-addr1 u c-addr2 --
\ *G Copy the string described by c-addr1 u to a counted string at
\ ** the memory address described by c-addr2.
   2dup 2>r			\ write count last
   1 chars + swap move
   2r> c!			\ to avoid in-place problems
;

: $,		\ caddr len --
\ *G Lay the string into the dictionary at *\fo{HERE}, reserve
\ ** space for it and *\fo{ALIGN} the dictionary.
   dup >r
   here place
   r> 1 chars + allot
   align
;

: addchar       \ char string --
\ *G Add the character to the end of the counted string.
   tuck count + c!
   1 swap c+!
;

: append	\ c-addr u $dest --
\ *G Add the string described by C-ADDR U to the counted string at
\ ** $DEST. The strings must not overlap.
   >r
   tuck  r@ count +  swap cmove          \ add source to end
   r> c+!                                \ add length to count
;

: extract2H	\ caddr len -- caddr' len' u
\ *G Extract a two-digit hex number in the given base from the
\ ** start of the* string, returning the remaining string
\ ** and the converted number.
   base @ >r  hex
   0 0 2over drop 2 >number 2drop drop
   >r  2 /string r>
   r> base !
;

create EscapeTable	\ -- addr
\ *G Table of translations for \a..\z.
   7 c,		\ \a
   8 c,		\ \b
   char c c,	\ \c
   char d c,	\ \d
   #27 c,	\ \e
   #12 c,	\ \f
   char g c,	\ \g
   char h c,	\ \h
   char i c,	\ \i
   char j c,	\ \j
   char k c,	\ \k
   #10 c,	\ \l
   char m c,	\ \m
   #10 c,	\ \n (Unices only)
   char o c,	\ \o
   char p c,	\ \p
   char " c,     \ \q
   #13 c,	\ \r
   char s c,	\ \s
   9 c,		\ \t
   char u c,	\ \u
   #11 c,	\ \v
   char w c,	\ \w
   char x c,	\ \x
   char y c,	\ \y
   0 c,		\ \z

create CRLF$	\ -- addr ; CR/LF as counted string
  2 c,  #13 c,  #10 c,

\ internal
: addEscape	\ caddr len dest -- caddr' len'
\ *G Add an escape sequence to the counted string at dest,
\ ** returning the remaining string.
   over 0=				\ zero length check
   if  drop  exit  endif
   >r					\ -- caddr len ; R: -- dest
   over c@ [char] x = if			\ hex number?
     1 /string extract2H r> addchar  exit
   endif
   over c@ [char] m = if			\ CR/LF pair?
     1 /string  #13 r@ addchar  #10 r> addchar  exit
   endif
   over c@ [char] n = if			\ CR/LF pair?
     1 /string  crlf$ count r> append  exit
   endif
   over c@ [char] a [char] z 1+ within if
     over c@ [char] a - EscapeTable + c@  r> addchar
   else
     over c@ r> addchar
   endif
   1 /string
;
\ external

: parse\"	\ caddr len dest -- caddr' len'
\ *G Parses a string up to an unescaped '"', translating '\'
\ ** escapes to characters much as C does. The
\ ** translated string is a counted string at *\i{dest}
\ ** The supported escapes (case sensitive) are:
\ *D \a      BEL (alert)
\ *D \b      BS (backspace)
\ *D \e      ESC (not in C99)
\ *D \f      FF (form feed)
\ *D \l      LF (ASCII 10)
\ *D \m      CR/LF pair - for HTML etc.
\ *D \n      newline - CRLF for Windows/DOS, LF for Unices
\ *D \q      double-quote
\ *D \r      CR (ASCII 13)
\ *D \t      HT (tab)
\ *D \v      VT
\ *D \z      NUL (ASCII 0)
\ *D \"      "
\ *D \xAB    Two char Hex numerical character value
\ *D \\      backslash itself
\ *D \       before any other character represents that character
   dup >r  0 swap c!			\ zero destination
   begin					\ -- caddr len ; R: -- dest
     dup
    while
     over c@ [char] " <>			\ check for terminator
    while
     over c@ [char] \ = if		\ deal with escapes
       1 /string r@ addEscape
     else				\ normal character
       over c@ r@ addchar  1 /string
     endif
   repeat then
   dup 					\ step over terminating "
   if  1 /string  endif
   r> drop
;

: readEscaped	\ "string" -- caddr
\ *G Parses an escaped string from the input stream according to
\ ** the rules of *\fo{parse\"} above, returning the address
\ ** of the translated counted string in *\fo{PAD}.
   source >in @ /string tuck		\ -- len caddr len
   pad parse\" nip
    - >in +!
   pad
;

: S\"		\ "string" -- caddr u
\ *G As *\fo{S"}, but translates escaped characters using
\ ** *\fo{parse\"} above.
    readEscaped count  state @ if
        postpone sliteral
    then
; IMMEDIATE
