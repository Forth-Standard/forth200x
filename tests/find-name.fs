require ttester.fs

: >lower ( c1 -- c2 )
    dup 'A' 'Z' 1+ within bl and or ;
: istr= ( addr1 u1 addr2 u2 -- flag )
   rot over <> IF  2drop drop false  EXIT  THEN
    bounds ?DO
        dup c@ >lower I c@ >lower <> IF  drop false  unloop  EXIT  THEN
        1+
    LOOP  drop true ;

wordlist constant fntwl
get-current fntwl set-current
: fnt1 25 ;
: fnt2 34 ; immediate
set-current
t{ s" fnt1" fntwl find-name-in name>interpret execute -> 25 }t
t{ : fnt3 [ s" fnt1" fntwl find-name-in name>compile execute ] ; fnt3 -> 25 }t
t{ s" fnt1" fntwl find-name-in name>string s" fnt1" istr= -> true }t
t{ s" fnt2" fntwl find-name-in name>interpret execute -> 34 }t
t{ s" fnt2" fntwl find-name-in name>compile   execute -> 34 }t
: fnt4 fntwl find-name-in name>compile execute ; immediate
t{ s" fnt2" ] fnt4 [ -> 34 }t
t{ s" fnt0" fntwl find-name-in -> 0 }t
: fnt5 42 ;
: fnt6 51 ; immediate
t{ s" fnt5" find-name name>interpret execute -> 42 }t
t{ : fnt7 [ s" fnt5" find-name name>compile execute ] ; fnt7 -> 42 }t
t{ s" fnt5" find-name name>string s" fnt5" istr= -> true }t
t{ s" fnt6" find-name name>interpret execute -> 51 }t
t{ s" fnt6" find-name name>compile   execute -> 51 }t
: fnt8 find-name name>compile execute ; immediate
t{ s" fnt6" ] fnt8 [ -> 51 }t
t{ s" fnt0hfshkshdfskl" find-name -> 0 }t
t{ s\" s\"" find-name name>interpret execute bla" s" bla" compare -> 0 }t
t{ : fnt9 [ s\" s\"" find-name name>compile execute ble" ] ; -> }t
t{ fnt9 s" ble" compare -> 0 }t
: fnta find-name name>interpret execute ; immediate
t{ : fntb [ s\" s\"" ] fnta bli" 2literal ; -> }t
t{ fntb s" bli" compare -> 0 }t
: fnt-interpret-words ( ... "rest-of-line" -- ... )
    begin
        parse-name dup while
            2dup find-name dup 0= -13 and throw
            nip nip state @ if name>compile else name>interpret then execute
    repeat 2drop ;
t{ fnt-interpret-words fnt5 value fntd fnt6 to fntd
fntd -> fnt6 }t
t{ fnt-interpret-words s" yyy" : fnte s" yyy" ; fnte compare
-> 0 }t
t{ fnt-interpret-words : fntc {: xa xb :} xa xb to xa to xb xa xb s" xxx" ; s" xxx" swap fntc compare
-> 0 }t
