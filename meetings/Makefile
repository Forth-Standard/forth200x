MD = $(wildcard *.md)
#HTML = $(pathsubst %.md,%.o,$(MD))
HTML := $(MD:.md=.html)

all: $(HTML)

%.html: %.md
	pandoc -o $@ $<
