namespace LinAlg
{
    using System;
    using LinAlg.Utility;

    public class Matrix2D: IEquatable<Matrix2D>
    {
        private float[] underlyingArray = null;
        private int numberOfLines = 0;
        private int numberOfColumns = 0;

        public int NumberOfLines
        {
            get { return numberOfLines; }
            protected set {numberOfLines = value; }
        }

        public int NumberOfColumns
        {
            get { return numberOfColumns; }
            protected set {numberOfColumns = value; }
        }            

        // Uses a single dimension array under the hood (1 fewer dereference, replaced by a multiplication)
        public float this[int lineIdx, int columnIdx]
        {
            get 
            {
                if (lineIdx >= numberOfLines || columnIdx >= numberOfColumns) { throw new System.IndexOutOfRangeException("[Matrix2D] Index out of range"); }
                return underlyingArray[lineIdx * this.numberOfColumns + columnIdx]; 
            }
            set 
            { 
                if (lineIdx >= numberOfLines || columnIdx >= numberOfColumns) { throw new System.IndexOutOfRangeException("[Matrix2D] Index out of range"); }
                underlyingArray[lineIdx * this.numberOfColumns + columnIdx] = value; 
            }
        }

        public Matrix2D(int numberOfLines, int numberOfColumns)
        {
            if (numberOfLines <= 0) { throw new System.Exception("[Matrix2D constructor] Number of lines must be strictly positive."); }
            if (numberOfColumns <= 0) { throw new System.Exception("[Matrix2D constructor] Number of columns must be strictly positive."); }

            this.numberOfLines = numberOfLines;
            this.numberOfColumns = numberOfColumns;

            this.underlyingArray = new float[numberOfLines*numberOfColumns];
        }

        public float Trace()
        {
            if(!this.IsSquare()) { throw new System.Exception("[Matrix2D Trace] Trace only makes sense for square matrices."); }

            float trace = 0.0f;
            for (int i = 0; i < numberOfColumns; i++)
            {
                trace += this[i,i];
            }
            return trace;
        }

        public Matrix2D DeepCopy()
        {
            Matrix2D copy = new Matrix2D(this.numberOfLines, this.numberOfColumns);

            for (int i = 0; i < numberOfLines; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    copy[i,j] = this[i,j];
                }
            }

            return copy;
        }

        public bool IsSquare()
        {
            return (this.NumberOfLines == this.NumberOfColumns);
        }

        public static Matrix2D Identity(int dimensions)
        {
            Matrix2D identity = new Matrix2D(dimensions, dimensions);

            for (int i = 0; i < dimensions; i++)
            {
                identity[i,i] = 1.0f;
            }

            return identity;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < this.numberOfLines; i++)
            {
                for (int j = 0; j < this.numberOfColumns; j++)
                    s += this[i,j].ToString("F6").PadLeft(8) + " ";
                s += "\n";
            }
            return s;
        }

        public bool EqualsValue(Matrix2D matrix, float epsilon)
        {
            if(this.numberOfColumns != matrix.numberOfColumns || this.numberOfLines != matrix.numberOfLines) { throw new System.Exception("[Matrix2D EqualsValue] Matrices are non-conformable, value equality cannot be tested."); }
            
            for (int i = 0; i < this.numberOfLines; i++)
            {
                for (int j = 0; j < this.numberOfColumns; j++)
                {
                    if(MathUtility.Abs(this[i,j] - matrix[i,j]) > epsilon)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator ==(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (ReferenceEquals(matrix1, matrix2))
            {
                return true;
            }
            if (ReferenceEquals(matrix1, null))
            {
                return false;
            }
            if (ReferenceEquals(matrix2, null))
            {
                return false;
            }

            return matrix1.Equals(matrix2);
        }

        public static bool operator !=(Matrix2D matrix1, Matrix2D matrix2)
        {
            return !(matrix1 == matrix2);
        }

        public bool Equals(Matrix2D other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Matrix2D);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return underlyingArray.GetHashCode();
            }
        }

        public float GetBiggestCoefficient()
        {
            float biggestCoefficient = float.MinValue;
            for (int i = 0; i < this.numberOfLines; i++)
            {
                for (int j = 0; j < this.numberOfColumns; j++)
                {
                    if (this[i,j] > biggestCoefficient)
                    {
                        biggestCoefficient = this[i,j];
                    }
                }
            }

            return biggestCoefficient;
        }

        public float GetSmallestCoefficient()
        {
            float smallestCoefficient = float.MaxValue;
            for (int i = 0; i < this.numberOfLines; i++)
            {
                for (int j = 0; j < this.numberOfColumns; j++)
                {
                    if (this[i,j] < smallestCoefficient)
                    {
                        smallestCoefficient = this[i,j];
                    }
                }
            }

            return smallestCoefficient;
        }

        public float GetBiggestAbsoluteCoefficient()
        {
            float biggestCoefficient = float.MinValue;
            float sign = 1.0f;
            for (int i = 0; i < this.numberOfLines; i++)
            {
                for (int j = 0; j < this.numberOfColumns; j++)
                {
                    if (MathUtility.Abs(this[i,j]) > biggestCoefficient)
                    {
                        sign = (this[i,j] > 0.0f) ? 1.0f : -1.0f;
                        biggestCoefficient = MathUtility.Abs(this[i,j]);
                    }
                }
            }

            return biggestCoefficient * sign;
        }

        public float Determinant()
        {
            if (!this.IsSquare()) { throw new System.Exception("[MatrixOperations Determinant] Can't compute determinant of a non-square matrix."); }

            Matrix2D lowerTriangleMatrix = new Matrix2D(this.NumberOfLines, this.NumberOfColumns);
            Matrix2D upperTriangleMatrix = new Matrix2D(this.NumberOfLines, this.NumberOfColumns);

            this.DoolittleLUDecomposition(ref lowerTriangleMatrix, ref upperTriangleMatrix);

            float determinant = 1;
            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                determinant *= lowerTriangleMatrix[i,i] * upperTriangleMatrix[i,i];
            }

            return determinant;
        }

        public bool IsInvertible()
        {
            if (!this.IsSquare()) { throw new System.Exception("[MatrixOperations IsInvertible] Matrix isn't square, cannot check if it's invertible."); }

            return (this.Determinant() != 0);
        }

        public Matrix2D GetTransposed(ref Matrix2D transposedMatrix)
        {
            if (this.NumberOfLines != transposedMatrix.NumberOfColumns) { throw new System.Exception("[Matrix2D GetTransposed] Number of lines of the transposed matrix must be the same as the number of columns in the matrix to transpose."); }
            if (this.NumberOfColumns != transposedMatrix.NumberOfLines) { throw new System.Exception("[Matrix2D GetTransposed] Number of columns of the transposed matrix must be the same as the number of lines in the matrix to transpose."); }

            for (int i = 0; i < this.NumberOfLines; i++)
            {
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    transposedMatrix[i,j] = this[j,i];
                }
            }
            return transposedMatrix;
        }
    }
}
