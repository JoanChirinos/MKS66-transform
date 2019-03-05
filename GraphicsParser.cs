using System;
using System.IO;
using GraphicsMatrixApp;
using CanvasApp;

namespace GraphicsParserApp {
  class GraphicsParser {

    public static void ParseLines(string inp, GraphicsMatrix masterTransformationMatrix, GraphicsMatrix edgeMatrix, Canvas c) {
      string[] lines = inp.Split('\n');
      int i;
      for (i = 0; i < lines.Length; i++) {
        string command = lines[i];
        if (command == "ident") {
          masterTransformationMatrix = GraphicsMatrix.GetIdentity(4);
        }
        else if (command == "apply") {
          masterTransformationMatrix.Multiply(edgeMatrix);
        }
        else if (command == "display") {
          c.Clear();
          c.DrawLines(edgeMatrix, new int[3] {250, 163, 232});
          c.Display();
        }
        else if (command == "save") {
          i++;
          c.Clear();
          c.DrawLines(edgeMatrix, new int[3] {250, 163, 232});
          string filename = lines[i];
          c.WriteFile(filename);
        }
        else if (command == "line") {
          i++;
          string[] args = lines[i].Split(' ');
          edgeMatrix.AddEdge(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]), Convert.ToDouble(args[2]), Convert.ToDouble(args[3]), Convert.ToDouble(args[4]), Convert.ToDouble(args[5]));
        }
        else if (command == "scale") {
          i++;
          string[] args = lines[i].Split(' ');
          GraphicsMatrix scaleMatrix = GraphicsMatrix.GetScaleMatrix(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));
          scaleMatrix.Multiply(masterTransformationMatrix);
        }
        else if (command == "move") {
          i++;
          string[] args = lines[i].Split(' ');
          GraphicsMatrix translationMatrix = GraphicsMatrix.GetTranslationMatrix(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));
          translationMatrix.Multiply(masterTransformationMatrix);
        }
        else if (command == "rotate") {
          i++;
          string[] args = lines[i].Split(' ');
          GraphicsMatrix rotationMatrix = null;
          if (args[0] == "x") {
            rotationMatrix = GraphicsMatrix.GetXRotationMatrix(Convert.ToDouble(args[1]));
          }
          else if (args[0] == "y") {
            rotationMatrix = GraphicsMatrix.GetYRotationMatrix(Convert.ToDouble(args[1]));
          }
          else if (args[0] == "z") {
            rotationMatrix = GraphicsMatrix.GetZRotationMatrix(Convert.ToDouble(args[1]));
          }
          rotationMatrix.Multiply(masterTransformationMatrix);
        }
      }
    }

  } // end GraphicsParser class
} // end GraphicsParserApp namespace














//
