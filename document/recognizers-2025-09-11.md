# Recognizer committee proposal 2025-09-11

The committee has found consensus on the words in this proposal.  I
was asked to write it up.

## Author:

M. Anton Ertl (based on previous work by Matthias Trute, Bernd Paysan,
and others, and the input of the standardization committee).

## Change Log:

* 2026-02-08 Fleshed out proposal; worked in feedback up to now.
* 2025-09-12 [r1535] Some fixes 
* 2025-09-12 [412] Initial version

## Problem:

The classical text interpreter is inflexible: E.g., adding
floating-point recognizers requires hardcoding the change; several
systems include system-specific hooks (sometimes more than one) for
plugging in functionality at various places in the text interpreter.

The difficulty of adding to the text interpreter may also have led to
missed opportunities: E.g., for string literals the standard did not
task the text interpreter with recognizing them, but instead
introduced `S"` and `S\"` (and their complicated definition with
interpretation and compilation semantics).

## Solution:

The recognizer proposal is a factorization of the central part of the
text interpreter.

As before the text interpreter parses a white-space-delimited string.
Unlike before, the string is now passed to the recognizers in the
default recognizer sequence `rec-forth`, one recognizer after another,
until one matches.  The result of the matching recognizer is a
*translation*, an on-stack representation of the word or literal.  The
translation is then processed according to the text-interpreter's
state (interpreting, compiling, postponing).

There are five usage levels of recognizers and related recognizer words:

1. Programs that use the default recognizers.  This is the common
case, and is essentially like using the traditional hard-coded Forth
text interpreter.  You do not need to use recognizer words for this
level, but you can inform yourself about the recognizers in the
current default recognizer sequence with `recs`.  The default
recognizer sequence contains at least `rec-name` and `rec-number`,
and, if the Floating-Point wordset is present, `rec-float`.  Moreover,
programmers can now `postpone` numbers and other recognized things.

2. Programs that change which of the existing recognizers are used and
in what order.  The default recognizer sequence is `rec-forth`.  You
can get the recognizers in it with `get-recs` and set them with
`set-recs`.  You can also create a recognizer sequence (which is a
recognizer itself) with `rec-sequence:`.  This proposal contains
pre-defined recognizers `rec-name rec-number rec-float rec-none`,
which can be used with `set-recs` or for defining a recognizer
sequence.

3. Programs that define new recognizers that use existing translation
tokens.  New recognizers are usually colon definitions,
proposed-standard translation tokens are `translate-none
translate-cell translate-dcell translate-float translate-name`.

4. Programs that define new translation tokens.  New translation
tokens are defined with `translate:`.

5. Programs that define text interpreters and programming tools that
have to deal with recognizers.  Words for achieving that are not
defined in this proposal, but discussed in the rationale.

See the rationale for more detail and answers to specific questions.

## Reference implementation:

TBD.


## Testing:

TBD.

## Proposal:

### Usage requirements:

#### Data Types

**translation**: The result of a recognizer; the input of
`interpreting`, `compiling`, and `postponing`; it's a semi-opaque type
that consists of a translation token at the top of the data stack and
additional data on various stacks below.

**translation token**: (This has formerly been called a rectype.)
  Single-cell item that identifies a certain translation.

#### Translations and text-interpretation

A recognizer pushes a translation on the stack(s).  The text interpreter
(and other users, such as `postpone`) removes the translation from the
stack(s), and then either performs the interpreting run-time,
compiling run-time, or postponing run-time.

All the proposed-standard `translate-...` words only push a
translation token.  Their stack effects are specified as expecting
some data on the stack and pushing a translation.  This shows what
data is required in addition to the translation token to form a
complete translation.  A proposed-standard `translate-...` word pushes
the same translation token every time it is invoked.

#### Compiling and postponing run-time 

Unless otherwise specified, the compiling run-time compiles the
interpreting run-time.  The postponing run-time compiles the compiling
run-time.

### Exceptions

Add the following exception to table 9.1:

-80 too many recognizers

### Words

#### `rec-name` ( c-addr u -- translation )

(formerly `rec-nt`)

