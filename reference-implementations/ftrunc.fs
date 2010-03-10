\ The reference implementation below was posted to comp.lang.forth, by 
\ George A. Hubert, on 12 April 2009. 
\ Message-ID: <18c1e72c-43f0-47c2-9882-477b99cf7923@m24g2000vbp.googlegroups.com>
\ no license information came in the posting

: FTRUNC   ( r1 -- r2 )
         FDUP F0= 0=
 IF      FDUP F0<
 IF      FNEGATE FLOOR FNEGATE
 ELSE    FLOOR
 THEN
 THEN    ;
