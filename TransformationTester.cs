using System;
using System.IO;
using GraphicsMatrixApp;
using CanvasApp;
using GraphicsParserApp;

namespace TransformationTesterApp {
  class TransformationTester {

    public static void Main() {
        Canvas c = new Canvas();
        GraphicsMatrix edgeMatrix = new GraphicsMatrix();
        GraphicsMatrix masterTransformationMatrix = new GraphicsMatrix(4);

        // read script file
        try {
          using (StreamReader sr = new StreamReader("script")) {
            string filecontents = sr.ReadToEnd();
            GraphicsParser.ParseLines(filecontents, masterTransformationMatrix, edgeMatrix, c);
          }
        }
        catch (Exception e) {
          Console.Write("Script file could not be read:\n\t");
          Console.WriteLine(e.Message);
          Console.WriteLine("Name your script file \"script\"");
          throw;
        }

    } // end Main method

  } // end TransformationTester class
} // end TransformationTesterApp namespace
