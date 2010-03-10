\ tests written by Marcel Hendrix and submitted with the CfV

T{ 0e0 FVALUE Tval -> }T
T{ Tval -> 0e0 }T
T{ 1e0 TO Tval -> }T
T{ Tval -> 1e0 }T
: SETTval Tval FSWAP TO Tval ; 
T{ 2e0 SETTval Tval -> 1e0 2e0 }T
T{ 5e0 FVALUE x -> }T
: [execute] EXECUTE ; IMMEDIATE
T{ ' Tval ] [execute] [ -> 2e0 }T
