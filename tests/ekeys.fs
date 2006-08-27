require test/tester.fs

{ k-left  true -> cr ." Please press <left>" ekey ekey>fkey }
{ k-right true -> cr ." Please press <right>" ekey ekey>fkey }
{ k-up    true -> cr ." Please press <up>" ekey ekey>fkey }
{ k-down  true -> cr ." Please press <down>" ekey ekey>fkey }
{ k-home  true -> cr ." Please press <home>" ekey ekey>fkey }
{ k-end   true -> cr ." Please press <end>" ekey ekey>fkey }
{ k-prior true -> cr ." Please press <prior>" ekey ekey>fkey }
{ k-next  true -> cr ." Please press <next>" ekey ekey>fkey }

{ k-f1  true -> cr ." Please press <F1>" ekey ekey>fkey }
{ k-f2  true -> cr ." Please press <F2>" ekey ekey>fkey }
{ k-f3  true -> cr ." Please press <F3>" ekey ekey>fkey }
{ k-f4  true -> cr ." Please press <F4>" ekey ekey>fkey }
{ k-f5  true -> cr ." Please press <F5>" ekey ekey>fkey }
{ k-f6  true -> cr ." Please press <F6>" ekey ekey>fkey }
{ k-f7  true -> cr ." Please press <F7>" ekey ekey>fkey }
{ k-f8  true -> cr ." Please press <F8>" ekey ekey>fkey }
{ k-f9  true -> cr ." Please press <F9>" ekey ekey>fkey }
{ k-f10 true -> cr ." Please press <F10>" ekey ekey>fkey }
{ k-f11 true -> cr ." Please press <F11>" ekey ekey>fkey }
{ k-f12 true -> cr ." Please press <F12>" ekey ekey>fkey }

{ k-left k-shift-mask or true -> cr ." Please press <shift-left>" ekey ekey>fkey }
{ k-left k-ctrl-mask  or true -> cr ." Please press <ctrl-left>" ekey ekey>fkey }
{ k-left k-alt-mask   or true -> cr ." Please press <alt-left>" ekey ekey>fkey }

{ cr ." Please press <a>" ekey ekey>fkey nip swap ekey>char -> false char a true }
