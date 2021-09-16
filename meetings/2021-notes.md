# Forth200x 2021 meeting notes

## Participants:


* M. Anton Ertl
* Bernd Paysan
* Bill Ragsdale (observer)
* Gerald Wodni
* Brad Rodriguez
* Frank Guidi (observer)
* Howerd Oakford
* Jermaine Davies
* Krishna Myneni
* Leon Wagner
* Peter Knaggs (from Tu 16:25)
* Rolf Hemmerling (observer)
* Stephen Pelc (from Tu 15:40)
* Ulrich Hoffmann
* Willem Botha

Session chair until Tu 15:40: GW

Session chair from Wed 18:00-19:30 AE

Andre Haley has missed this meeting.


## Review of Procedures

1. Covid consequences

Nothing of note.

2. Brexit consequences

Nothing of note.

3. Payment for services and licences Until now, this has always been
done informally.

Continues informally.

4. Closure mechanism for discussions to avoid inconsequential stuff
being discussed at the next meeting.

Not as much closed as we wanted.

5. Real name of user should be required by the web site, but need not
be displayed. A standard is a public document.



## Reports

1. Chair 

Nothing of note

2. Editor

Was not present at the time

3. Technical

Nothing of note

4. Treasurer

Nothing of note


## Election of officers

All elected officers accept the election

1. Chair

Ulrich Hoffman volunteers  11Y:0N:0A

2. Editor

Peter Knaggs volunteers  11Y:0N:1A

3. Technical

Gerald Wodni volunteers   11Y:0N:0A

4. Treasurer

Bernd Paysan volunteers   11Y:0N:0A


## Snapshot of the standard

We make a snapshot and will review the document for the next meeting.


## Review of Proposals

### Reword the term &quot;execution token&quot; ([reword-the-term-execution-token- #157](https://forth-standard.org/proposals/reword-the-term-execution-token-#contribution-157))

The consensus from 2020 has evaporated, leading to a long discussion.
Workshop set up for a potential alternative proposal, revisit in main
session on Wednesday.

After a workshop and some more discussions, a consensus emerged for
the following new text:

A value that can be passed to EXECUTE (6.1.1370)

