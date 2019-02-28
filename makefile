all:
	csc TransformationTester.cs Canvas.cs GraphicsMatrix.cs
	mono TransformationTester.exe

clean:
	-rm -f *.exe *.ppm *.png *.gif