If c-addr u is the name of a visible local or a visible named word,
translation represents the text-interpretation semantics
(interpreting, compiling, postponing) of that word (see
`translate-name`).  If not, translation is `translate-none`.


#### `rec-number` ( c-addr u -- translation )

(formerly `rec-num`)
If c-addr u is a single or double number (without or with prefix), or
a character, all as described in section 3.4.1.3 (Text interpreter
input number conversion), translation represents pushing that number
at run-time (see `translate-cell`, `translate-dcell`).  If not,
translation is `translate-none`. 

#### `rec-float` ( c-addr u -- translation )

If c-addr u is a floating-point number, as described in section 12.3.7
(Text interpreter input number conversion), translation represents
pushing that number at run-time (see `translate-float`). If c-addr u
has the syntax of a double number without prefix according to section
8.3.1 (Text interpreter input number conversion), and it corresponds
to the floating-point number r according to section 12.6.1.0558
(`>float`), translation may represent pushing r at run-time.  If
c-addr u is not recognized as a floating-point number, translation is
`translate-none`.

#### `rec-none` ( c-addr u -- translation )

This word does not recognize anything.  For its translation, see
`translate-none`.

#### `recs` ( -- )

(formerly `.recognizers`)
Print the recognizers in the recognizer sequence in `rec-forth`, the
first searched recognizer leftmost. 

#### `rec-forth` ( c-addr u -- translation )

(formerly `forth-recognize`) This is a deferred word that contains the
recognizer (sequence) that is used by the Forth text interpreter.

#### `rec-sequence:` ( xtu .. xt1 u "name" -- )

Define a recognizer sequence "name" containing u recognizers
represented by their xts.  If `set-recs` is implemented, the sequence
must be able to accomodate at least 16 recognizers.

name execution: ( c-addr u -- translation )

Execute xt1; if the resulting translation is the result of
`translate-none`, restore the data stack to ( c-addr u -- ) and try
the next xt.  If there is no next xt, remove ( c-addr u -- ) and
perform `translate-none`.

#### `translate-none` ( -- translation )

(formerly `r:fail` or `notfound`)

translation interpreting run-time: ( ... -- )

`-13 throw`

translation compiling run-time: ( ... --  )

`-13 throw`

translation postponing run-time: ( ... --  )

`-13 throw`

#### `translate-cell` ( x -- translation )

(formerly `translate-num`)

translation interpreting run-time: ( -- x )

#### `translate-dcell` ( xd -- translation )

(formerly `translate-dnum`)

translation interpreting run-time: ( -- xd )

#### `translate-float` ( r -- translation )

translation interpreting run-time: ( -- r )

#### `translate-name` ( nt -- translation )

(formerly translate-nt)

translation interpreting run-time: ( ... -- ... )

Perform the interpretation semantics of nt.

translation compiling run-time: ( ... -- ... )

Perform the compilation semantics of nt.

#### `translate:` ( xt-int xt-comp xt-post "name" -- )

(formerly `rectype:`)

Define "name"

"name" exection: ( i\*x -- translation )

"name" interpreting action: ( ... translation -- ... )

Remove the top of stack (the translation token) and execute xt-int.

"name" compiling action: ( ... translation -- ... )

Remove the top of stack (the translation token) and execute xt-comp.

"name" postponing action: ( translation -- )

Remove the top of stack (the translation token) and execute xt-post.

#### `get-recs` ( xt -- xt_u ... xt_1 u )

xt is the execution token of a recognizer sequence.  xt_1 is the first
recognizer searched by this sequence, xt_u is the last one.

#### `set-recs` ( xt_u ... xt_1 u xt -- )

xt is the execution token of a recognizer sequence.  Replace the
contents of this sequence with xt_u ... xt_1, where xt_1 is searched
first, and xt_u is searched last.  Throw -80 (too many recognizers) if
u exceeds the number of elements supported by the recognizer sequence.

#### `postpone`

##### Interpretation:

Interpretation semantics for this word are undefined.

##### Compilation: ( "<spaces>name" -- )

Skip leading space delimiters.  Parse *name* delimited by a space.
Use `rec-forth` to recognize *name*, resulting in *translation* with
*translation-token*.  For a system-defined translation token, first
consume the translation, then compile the 'compiling' run-time. For a
user-defined translation token, remove it from the stack and execute
its post-xt.

