At the online meeting on 2025-02-13 I was asked to present a
subproposal for factoring the state-dependent component out of
`TRANSLATE:`.

There are many possible ways to skin this cat, e.g., the one in
Matthias Trute's proposal, or the way that present proposal used up to [v4](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers#reply-872)
and earlier.  Here I present a way that requires relatively few
changes to the current version of this proposal.

### XY.3.1 Definition of terms

Replace the definition of **translator** with:

**translator**: a cell-sized opaque token that represents how a
 recognized lexeme can be interpreted, compiled, or postponed.  A
 translator usually needs additional data about the recognized lexeme
 that is deeper in the stacks.

Replace uses of *translator-xt* in `?NOTFOUND` with *translator*, and
likewise for other words that, in [[r1412]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1412), consume or
push the xt of a translator.

### XY.6 Glossary

#### TRANSLATOR:

Replace the definition of `TRANSLATE:` with

`TRANSLATOR:` ( *xt-int xt-comp xt-post "<spaces>name"* -- )

Skip leading space delimiters. Parse *name* delimited by a
space. Create a definition for *name* with the execution semantics
defined below.

*name* is referred to as translator.

*name* Execution: ( -- *translator* )

*translator* represents a translator with interpretation action
*xt-int*, compilation action *xt-comp*, and postpone action *xt-post*..

#### Modified words:

`INTERPRETING` ( *i\*x translator* -- *k\*x* )

Execute *xt-int* of *translator*.

`COMPILING` ( *j\*x translator* -- *l\*x* )

Execute *xt-comp* of *translator*.

`POSTPONING` ( *j\*x translator* -- )

Execute *xt-post* of *translator*.


#### STATE-TRANSLATING

Add:

`STATE-TRANSLATING` ( *i\*x translator* -- *j\*x* )

Remove *translator* from the stack.

If the system has a postpone state, and is currently is in postpone
state, execute *xt-post* of *translator*.

Otherwise, if the system is in interpretation state, execute *xt-int*
of *translator*.

Otherwise, execute *xt-comp* of *translator*.

### Discussion

The benefit of having each translator word return a translator token
is that one does not need to tick the translator words in all the
recognizers.  A slight improvement in writability and readability with
no downside (compared to [[r1412]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1412)).

The benefit of factoring out `state-translating` is that the `state`
dependence can be confined to the place(s) that actually need state
dependence: The standard Forth text interpreter (and user-defined text
interpreters that are intended to work similarly).  It does not infect
all translators.


### Typical use

The standard interpreter loop:

````
: interpret ( i\*x -- j\*x )
  BEGIN  parse-name dup  WHILE  forth-recognize ?found state-translating  REPEAT
  2drop ;
````

Implementation of `POSTPONE` is the same as in the existing proposal:

````
: postpone ( "name" -- )
  parse-name forth-recognize ?found postponing ; immediate
````

The implementation of `'` becomes slightly shorter (no need to tick
`translate-nt`:

````
: ' ( "name" -- xt )
  parse-name forth-recognize ?found
  translate-nt <> #-32 and throw
  name>interpret ;
````

Now for interpreter loops that do not use `STATE`.

First, the polyForth division of interpreter and compiler:

````
: parse-name-refill ( -- c-addr u )
  begin
    parse-name dup 0= while
      2drop refill 0= if
        0 0 exit then
  repeat ;

: ] ( i\*x -- j\*x )
  BEGIN
    parse-name-refill dup while
      2dup "[" str= 0= while
        forth-recognize ?found compiling
  REPEAT
  2drop ;

: pf-interpret ( i\*x -- j\*x )
  BEGIN  parse-name-refill dup  WHILE  forth-recognize ?found interpreting  REPEAT
  2drop ;
````

And here's one for colorforth-bw:

````
: cfbw-interpret ( i\*x -- j\*x )
  begin
    parse-name dup  while
      over c@ >r 1 /string forth-recognize ?found r> case
        '[' of interpreting endof
        '_' of compiling endof
        ']' of postponing endof
        -13 throw
      endcase
  repreat ;
