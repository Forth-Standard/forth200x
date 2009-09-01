require tester.fs

\ should be the same for any query starting with X:
{ s" X:deferred" ENVIRONMENT? DUP 0= XOR INVERT -> 0 }
{ s" X:jdfkjdls" ENVIRONMENT? DUP 0= XOR INVERT -> 0 }
