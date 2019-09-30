# 2019 Forth 200x Meeting Notes

## Attendance:

- Willem Botha
- Jermaine Davies
- M. Anton Ertl
- Ulrich Hoffmann
- Peter Knaggs
- Howerd Oakford
- Bernd Paysan from Mi 15:30
- Stephen Pelc (chair)
- Leon Wagner (remote)
- Gerald Wodni

## 2 Review of Procedures

Remote observers: No, because committee members want to freely speak
their mind, and need to know who observes.  The prospect of being
recorded was seen as particularly problematic.

### 2a) (SFP) The meeting minutes are not detailed enough for people who did not participate in the meeting.

Needs volunteers, but we will try to be more verbose.

### 2b) (SFP) GW should put the meeting agendas and minutes at forth-standard.org
### 2c) (SFP) Other actions to migrate forth200x.org to forth-standard.org have not been implemented.

Workshop on forth-standard.org and forth200x.org integration.  GW, AE, PK

First part already done: <https://github.com/Forth-Standard/forth200x>

### 2d) (PJK et al) Move updated proposal process to document.

Both the 2018 and 2019 meeting results will be integrated into the
document in one step.

### 2e) CfV on forth-standard.org

Still to be done

### 2f) Vote to accept 2018 minutes

7A:0N:2A (abstentions due to non-attendance in 2018)

### 2g) Brexit Consequences

Take action when needed (and not before)

## 3 Reports

### 3a) Chair

No electronic meeting

### 3b) Editor

See 2d.  There is <https://github.com/Forth-Standard/forth-standard>

### 3c) Technical

A few improvents have been implemented, more to come.

### 3d) Treasurer

Balance: 0, no income, no expenses


## 4 Review of Proposals and Activities

### 4a) Recognizers

For personal reasons Matthias Trute cannot continue.  BP is picking up
the proposal.

Postpone action discussion: Workshop: BP, SP, AE

### 4b) Multi-tasking

Discussion of the initialization of UVALUEs: Initialize by cloning an
existing task, not by a HIS-like mechanism.

Contact Andrew Haley (SP).  Workshop topic (pretty much everyone)

### 4c) External Functions

Workshop where GW explains the SWIG approach: Instead of doing a lot
in Forth, do a lot in SWIG, and the result provides everything that
Forth needs.

### 4d) From forth-standard.org

BL Rationale:
GW volunteers for rewording

### 4e) Proposal for source-relative filenames

Presentation from GW (in workshop).  Committee encourages: Move to
proposal

### 4f) Internationalisation

Discussion of character sets.  Discussion of language and country.

### 4g) from forth-standard.org:

#### [ALSO without VOCABULARY](http://forth-standard.org/standard/search/ALSO#contribution-70)

Proposal for >ORDER AE

Proposal for VOCABULARY UH

Response on behalf of the committee AE

#### [Should TO be findable?](http://forth-standard.org/standard/core/TO#contribution-72)

Related to Clarify FIND

In addition, make a proposal to fix the shortcomings pointed out by
Ruvim: execution token AE

#### Case sensitivity, Case insensitivity

Discuss them, finally do CfV

#### [[ELSE] without preceding [IF]](http://forth-standard.org/standard/tools/BracketELSE#contribution-54)

Write a rationale why this is a bad idea BP

#### [[if] and [else] parse white space - including comments](http://forth-standard.org/standard/tools/BracketELSE#contribution-74)

Discuss in the rationale the caveats of the interaction of parsing
words and [else], [if], [then] etc.

Changes to [ELSE] and [IF] rationale 10Y:0:0

#### [F>R and FR> to support dynamically-scoped floating point variables](http://forth-standard.org/standard/float#contribution-75)

AE: Write a reply inquiring about existing practice, wheter FP locals
have more existing practice, and possibly turning it into a proper
proposal.

Flocals F: proposal to bewritten by SP

add [ notation for locals to Gforth BP

#### [The case of undefined interpretation semantics](http://forth-standard.org/standard/tools/BracketDEFINED#contribution-86)

BP will write a proposal for fixing this issue

#### [Interpretation semantics](http://forth-standard.org/standard/core/COMPILEComma#contribution-87)

AE: write a proposal to define interpretation semantics of COMPILE,

#### [Terminology and wording regarding "dictionary"](http://forth-standard.org/standard/notation#contribution-82)

AE: write a reply, suggesting that ruv writes proposals;
Renaming "name space" to "header space" should find consensus
Some committee members are nervous about touching dictionary

#### [Ambiguous condition could be removed](http://forth-standard.org/standard/core/POSTPONE#contribution-98)

PK: Write on the committee's behalf


#### [GNU C RESTRICT would make sense in the standard](http://forth-standard.org/standard/core#contribution-104)

PK: Wrote a reply on the committee's behalf

#### [CS-DROP](http://forth-standard.org/standard/tools/CS-PICK#contribution-113)

UH: Adjust proposal to allow exactly one use of orig.  Proceed to CfV.


### 4h) Further forth-standard.org contributions:

For this part I (AE) presented the contributions to the committee, so
I noted only those parts where the committee voted and those where I
should take an action.  The Minutes should contain more about this. 

#### [Behavior when no text found](http://forth-standard.org/standard/core/CHAR#contribution-78)

AE: Make a proposal to make it explicit

#### [Revise Rationale of Buffer:](http://forth-standard.org/standard/core/BUFFERColon#contribution-90)

Committee vote on this wording change: 0Y:5N:4A

#### [Executing compilation semantics](http://forth-standard.org/standard/doc#contribution-94)

Accepted 9Y:0N:1A

#### [description of "nt" in the standard](http://forth-standard.org/standard/tools#contribution-109)

Move the definition of "name token" from 15.3 to 15.2 8Y:0N:0A

Coming out of that:

Definition of "search order" and rewording of [defined] and [undefined]
9Y:0:0


## 6) Workshops

### Clarify find

Move to CfV

### Header flags

Discussion

### forth-standard.org

Discuss ways to avoid overloading the committee.  Closing (with the
option to reopen) a contribution would take it off the ToDo list of
the committee.  If a committee member replies to a requests for
clarification, he will normally close the contribution.


