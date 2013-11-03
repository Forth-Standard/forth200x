#frozen proposals:

CFVS=	2value \
	buffer \
	deferred \
	defined \
	ekeys \
	escaped-strings \
	extension-query \
	fasinh-lt0 \
	fatan2 \
	fp-stack \
	ftrunc \
	fvalue \
	locals \
	n-to-r \
	number-prefixes \
	parse-name \
	required \
	s-to-f \
	structures \
	synonym \
	text-substitution \
	throw-iors \
	traverse-wordlist \
	xchar


all: forth200x-code.zip extensions.zip

forth200x-code.zip: FORCE
	zip -9r forth200x-code.zip $(foreach file, $(CFVS), forth200x-code/extensions/$(file).fs forth200x-code/tests/$(file).fs)

extensions.zip: FORCE
	cd forth200x-code && zip -9r ../extensions.zip $(foreach file, $(CFVS), extensions/$(file).fs)

FORCE:
