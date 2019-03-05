using System;
using System.IO;

namespace GraphicsMatrixApp {
  class GraphicsMatrix {

    /* public instance vars */
    public double[,] Matrix { get; private set; }
    public int Rows { get; private set; }
    public int Cols { get; private set; }
    public int MaxCols { get; private set; }

    /* constructors */
    public GraphicsMatrix() {
      this.Matrix = new double[4,100];
      this.Cols = 0;
      this.MaxCols = 100;
    }

    public GraphicsMatrix(int cols) {
      this.Matrix = new double[4,cols];
      this.Cols = 0;
      this.MaxCols = cols;
    }

    public GraphicsMatrix(double[,] matrix) {
      this.Matrix = matrix;
      this.Cols = matrix.GetLength(1);
      this.MaxCols = matrix.GetLength(1);
    }

    public void Expand() {
      // doubles in size
      double[,] newMatrix;
      if (this.MaxCols == 0) {
        newMatrix = new double[4,1];
        this.MaxCols = 1;
      }
      else {
        newMatrix = new double[4,this.MaxCols * 2];
        this.MaxCols *= 2;
      }
      int x, y;
      for (x = 0; x < 4; x++) {
        for (y = 0; y < this.Cols; y++) {
          newMatrix[x,y] = this.Matrix[x,y];
        }
      }
      this.Matrix = newMatrix;
    }

    public void AddPoint(double x, double y, double z) {
      int col = this.Cols;
      if (this.Cols >= this.MaxCols) {
        this.Expand();
      }
      this.Matrix[0,col] = x;
      this.Matrix[1,col] = y;
      this.Matrix[2,col] = z;
      this.Matrix[3,col] = 1;
      this.Cols += 1;
    }

    public void AddEdge(double x0, double y0, double z0, double x1, double y1, double z1) {
      this.AddPoint(x0, y0, z0);
      this.AddPoint(x1, y1, z1);
    }

    /* public methods */
    public void Print() {
      Console.WriteLine("");
      int rowAt, colAt;
      for(rowAt = 0; rowAt < 4; rowAt++) {
        Console.Write("|");
        for (colAt = 0; colAt < this.Cols; colAt++) {
          Console.Write(String.Format(" {0, 4} ", Math.Round(this.Matrix[rowAt, colAt])));
        }
        Console.WriteLine("|");
      }
      Console.WriteLine("");
    }

    public void Multiply(GraphicsMatrix matrix) {
      // the product
      double[,] matrixOut = new double[4, matrix.Cols];
      int row, col, index;
      for (row = 0; row < 4; row++) {
        for (col = 0; col < matrix.Cols; col++) {
          double sum = 0;
          for (index = 0; index < 4; index++) {
            sum += this.Matrix[row, index] * matrix.Matrix[index, col];
          }
          matrixOut[row, col] = sum;
        }
      }
      matrix.Matrix = matrixOut;
    }

    public double[] GetPoint(int index) {
      double[] point = new double[3];
      point[0] = this.Matrix[0,index];
      point[1] = this.Matrix[1,index];
      point[2] = this.Matrix[2,index];
      return point;
    }

    /* static methods */
    public static GraphicsMatrix GetIdentity(int dimensions) {
      double[,] matrixOut = new double[dimensions, dimensions];
      int i;
      for (i = 0; i < dimensions; i++) {
        matrixOut[i, i] = 1;
      }
      GraphicsMatrix gmOut = new GraphicsMatrix(matrixOut);
      return gmOut;
    }

    // theta is in degrees
    public static GraphicsMatrix GetXRotationMatrix(double thetaX) {
      double radX = Math.PI / 180 * thetaX;
      GraphicsMatrix x = new GraphicsMatrix(new double[4,4] {
        {1, 0, 0, 0},
        {0, Math.Cos(radX), -1 * Math.Sin(radX), 0},
        {0, Math.Sin(radX), Math.Cos(radX), 0},
        {0, 0, 0, 1}
      });
      return x;
    }

    public static GraphicsMatrix GetYRotationMatrix(double thetaY) {
      double radY = Math.PI / 180 * thetaY;
      GraphicsMatrix y = new GraphicsMatrix(new double [4,4] {
        {Math.Cos(radY), 0, Math.Sin(radY), 0},
        {0, 1, 0, 0},
        {-1 * Math.Sin(radY), 0, Math.Cos(radY), 0},
        {0, 0, 0, 1}
      });
      return y;
    }

    public static GraphicsMatrix GetZRotationMatrix(double thetaZ) {
      double radZ = Math.PI / 180 * thetaZ;
      GraphicsMatrix z = new GraphicsMatrix(new double[4,4] {
        {Math.Cos(radZ), -1 * Math.Sin(radZ), 0, 0},
        {Math.Sin(radZ), Math.Cos(radZ), 0, 0},
        {0, 0, 1, 0},
        {0, 0, 0, 1}
      });
      return z;
    }

    public static GraphicsMatrix GetScaleMatrix(double kx, double ky, double kz) {
      GraphicsMatrix s = new GraphicsMatrix(new double[4,4] {
        {kx, 0, 0, 0},
        {0, ky, 0, 0},
        {0, 0, kz, 0},
        {0, 0, 0, 1}
      });
      return s;
    }

    public static GraphicsMatrix GetTranslationMatrix(double dx, double dy, double dz) {
      GraphicsMatrix t = new GraphicsMatrix(new double[4,4] {
        {1, 0, 0, dx},
        {0, 1, 0, dy},
        {0, 0, 1, dz},
        {0, 0, 0, 1}
      });
      return t;
    }


    // public static void Main() {
    //   /*
    //   | 1  2  3  4 |
    //   | 5  6  7  8 |
    //   | 9  10  11  12 |
    //   | 13  14  15  16 |
    //    */
    //   GraphicsMatrix m1 = new GraphicsMatrix(new double[,] {{1,2,3,4},{5,6,7,8},{9,10,11,12},{13,14,15,16}});
    //   m1.Print();
    //
    //   GraphicsMatrix m2 = new GraphicsMatrix(2);
    //   m2.Print();
    //   m2.AddPoint(1, 2, 3);
    //   m2.Print();
    //   m2.AddPoint(4, 5, 6);
    //   m2.Print();
    //   m2.AddEdge(7, 8, 9, 10, 11, 12);
    //   m2.Print();
    // }

  } // end MathHelper class
} // end MathHelperApp namespace
