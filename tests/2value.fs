\ tests written by Gerry Jackson and submitted with the CfV

t{ 1 2 2VALUE t2val -> }t
t{ t2val -> 1 2 }t

t{ 3 4 TO t2val -> }t
t{ t2val -> 3 4 }t

: sett2val t2val 2SWAP TO t2val ;
t{ 5 6 sett2val t2val -> 3 4 5 6 }t
