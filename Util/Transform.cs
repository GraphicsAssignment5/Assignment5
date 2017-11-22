using asgn5v1.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace asgn5v1.Util
{
    public class TransformBuilder
    {
        private double[,] tnet;

        public TransformBuilder(int row, int col)
        {
            this.tnet = this.SetIdentity(row, col);
        }

        public TransformBuilder(double[,] matrix)
        {
            this.tnet = matrix;
        }

        public double [,] GetTransNet()
        {
            return this.tnet;
        }

        public double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            double[,] res = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    double temp = 0.0d;
                    for (int k = 0; k < 4; k++)
                        temp += A[i, k] * B[k, j];
                    res[i, j] = temp;
                }
            }
            return res;
        }
       
        public TransformBuilder Scale(double factor)
        {
            double[,] A = SetIdentity(4, 4);

            for (int i = 0; i < 3; i++)
            {
                A[i, i] = 1.0d * factor;
            }

            this.tnet = this.MultiplyMatrix(this.tnet, A);
            return this;
        }
        
        public TransformBuilder Translate(double x, double y, double z)
        {
            double[,] A = SetIdentity(4, 4);

            A[3, 0] = x;
            A[3, 1] = y;
            A[3, 2] = z;

            this.tnet = this.MultiplyMatrix(this.tnet, A);
            return this;
        }
        
        public TransformBuilder ReflectIn(Plane plane)
        {
            double[,] A = SetIdentity(4, 4);

            switch (plane)
            {
                case Plane.XZ:
                    A[1, 1] = -1.0d;
                    break;
                case Plane.XY:
                    A[2, 2] = -1.0d;
                    break;
                case Plane.YZ:
                    A[0, 0] = -1.0d;
                    break;
            }

            this.tnet = this.MultiplyMatrix(this.tnet, A);

            return this;
        }

        public TransformBuilder Shear(Direction direction)
        {
            double[,] A = SetIdentity(4, 4);
            if (direction == Direction.LEFT)
                A[1, 0] = 0.1d;
            else if (direction == Direction.RIGHT)
                A[1, 0] = -0.1d;

            this.tnet = this.MultiplyMatrix(this.tnet, A);

            return this;
        }

        public TransformBuilder RotateIn(Axis axis)
        {
            double cos = Math.Cos(0.05);
            double sin = Math.Sin(0.05);
            double[,] matrix = SetIdentity(4, 4);

            switch (axis)
            {
                case Axis.X:
                    matrix[1, 1] = cos;
                    matrix[1, 2] = sin;
                    matrix[2, 1] = -sin;
                    matrix[2, 2] = cos;
                    break;
                case Axis.Y:
                    matrix[0, 0] = cos;
                    matrix[0, 2] = -sin;
                    matrix[2, 0] = sin;
                    matrix[2, 2] = cos;
                    break;
                case Axis.Z:
                    matrix[0, 0] = cos;
                    matrix[0, 1] = sin;
                    matrix[1, 0] = -sin;
                    matrix[1, 1] = cos;
                    break;
            }

            this.tnet = this.MultiplyMatrix(this.tnet, matrix);

            return this;
        }

        public double[,] SetIdentity(int nrow, int ncol)
        {
            double[,] A = new double[nrow,ncol];

            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++) A[i, j] = 0.0d;
                A[i, i] = 1.0d;
            }

            return A;
        }
    }
}
