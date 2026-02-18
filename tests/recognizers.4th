\ for now this test file assumes a full-featured system with
\ FIND-NAME, floating point etc.  Disabling various tests based on the
\ absence of features is future work.

require ttester.fs

decimal \ otherwise the floating-point recognizer may fail

TESTING postpone name cell dcell

t{ : postpone-cell postpone #123 ; immediate -> }t
t{ : test-postpone-cell postpone-cell ; -> }t
t{ test-postpone-cell -> #123 }t

t{ : postpone-name postpone test-postpone-cell ; immediate -> }t
t{ : test-postpone-name postpone-name ; -> }t
t{ test-postpone-name -> #123 }t

t{ : postpone-imm postpone postpone-cell ; immediate -> }t
t{ : test-postpone-imm postpone-imm ;  -> }t
t{ test-postpone-imm -> #123 }t

t{ : postpone-dcell postpone #1234. ; immediate -> }t
t{ : test-postpone-dcell postpone-dcell ; -> }t
t{ test-postpone-dcell -> #1234. }t

s" floating" environment? [if] [if]
        TESTING postpone float
        t{ : postpone-float postpone 1234.5e ; immediate -> }t
        t{ : test-postpone-float postpone-float ; -> }t
        t{ test-postpone-float -> 1234.5e }t
[then] [then]
        
TESTING rec-name rec-number rec-float rec-none rec-forth

t{ : #12345 #12345 ; -> }t

t{ s" dup"     rec-name -> s" dup" find-name translate-name }t
t{ s" ("       rec-name -> s" ("   find-name translate-name }t
t{ s" if"      rec-name -> s" if"  find-name translate-name }t
t{ s" unr-str" rec-name -> translate-none }t
t{ s" #123"    rec-name -> translate-none }t
t{ s" #12345"  rec-name -> s" #12345" find-name translate-name }t
t{ s" #1234."  rec-name -> translate-none }t
t{ s" 1234.5e" rec-name -> translate-none }t

t{ s" dup"     rec-number -> translate-none }t
t{ s" unr-str" rec-number -> translate-none }t
t{ s" #123"    rec-number -> #123   translate-cell  }t
t{ s" #12345"  rec-number -> #12345 translate-cell }t
t{ s" #1234."  rec-number -> #1234. translate-dcell }t
t{ s" 1234.5e" rec-number -> translate-none }t

t{ s" dup"     rec-float -> translate-none }t
t{ s" unr-str" rec-float -> translate-none }t
t{ s" #123"    rec-float -> translate-none }t
t{ s" #12345"  rec-float -> translate-none }t
t{ s" #1234."  rec-float -> translate-none }t
t{ s" 1234.5e" rec-float -> 1234.5e translate-float }t

t{ s" dup"     rec-none -> translate-none }t
t{ s" unr-str" rec-none -> translate-none }t
t{ s" #123"    rec-none -> translate-none }t
t{ s" #12345"  rec-none -> translate-none }t
t{ s" #1234."  rec-none -> translate-none }t
t{ s" 1234.5e" rec-none -> translate-none }t

t{ s" dup"     rec-forth -> s" dup" find-name translate-name }t
t{ s" ("       rec-forth -> s" ("   find-name translate-name }t
t{ s" if"      rec-forth -> s" if"  find-name translate-name }t
t{ s" unr-str" rec-forth -> translate-none }t
t{ s" #123"    rec-forth -> #123   translate-cell  }t
t{ s" #12345"  rec-forth -> s" #12345" find-name translate-name }t
t{ s" #1234."  rec-forth -> #1234. translate-dcell }t
t{ s" 1234.5e" rec-forth -> 1234.5e translate-float }t

TESTING not testing recs
\ the output cannot be checked, and the stack effect is trivial

TESTING rec-sequence:

t{ ' rec-name ' rec-number 2  rec-sequence: rec-number-name -> }t
t{ s" #12345"  rec-number-name -> #12345 translate-cell }t
t{ s" 1234.5e" rec-number-name -> translate-none }t

t{ action-of rec-forth constant default-rec-forth -> }t
t{ ' rec-number-name is rec-forth -> }t
t{ s" 1234.5e" rec-forth -> translate-none }t
t{ default-rec-forth is rec-forth -> }t
t{ s" 1234.5e" rec-forth -> 1234.5e translate-float }t

TESTING get-recs set-recs

t{ ' rec-number-name get-recs -> ' rec-name ' rec-number 2 }t
t{ ' rec-number-name is rec-forth -> }t
t{ action-of rec-forth get-recs -> ' rec-name ' rec-number 2 }t
t{ default-rec-forth is rec-forth -> }t
t{ ' rec-number-name get-recs ' rec-float swap 1+ ' rec-number-name set-recs }t
t{ s" 1234.5e" rec-number-name -> 1234.5e translate-float }t
t{ ' rec-number-name get-recs -> ' rec-name ' rec-number ' rec-float 3 }t

TESTING translate-none translate-cell translate-dcell translate-float translate-name

: rec-test-translates ( c-addr u -- translation )
    2dup s" test-translate-cell"  compare 0= if 2drop #123               translate-cell  exit then
    2dup s" test-translate-dcell" compare 0= if 2drop #123.              translate-dcell exit then
    2dup s" test-translate-float" compare 0= if 2drop 123e               translate-float exit then
    2dup s" test-translate-swap"  compare 0= if 2drop s" swap" find-name translate-name  exit then
    2dup s" test-translate-if"    compare 0= if 2drop s" if"   find-name translate-name  exit then
    2drop translate-none ;

' rec-name ' rec-test-translates 2 rec-sequence: rec-seq-translates

: evaluate-seq-translates ( ... c-addr u -- ... )
    action-of rec-forth >r ['] rec-seq-translates is rec-forth
    ['] evaluate catch
    r> is rec-forth
    throw ;

\ interpretation
t{ s" test-translate-cell"     evaluate-seq-translates -> #123  }t
t{ s" test-translate-dcell"    evaluate-seq-translates -> #123. }t
t{ s" test-translate-float"    evaluate-seq-translates -> 123e  }t
t{ 1 2 s" test-translate-swap" evaluate-seq-translates -> 2 1   }t
t{ s" #012no-word" ' evaluate-seq-translates catch nip nip -> #-13 }t

\ compilation
t{ s" : ttc test-translate-cell  ;" evaluate-seq-translates -> }t
t{ s" : ttd test-translate-dcell ;" evaluate-seq-translates -> }t
t{ s" : ttf test-translate-float ;" evaluate-seq-translates -> }t
t{ s" : tts test-translate-swap  ;" evaluate-seq-translates -> }t
t{ s" : tti test-translate-if test-translate-cell else test-translate-dcell then ;" evaluate-seq-translates -> }t
t{ s" : ttn #012no-word ;" ' evaluate-seq-translates catch [ nip nip -> #-13 }t

t{ ttc      -> #123  }t
t{ ttd      -> #123. }t
t{ ttf      -> 123e  }t
t{ 1 2 tts  -> 2 1   }t
t{ 2 tti    -> #123  }t
t{ 0 tti    -> #123. }t

\ postpone
t{ s" : tptc1 postpone test-translate-cell  ; immediate" evaluate-seq-translates -> }t
t{ s" : tptd1 postpone test-translate-dcell ; immediate" evaluate-seq-translates -> }t
t{ s" : tptf1 postpone test-translate-float ; immediate" evaluate-seq-translates -> }t
t{ s" : tpts1 postpone test-translate-swap  ; immediate" evaluate-seq-translates -> }t
t{ s" : tpti1 postpone test-translate-if    ; immediate" evaluate-seq-translates -> }t
t{ s" : tptn1 postpone #012no-word ;" ' evaluate-seq-translates catch [ nip nip -> #-13 }t

t{ : tptc2 tptc1 ; -> }t
t{ : tptd2 tptd1 ; -> }t
t{ : tptf2 tptf1 ; -> }t
t{ : tpts2 tpts1 ; -> }t
t{ : tpti2 tpti1 1 else 2 then ; -> }t

t{ tptc2      -> #123  }t
t{ tptd2      -> #123. }t
t{ tptf2      -> 123e  }t
t{ 1 2 tpts2  -> 2 1   }t
t{ 2 tpti2    -> 1     }t
t{ 0 tpti2    -> 2     }t

TESTING translate:

\ first, an absurd translation token
t{ : ttti 9 ; -> }t
t{ : tttc 8 ; -> }t
t{ variable tttpv 5 tttpv ! -> }t
t{ : tttp state @ tttpv ! ; -> }t
t{ ' ttti ' tttc ' tttp translate: translate-ttt -> }t

\ next, a translation token for a two-float literal (e.g., for complex)
t{ : 2flit, ( r1 r2 -- ) fswap postpone fliteral postpone fliteral ; -> }t
t{ :noname ; ' 2flit, :noname 2flit, postpone 2flit, ; translate: translate-2float }t

: rec-ttt ( c-addr u -- translation )
    2dup s" test-ttt"    compare 0= if 2drop translate-ttt exit then
    2dup s" test-2float" compare 0= if 2drop 3e 4e translate-2float exit then
    rec-none ;

t{ ' rec-name ' rec-ttt 2 ' rec-seq-translates set-recs -> }t

cr
t{         s" test-ttt"              evaluate-seq-translates         -> 9 }t
t{         s" ] test-ttt ["          evaluate-seq-translates         -> 8 }t
t{ tttpv @ s" ] postpone test-ttt [" evaluate-seq-translates tttpv @ -> 5 -1 }t

t{ s" test-2float"     evaluate-seq-translates -> 3e 4e }t
t{ s" : t2fc test-2float ;" evaluate-seq-translates -> }t
t{ t2fc -> 3e 4e }t
t{ s" : t2fp1 postpone test-2float ; immediate" evaluate-seq-translates -> }t
t{ : t2fp2 t2fp1 ; -> }t
t{ t2fp2 -> 3e 4e }t


TESTING locals

variable ltv

variable depth1
variable fdepth1

: save-depths ( -- )
    depth depth1 ! fdepth fdepth1 ! ;

: drop-translation ( ... -- )
    fdepth fdepth1 @ ?do fdrop loop
    depth   depth1 @ ?do  drop loop ;

\ first test whether locals work with rec-forth
t{ : tl1 {: mylocal :} [ save-depths s" mylocal" rec-forth dup ltv ! drop-translation ] ; ltv @ -> translate-local }t

\ is the local from tl1 no longer visible?
t{ s" mylocal" rec-forth -> translate-none }t

\ does shadowing work?
t{ : tl3 {: swap :} [ save-depths s" swap" rec-forth dup ltv ! drop-translation ] ; ltv @ -> translate-local }t


\ now also test whether locals work with rec-name (depending on how
\ the discussion turns out, this may change into a check whether
\ locals work with rec-local).

t{ : tl4 {: mylocal :} [ save-depths s" mylocal" rec-name dup ltv ! drop-translation ] ; ltv @ -> translate-local }t

\ is the local from tl1 no longer visible?
t{ s" mylocal" rec-name -> translate-none }t

\ does shadowing work?
t{ : tl5 {: swap :} [ save-depths s" swap" rec-name dup ltv ! drop-translation ] ; ltv @ -> translate-local }t





