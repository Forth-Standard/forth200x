require ./ttester.fs

SET-EXACT

t{ -0E          FTRUNC  F0=  ->  TRUE  }t 
t{ -1E-9        FTRUNC  F0=  ->  TRUE  }t
t{ -0.9E        FTRUNC  F0=  ->  TRUE  }t
t{ -1E  1E-5 F+ FTRUNC  F0=  ->  TRUE  }t
t{ 0E           FTRUNC  ->   0E  }t    
t{ 1E-9         FTRUNC  ->   0E  }t
t{ -1E -1E-5 F+ FTRUNC  ->  -1E  }t
t{ 3.14E        FTRUNC  ->   3E  }t 
t{ 3.99E        FTRUNC  ->   3E  }t    
t{ 4E           FTRUNC  ->   4E  }t
t{ -4E          FTRUNC  ->  -4E  }t
t{ -4.1E        FTRUNC  ->  -4E  }t
