require test/tester.fs

{ PARSE-WORD abcd S" abcd" COMPARE -> 0 }
{ PARSE-WORD   abcde   S" abcde" COMPARE -> 0 }
\ test empty parse area
{ PARSE-WORD
  NIP -> 0 }
{ PARSE-WORD   
  nip -> 0 }

{ : parse-word-test ( "name1" "name2" -- n )
    PARSE-WORD PARSE-WORD COMPARE ; -> }
{ parse-word-test abcd abcd -> 0 }
{ parse-word-test  abcd   abcd   -> 0 }
{ parse-word-test abcde abcdf -> -1 }
{ parse-word-test abcdf abcde -> 1 }
{ parse-word-test abcde abcde
  -> 0 }
{ parse-word-test abcde abcde  
  -> 0 }

