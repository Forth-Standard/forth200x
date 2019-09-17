rem @echo off

if x%1 == x	 goto single
if x%1 == xdvi   goto dvi
if x%1 == xps    goto ps
if x%1 == xpdf   goto pdf
if x%1 == xclean goto clean

rem Usage
echo "Usage: make [ dvi | ps | pdf | clean ]"

:single
latex ans
latex ans
goto clean

:dvi
set latex=latex
goto process

:pdf
set latex=pdflatex
goto process

:process
%latex% ans
%latex% ans
perl sort.pl < ans.wrd > ans.wds
%latex% ans

if not x%1 == xps goto clean

:ps
if not exist ans.dvi goto dvi

dvips -Pcmz -Pamz -Ppdf -K -t A4 ans

:clean
del *.rat
del *.aux
del *.out
del *.log