````

The problem with these interpreters is that there is no standardized
or proposed way to plug this `interpret` into the existing
infrastructure (e.g., `included`), so the benefit of being able to
write this is limited to one line (in case of colorforth-bw) or the
rest of the file in case of the polyForth-style interpreter.

But the recognizer proposal allows to replace `forth-recognizer`, and
this allows us to plug in colorforth-bw into the text interpreter
until further notice.  I presented a way to do it with an earlier
version of this proposal in
[[r1397]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1397),
here's a way for doing it with [[r1412]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1412) modified by this
sub-proposal:

````
defer recognizer1 action-of forth-recognize is recognizer1

: translator-bw1 ( i\*x translator c -- j\*x )
  case
    '[' of interpreting endof
    '_' of compiling endof
    ']' of postponing endof
    -13 throw
  endcase ;

' translator-bw1 dup dup translator: translator-bw

: recognize-colorforth-bw ( c-addr u -- translator )
  dup 0= if 2drop 0 exit then
  over c@ >r 1 /string recognizer1
  r> over if translator-bw else drop then ;

' recognize-colorforth-bw is forth-recognize
````

### Reference implementation:

A straightforward implementation is:

````
: translator: ( xt-int xt-comp xt-post "<spaces>name" -- )
  create , , , ;

: state-translating ( i\*x translator -- j\*x )
  state @ if compiling else interpreting then ;
````

This does not cover a potential postpone state; if a system has a
postpone state and can enter the standard text interpreter in this
state, then the implementation of `state-translating` should be
extended accordingly.

Of course, this implementation of `state-translating` is far too
inefficient for some tastes, so here's a more clever one:

````
: state-translating ( i\*x translator -- j\*x )
  2 state @ 0<> + cells + @ execute ;
````

