\ these tests should also work on a system with address-unit-bits>8


require ttester.fs

TESTING c@ c! c>s
t{ create sma1 $89 c, $01 c, $23 c, $45 c, $67 c, $89 c, $ab c, $cd c, $de c, -> }t
t{ create sma2 16 allot -> }t
t{ sma1 7 + c@ -> $cd }t
t{ sma1 7 + c@ c>s -> #-51 }t
t{ sma1 3 + c@ c>s -> $45 }t
t{ #-51 sma2 3+ c! sma2 3+ sma2 1 move sma2 c@ $ff and -> $cd }t

TESTING w@ w! w>s wbe wle
t{ sma1 5 + w@ wbe -> $89ab }t
t{ sma1 5 + w@ wle -> $ab89 }t
t{ sma1 5 + w@ wbe w>s -> #-30293 }t
t{ sma1 5 + w@ wle w>s -> #-21623 }t
t{ sma1 3 + w@ wbe w>s -> $4567 }t
t{ sma1 3 + w@ wle w>s -> $6745 }t

TESTING l@ l! l>s lbe lle
t{ sma1 5 + l@ lbe -> $89abcdef }t
t{ sma1 5 + l@ lle -> $efcdab89 }t
t{ sma1 5 + l@ lbe l>s -> $-76543211 }t
t{ sma1 5 + l@ lle l>s -> $-10325477 }t
t{ sma1 1 + l@ lbe l>s -> $01234567 }t
t{ sma1 1 + l@ lle l>s -> $67452301 }t

TESTING x@ x! x>s xbe xle
t{ sma1 1 + x@ xbe -> $89abcdef }t
t{ sma1 1 + x@ xle -> $efcdab89 }t
t{ sma1 1 + x@ xbe x>s -> $-76543211 }t
t{ sma1 1 + x@ xle x>s -> $-10325477 }t

