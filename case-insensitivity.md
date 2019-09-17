## Problem

This is an alternative proposal for the same problem as in proposal
[Case sensitivity](http://forth-standard.org/standard/usage#contribution-73)


Forth-2012 states:

> Programs that use lower case for standard definition names or depend
> on the case-sensitivity properties of a system have an environmental
> dependency.

This differs from common practice:

It is common practice for programs to use lower case for standard
definition names, and also not uncommon practice to use capitalized
(i.e., mixed-case) names for some standard definition names.

It is common practice for systems to support case insensitivity for
ASCII characters, either by default (Gforth, iForth, SwiftForth, VFX),
or after invoking a special command (SP-Forth).

## Solution

Standardize the common practice of systems.

## Typical use

```
Create a 5 cells allot
```

## Remarks

What about non-ASCII characters?  They are treated case-sensitively.

The advantages of this approach are: This approach is common practice.
The implementation is relatively simple (especially if you consider
the complexity of locale-dependent case insensitivity in UTF-8).
Forth source files work independent of the encoding and locale, i.e.,
the system does not need to know the encoding to know whether a word
matches a dictionary entry (of course, the application itself may be
locale-dependent).  The main purpose

The disadvantage of this approach is that users might be confused by
the difference in case sensitivity between ASCII and non-ASCII
characters.  E.g., "WIEN" would match "Wien", but "KÖLN" would not
match "Köln".

### Comparison with the [Case sensitivity](http://forth-standard.org/standard/usage#contribution-73) proposal

The present proposal covers the practice of using mixed-case names.
It makes this part of the standard air-tight rather than being
unnecessarily loose: while having special case-sensitivity rules for
standard words and other rules for other words has been discussed, the
common and simpler practice is to just implement case insensitivity
for ASCII characters.

## Proposal

In 3.3.1.2, delete

> Programs that use lower case for standard definition names or depend
> on the case-sensitivity properties of a system have an environmental
> dependency.

In 3.4.2, replace

> The case sensitivity (whether or not the upper-case letters match
> the lower-case letters) is implementation defined. A system may be
> either case sensitive, treating upper- and lower-case letters as
> different and not matching, or case insensitive, ignoring
> differences in case while searching.
> 
> The matching of upper- and lower-case letters with alphabetic
> characters in character set extensions such as accented
> international characters is implementation defined.
> 
> A system shall be capable of finding the definition names defined by
> this standard when they are spelled with upper-case letters.


with

> ASCII characters are matched case-insensitively.  All other
> characters are matched exactly (case sensitively).

## Reference implementation

System-dependent

## Testing

```
T{ 1 constant case-insensitive -> }T
T{ 2 Constant Case-INSENSITIVE -> }T
T{ case-insensitive -> 2 }T
```

## Experience

Gforth has implemented this approach since its inception.  Several
other systems (SwiftForth, VFX, iForth) have also done so for as long
as I have used them.

Many published programs use lower-case or mixed-case system words.
