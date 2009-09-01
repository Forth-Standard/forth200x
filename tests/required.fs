require tester.fs

{ 0
  s" required-helper1.fs" required
  require required-helper1.fs
  include required-helper1.fs
  -> 2 }
{ 0
  include required-helper2.fs
  s" required-helper2.fs" required
  require required-helper2.fs
  s" required-helper2.fs" included
  -> 2 }
