require ttester.fs

wordlist constant fntwl
get-current fntwl set-current
: fnt1 25 ;
: fnt2 34 ; immediate
set-current
t{ s" fnt1" fntwl find-name-in name>interpret execute -> 25 }t
t{ : fnt3 [ s" fnt1" fntwl find-name-in name>compile execute ] ; fnt3 -> 25 }t
t{ s" fnt1" fntwl find-name-in name>string s" fnt1" compare -> 0 }t
t{ s" fnt2" fntwl find-name-in name>interpret execute -> 34 }t
t{ s" fnt2" fntwl find-name-in name>compile   execute -> 34 }t
: fnt4 fntwl find-name-in name>compile execute ; immediate
t{ s" fnt2" ] fnt4 [ -> 34 }t
t{ s" fnt0" fntwl find-name-in -> 0 }t
: fnt5 42 ;
: fnt6 51 ; immediate
t{ s" fnt5" find-name name>interpret execute -> 42 }t
t{ : fnt7 [ s" fnt5" find-name name>compile execute ] ; fnt7 -> 42 }t
t{ s" fnt5" find-name name>string s" fnt5" compare -> 0 }t
t{ s" fnt6" find-name name>interpret execute -> 51 }t
t{ s" fnt6" find-name name>compile   execute -> 51 }t
: fnt8 find-name name>compile execute ; immediate
t{ s" fnt6" ] fnt8 [ -> 51 }t
t{ s" fnt0hfshkshdfskl" find-name -> 0 }t
