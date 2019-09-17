
single:
	latex ans
	latex ans

dvi:
	latex ans
	latex ans 
	perl sort.pl < ans.wrd > ans.wds
	latex ans

pdf:
	pdflatex ans
	pdflatex ans 
	perl sort.pl < ans.wrd > ans.wds
	pdflatex ans

ps: dvi
	dvips -Pcmz -Pamz -Ppdf -K -t A4 ans

clean:
	rm *.rat *.aux *.out *.log
