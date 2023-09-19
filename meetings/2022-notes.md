# Notes from the Forth200x meeting in 2022

## Participants

* AE M. Anton Ertl 
* BP Bernd Paysan 
* BR Brad Rodriguez 
* GW Gerald Wodni 
* HO Howerd Oakford
* KM Krishna Myneni 
* LW Leon Wagner 
* PK Peter Knaggs 
* RP Ruvim Pinka
* SP Stephen Pelc
* UH Ulrich Hoffmann (chair)

Everyone is allowed to vote.

## Review of Procedures

### Document

PK resigns as editor

#### Should we do another snapshot?

Several people answered "yes"

#### Rolling document

Having a rolling document has its challenges

### Memory access words
  
The le-s16@ approach has been abandoned because it takes too many
words.  The toolbox approach has not become a proposal.  SP does not
like it.

### Multi-tasking

Workshop SP LW

### IEEE FP wordset

Workshop KM

### Slow progress over the last years

Some discussion

### Minutes

How do we get better (more detailed?) minutes?  Can these notes serve
as minutes?  Report about the completion of the draft notes/minutes in
email.  Committee members are encouraged to check them while the
meeting is still on our mind.

## Reports

1. Chair

Reports on the intermediate meeting.

2. Editor

no progress

3. Technical

CfV system arriving.  Links to markdown lists for proposals at the
bottom of the proposals page.

4. Treasurer

Balance: 0

No income, no expenses

## Election/Confirmation of officers

We only have one officer to elect: The editor.

Candidates: Only Anton Ertl volunteered, Ruvim volunteered as assistant.
Accepted with (11Y:0N:0A)

## Review of Proposals/Contributions


### Proposals from [forth-standard.org/proposals](https://forth-standard.org/proposals)

#### Proposals in the state *formal*

1. Specify that 0 THROW pops the 0 ([https://forth-standard.org/proposals/specify-that-0-throw-pops-the-0#reply-794](https://forth-standard.org/proposals/specify-that-0-throw-pops-the-0#reply-794))

Fine-tune the wording, and do a committee vote.  Vote #27 Accepted 10Y:0N:1A

#### Proposals in the state *voting*

1. PLACE +PLACE ([https://forth-standard.org/proposals/place-place#reply-745](https://forth-standard.org/proposals/place-place#reply-745))

Revise, taking comments into account.  Action: UH


#### Proposals in the state *informal*

During this meeting we retired or otherwise removed a number of
proposals from the next meeting schedule.

### Contributions on [forth-standard.org](https://forth-standard.org) since last meeting

There are a lot of contributions since the interim meeting in
February. Find them in the appendix of the Agenda.  The appendix of
these notes contains the contributions we have processed during this
meeting.


## Workshop Topics

None were explicitly organized, but there was a workshop on recognizers.

## Consideration of proposals + CfV votes

Mentioned elsewhere in these notes.

## Workshop reports

The result of a recognizer workshop has been presented and discussed
in the minimal recognizer proposal.

## Matters arising

At EuroForth present a summary of what we did on the standard.
Action: UH

The Forth-20.1 document is available.  Discussion about having a
snapshot, and possibly a train-station model.

## Any other business

Something else?

## Date of next meeting

Provisional dates:

* virtual Forth200x meeting on 2023-02-17
* Forth200x 2023-09-13 .. 2023-09-15
* EuroForth 2023-09-15 .. 2023-09-17

---

# Appendix to *Review of Proposals/Contributions*

## Proposals in the state *informal* (most recent first)

1. Pronounciations ([pronounciations #261](https://forth-standard.org/proposals/pronounciations#contribution-261))

The committee prefers 'Have "than"', but not for <# and #>.  Revise
proposal and submit it for committee vote.  Done: AE Vote #26 Accepted: 10Y:0N:1A

2. Exclude zero from the data types that are identifiers ([exclude-zero-from-the-data-types-that-are-identifiers #252](https://forth-standard.org/proposals/exclude-zero-from-the-data-types-that-are-identifiers#contribution-252))

Lots of discussion, especially about the validity of file-ids with the
value 0.  When asked, none of the participants could name a system
that has a problem with disallowing 0 as address, and none claimed
that he had never used 0 as impossible address.

Prepare a new version of the proposal addressing the things discussed.
Action: RP

3. Clarification for execution token ([clarification-for-execution-token #251](https://forth-standard.org/proposals/clarification-for-execution-token#contribution-251))

Should the "Definitions of terms" contain informal explanations of the
terms that are useful for understanding the standard or formal
definitions that try to avoid ambiguity?

Formal: RP, PK;
Informal: AE, UH, HO

Organize a subcommitte on this kind of topic. Action: RP

4. Formatting: spaces in data type symbols ([formatting-spaces-in-data-type-symbols #250](https://forth-standard.org/proposals/formatting-spaces-in-data-type-symbols#contribution-250))

Go to committee vote.  Done: RP.  Vote #30 accepted with 10Y:0N:1A

5. Revert rewording the term &quot;execution token&quot; ([revert-rewording-the-term-execution-token- #249](https://forth-standard.org/proposals/revert-rewording-the-term-execution-token-#contribution-249))

Assign to the subcommittee above.

6. Better wording for &quot;Glossary notation&quot; ([better-wording-for-glossary-notation- #215](https://forth-standard.org/proposals/better-wording-for-glossary-notation-#contribution-215))

Wordsmithing by LW.

> Each glossary entry specifies a Forth definition and consists of the index line and one or more semantic descriptions for the definition.
>
> The first paragraph of a semantic description contains an optional label for the semantics and a stack diagram for each stack affected by performing these semantics.

Go to committee vote.  Done: RP  vote #31 10Y:0N:1A


7. Better wording for &quot;data field&quot; term ([better-wording-for-data-field-term #214](https://forth-standard.org/proposals/better-wording-for-data-field-term#contribution-214))

Wordsmithing resulted in:

> A data space region associated with a Forth word defined by CREATE (6.1.1000)

Goto committe vote. Done: RP Vote #32 8Y:1N:1A

8. Tick and undefined execution semantics - 2 ([tick-and-undefined-execution-semantics-2 #212](https://forth-standard.org/proposals/tick-and-undefined-execution-semantics-2#contribution-212))

Retract.  Action: SP

9. EMIT and non-ASCII values ([emit-and-non-ascii-values #184](https://forth-standard.org/proposals/emit-and-non-ascii-values#contribution-184))

Maybe use code unit to explain character.

Go to CfV (as suggested in the 2021 meeting). Action: AE


10. Tick and undefined execution semantics ([tick-and-undefined-execution-semantics #163](https://forth-standard.org/proposals/tick-and-undefined-execution-semantics#contribution-163))

Assign to the subcommittee above.


11. Common terminology for recognizers discurse and specifications ([common-terminology-for-recognizers-discurse-and-specifications #161](https://forth-standard.org/proposals/common-terminology-for-recognizers-discurse-and-specifications#contribution-161))

Discussed with the minimalistic core API for recognizers below.


12. minimalistic core API for recognizers ([https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers#reply-867](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers#reply-867))

Presentation and Discussion

The committee recommends taking this to CfV.  Action: BP
    
13. An alternative to the RECOGNIZER proposal ([https://forth-standard.org/proposals/an-alternative-to-the-recognizer-proposal#reply-493](https://forth-standard.org/proposals/an-alternative-to-the-recognizer-proposal#reply-493))

Retired, because it is no longer championed.  Done: AE

14. Call for Vote - Ambiguous condition in 16.3.3 ([https://forth-standard.org/proposals/call-for-vote-ambiguous-condition-in-16-3-3#reply-460](https://forth-standard.org/proposals/call-for-vote-ambiguous-condition-in-16-3-3#reply-460))

Retired.  This was made up on the spot in the 2020 meeting in reaction
to a discussion at this meeting.  It misses context to be useful today.
Action: GW

15. XML Forth Standard - migration from LaTeX to DocBook ([xml-forth-standard-migration-from-latex-to-docbook #154](https://forth-standard.org/proposals/xml-forth-standard-migration-from-latex-to-docbook#contribution-154))

Retire it (for possible future use, but not committee consideration).
Action: GW
     
16. Nestable Recognizer Sequences ([nestable-recognizer-sequences #149](https://forth-standard.org/proposals/nestable-recognizer-sequences#contribution-149))

Retired, because it is integrated into the [[160] minimalistic core
API for
recognizers](https://forth-standard.org/proposals/minimalistic-core-api-for-recognizers#reply-892).
Done: AE


17. OPTIONAL IEEE 754 BINARY FLOATING-POINT WORD SET ([https://forth-standard.org/proposals/optional-ieee-754-binary-floating-point-word-set#reply-420](https://forth-standard.org/proposals/optional-ieee-754-binary-floating-point-word-set#reply-420))

EuroForth workshop, will be revised.  Action: KM
      
18. Recognizer ([recognizer #142](https://forth-standard.org/proposals/recognizer#contribution-142))

Retired, because it is superseded by the minimal core API for
recognizers.  Action: BP

19. Same name token for different words ([same-name-token-for-different-words #136](https://forth-standard.org/proposals/same-name-token-for-different-words#contribution-136))

Retired.  Originally intended as comment.  Action: RP

20. Recognizer RfD rephrase 2020 ([recognizer-rfd-rephrase-2020 #131](https://forth-standard.org/proposals/recognizer-rfd-rephrase-2020#contribution-131))

Retired.  Superseded by the minimal core API for recognizers.  Action:
UH

21. NAME&gt;INTERPRET wording ([name-interpret-wording #129](https://forth-standard.org/proposals/name-interpret-wording#contribution-129))

Will be discussed in the subcommittee above.

22. Clarify FIND, more classic approach ([https://forth-standard.org/proposals/clarify-find-more-classic-approach#reply-682](https://forth-standard.org/proposals/clarify-find-more-classic-approach#reply-682))

Refer this to the subcommittee above.


23. Remove the “rules of FIND” ([https://forth-standard.org/proposals/remove-the-rules-of-find-#reply-465 ](https://forth-standard.org/proposals/remove-the-rules-of-find-#reply-465 )) 

Accepted in 2020 meeting.  Action: BP
 
24. Case insensitivity ([[114]case-insensitivity](https://forth-standard.org/proposals/case-insensitivity#contribution-114))

Go to CfV.  Action: AE

25. CS-DROP (revised 2019-08-22) ([https://forth-standard.org/proposals/cs-drop-revised-2019-08-22-#reply-471](https://forth-standard.org/proposals/cs-drop-revised-2019-08-22-#reply-471))

Revise it.  Action: UH
    
26. Right-justified text output ([right-justified-text-output #101](https://forth-standard.org/proposals/right-justified-text-output#contribution-101))

Retire it, because the proponent is apparently no longer interested.
Done: AE
    
27. Executing compilation semantics ([executing-compilation-semantics #94](https://forth-standard.org/proposals/executing-compilation-semantics#contribution-94))

Committee Vote.  Done: UH  Vote #29 10Y:0N:1A

28. We did not get around to processing more proposals.
