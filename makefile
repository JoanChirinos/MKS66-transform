all:
	./gifgen.sh

images:
	csc TransformationTester.cs Canvas.cs GraphicsMatrix.cs GraphicsParser.cs
	mono TransformationTester.exe

clean:
	-rm -f *.exe *.ppm *.png *.gif
