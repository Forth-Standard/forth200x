# Special memory access words

## Author:

M. Anton Ertl

## Change Log:

2024-06-14 initial version

## Problem:

Data coming from or going to a file or another computer often contain
16-bit, 32-bit, and 64-bit integer values that may be signed or
unsigned, may be naturally aligned or not, and may be in big-endian or
little-endian instead of the native byte order.  Architectures tend to
provide convenient instructions for accessing these data, but the
Forth standard does not provide words for that, and synthesizing the
operations from C@ and C! is not just cumbersome, but also leads to
inefficient code.

## Solution:

This proposal targets primarily byte-addressed systems. See the
discussion below about larger address units.

We use the following prefixes:

| prefix | Meaning | informal name |
| ------ | ------- | ------------- |
| `c`    | 8 bits  | Byte          |
| `w`    | 16 bits | Wyde          |
| `l`    | 32 bits | Long          |
| `x`    | 64 bits | eXtended      |

The `l`-prefixed words are not useful on systems with cell size <32
bits, and such systems are therefore expected not to implement them.
Likewise for the `x`-prefixed words and cell sizes <64 bits.

For the `w` prefix this proposal specifies the following set of words:

`w@` `w!` for unaligned 16-bit memory accesses; `w@` zero-extends.

Right after `w@` or right before `w!` you can adjust the byte order:
`wbe` converts from big-endian to native byte-order and from native to
big-endian byte order. `wle` is the corresponding word for
little-endian byte order.

On fetching signed values can then be sign-extended with `w>s`.
Unsigned values are already in the proper zero-extended form.  On
storing all the target bits are present in the cell, so no extension
is necessary.

These five words allow us to fetch and store big-endian, little-endian
or natively ordered signed and unsigned 16-bit values, with sequences
like:

````
w@ wbe w>s   \ 16-bit unaligned signed big-endian fetch
>r wle r> w! \ 16-bit unaligned     little-endian store
````

For the `c` prefix byte order and alignment are not issues, so there
are no words for that.

These words do not work properly if the data does not fit into a cell,
so a 16-bit system would only implement the `b` and `w` words, a
32-bit system only the `b`, `w` and `l` words, and only systems with
cell size >= 64 bits would implement all the words.

## Typical use: (Optional)

````
( c-addr ) l@ lle l>s \ 32-bit unaligned signed little-endian fetch
( c-addr ) w@         \ 16-bit unaligned unsigned native-order fetch
( n|u c-addr ) >r xbe r> x! \ 64-bit unaligned big-endian   store
( n|u c-addr ) l!           \ 32-bit unaligned native-order store
````

## Discussion

### Previous work

The present proposal can be seen as another take on the problems
attacked with the following proposals.

#### [Memory Access](http://www.forth200x.org/memory-2010-06-26.txt)
Federico de Ceballos (with Stephen Pelc) has proposed a wordset for
solving the same problem by having words like

````
be-w@ \ 16-bit unaligned unsigned big-endian fetch
le-w! \ 16-bit unaligned       little-endian store
````

