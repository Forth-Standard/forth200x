\ taken from the RfD

require ./tester.fs

( The same tests as for S" )

{ : GC5 S\" XY" ; -> }
{ GC5 SWAP DROP -> 2 }
{ GC5 DROP DUP C@ SWAP CHAR+ C@ -> 58 59 }

( The following are inspired by the gForth test suite )

{ S\" " SWAP DROP -> 0 }

{ S\" \a" SWAP C@ -> 1 07 } \ BEL Bell
{ S\" \b" SWAP C@ -> 1 08 } \ BS  Backspace
{ S\" \e" SWAP C@ -> 1 1B } \ ESC Escape
{ S\" \f" SWAP C@ -> 1 0C } \ FF  Formfeed
{ S\" \l" SWAP C@ -> 1 0A } \ LF  Linefeed
{ S\" \q" SWAP C@ -> 1 22 } \ "   Double Quote
{ S\" \r" SWAP C@ -> 1 0D } \ CR  Carage Return
{ S\" \t" SWAP C@ -> 1 09 } \ TAB Horisontal Tab
{ S\" \v" SWAP C@ -> 1 0B } \ VT  Virtical Tab
{ S\" \z" SWAP C@ -> 1 00 } \ NUL No Character
{ S\" \"" SWAP C@ -> 1 22 } \ "   Double Quote
{ S\" \\" SWAP C@ -> 1 5C } \ \   Back Slash

{ S\" \n" 2DROP -> }                                \ System dependent
{ S\" \m" SWAP DUP C@ SWAP CHAR+ C@ -> 2 0D 0A }    \ CR\LF pair
{ S\" \x1Fa" SWAP DUP C@ SWAP CHAR+ C@ -> 2 1F 61 } \ Specified Char

{ S\" S\\\" \\a\"" EVALUATE SWAP C@ -> 1 7 }
