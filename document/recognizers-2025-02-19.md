## Multiline strings

Checking on Python3, I see that it uses C's syntax for strings
starting with `"`.  In particular, if you just do a newline in the
middle of a string without escaping the newline, you get an error:

```
>>> print("abc
  File "<stdin>", line 1
    print("abc
          ^
SyntaxError: unterminated string literal (detected at line 1)
```

An escaped newline is ignored, and you need to write `\n` to get an
actual newline.  I expect that it's the same for most other languages
you mention, because they all use a different syntax for "proper
multiline strings".  I have no problem with an additional recognizer
for "proper multiline strings" with a distinguishable syntax (such as
`"""`); I can even live with `rec-string` doing the additional syntax,
but I think that there might be others who will disparage it as a
WIBNI or somesuch.

But I think that, for `"`-delimited strings, `rec-string` should either
not do multi-line strings at all or do it the C/Python3/etc. way.


## STATE-TRANSLATING

> Why is this better than making translator a subtype of xt, and using
> EXECUTE instead of STATE-TRANSLATING?

It is better because it isolates the state-dependence in the word(s)
calling `state-translating` rather than having it in the translator
coming out of the recognizer and potentially being invoked through any
`execute`, `compile,`, `is` or `defer!` in the system (with data-flow
analysis necessary to reduce the number of potential invocations, and
the result of that analysis probably still showing more occurences
than what searching for `state-translating` would otherwise give us).

It's similar to the difference between arming a bomb at the factory,
or arming it only just before dropping it (which may never happen).

## Examples of translators not produced with `translate:`

The proposal states about `translate:`

```
This word is the only standard way to define a general purpose translator.
```

Any argument based on defining translators in other ways is therefore
not in line with the proposal.

This applies to
[[r1432]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1432)
as well as
[[r1433]](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers?hideDiff#reply-1433).

So the usages you show may work on some particular implementation, but
may fail on a different implementation of the proposal.

And if you are willing to design an implementation for some convenient
code of your interpretation-only recognizers, I am sure that your are
able to design an implementation of recognizers with
`state-translating` that's just as convenient.