That would require 6 words `be-w@` `le-w@` `w@` `be-w!` `le-w!` `w!`,
but would still not work for fetching signed values, so you either
need `w>s` to be possibly used after any of the `...w@` words (for a
total of 7 `w` words, but it's still a composing approach), or it
would need a doubling of the `...w@` words (for a total of 9 `w`
words, but now everything is precomposed).

This proposal also includes words like `w,` `walign` `waligned`
`wfield:` discussed below.

This proposal has been met with significant resistance due to the
large number of words proposed.

#### [16-bit memory access](https://forth-standard.org/proposals/16-bit-memory-access#contribution-301)

This proposes `w@` `w!` as working with w-addr addresses that are not
defined in the proposal (but I would expect them to require 16-bit
alignment, but OTOH neither `waligned` nor `walign` are proposed).  No
solution for byte order or sign extension is presented.  The proposal
includes `w,` which requires 16-bit alignment of the data-space
pointer.

#### [32-bit memory operators](https://forth-standard.org/proposals/32-bit-memory-operators#contribution-302)

This is the 32-bit variant (using `l` as prefix) of the "16-bit memory
access" proposal discussed above.

### Efficiency

Does the proposed approach not lead to less efficient code than the
approach of the Memory Access proposal mentioned above?  The more
advanced Forth systems combine code sequences and produce efficient
code for them.  E.g., in the present case, for `l@ lle l>s`
gforth-fast on AMD64 produces:

````
movsx   r8,dword PTR [r8]
````

Simpler systems will indeed be less efficient when such special memory
accesses are performed, but the present proposal proposes fewer words,
which is often more in line with the philosophy behind many simple
systems.

Also, is a lot of time spent accessing input and output data?

### Larger address units

On some systems (in particular on word-addressed hardware) the address
unit is larger than one byte (8 bits).  How can these words work
there?  The only way I can see is to work with a special memory layout
where only the least significant 8 bits of each address unit are used,
and the other bits are ignored on fetching, and are set to 0 on
storing.  The reference implementation of the proposed words can be
used in such a setting.

This memory layout would be used between I/O words that produce or
consume this layout, and the words that use the special memory access
words to fetch from or store to this layout.  For the file words, this
layout could come into play through a file access mode modifier
(similar to `bin`).

To support this scheme, we would specify "bytewise" memory access for
words like `w@` and `w!`, a type b-addr for words that perform such
memory accesses, and some wording in "3.1.3.3 Addresses" about that.
We would also need a word `b@` (for zero-extending the least
significant 8 bits, while `c@` zero-extends possibly bigger units;
`b!` is not needed, `c!` is good enough), and a word `bytewise`, which
is the file access mode modifier mentioned above.

However, during the discussion no implementor of a system with larger
address units emerged, who announced interest in implementing such a
wordset.  So, in order to avoid standardizing unnecessary words, `b@`,
`b!` and `bytewise` are not proposed.  Instead, the issue is discussed
in the Rationale.

### `c` vs. `b` prefix

Some people have expressed a preference for the `b` prefix even if the
result would mean introducing synonyms of `c@` and `c!`.  However, in
the interest of avoiding superfluous words, such words are not
proposed.  Those interested in such words can define them themselves:

````
synonym b@ c@
synonym b! c!
synonym b>s c>s
````

### Require alignment or not

One might wonder whether we should not have versions of the fetch and
store words that require alignment as well as versions that do not,
but we have decided to only supply the words that do not require
alignment, for the following reasons.  All the surviving
general-purpose architectures (IA-32/AMD64, ARMv7-A/R ff. (since 2005),
RISC-V, Power, S/390x) have converged on supporting unaligned
accesses, so on these architectures both variants would use the same
instructions.

On other architectures `w@` will be slower than a hypothetical `w@a`,
but given that these words are not used that often, that these
machines are no longer widespread, and that alignment is sometimes
lost by embedding one structure inside another (as has occured in
network protocols), we decided that `w@a` and friends are more trouble
than they are worth.

### Upper bit handling for the byte-order words

How do we specify the upper bits in the results for `wle`, `wbe` etc.?
E.g., on 64-bit Gforth I see:

````
$1234567890abcdef wbe hex. \ output: $EFCD  ok
$1234567890abcdef wle hex. \ output: $1234567890ABCDEF  ok
````

So in one case it sets the other bits to zero, in the other case it
leaves them alone.  However, we do not want to specify that the upper
bits can be anything, otherwise `w@ wle` would not work as unsigned
little-endian 16-bit fetch, and we would need to add a word `w>u` or
somesuch.

So we specify that the upper bits of the result are either untouched
or 0 (when applying `wbe`/`wle` to the result of `w@`, that produces
the same result in either case).

### Types

There has been some discussion whether 'c-addr' or 'addr' is more
appropriate (they are formally equivalent).  Using 'c-addr' makes the
intent of supporting unaligned accesses more obvious.

Another discussion is about 'u' vs. 'x'.  The 'u' (where used)
expresses that the values are zero-extended, even if they happen to be
a zero-extended representation of a possibly signed, possibly
byte-swapped integer.  If more precise typing is required,
[r1259](https://forth-standard.org/proposals/special-memory-access-words#reply-1259)
outlines a precise way to specify the typing.  However, it is doubtful
that such a specification would result in enough additional clarity
(if any) to justify the longer specification.

### Accesses to values larger than one cell

Gforth has `xd` words where the on-stack representation is a
double-cell.  This allows implementing 64-bit accesses on systems with
32-bit cells.  When I presented these words at the 2023 Forth200x
meeting, I was asked not to include them in this proposal.  So access
to values larger than a cell is not supported by the proposed words.

### Signed number representations

Given that the other Forth words (e.g. `+`) work for 2s-complement
numbers, any other signed-number representation in the input and
output requires additional conversion work.  No words for such
conversions are proposed, because there is no existing practice.

The description of `c>s` `w>s` `l>s` `x>s` as sign-extending makes it
clear what operation they perform.  Specifying a number format is
redundent and actually reduced generality (these words could also be
used for 1s-complement, but other words would be needed to then
convert the results into 2s-complement or perform computations in
1s-complement).

### Additional words

Gforth has the following words related to this proposal:

* `/w ( -- u )` specifies the size of a 16-bit value, i.e. `2`.

* `w, ( x -- )` allocates and stores a 16-bit value.  `wbe` or `wle`
  can be used before.  SwiftForth and VFX Forth also have `w,`.

* `walign ( -- )` naturally aligns the dictionary pointer to a 16-bit
  boundary..

* `waligned ( u1 -- u2 )` does the same for an address or offset on
  the stack.

* `*aligned ( u1 u2 -- u )`: *u2* divides *u*, and *u* is the next
  value *u* >= *u1* with that property.  The result of the operation
  is *not specified if *u2* is not a power of two.

* `wfield: ( u1 "name" -- u2 )` defines a naturally (i.e., 16-bit)
  aligned 16-bit field.  `wfield:` is equivalent to `waligned /w
  +field`.

* `wvalue: ( u1 "name" -- u2 )` defines a naturally-aligned
  value-flavoured 16-bit field.  No easy way exists to define a
  value-flavoured field without imposing alignment.

These words (and their `l` and `x` siblings) were not in my
presentation at the 2023 meeting, so I have not been asked to include
them in this proposal, and therefore I have not included them, but if
consensus emerges that we want to include some of them, I am prepared
to do that.  But do we need them and do we need them in this form?

* `/w` just means `2`, but documents the intent (number of bytes
  accessed by `w@`) better.

* `w,` is convenient in interactive usage, but for maintained code its
  usage often is problematic: In many cases it redundantly respecifies
  the layout of a data structure (already defined with the `field`
  words), which means that a change to the layout results several
  changes in the code.

  There was some discussion of `w,` at the 2024 meeting, and the
  result of the discussion was not to include such words in this
  proposal.  They may be proposed separately.

* `walign` may be useful in connection with `w,`, but has the same
  problem of redundancy.

* `waligned` may be useful for influencing field layout, but one could
  also write `/w *aligned` (replacing three `aligned` words with one).
  Also, if the structure layout is coming from outside the Forth
  system, we probably just want to transfer it using the C interface
  rather than defining it the way we would a Forth-internal data
  structure.

* The automatic alignment of `wfield:` and `wvalue:` is in line with
  the automatic alignment of `field:` etc., but is at odds with with
  the idea that these words are for data structures defined outside of
  Forth where fields may be unaligned.  Variable-flavoured fields for
  such data structures can be defined with `+field`, e.g., `15 0
  +field <name> drop`.  For value-flavoured fields an unaligned
  version of `wvalue:` would be useful, with the possible usage `15
  wvalue:u <name> drop`.

* Value-flavoured fields also inspire the idea that the byte order and
  signedness should also be part of the field definition.

Do we want to add any such words to the proposal?

### FP memory accesses

The words `sf@` `sf!` `df@` `df!` are also intended for data exchange
with the outside world, but they require alignment and there is no
provision for dealing with different byte orders.

For dealing with alignment we could add support for unaligned accesses
to these words.  This would require a change in the standard.  What is
your opinion about that?

For dealing with different byte orders one can do the potential byte
swapping on the integer side, as follows:

````
create dfbuf 1 dfloats allot

: be-df@ ( c-addr -- r ) x@ xbe dfbuf x! dfbuf df@ ;
: be-df! ( r c-addr -- ) dfbuf df! dfbuf x@ xbe swap x! ;
````


## Proposal (Changes to the standard document):

Add the following words:

`w@` ( c-addr -- u ) "w-fetch"

  u is the zero-extended 16-bit value stored at c_addr.

`w!` ( x c-addr -- ) "w-store"

   Store the least significant 16 bits of x at c_addr.

`wbe` ( u1 -- u2 )

   Convert 16-bit value in u1 from native byte order to big-endian or
   from big-endian to native byte order (the same operation).  The
   other bits are either untouched or set to 0.

`wle` ( u1 -- u2  )

   Convert 16-bit value in u1 from native byte order to little-endian
   or from little-endian to native byte order (the same operation).
   The other bits are either untouched or set to 0.

`w>s` ( x -- n ) "w-to-s"

   Sign-extend the low-order 16 bits in x to the full cell width.

`l@` ( c-addr -- u ) "l-fetch"

   u is the zero-extended 32-bit value stored at c_addr.

`l!` ( x c-addr -- ) "l-store"

   Store the least significant 32 bits of x at c_addr.

`lbe` ( u1 -- u2 )

   Convert 32-bit value in u1 from native byte order to big-endian or
   from big-endian to native byte order (the same operation).  The
   other bits are either untouched or set to 0.

`lle` ( u1 -- u2  )

   Convert 32-bit value in u1 from native byte order to little-endian
   or from little-endian to native byte order (the same operation).
   The other bits are either untouched or set to 0.

`l>s` ( x -- n ) "l-to-s"

   Sign-extend the low-order 32 bits in x to the full cell width.

`x@` ( c-addr -- u ) "x-fetch"

   u is the zero-extended 64-bit value stored at c_addr.

`x!` ( x c-addr -- ) "x-store"

   Store the least significant 64 bits of x at c_addr.

`xbe` ( u1 -- u2 )

   Convert 64-bit value in u1 from native byte order to big-endian or
   from big-endian to native byte order (the same operation).  The
   other bits are either untouched or set to 0.

`xle` ( u1 -- u2  )

   Convert 64-bit value in u1 from native byte order to little-endian
   or from little-endian to native byte order (the same operation).
   The other bits are either untouched or set to 0.

`x>s` ( x -- n ) "l-to-s"

   Sign-extend the low-order 64 bits in x to the full cell width.

`c>s` ( x -- n ) "c-to-s"

   Sign-extend the low-order 8 bits in x to the full cell width.

Add the following Rationale for these words:

Typical use: (Optional)

````
( c-addr ) l@ lle l>s \ 32-bit unaligned signed little-endian fetch
( c-addr ) w@         \ 16-bit unaligned unsigned native-order fetch
( n|u c-addr ) >r xbe r> x! \ 64-bit unaligned big-endian   store
( n|u c-addr ) l!           \ 32-bit unaligned native-order store
````

Implementation on systems with address-unit-bits > 8:

This wordset primarily addresses byte-addressed machines, but can also
be used on others (in particular, word-addressed machines), by using
only the lower 8 bits of each address unit (e.g., each word).  The
application would use a file-access-mode modifier `bytewise` (not
standardized) to read the input into memory in that format, then use
`c@ $ff and` `w@` `l@` `x@` to convert from this 8-bit-per-au format
into data on the data stack, work with that, then use `c!` `w!` ´l!`
`x!` to convert back to the 8-bit-per-au format, and finally submit
the data to the external destination by writing to a file opened in
`bytewise` mode.

## Reference implementation:

Will be provided at a later time.


## Testing: (Optional)

Will be provided at a later time.
