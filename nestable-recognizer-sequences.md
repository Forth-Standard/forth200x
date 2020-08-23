# Nestable Recognizer Sequences

## Author

M. Anton Ertl

## Base proposal

[Recognizer](https://forth-standard.org/proposals/recognizer)

## Problem

There are similarities between a word list, a recognizer, a search
order, and a recognizer sequence: All of them take a string as input,
and either recognize it, or not.  if they recognize it, word lists and
the search order produce a name token (or an xt and an immediate
flag), while recognizers and recognizer sequences produce some data
and a rectype.

The similarity between wordlists and a search order has inspired the
idea of nestable search orders: Several wordlists could be combined
into a sequence that itself would work like a wordlist in other search
orders.  However, the search order words had already been
standardized, so this idea never made it out of the concept stage.

The similarity between the search order and recognizer sequences has
led to the present recognizer proposal containing the words
GET-RECOGNIZER and SET-RECOGNIZER, which are mostly modeled on
GET-ORDER and SET-ORDER.

As an alternative, this proposal proposes the idea of nestable
(but not necessarily changeable) recognizer sequences.

## Solution

Add the following words:

rec-sequence ( xt1 .. xtn n "name" -- )

 Defines a recognizer "name".

 "name" execution: ( c-addr u -- ... rectype )

 Tries to recognize c-addr u using the recognizers xtn...xt1 (in this
 order).  The first successful recognizer in the sequence returns from
 "name" with its result.  If no recognizer succeeds, return RECTYPE-NULL.

[On the order of xts: This order is modeled on the order in the search
order, but one could use the reverse order without suffering
disadvantages; I am leaving this open to ~~bikeshedding~~ discussion.]

get-rec-sequence ( xt -- xt1 .. xtn n )

 If xt refers to a recognizer sequence, return the contained
 recognizers.  If xt refers to a deferred word, perform DEFER@
 followed by GET-REC-SEQUENCE (i.e., GET-REC-SEQUENCE works through
 deferred words).  IF xt refers to neither, return 0.

FORTH-RECOGNIZER now contains the xt of a recognizer or a
rec-sequence.  RECOGNIZE is unnecessary, because it's functionality is
performed by running a rec-sequence.  GET-RECOGNIZER, SET-RECOGNIZER,
NEW-RECOGNIZER-SEQUENCE are replaced by the words above.

## Typical Use

Define a recognizer sequence for the classical text interpreter:

    ' rec-num ' rec-nt 2 rec-sequence rec-forth-cm ( c-addr u -- ... rectype )

Extend it with FP numbers:

    ' rec-float ' rec-forth-cm 2 rec-sequence rec-forth ( c-addr u -- ... rectype )

Make this the text interpreter

    ' rec-forth to forth-recognizer

Have a dot-parser to be searched first:

    ' rec-forth ' rec-dot 2 rec-sequence rec-.forth ( c-addr u -- ... rectype )

Put a user-defined recognizer REC-USER behind the currently active
recognizers, temporarily:

```
' rec-user forth-recognizer 2 rec-sequence rec-forthuser
forth-recognizer ( old )
' rec-forthuser to forth-recognizer
\ some code that uses REC-USER:
...
\ now restore the old recognizer sequence
( old ) to forth-recognizer
```

You can insert a recognizer in the middle of a sequence by picking the
existing sequence apart and using it for constructing a new recognizer:

    ' rec-forth-cm get-rec-sequence swap ' rec-foo rot 1+ rec-sequence rec-FOOrth-cm

This inserts REC-FOO to be searched as second recognizer (after
REC-NT).  This approach has the disadvantage that you need to know
pretty well what the recognizer currently contains (it shares this
disadvantage with the GET-RECOGNIZER interface).  It also has the
disadvantage that you have no easy way to update all the recognizer
sequences that contain REC-FORTH-CM.  To avoid these disadvantages,
you can put deferred words into recognizer sequences from the start:

```
: rec-nothing ( c-addr u -- rectype-null )
  2drop rectype-null ;

defer rec-foo-deferred ' rec-nothing is rec-foo-deferred

' rec-num ' rec-foo-deferred ' rec-nt 3 rec-sequence rec-forth-cm
```

Then you can plug in REC-FOO:

```
' rec-foo is rec-foo-deferred
```

And of course you can deactivate it later.  Of course, this approach
works only if you have the foresight to insert REC-FOO-DEFERRED from
the start, or if you can change the source code of REC-FORTH-CM later.

An alternative would be to be able to change the rec-sequences in
words defined with REC-SEQUENCE; for that we would need something like
SET-REC-SEQUENCE.  It's not clear to me that this is really needed,
though.

## Proposal

TBD (if this informal proposal is actually is popular enough to merit
further development).

## Existing practice

A word REC-SEQUENCE: (but without GET-REC-SEQUENCE) has been in Gforth
since 2016.  It has not been used; instead, the mainstream
GET-RECOGNIZER SET-RECOGNIZER interface was used.

## Reference Implementation

TBD

## Testing

TBD

## Credits

Ruvim has recently suggested something in this vein, rekindling my
interest in this kind of interface.

