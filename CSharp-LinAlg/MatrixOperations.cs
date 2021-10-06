namespace LinAlg
{
    using LinAlg.Utility;

    public static class MatrixOperations
    {
        public static Matrix2D MatrixProduct(Matrix2D leftMatrix, Matrix2D rightMatrix, ref Matrix2D matrixProduct)
        {
            if (leftMatrix.NumberOfColumns != rightMatrix.NumberOfLines)
                throw new System.Exception("[MatrixOperations MatrixProduct] Non-conformable matrices, cannot calculate a product");

            if ((matrixProduct.NumberOfLines != leftMatrix.NumberOfLines) || (matrixProduct.NumberOfColumns != rightMatrix.NumberOfColumns))
            {
                throw new System.Exception("[MatrixOperations MatrixProduct] Result matrix doesn't have the right dimensions");
            }

            for (int i = 0; i < leftMatrix.NumberOfLines; i++)
            {
                for (int j = 0; j < rightMatrix.NumberOfColumns; j++)
                {
                    for (int k = 0; k < leftMatrix.NumberOfColumns; k++) // rightMatrix.NumberOfLines
                    {
                        matrixProduct[i,j] += leftMatrix[i,k] * rightMatrix[k,j];
                    } 
                }     
            } 
    
            return matrixProduct;
        }

        public static Matrix2D Remap(this Matrix2D matrix, float originFrom, float originTo, float targetFrom, float targetTo)
        {
            for (int i = 0; i < matrix.NumberOfLines; ++i)
            {
                for (int j = 0; j < matrix.NumberOfColumns; ++j)
                {
                    matrix[i,j] = matrix[i,j].Remap(originFrom, originTo, targetFrom, targetTo);
                }
            }
            return matrix;
        }

        public static Matrix2D ClampedRemap(this Matrix2D matrix, float originFrom, float originTo, float targetFrom, float targetTo)
        {
            for (int i = 0; i < matrix.NumberOfLines; ++i)
            {
                for (int j = 0; j < matrix.NumberOfColumns; ++j)
                {
                    matrix[i,j] = matrix[i,j].RemapClamped(originFrom, originTo, targetFrom, targetTo);
                }
            }
            return matrix;
        }

        public static Matrix2D ClampCoefficients(this Matrix2D matrix, float from, float to)
        {
            for (int i = 0; i < matrix.NumberOfLines; ++i)
            {
                for (int j = 0; j < matrix.NumberOfColumns; ++j)
                {
                    matrix[i,j] = MathUtility.Clamp(matrix[i,j], from, to);
                }
            }
            return matrix;
        }

// ==================================
#region MATRIX-WISE SCALAR OPERATIONS

        public static Matrix2D AddToEveryCoefficient(this Matrix2D matrix, float valueToAdd)
        {
            for (int i = 0; i < matrix.NumberOfLines; ++i)
            {
                for (int j = 0; j < matrix.NumberOfColumns; ++j)
                {
                    matrix[i,j] += valueToAdd;
                }
            }
            return matrix;
        }

        public static Matrix2D SubstractFromEveryCoefficient(this Matrix2D matrix, float valueToSubstract)
        {
            return matrix.AddToEveryCoefficient(-valueToSubstract);
        }

        public static Matrix2D MultiplyEveryCoefficient(this Matrix2D matrix, float multiplier)
        {
            for (int i = 0; i < matrix.NumberOfLines; ++i)
            {
                for (int j = 0; j < matrix.NumberOfColumns; ++j)
                {
                    matrix[i,j] *= multiplier;
                }
            }
            return matrix;
        }

        public static Matrix2D DivideEveryCoefficient(this Matrix2D matrix, float divider)
        {
            divider = 1.0f / divider;
            return matrix.MultiplyEveryCoefficient(divider);
        }        
        
#endregion
// ==================================

// ===================
#region ROW-WISE OPERATIONS

        public static void SwapRows(this Matrix2D matrix, int idxFrom, int idxTo)
        {
            if (idxFrom != idxTo) 
            {
                float rowValueBuffer;
                for (int i = 0; i < matrix.NumberOfColumns; i++)
                {
                    rowValueBuffer = matrix[idxFrom,i];
                    matrix[idxFrom,i] = matrix[idxTo,i];
                    matrix[idxTo,i] = rowValueBuffer;
                }
            }
        }

        public static void MultiplyRow(this Matrix2D matrix, int rowIdx, float value)
        {
            for (int i = 0; i < matrix.NumberOfColumns; i++)
            {
                matrix[rowIdx, i] *= value;
            }
        }

        public static void DivideRow(this Matrix2D matrix, int rowIdx, float value)
        {
            value = 1.0f / value;
            matrix.MultiplyRow(rowIdx, value);
        }

        public static void AddRow(this Matrix2D matrix, int idxOfTheRowToAddTo, int idxOfTheAddedRow)
        {
            for (int i = 0; i < matrix.NumberOfColumns; i++)
            {
                matrix[idxOfTheRowToAddTo,i] += matrix[idxOfTheAddedRow,i];
            }
        }

        public static void SubstractRow(this Matrix2D matrix, int idxOfTheRowToSubstractFrom, int idxOfTheRowToSubstractor)
        {
            for (int i = 0; i < matrix.NumberOfColumns; i++)
            {
                matrix[idxOfTheRowToSubstractFrom,i] -= matrix[idxOfTheRowToSubstractor,i];
            }
        }

#endregion
// ===================

// ===================
#region COLUMN-WISE OPERATIONS

        public static void SwapColumns(this Matrix2D matrix, int idxFrom, int idxTo)
        {
            if (idxFrom != idxTo) 
            {
                float columnValueBuffer;
                for (int i = 0; i < matrix.NumberOfLines; i++)
                {
                    columnValueBuffer = matrix[i,idxFrom];
                    matrix[i,idxFrom] = matrix[i,idxTo];
                    matrix[i,idxTo] = columnValueBuffer;
                }
            }
        }

        public static void MultiplyColumn(this Matrix2D matrix, int columnIdx, float value)
        {
            for (int i = 0; i < matrix.NumberOfLines; i++)
            {
                matrix[i, columnIdx] *= value;
            }
        }

        public static void DivideColumn(this Matrix2D matrix, int columnIdx, float value)
        {
            value = 1.0f / value;
            matrix.MultiplyColumn(columnIdx, value);
        }

        public static void SubstractColumn(this Matrix2D matrix, int idxOfTheColumnToSubstractFrom, int idxOfTheColumnToSubstractor)
        {
            for (int i = 0; i < matrix.NumberOfLines; i++)
            {
                matrix[i,idxOfTheColumnToSubstractFrom] -= matrix[i,idxOfTheColumnToSubstractor];
            }
        }

        public static void AddColumn(this Matrix2D matrix, int idxOfTheColumnToAddTo, int idxOfTheAddedColumn)
        {
            for (int i = 0; i < matrix.NumberOfLines; i++)
            {
                matrix[i,idxOfTheColumnToAddTo] += matrix[i,idxOfTheAddedColumn];
            }
        }

#endregion
// ===================

    }
}