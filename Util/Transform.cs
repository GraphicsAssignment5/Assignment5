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
