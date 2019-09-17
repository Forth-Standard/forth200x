#! /bin/perl

%words = ();

foreach $line (<STDIN>) {
	$line =~ /\\entry\{([\d\.]*)\}(.*)/o;
	$words{$1} = $line;
}

foreach $num (sort nums keys %words) {
	print $words{$num};
}

sub nums {
	local $ans = substr($a, -4) <=> substr($b, -4);
	if ( $ans == 0 ) {
		$ans = $a <=> $b;
	}
	return $ans;
}