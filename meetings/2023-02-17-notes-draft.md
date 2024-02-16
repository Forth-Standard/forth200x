## Agenda Forth-200x interim Meeting 2023-02-17T15:00Z

16:00 MET
07:00 PST

Estimated duration 120 minutes + workshop

- Welcome / Participants (10 min)

Anton Ertl
Ulrich Hoffmann
Leon Wagner
Gerald Wodni
ruv
Bernd Paysan
Krishna Myneni

- Agreement on the agenda  (5 min)
  Do we have any additional points to discuss?

- Review of Procedures (5 min)

Status of CfV voting?  Not yet complete.

- Review of new Proposals and Contributions (since the September'22
meeting, 2022-09-14/15) (60 min)
  see https://forth-standard.org/proposals and
https://forth-standard.org/contributions
   - [250] Formatting: spaces in data type symbols
     in formatting-spaces-in-data-type-symbols
     2022-09-17 11:27:57 - ruv

Leave it to the discretion of the editors (Action: AE, ruv)


     exception, THROW
     Input source after THROW
     Comment
     2023-02-15 22:50:52

There should be a specification of an input stack that is also
restored by THROW.  Fixing this is future work.  (Action: AE)
     
     avatar of JohanKotlinski
     core, SAVE-INPUT
     Is data stack required?
     Request for clarification
     2023-02-14 20:17:30

Recommend not to implement it (Action: AE)
     
     avatar of JimPeterson
     tools, NtoR
     Bad Stack Notation?
     Comment
     2023-02-13 15:22:04

Table this until the next meeting.

   - block, LOAD -
     Possible Test Case, Suggested Testcase
     2023-02-11 16:39:10 - JimPeterson

Actually run it and then respond (Action: UH)

   - block, bs  -
     "... the remainder of the current,
     line."? Request for clarification
     2023-02-11 16:04:29 - JimPeterson

The remainder of the line is determined by character count, not
contents (Action: UH)

   - block, THRU  -
     Possible Reference Implementation, Suggested reference implementation
     2023-02-11 15:56:14 - JimPeterson

Actually run it and then respond (Action: UH)

   - core, PICK  -
     Example implementation for PICK,
     Suggested reference implementation
     2023-02-10 21:51:27 - ruv

Fix the stack picture (Action: ruv)
Accept (Action: AE)
No common practice for stick

   - core, ROLL  -
     Example implementation for ROLL,
     Suggested reference implementation
     2023-02-10 21:32:22 - ruv

Accept (Action: AE)

   - core, RESTORE-INPUT  -
     About behavior or RESTORE-INPUT,
     Comment
     2023-02-07 16:09:01 - ruv

prepare a proposal to destandardize SAVE-INPUT and RESTORE-INPUT (Action: ruv)

   - core, RESTORE-INPUT  -
     Address of the input buffer after RESTORE-INPUT,
     Request for clarification
     2023-02-07 10:03:28  - ruv

If we fail to de-standardize SAVE-INPUT RESTORE-INPUT, we need to look
at this issue again.

   - core, d  -
     Suggested reference implementation
     2023-01-12 08:26:50 - JohanKotlinski

Actually run it and then respond (Action: AE)

   - relax-documentation-requirements-of-ambiguous-conditions
   [272] Relax documentation requirements of Ambiguous Conditions -
Proposal
   2022-12-29 19:25:35 - JohanKotlinski

Proceed to CfV (Action: AE)

   - doc - Comment
   2022-12-21 11:40:45  - JohanKotlinski

Close, and ask contributors to the discussion to do separate topics if
they need the stuff to be open. (Action: AE)

   - core, CHAR   -
     Describe Compile time and Run time behavior,
     Comment
     2022-11-19 09:25:07 - PopovMP

Done

- Bio Break (10 min)

- Any other business (20 min)
   - A Forth 2022/2023 snapshot?

   Will be done when the editors find the time.
   Supported by Leon Wagner
   Discussion about website that supports multiple versions.
   
- Date of next meeting (5 min)
   - EuroForth-2023?

Standard meeting 2023-09-13 .. 2023-09-15 in Rome.

- Workshop (Forth Modification Lab) (open end)
   - What are current Forth community topics
     - Memory operators 8, 16, 32, 64 bits - signed/unsigned

Action: BP

     - other topics?
   - discussion

Do we add things to the standard that are common practice but maybe
not desired practice?

Put common but undesired words in Attic or (to be last in the list of
wordsets) Zattic.


