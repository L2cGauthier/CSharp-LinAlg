namespace LinAlg
{
    public static class MatrixDecomposition
    {
        public static void DoolittleLUDecomposition(this Matrix2D matrix, ref Matrix2D lowerTriangleMatrix, ref Matrix2D upperTriangleMatrix)
        {
            // Doolittle LU decomposition 
            // L: Lower triangle matrix with 1s on the diagonal
            // U: Upper triangle matrix

            if (!matrix.IsSquare()) { throw new System.Exception("[MatrixDecomposition DoolittleLUDecomposition] Cannot compute LU decomposition on non-square matrix."); }

            int dimension = matrix.NumberOfLines;

            if (!lowerTriangleMatrix.IsSquare() || lowerTriangleMatrix.NumberOfColumns != dimension || lowerTriangleMatrix.NumberOfLines != dimension)
            { throw new System.Exception("[MatrixDecomposition DoolittleLUDecomposition] lowerTriangleMatrix needs to be square and have the same dimensions as the input matrix"); }

            if (!upperTriangleMatrix.IsSquare() || upperTriangleMatrix.NumberOfColumns != dimension || upperTriangleMatrix.NumberOfLines != dimension)
            { throw new System.Exception("[MatrixDecomposition DoolittleLUDecomposition] upperTriangleMatrix needs to be square and have the same dimensions as the input matrix"); }

            // Doolittle algorithm
            for (int i = 0; i < dimension; i++)
            {
                for (int k = i; k < dimension; k++)
                {
                    float sum = 0;

                    for (int j = 0; j < i; j++)
                    {
                        sum += (lowerTriangleMatrix[i, j] * upperTriangleMatrix[j, k]);
                    }

                    upperTriangleMatrix[i, k] = matrix[i, k] - sum;
                }
    
                for (int k = i; k < dimension; k++)
                {
                    if (i == k)
                        lowerTriangleMatrix[i, i] = 1;
                    else
                    {
                        float sum = 0;
                        for (int j = 0; j < i; j++)
                            sum += (lowerTriangleMatrix[k, j] * upperTriangleMatrix[j, i]);
    
                        lowerTriangleMatrix[k, i] = (matrix[k, i] - sum) / upperTriangleMatrix[i, i];
                    }
                }
            }
        }
    }
}