For even more efficiency we can redefine `]' and '[':

````
defer state-translating

: [ ( -- )
  [ ( old implementation ) ['] interrpreting is state-translating ; immediate

[ \ initialize state-translating

: ] ( -- )
  ] ( old implementation ) ['] compiling is state-translating ;
````

If there is a word that sets the postpone state, that word should also
set `state-translating` accordingly.

There are also a changes involving words that push literal translator
tokens.  In [[r1412]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1412) the translator word needs to be
ticked, in this subproposal you do not do that.  E.g., `rec-nt` now
looks as follows:

````
: rec-nt ( addr u -- nt nt-translator | 0 )
  forth-wordlist find-name-in dup IF  translate-nt  THEN ;
````

----------------------------------------------------------------------



## STATE-dependence

[[r1412]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1412)
still contains a defining word for state-dependent translators (and
none for translators without this mistake), which are unacceptable to
me.  I have suggested an improvement in
[[r1426]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers#reply-1426).

## Dividing the proposal?

There have been some discussions about dividing the proposal.  I don't
think that that's a good idea for the discussion, but in usage I see
the division into the following hierarchy of use cases, which require
different words; the later use cases usually require also implementing
the words for the earlier use cases:

1. Programs that use the default recognizers.  For them we need to
   specify a standard recognizer sequence (including how to deal with
   locals): `REC-NT` `REC-NUM` `REC-FLOAT` (if present) corresponds to
   Forth-2012.  I expect that systems that have `REC-STRING` and
   `REC-TICK` to put these into their recognizer sequence, too.  How
   do we document in the program documentation which recognizers are
   needed?  Probably we need to extend the program documentation
   requirements (until now the recognition of doubles, floats and
   locals has been coupled with documenting the double, float and
   local wordset, respectively, but for `REC-STRING` and `REC-TICK`
   that's probably not the way to go).
   
   The new `POSTPONE` is also at that usage level.

2. Programs that change which of the existing recognizers are used and
   in what order.  For them we need the names of the existing
   recognizers (not sure about the translators), `FORTH-RECOGNIZE`,
   `SET-RECOGNIZER-SEQUENCE`, `GET-RECOGNIZER-SEQUENCE`,
   `.RECOGNIZERS` (not yet proposed) and maybe `RECOGNIZER-SEQUENCE:`.
   If all the standardized recognizers are in `FORTH-RECOGNIZE` by
   default, there will probably not be much of this kind of usage,
   except maybe to put `REC-FLOAT` in front of `REC-NUM` (to recognize
   "1." as float; `REC-FLOAT` would have to be to defined in more
   detail for that to work).

3. Programs that define new recognizers that use existing translators.
   This usage needs the names of the translators.

4. Programs that define new translators.  This usage needs
   `TRANSLATE:` (or `TRANSLATOR:`).

5. Programs that define text interpreters and programming tools that
   have to deal with recognizers (such as a recognizer-aware
   `postpone`).  These programs need `INTERPRETING`, `COMPILING`,
   `POSTPONING` or `STATE-TRANSLATING`.

A system with recognizers is a program of all these types, so all
these words will be present in every such system (with the exception
of some recognizers and related translators), so there is little point
in making most of these words optional (except `rec-float`,
`rec-string`, `rec-tick` and translators used only by those
recognizers).  But it is still a good idea to present the words
divided by these usages.  We usually present words in alphabetical
order in the document.  Should we continue this tradition for these
words?  If so, the division of words above should probably be
documented in the rationale.

## For word counters

Given that usage 5 above is rare in user programs, word counters may
prefer to replace the four words `INTERPRETING`, `COMPILING`,
`POSTPONING` or `STATE-TRANSLATING` with one word

`TRANSLATING` ( i*x translator n -- j*x )

where

* `0 TRANSLATING` is equivalent to `INTERPRETING`

* `-1 TRANSLATING` is equivalent to `COMPILING`

* `-2 TRANSLATING` is equivalent to `POSTPONING`

* `STATE @ 0<> TRANSLATING` is equivalent to the reference
  implementation of `STATE-TRANSLATING`

A simple Forth system has only one use of `POSTPONING` (in `POSTPONE`)
and one use of `STATE-TRANSLATING` (in `INTERPRET`), so defining 4
words for the purpose may seem excessive.  And replacing them with
`TRANSLATING` saves a tiny bit of source code and memory.

OTOH, there is no standard way to use `TRANSLATING` for
`STATE-TRANSLATING` in the general case, where the system has a
postpone state, because there is no standard way to determine postpone
state.  Moreover, the specification of `TRANSLATING` is not so nice
(that's why I left it out in the above), and the code using it will be
less readable.

## Gerund

It's not clear to me why the gerund form is used (`INTERPRETING`
etc.), although I kept with it for my suggestions (for consistency).
I would use an imperative form; and because "interpret", "compile" and
"postpone" are already taken, maybe something like
`TRANSLATOR>INTERPRET` or somesuch, which would parallel
`NAME>INTERPRET`.  However, the latter pushes an xt, the former
executes it, so either we let `TRANSLATOR>INTERPRET` also produce an
xt, or use a slightly different naming scheme, such as
`TRANSLATOR*INTERPRET`.

## GET-STATE SET-STATE

It's unclear what `get-state` and `set-state` do, and their names
suggest a stack effect ( -- f ) and ( f -- ).

The reference implementation does not make that any clearer; in
particular, the reference implementation of `set-state` does not make
any sense at all, and I would not know why anybody would want to use
`get-state`.

## [IF] parts

This makes the proposal hard to understand and discuss.  Take a
decision (possible after asking around, but I doubt that anyone but
you and maybe ruv has a proper basis for an opinion), put it in the
proposal, and give a rationale for the decision in a section
*Discussion*.

## Side effects

I do not see a good way to specify in the normative part of the
document that a recognizer must not have a side effect.  The proposal
mentions "supposed to" and "promise".  The normative part says what
specific words do (or there is an ambiguous condition).  It seems to
me that the discussion about side effects should go into the
non-normative rationale.  It's clear enough what happens when somebody
uses a word that invokes a recognizer, and that recognizer has a side
effect; no need for an ambiguous condition.

## NOTFOUND

I have no preference here, but I remember that Matthias Trute
presented a case for notfound, and that sounded convincing.  Why do
his arguments no longer hold (or did they not hold in the first
place)?

## `FORTH-RECOGNIZE`, deferred or getter and setter?

I see no benefits to having a getter and setter here.  Deferred words
are fine.

## Presentation

The "Solution" chapter is not comprehensible except to those deep into
the discussion: It is full of unexplained terms, such as "data
parsing", "token type".  And "translator" is not comprehensible to
anybody who comes fresh to the proposal, and even to those who have
seen some earlier recognizer proposals.

The second part of "Solution" should be a separate section "Transition
for some implementors/users of Matthias Trute's proposal".

## More `NOTFOUND` stuff

The proposal defines `?FOUND`, `?NOTFOUND`, and `NOTFOUND` only for
NOTFOUND=0.  This looks like a bug to me.

The stack effect of `?FOUND` and other words: We do not have "never" in
the standard.  What's that supposed to mean?

## XY.3.1 Translator

"named subtype"?  What's that?  The rest of the wording is woefully
inadequate.  A careful specification would reveal the complexity that
you get with state-dependent translators.

## ?NOTFOUND

`?NOTFOUND` has a horrible stack effect.  This word is not shown in
any typical use examples?  Is it needed?  If it is needed, maybe the
stack effects of the other words can be changed to make it
unnecessary; although, admittedly, when I worked on combining
recognizers, I did not find a solution with a nice stack flow (and I
have tried).  Hmm, maybe with a variant of `case` with a specialized
variant of `of`?

## POSTPONE

"if the exception wordset is not present".  The exception wordset has
been a required part of Forth200x for several years.

## SET-RECOGNIZER-SEQUENCE

As specified, the sequence will always fit.  Can the sequence fail to
fit?  If so, specify what happens.

## REC-NUM

Should this be the all-singing, all-dancing variant (including
doubles, number prefixes and '<char>')?  Given existing practice and
the legacy code base, yes.  OTOH, with recognizers it seems a
conceptually attractive option to have the `rec-num` be a decomposable
sequence consisting of the various cases.  But given nestable
recognizer sequences, that's always an option for the future.

## SCAN-TRANSLATE-STRING

This should follow C conventions for newlines like the rest of the
string syntax, i.e., escape newlines with \\.  If other conventions are
desired (e.g. what may or may not be JSON syntax), that would be for another
recognizer and another translator.

The specification should be clear about what it does: "`REFILL` can be
used to read in more lines" is neither here nor there.

## TRANSLATE-STRING ?SCAN-STRING

What are these words good for?  `REC-STRING` apparently does not need
them.

## [[

A word without interpretation nor compilation semantics?

Should we specify whether there is a postpone state, or alternatively
that `]]` has its own text interpreter loop?  There are ways to
distinguish these two kinds of implementation; does it matter?  Maybe
if you want to `EVALUATE` something in postpone state or somesuch.

`]]` and `[[` should probably go into a separate proposal.

## STATE

Changing the specification of `state` such that there is at least one
non-zero value that does not mean "compilation state" is not an
extension of the current specification of `state`, but a change.
However, existing practice of systems which use -2 as postpone state
suggest that this does not break existing code in practice.  That's
probably because so little existing code actually uses postpone state.
With wider use of postpone state, some breakage may actually turn up.

The safe option would be to represent postpone state (if we have it at
all) in a way other than through a value of `STATE`.  E.g., have
another variable `POSTPONE-STATE`: if it's false, then `STATE`
determines the state; if it's true, the system is in postpone state.

In any case, if we put `]]` in another proposal, that's where we
should have this discussion.





