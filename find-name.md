## Problem

FIND has several problems:

 1. It takes a counted string.

 2. Its interface is designed for single-xt+immediate-flag systems
 (which cannot implement FILE S" correctly in all cases), and there is
 allowance for a STATE-dependent result to support other systems.

 3. As currently specified (there is a proposal for fixing that), it
 fails to meet the goal of guaranteeing that the user-defined
 text-interpreter (outlined in the [rationale of
 COMPILE,](http://forth-standard.org/standard/rationale#rat:core:COMPILE,)).

As a consequence of problem 2, the following implementation of ' is
wrong:

: ' bl word find 0= -13 and throw ;

This implementation fails to produce an xt representing interpretation
semantics in compile state on, at least, Gforth (and that's because
Gforth implements S" correctly, and FIND in a way that the
user-defined text interpreter works).  The following definition would
fix this problem on Gforth:

: ' bl word state @ >r postpone [ find r> if ] then 0= -13 and throw ;

but actually the definition of FIND currently is sufficiently unclear
that we cannot be sure that that's correct, either.

SEARCH-WORDLIST has a related, but different set of problems:

 1. It produces results with a varying number of stack items.  In some
 situations that makes it more cumbersome to use.

 2. Its interface is designed for single-xt+immediate-flag systems,
 but this time without allowing a STATE-dependent result.  As a
 consequence, Gforth produces the xt representing the interpretation
 semantics of the word, independent of STATE, with no way to get the
 compilation semantics of the word.

 3. In the general case, given that the xt part of the result does not
 represent a part of the compilation semantics, the 1/-1 part of the
 result is pointless.

## Solution

Introduce FIND-NAME (for the search order) and FIND-NAME-IN (for
specific wordlists).  Both take strings in c-addr u form, and both
return the name token of the found word (or 0 if not found).  We can
then go from the name token to the interpretation semantics with
[NAME>INTERPRET](http://forth-standard.org/standard/tools/NAMEtoINTERPRET),
and to the compilation semantics with
[NAME>COMPILE](http://forth-standard.org/standard/tools/NAMEtoCOMPILE).

## Typical use

```
: ' parse-name find-name dup 0= -13 and throw name>interpret ;

: postpone
  parse-name find-name dup 0= -13 and throw
  name>compile swap postpone literal compile, ; immediate

\ user-defined text interpreter
: interpret-word
  parse-name 2dup find-name if
     nip nip state @ if name>compile else name>interpret then execute
  else
     ... \ process numbers
  then ;

\ alternative:
defer name>statetoken ( nt -- ... xt )
: [ ['] name>interpret is name>statetoken false state ! ;
[ \ initialize STATE and NAME>STATETOKEN
: ] ['] name>compile is name>statetoken true state ! ;

: interpret-word
  parse-name 2dup find-name if
     nip nip state name>statetoken execute
  else
     ... \ process numbers
  then ;
```

## Remarks

This approach is not compatible with the cmForth approach and Mark
Humphries' approach, because they both use different word headers for
interpretation and compilation semantics.  This is already true for
the other words that deal with name tokens, but the present proposal
would make name tokens more important, and systems that do not support
them less viable.  However, both approaches have been known for at
least two decades, and have seen little to no uptake in standard
systems.  And we have good approaches for implementing systems with
name tokens, so excluding these approaches is not a significant loss.

## Proposal

Add the following words:

FIND-NAME ( c-addr u -- nt | 0 )

Find the definition identified by the string c-addr u in the current
search order. Return its name token nt, if found, otherwise 0.


FIND-NAME-IN ( c-addr u wid -- nt | 0 )

Find the definition identified by the string c-addr u in the wordlist
wid. Return its name token nt, if found, otherwise 0.

## Reference implementation

Implementing `FIND-NAME-IN` requires carnal knowledge of the system.

```
: find-name {: c-addr u -- nt | 0 :}
  get-order 0 swap 0 ?do ( widn...widi nt|0 )
    dup 0= if
      drop c-addr u rot find-name-in
    else
      nip
    then
  loop ;
```

## Testing

[Testcases](http://www.forth200x.org/tests/find-name.fs)


## Experience

Gforth has implemented `FIND-NAME` and (under a different name at
first) `FIND-NAME-IN` since 1996.  No problems were found or reported.

Several other systems have been reported to implement `FIND-NAME`
under this or other names (e.g., FOUND in ciforth).