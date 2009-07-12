use strict;

my $JAR = "C:/j2sdk1.4.0_01/bin/jar";

system $JAR . " " .
  "cvmf " .
  "Manifest.txt " .
  "chesseditor.jar " .
  "*.class " .
  "*.gif " .
  "Pieces/HandDrawn/*.gif " .
  "Pieces/Modelled/*.gif ";
system "rm *.class";