As a result, AE posted a [new version of the
proposal](https://forth-standard.org/proposals/reword-the-term-execution-token-?hideDiff#reply-742)
and this was voted on with vote #21: 12Y:0:0.

### minimalistic core API for recognizers ([minimalistic-core-api-for-recognizers #160](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers#contribution-160))

Presentation by Bernd Paysan.  Discussion.  Several participants found
it too minimal.  Some criticism of the factoring.   Workshop proposed.

See [workshop report below](#recognizers), which also covered the next
two topics.


### An alternative to the RECOGNIZER proposal ([an-alternative-to-the-recognizer-proposal #159](https://forth-standard.org/proposals/an-alternative-to-the-recognizer-proposal#contribution-159))

Some discussion.  Some people think it goes too far.  At the same
time, some consider it too inflexible.

### Common terminology for recognizers discurse and specifications ([common-terminology-for-recognizers-discurse-and-specifications #161](https://forth-standard.org/proposals/common-terminology-for-recognizers-discurse-and-specifications#contribution-161))

Several people like the terms proposed in the proposal.  Discussion on
whether "perceptor" is actually needed.

### Tick and undefined execution semantics ([tick-and-undefined-execution-semantics #163](https://forth-standard.org/proposals/tick-and-undefined-execution-semantics#contribution-163))

Some discussion of whether every word should be tickable.  SP
volunteers to write [a
proposal](https://forth-standard.org/proposals/tick-and-undefined-execution-semantics-2#contribution-212).

### EMIT and non-ASCII values ([emit-and-non-ascii-values #184](https://forth-standard.org/proposals/emit-and-non-ascii-values#contribution-184))

Are there any systems where EMIT does not send raw bytes?  Find out
with a CfV.

### Reference implementations are not normative ([reference-implementations-are-not-normative #187](https://forth-standard.org/proposals/reference-implementations-are-not-normative#contribution-187))

Go to committee vote.  AE.  Vote #22.

### Wording change for &quot;COMPILE,&quot;: harmonization with terms ([wording-change-for-compile-harmonization-with-terms #200](https://forth-standard.org/proposals/wording-change-for-compile-harmonization-with-terms#contribution-200))

Change the proposed sentence as follows:

Append the semantics identified by xt to the execution semantics of the current definition.

Committee vote on that.  AE.  Vote #23: 12Y:0:0

### Let us adopt the Gerry Jackson test suite as part of Forth 200x ([let-us-adopt-the-gerry-jackson-test-suite-as-part-of-forth-200x #63](https://forth-standard.org/proposals/let-us-adopt-the-gerry-jackson-test-suite-as-part-of-forth-200x#contribution-63))

Discussion about how to deal with the test suite in the document and
forth-standard.org.

Contact Gerry Jackson on working together for making it easier to get
the code from the test suite into forth-standard.org.  GW

Either add some markup to the suite to allow integrating it or do some
more automatic program slicing.

### PLACE +PLACE ([place-place #206](https://forth-standard.org/proposals/place-place#contribution-206))

Some discussion about the name of +PLACE and the wording.

UH updates the proposal for the suggested wording and prepares a
committee vote.

### Clarify FIND ([clarify-find #55](https://forth-standard.org/proposals/clarify-find#contribution-55))

Do we want to be able to write a user-defined text interpreter, and do
we want to be able to do it with FIND?

Retract.  AE Done.

### Clarify FIND, more classic approach ([clarify-find-more-classic-approach #122](https://forth-standard.org/proposals/clarify-find-more-classic-approach#contribution-122))

Contact ruv about this proposal. Action: AE

### NAME&gt;INTERPRET wording ([name-interpret-wording #129](https://forth-standard.org/proposals/name-interpret-wording#contribution-129))

Contact ruv about this proposal. Action: UH

### Same name token for different words ([same-name-token-for-different-words #136](https://forth-standard.org/proposals/same-name-token-for-different-words#contribution-136))

A system is allowed to do it, but is not required to do it.  The
committee does not see a need to add such a clause to the standard.
AE.  Done

### [Tick and undefined execution semantics - 2](https://forth-standard.org/proposals/tick-and-undefined-execution-semantics-2#contribution-212)

Throw -13 is name is not found.

performing the semantics identified by xt

[Workshop in the evening](#xt)

and then let the community have a go at it until the next meeting





## Requests for clarification

### Why &quot;[&quot; is specified using immediacy? ([core, Bracket #205](https://forth-standard.org/standard/core/Bracket#contribution-205))

SP writes up an answer and closes it.  Done.

### Implementation for S&quot; via the immediacy mechanism ([file, Sq #201](https://forth-standard.org/standard/file/Sq#contribution-201))

Answer by BP and closed.  Done.

### Web site problem? ([right-bracket #197](https://forth-standard.org/standard/right-bracket#contribution-197))

GW answers it.  Done

### Return value of XC-WIDTH for control characters ([xchar, XC-WIDTH #196](https://forth-standard.org/standard/xchar/XC-WIDTH#contribution-196))

Write an answer AE. Done

### How many cells can be compared ? Must have any limit ? ([string, COMPARE #193](https://forth-standard.org/standard/string/COMPARE#contribution-193))

SP closes it again. Done

### ALLOT in ROMable systems ([core, ALLOT #190](https://forth-standard.org/standard/core/ALLOT#contribution-190))

Closed by AE.  Done.

### Stack effect of LEAVE during compilation ([core, LEAVE #185](https://forth-standard.org/standard/core/LEAVE#contribution-185))

Answer on behalf of the committee and close.  AE.  Done

### Size of implementation dependent data types ([usage #183](https://forth-standard.org/standard/usage#contribution-183))

Answer on behalf of the committee and close.  AE.  Done

### Is a counted string limited to 255 chars? ([core, COUNT #181](https://forth-standard.org/standard/core/COUNT#contribution-181))

Closed by AE.  Done.

### Can `BLOCK` transfer from mass storage in the case when block u is already in a block buffer? ([block, BLOCK #179](https://forth-standard.org/standard/block/BLOCK#contribution-179))

Closed by AE.  Done.

### GT1 not defined  ([core, POSTPONE #175](https://forth-standard.org/standard/core/POSTPONE#contribution-175))

Answer by GW on behalf of the committe.  Done

### write-cell-mem undefined ([memory, ALLOCATE #174](https://forth-standard.org/standard/memory/ALLOCATE#contribution-174))

Answer by GW on behalf of the committee.  Done

### Extending MARKER ([core, MARKER #162](https://forth-standard.org/standard/core/MARKER#contribution-162))

Answer by UH on behalf of the committee.  Action: UH




## Suggested reference implementation

### CMOVE implementation based on MOVE ([string, CMOVE #211](https://forth-standard.org/standard/string/CMOVE#contribution-211))

Leave it open.
github issue to accept reference implementations.  AE.  Done

### Possible Reference Implementation ([string, DivSTRING #203](https://forth-standard.org/standard/string/DivSTRING#contribution-203))

Leave it open.  It will be accepted as reference implementation in a
future version.

### Portable implementation for SYNONYM ([tools, SYNONYM #202](https://forth-standard.org/standard/tools/SYNONYM#contribution-202))

Reply and close by AE.  Done

### Portable implementation for POSTPONE ([core, POSTPONE #198](https://forth-standard.org/standard/core/POSTPONE#contribution-198))

Ruv should run it through the test suite.  Action: ??

### off-by-one error in reference implementation ([search, DEFINITIONS #165](https://forth-standard.org/standard/search/DEFINITIONS#contribution-165))

Leave it open.  Update the document with AE's implementation.  Action: PK


## Suggested Testcase

### More general Testcase ([core, DEPTH #207](https://forth-standard.org/standard/core/DEPTH#contribution-207))

Response. AE. Done

### Inaccurate Test Cases? ([double, DtoS #199](https://forth-standard.org/standard/double/DtoS#contribution-199))

Reply and closed by AE. Done


###  A final test case for [COMPILE]  ([core, BracketCOMPILE #195](https://forth-standard.org/standard/core/BracketCOMPILE#contribution-195))

Leave it open.  [COMPILE] is obsolescent.

### Additional test for UTF-16 ([core, CComma #194](https://forth-standard.org/standard/core/CComma#contribution-194))

AE and PK replied.



## Example

### Missed-order in section F.3.10 Division ([testsuite #209](https://forth-standard.org/standard/testsuite#contribution-209))

Action: PK

### Needs an example of replacing COMPILE ([core, POSTPONE #177](https://forth-standard.org/standard/core/POSTPONE#contribution-177))

Closed by AE. Done

### Relationship to block set ([core, SOURCE #172](https://forth-standard.org/standard/core/SOURCE#contribution-172))

Closed by AE. Done.  However, the editor might want to look at [AE's
answer from
2021-01-14](https://forth-standard.org/standard/core/SOURCE#reply-591).
Action: PK


## Comment

### Mistake in implementation? ([double, TwoVALUE #204](https://forth-standard.org/standard/double/TwoVALUE#contribution-204))

Editing fix.  Action: PK

### How to avoid default compilation semantics in the specification for [COMPILE] ([core, BracketCOMPILE #192](https://forth-standard.org/standard/core/BracketCOMPILE#contribution-192))

Answered by AE.  Done

### Throwing past DO/LOOP ([exception, THROW #191](https://forth-standard.org/standard/exception/THROW#contribution-191))

Closed by UH.  Done

### Note incompatability (double v single) with some older Forth's. ([core, num #182](https://forth-standard.org/standard/core/num#contribution-182))

Closed by AE.  Done

### Run-Time Section is Missing Words ([locals, bColon #178](https://forth-standard.org/standard/locals/bColon#contribution-178))

Investigate the error in converting the document.  Action: PK; Action: GW

### THROW: text doesn't match implementation example ([exception, THROW #170](https://forth-standard.org/standard/exception/THROW#contribution-170))

Answer and closed by AE.  Done

### Cells numeration in a cell-pair ([core, TwoStore #169](https://forth-standard.org/standard/core/TwoStore#contribution-169))

Closed by SP: Done

Maybe change 3.1.4.  Possible Action: PK (Looking at 3.1.4 again, I
don't see how it could be improved)

### Data object notion usage ([usage #168](https://forth-standard.org/standard/usage#contribution-168))

Leave it open

### Query re status; and minor comments ([multi-tasking-proposal #167](https://forth-standard.org/proposals/multi-tasking-proposal#contribution-167))

Invite MitraArdron to collaborate.  UH.  Done

### Data type for strings ([usage #166](https://forth-standard.org/standard/usage#contribution-166))

Closed by AE.  Done.

### Typo in stack comment ([search, SET-ORDER #164](https://forth-standard.org/standard/search/SET-ORDER#contribution-164))

Editing fix.  Action: PK

## Workshop Topics and Reports

### Recognizers

Report by UH

We discussed a blend of the Minimal RECOGNIZER API proposal (Bernd),
Recognizer Sequences (Anton) and the Recognizer Rewording Proposal
(uho)

Just before and at the beginning of our recognizer workshop, I (uho)
implemented the minimal proposal for seedForth. RECOGNIZER CORE
(FORTH-RECOGNIZER to be named differently?, NOTFOUND) and
seedForth-specific recognizers (for words, numbers). That took about
45 minutes and some debugging.

Recognizers have the stack effect ( addr u -- i*x rectype | notfound )

For RECOGNIZER EXT we agreed on having:

    the RECOGNIZER: ( xt-interp xt-comp xt-post <name> -- ) defining word
    the COMPOUND-RECOGNIZER: ( rec1 rec2 ... recn n <name> -- ) defining word
    the word GET-RECOGNIZERS ( compound-rec -- rec1 rec2 ... recn n ) to retrieve the recognizers that make up a compound recognizer
    the word SET-SECOGNIZERS ( rec1 rec2 ... recn n compound-rec -- ) to store the recognizers in a compound recognizer
    the lit-recognizer

All names are subject to change.

Given the two variations to handle STATE (either in RECOGNIZER:'s
DOES> part or in INTERPRET), yesterdays participants favoured to have
the single occurrence of STATE in INTERPRET. Further investigation and
model implementations will show whether one or the other is beneficial.

### xt

Resulted in the text for the proposal [Reword the term &quot;execution
token&quot;](https://forth-standard.org/proposals/reword-the-term-execution-token-?hideDiff#reply-742)

### Forth Document Format

Report by GW

The workflow is as follows:

- download document repository
- `make pdf`
- download Forth2HTML
- build
- run in document repository as working directory
- run assimilate script in forth-standard.org repository

#### TODOS:

##### Forth2HTML

- generate title page
- fix compilation of latest document
- fix right-bracket output directory and name

##### forth-standard.org assimilate
- add title page
- remove legacy latex-html directory
- store multiple snapshots in json format in repository
- allow switching between different versions of standard
- make section numbers visible somehow on website
- current links show latest version
- switcher to older version
- allow contributions only on current version
- allow some contributions like request-for-clarifications on latest
snapshot (i.e. Forth-2012)
- display "latest version differs" warning

### Stack comments

Stack comments should be parseable for automatic processing

## Matters arising

Meeting in half a year, for an afternoon.  Agenda for that amount of
time.  Action: UH.

## Any other business

Thanks to SP for serving as chair

## Date of next meeting

Half-year meeting 2022-02-18 13:00 UTC
Full meeting in front of EuroForth, probably mid-September 2022


