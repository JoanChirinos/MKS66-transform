using System;
using System.IO;
using GraphicsMatrixApp;
using CanvasApp;
using GraphicsParserApp;

namespace TransformationTesterApp {
  class TransformationTester {

    public static void Main(string[] args) {

        // read script file
        int frame;
        for (frame = 0; frame < 8; frame++) {
          try {
            Canvas c = new Canvas(new int[3] {0, 0, 0});
            GraphicsMatrix edgeMatrix = new GraphicsMatrix(0);
            GraphicsMatrix masterTransformationMatrix = new GraphicsMatrix(4);
            using (StreamReader sr = new StreamReader("feet")) {
              string filecontents = sr.ReadToEnd();
              GraphicsParser.ParseLines(filecontents, masterTransformationMatrix, edgeMatrix, c);
            }
            int dx = (Math.Abs(frame / 2 - 2) - 2);
            Console.WriteLine(dx);
            masterTransformationMatrix = GraphicsMatrix.GetTranslationMatrix(dx, 0, 0);
            /*
            display
            apply
            display
             */
            // c.Clear();
            // c.DrawLines(edgeMatrix, new int[3] {250, 163, 232});
            // c.Display();
            // masterTransformationMatrix.Print();
            // masterTransformationMatrix.Multiply(edgeMatrix);
            using (StreamReader sr = new StreamReader("bodyAndHead")) {
              string filecontents = sr.ReadToEnd();
              GraphicsParser.ParseLines(filecontents, masterTransformationMatrix, edgeMatrix, c);
            }
            masterTransformationMatrix = GraphicsMatrix.GetIdentity(4);
            int dy = (Math.Abs(frame - 4) - 4);
            masterTransformationMatrix = GraphicsMatrix.GetTranslationMatrix(0, dy, 0);
            using (StreamReader sr = new StreamReader("end")) {
              string filecontents = sr.ReadToEnd();
              GraphicsParser.ParseLines(filecontents, masterTransformationMatrix, edgeMatrix, c);
            }
            c.WriteFile(String.Format("{0}.ppm", frame));
          }
          catch (Exception e) {
            Console.Write("Script file could not be read:\n\t");
            Console.WriteLine(e.Message);
            Console.WriteLine("Name your script file \"script\"");
            throw;
          }
        }

    } // end Main method

  } // end TransformationTester class
} // end TransformationTesterApp namespace
