MD := $(wildcard *.md)
HTML := $(MD:.md=.html)

all: $(HTML)

%.html: %.md
	pandoc -o $@ $<
