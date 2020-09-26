# Notes on the 2020 Forth-200x meeting

Notes taken by M. Anton Ertl

## Participants:

* Andrew Hayley (starting 2020-09-01 13:10 UTC)
* Andrew Read (observer)
* Anton Ertl
* Bernd Paysan
* Brad Rodriguez (Observer 2020-09-01 14:40 UTC)
* Gerald Wodni
* Gordon Charlton (Observer)
* Howerd Oakford (2020-09-01 13:18-13:30, 15:25- UTC)
* Jermaine Davies
* John Helmers (observer)
* Klaus Schleisiek (Observer)
* Krishna Myneni (accepted as voting member)
* Leon Wagner
* Peter Knaggs
* Stephen Pelc
* Ullrich Hoffmann (starting 2020-09-01 13:35 UTC)
* Willem Botha

Accept Krishna for vote? Vote #5 7Y:0N:0A

Sergey Baranov is no longer a committee member


## Review of Procedures

1. Covid consequences

   Electronic meetings

   Accepted the following amendment to the constitution (Vote #8, 12Y:0:0)

   > Delete the "with the exception of the Annual General Meeting" restriction from section 5.7 of the Constitution so it reads as follows:
   > 
   > "5.7. General Meetings may be virtual (e.g.,via Telephone, eMail, chat, etc.)."

2. Brexit consequences

   none

3. Payment for services/licences

   No change

4. Other

   Vote of officers in 2020

   Due to lack of secret voting: Delay until 2021 unless somebody objects


## Reports

1. Chair

   forth-standard.org works

2. Editor

   Last public version: 2018-1

   Get the document in XML with Peter Knaggs' document type

3. Technical

   Provide Moderator button on "Manage users".  Action: GW

   Usenet postings for forth-standard.org postings: Only proposals.
   Undecided whether automated or in person.

   Mailing lists: Yahoo no longer archives the mailing lists
   Move the mailing list to Forth-Gesellschaft.  Action: BP PK

4. Treasurer

   No income, no expenses, balance: 0


## Review of Proposals and Activities

1. Recognisers
   <br>Stay as experimental proposal?
   <br>Separate POSTPONE action?
   <br>Impact of dot parser on POSTPONE?

   Discussion without resolution.  Issues:
   * GET-RECOGNIZER and +RECOGNIZER -RECOGNIZER vs. REC-SEQUENCE
   * POSTPONE action
   * starting renaming from RfD-A instead of RfD-D

2. Multi-tasking from APH

   There is common practice
   
   Implementation on GitHub
   
   Formal proposal upcoming (memory model missing). Action: AH

3. [Ambiguous condition and IMMEDIATE](https://forth-standard.org/standard/core/IMMEDIATE#contribution-112)
   <br>TC answer by Bernd, 2019-09-12 15:19:24
   <br>Move from RUV, any further action?

   Answer the latest reply.  Action: AE

   Propose to remove the ambiguous condition (from 16.3.3) that RUV points out
   Vote #9: 6Y:4N:2A, i.e., not accepted (no consensus reached).

   no committee response

4. [CS-DROP](https://forth-standard.org/proposals/cs-drop-revised-2019-08-22-#contribution-113) from UH
   <br>say orig and dest must be same size
   <br>Go to vote?

   Approved by the committee.  Vote #18 11Y:0N:1A

5. [Case insensitivity](https://forth-standard.org/proposals/case-insensitivity#contribution-114)
   <br>ASCII case insensitivity only.
   <br>Go to vote?

   Go to CfV when forth-standard.org supports it.  Action: AE

6. [Remove the "rules of FIND"](https://forth-standard.org/proposals/remove-the-rules-of-find-#contribution-115) (BP)
   <br>Locals word set?
   <br>Go to vote?

   [Wording change proposal](https://forth-standard.org/proposals/remove-the-rules-of-find-?hideDiff#reply-465) was accepted: Vote #10, 10Y:0:0

7. [Reference implementation of SYNONYM](https://forth-standard.org/standard/tools/SYNONYM#contribution-116) (AE, RUV)
   <br>Broken reference implementation.
   <br>New reference implementation.

   Vote #6:

   > Delete reference implementation for SYNONYM and replace with this rationale text (from original implementation):
   >
   > "The implementation of SYNONYM requires detailed knowledge of the host implementation, which is why it is standardized." 

   12Y:0:0 Accepted

8. [VOCABULARY](https://forth-standard.org/proposals/vocabulary#contribution-117) (UH)

   The committee felt there was no question that this is common
   practice, so it skipped the CfV part and went directly to committee
   vote.

   Vote #7:

   > Adopt [the latest (2020-09-01)](https://forth-standard.org/proposals/vocabulary#reply-453) version of the VOCABULARY proposal to the standard.

   12Y:0:0 Accepted

9. [Unfindable definitions](https://forth-standard.org/standard/tools/TRAVERSE-WORDLIST#contribution-118) (RUV)

   [Wording change for TRAVERSE-WORDLIST](https://forth-standard.org/proposals/traverse-wordlist-does-not-find-unnamed-unfinished-definitions?hideDiff#reply-487) to address the issue

   Vote #20: 11Y:0:0 Accepted

10. [Case sensitivity in [IF] and friends](https://forth-standard.org/standard/tools/BracketELSE#contribution-121)

    Reference implementation looks good.  Vote #11 to accept this
    reference implementation: 10Y:0:0 Accepted.

11. [Clarify FIND](https://forth-standard.org/proposals/clarify-find?hideDiff#reply-165) and [Clarify FIND, more classic approach](https://forth-standard.org/proposals/clarify-find-more-classic-approach#contribution-122) 

    No action, look at it again next year.

12. FIND-NAME

    Accepted in 2018

13. License (JK, RUV)

    Addressed in [License to use reference implementations](https://forth-standard.org/proposals/licence-to-use-reference-implementations?hideDiff#reply-481)

    Vote #19: 12Y:0:0 Accepted

14. [String, REPLACES](https://forth-standard.org/standard/string/REPLACES#contribution-124) (RUV)
    <br>Error if macro does not exist during compilation?

    Proposed wording change in REPLACES:
    
    > This breaks the contiguity of the current region and is not allowed during compilation of a colon definition
    
    with
    
    > Therefore REPLACES cannot be performed during compilation of a colon definition or in the middle of a contiguous region.
    
    Vote #14: 11Y:0N:1A Accepted

15. Why RECURSE is needed (BI)
    <br>Pick a TC answer.

    Request answered

16. [Input values other than true and false [IF]](https://forth-standard.org/proposals/input-values-other-than-true-and-false#contribution-126)
    <br>Pick flag as z/nz, vote, TC response

    Vote #16 12Y:0:0 Accepted

17. [sample implementation that can also be interpreted (MAX)](https://forth-standard.org/standard/core/WITHIN#contribution-127)
    <br>Adopt RUV's response as TC answer.

    ruv's response is correct

18. [Better wording for Colon](https://forth-standard.org/proposals/better-wording-for-colon#contribution-128) (RUV)

    Instead of the proposed wording, the committee accepted the following  wording change (Vote #13: 11Y:0N:1A):
    
    > Replace the first paragraph of 6.1.0450 : (colon)
    > 
    > > Skip leading space delimiters. Parse name delimited by a space. Create a definition for name, called a "colon definition". Enter compilation state and start the current definition, producing colon-sys. Append the initiation semantics given below to the current definition.
    > 
    > with the following
    > 
    > > Skip leading space delimiters. Parse name delimited by a space. Create a definition for name. Enter compilation state and start the current definition, producing colon-sys. Append the initiation semantics given below to the current definition.

19. [NAME>INTERPRET wording](https://forth-standard.org/proposals/name-interpret-wording#contribution-129) (RUV)

    To address the execution token issue, [Proposal: Reword the term
    "execution
    token"](https://forth-standard.org/proposals/reword-the-term-execution-token-?hideDiff#reply-486)
    was drafted.  This is now up for review by the general public.

20. [The parts of execution semantics and the calling definition](https://forth-standard.org/standard/core/Colon#contribution-130) (RUV)

    Write a proposal eliminating initiation semantics of : and maybe other words. Action: AE

21. [Recognizer RfD rephrase 2020](https://forth-standard.org/proposals/recognizer-rfd-rephrase-2020#contribution-131) (UH)
    <br>Move to recogniser workshop

    Discussed, see point 1.

22. ["(" typo in a testcase](https://forth-standard.org/standard/core/p#contribution-132) (RUV)
    <br>Assign to editor

    Editor fixes it.  Action: PK

23. [Wording: declare undefined interpretation semantics for locals](https://forth-standard.org/proposals/wording-declare-undefined-interpretation-semantics-for-locals#contribution-133) (RUV)
    <br>Remove ambiguous conditions

    The committee accepted the following wording change (Vote #17, 12Y:0:0)

    > This is true for (LOCAL) so we should add:
    > 
    > local Interpretation:
    > <br>Interpretation semantics for this word are undefined.
    > 
    > 
    > LOCALS| refers to (LOCAL) so (LOCAL) covers the case.
    > 
    > For {: we need to add:
    >
    > name Interpretation
    > <br>The interpretation semantics of name are undefined
    > 
    > then remove the ambiguous condition in name Execution.




24. [Word set of S>D word](https://forth-standard.org/standard/core/StoD#contribution-135) (RUV)
    <br>Leave as is?

    [Committee response](https://forth-standard.org/standard/core/StoD#reply-468)

25. [Same name token for different words](https://forth-standard.org/proposals/same-name-token-for-different-words#contribution-136) (RUV)

    No committee response for now

26. [Recognizer for locals](https://forth-standard.org/standard/locals#contribution-137) (RUV)

    No committe response

27. [There is error in testing SM/REM](https://forth-standard.org/standard/core/SMDivREM#contribution-138) (MB)
    <br>Pass to editor

    Pass to editor. Action: PK

28. [Defer Implementation](https://forth-standard.org/standard/core/DEFER#contribution-141) (Tolich)

    [Committee answer](https://forth-standard.org/standard/core/DEFER#reply-562)

29. [Recogniser](https://forth-standard.org/proposals/recognizer#contribution-142) (BP)
    <br>Move to recogniser workshop.

    Move to workshop

30. [Does wording imply that if you SYNONYM a word with the same name](https://forth-standard.org/standard/tools/SYNONYM#contribution-143) (JN)

    No committee response

31. [What happens when parse reaches the end of the parse area?](https://forth-standard.org/standard/core/PARSE#contribution-144) (JN)

    Response.  Action: AE

32. [TEST instead of TEAT in F.1 para 2](https://forth-standard.org/standard/testsuite#contribution-145) (JN)
    <br>Pass to editor

    Pass to editor. Action: PK

## Workshop Topics
Workshops are topics for discussion outside the formal meeting.

1. Future Document Format

   Peter Knaggs may step down as editor.  Gerald Wodni (technical
   director, responsible for forth-standard.org) strongly prefers staying
   with LaTeX.  The XML format looks interesting; conversion cost,
   including tool conversion cost may be an issue, but there are some
   ideas on how to deal with it.

2. Stack comments
   <br>stack comments should be parseable
   <br>Stack naming S: D: F: N: R:
   <br>stack effect notation
   <br>stack effect conventions

   This workshop did not happen.

3. Test suites
   <br>Philosophy
   <br>J Hayes sequencing
   <br>G Jackson suite

   Connect with G Jackson. Action: PK

4. Licensing

   Resulted in [License to use reference implementations](https://forth-standard.org/proposals/licence-to-use-reference-implementations?hideDiff#reply-481)

    Vote #19: 12Y:0:0 Accepted

5. CfV

   Short workshop working out how CfVs should work on forth-standard.org

6. Recognizers

   Some bikeshedding about names and terminology.

   Remove the opacity of rectype to allow to perform rectype dispatch
   with `STATE @ CELLS + @ EXECUTE`.  This works better with the A/B
   POSTPONE, if you do a POSTPONE mode (]] ... [[).

   Dealing with C/D POSTPONE in cross-compiling is possible (Bernd Paysan
   knows how to do it).

7. Workshop reports

   See sections above



## Consideration of proposals + CfV votes

See above

## Matters arising

Voting on officers moved to 2021

## Any other business

1. quotations wording

   offline

2. [Reference implementation does not seem to cope with changes to the stack](https://forth-standard.org/standard/tools/BracketELSE#contribution-134)

   Had been withdrawn

3. Review votes

   Close votes at the end of EuroForth

## Date of next meeting

September 8-10, 2021 in Rome
<br>or
<br>September 7-9, 2021, online
