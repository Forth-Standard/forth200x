require test/tester.fs

{ k-left  -> cr ." Please press <left>" ekey }
{ k-right -> cr ." Please press <right>" ekey }
{ k-up    -> cr ." Please press <up>" ekey }
{ k-down  -> cr ." Please press <down>" ekey }
{ k-home  -> cr ." Please press <home>" ekey }
{ k-end   -> cr ." Please press <end>" ekey }
{ k-prior -> cr ." Please press <prior>" ekey }
{ k-next  -> cr ." Please press <next>" ekey }

{ k-f1  -> cr ." Please press <F1>" ekey }
{ k-f2  -> cr ." Please press <F2>" ekey }
{ k-f3  -> cr ." Please press <F3>" ekey }
{ k-f4  -> cr ." Please press <F4>" ekey }
{ k-f5  -> cr ." Please press <F5>" ekey }
{ k-f6  -> cr ." Please press <F6>" ekey }
{ k-f7  -> cr ." Please press <F7>" ekey }
{ k-f8  -> cr ." Please press <F8>" ekey }
{ k-f9  -> cr ." Please press <F9>" ekey }
{ k-f10 -> cr ." Please press <F10>" ekey }
{ k-f11 -> cr ." Please press <F11>" ekey }
{ k-f12 -> cr ." Please press <F12>" ekey }

{ k-left k-shift-mask or -> cr ." Please press <shift-left>" ekey }
{ k-left k-ctrl-mask  or -> cr ." Please press <ctrl-left>" ekey }
{ k-left k-alt-mask   or -> cr ." Please press <alt-left>" ekey }
