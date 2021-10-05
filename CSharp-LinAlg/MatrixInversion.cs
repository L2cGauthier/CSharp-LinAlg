namespace LinAlg
{
    using LinAlg.Utility;
    
    public static class MatrixInversion
    {
        public static void Invert(this Matrix2D matrix, ref Matrix2D resultMatrix, float epsilon = 0.00001f)
        {
            if (!matrix.IsSquare()) { throw new System.Exception("[Matrix2D Invert] Attempt to invert a non-square matrix."); }

            else if (resultMatrix.NumberOfLines != matrix.NumberOfLines || resultMatrix.NumberOfColumns != matrix.NumberOfColumns) 
            { 
                throw new System.Exception("[Matrix2D Invert] sameDimensionIdentity passed didn't have the same dimension as the matrix to invert. Pass null if you wish the invert function to generate it for you (this will allocate memory though).");
            }

            // Initializing the resultMatrix with 1s on the diagonal and 0s everywhere else (identity)
            for (int i = 0; i < resultMatrix.NumberOfLines; i++)
            {
                for (int j = 0; j < resultMatrix.NumberOfColumns; j++)
                {
                    if (i==j)
                    { 
                        resultMatrix[i,j] = 1.0f;
                    }
                    else
                    {
                        resultMatrix[i,j] = 0.0f;
                    }
                }
            }

            // ------------------------------------------------
            // Gauss Jordan elimination with "augmented matrix"
            // ------------------------------------------------

            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {
                // Finding the first row that has a non zero first term
                for (int i = j; i < matrix.NumberOfLines; i++)
                {
                    if (MathUtility.Abs(matrix[i,j]) > epsilon)
                    {
                        // Swap rows to be in a situation where the pivot is not 0
                        if (i != j)
                        {
                            matrix.SwapRows(i, j);
                            resultMatrix.SwapRows(i, j);
                        }

                        break;
                    }
                    
                    if (i == matrix.NumberOfLines)
                    {
                        // We went through every line, the j-th column is full of 0s: the matrix isn't invertible
                        throw new System.Exception("[Matrix2D Invert]" + j +"-th column is filled with zeroes, matrix isn't invertible");
                    }
                }

                // Pivoting
                for (int l = 0; l < matrix.NumberOfColumns; l++)
                {
                    if (l==j) { continue; }
                    matrix.DoPivot(j,l, ref resultMatrix);
                }
            }

            // Last step: matrix is diagonal, we divide each row by the value on the diagonal to form the identity matrix
            for (int i = 0; i < matrix.NumberOfColumns; i++)
            {
                float multiplicationValue = 1.0f/matrix[i,i];
                matrix.MultiplyRow(i, multiplicationValue); 
                resultMatrix.MultiplyRow(i, multiplicationValue); 
            }
        }

        private static void DoPivot(this Matrix2D matrix, int pivotRowIdx, int eliminationRowIdx, ref Matrix2D augmentationMatrix)
        {
            float pivotValue = matrix[pivotRowIdx, pivotRowIdx];
            float eliminationRowMultiplier = matrix[eliminationRowIdx, pivotRowIdx];
            
            for (int i = 0; i < matrix.NumberOfColumns; i++)
            {
                matrix[eliminationRowIdx, i] = matrix[eliminationRowIdx, i] * pivotValue - matrix[pivotRowIdx, i] * eliminationRowMultiplier;
                augmentationMatrix[eliminationRowIdx, i] = augmentationMatrix[eliminationRowIdx, i] * pivotValue - augmentationMatrix[pivotRowIdx, i] * eliminationRowMultiplier;
            }
        }
    }
}