## Rationale

### Names

The names of terms and the proposed Forth words in this proposal have
been arrived at after several lengthy discussions in the committee.
Experience tells me that many readers (including from the committee)
will take issue with one or the other name, but any suggestion for
changing names will be ignored by the me.  If you want them changed,
petition the committee (but I hope they will be as weary of renamings
as I am).

In particular, I suggested to use "recognized" instead of
"translation", and IIRC also to rename the `translate-...` words
accordingly, but the committee eventually decided to stay with
translation and `translate-...`.

Face it: The names are good enough.  Any renaming, even if it results
in a better name, increases the confusion more than it helps: even
committee members (culprits in the renaming game themselves) have
complained about being confused by the new, possibly better names for
concepts and words that have already been present in Matthias Trute's
proposal.

If you want to improve the proposal, please read it, play with the
words in Gforth, read the reference implementation and the tests when
they arrive, and point out any mistake or lack of clarity.

### Translation tokens and `translate-...` words

[[r1541]](https://forth-standard.org/proposals/recognizer-committee-proposal-2025-09-11#reply-1541)
points out interesting uses of knowledge about translation tokens,
and, conflictingly, potential implementation variations.  This
proposal decides against the implementation variations and for the
uses by specifying in the Usage Requirements that a `translate-...`
word just pushes a translation token, and it always pushes the same
one.

### Discarding a translation

[[r1541]](https://forth-standard.org/proposals/recognizer-committee-proposal-2025-09-11#reply-1541)
also asks for a way to discard a translation.  This need has also come
up in some recognizers implemented in Gforth (e.g., `rec-tick`), and
Gforth uses (non-standard) words like `sp@` and `sp!` for that.
Standard options would be to wrap the word that pushes a translation
into `catch` and discard the stacks with a non-zero `throw`, or to use
`depth` and `fdepth` in combination with loops of `drop` and `fdrop`;
both ways are cumbersome.  My feeling is that many in the committee
and in the wider Forth community do not see the need for
`discard-translation` yet; this may change in the future.

### Consumers of translations (Usage level 5)

The committee has decided not to standardize words
that consume translations for now.  Such words would be useful for
defining a user-defined text interpreter, but the experience with
recognizers has shown that a recognizer-using text interpreter is
flexible enough that it is no longer necessary to write such text
interpreters, so such words are only used internally in the text
interpreter, eliminating the need to standardize them.

However, to give an idea how all this works together, here's the words
that Gforth provides for that purpose:

#### `interpreting` ( ... translation -- ... )

For a system-defined translation token, first remove the translation
from the stack(s), then perform the interpreting run-time specified
for the translation token.  For a user-defined translation token,
remove it from the stack and execute its int-xt.

#### `compiling` ( ... translation -- ...  )

For a system-defined translation token, first remove the translation
from the stacks, then perform the compiling run-time specified for the
translation token, or, if none is specified, compile the
'interpreting' run-time.  For a user-defined translation token, remove
it from the stack and execute its comp-xt.

#### `postponing` ( ... translation --  )

For a system-defined translation token, first consume the translation,
then compile the 'compiling' run-time.  For a user-defined translation
token, remove it from the stack and execute its post-xt.

## Typical use:

````
s" 123" rec-forth ( translation ) interpreting ( n ) \ leaves 123 on the stack

: umin ( u1 u2 -- u )
  2dup u< if drop else nip then ;

: string-prefix? ( c-addr1 u1 c-addr2 u2 -- f )
    tuck 2>r umin 2r> compare 0= ;

: rec-tick ( addr u -- translation )
    2dup "`" string-prefix? if
        1 /string find-name dup if
            name>interpret translate-cell
        else
            drop translate-none then
        exit then
    \ this recognizer did not recognize anything, therefore:
    rec-none ;

' noop                       ( x -- x )                             \ int-xt
' lit,                       ( compilation: x -- ; run-time: -- x ) \ comp-xt
:noname lit, postpone lit, ; ( postponing: x -- ;  run-time: -- x ) \ post-xt
translate: translate-cell
````
