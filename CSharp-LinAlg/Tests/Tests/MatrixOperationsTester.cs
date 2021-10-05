namespace LinAlg.Tests
{
    using LinAlg;
    using LinAlg.Utility;

    public static class MatrixOperationTester
    {
        public static void RunTests()
        {
            Matrix2D testMatrix0 = new Matrix2D(3,4);
            Matrix2D testMatrix1 = new Matrix2D(4,2);

            testMatrix0[0,0] = 1.0f;
            testMatrix0[0,1] = 2.0f;
            testMatrix0[0,2] = 3.0f;
            testMatrix0[0,3] = 4.0f;
            testMatrix0[1,0] = 5.0f;
            testMatrix0[1,1] = 6.0f;
            testMatrix0[1,2] = 7.0f;
            testMatrix0[1,3] = 8.0f;
            testMatrix0[2,0] = 9.0f;
            testMatrix0[2,1] = 10.0f;
            testMatrix0[2,2] = 11.0f;
            testMatrix0[2,3] = 12.0f;

            testMatrix1[0,0] = 1.0f;
            testMatrix1[0,1] = 2.0f;
            testMatrix1[1,0] = 3.0f;
            testMatrix1[1,1] = 4.0f;
            testMatrix1[2,0] = 5.0f;
            testMatrix1[2,1] = 6.0f;
            testMatrix1[3,0] = 7.0f;
            testMatrix1[3,1] = 8.0f;

            Matrix2D testMatrixResultCalculated = new Matrix2D(3,2);
            Matrix2D testMatrixResult = new Matrix2D(3,2);

            testMatrixResultCalculated[0,0] = 50.0f;
            testMatrixResultCalculated[0,1] = 60.0f;
            testMatrixResultCalculated[1,0] = 114.0f;
            testMatrixResultCalculated[1,1] = 140.0f;
            testMatrixResultCalculated[2,0] = 178.0f;
            testMatrixResultCalculated[2,1] = 220.0f;

            MatrixOperations.MatrixProduct(testMatrix0, testMatrix1, ref testMatrixResult);

            Logger.Assert(testMatrixResultCalculated.EqualsValue(testMatrixResult, 0.0001f), "Matrix product");

            Matrix2D swapRowTestMatrix = new Matrix2D(3,4);
            swapRowTestMatrix[1,0] = 1.0f;
            swapRowTestMatrix[1,1] = 2.0f;
            swapRowTestMatrix[1,2] = 3.0f;
            swapRowTestMatrix[1,3] = 4.0f;
            swapRowTestMatrix[0,0] = 5.0f;
            swapRowTestMatrix[0,1] = 6.0f;
            swapRowTestMatrix[0,2] = 7.0f;
            swapRowTestMatrix[0,3] = 8.0f;
            swapRowTestMatrix[2,0] = 9.0f;
            swapRowTestMatrix[2,1] = 10.0f;
            swapRowTestMatrix[2,2] = 11.0f;
            swapRowTestMatrix[2,3] = 12.0f;

            testMatrix0.SwapRows(0, 1);
            Logger.Assert(testMatrix0.EqualsValue(swapRowTestMatrix, 0.00001f), "Swap rows");

            Matrix2D swapColumnTestMatrix = new Matrix2D(4,2);
            swapColumnTestMatrix[0,1] = 1.0f;
            swapColumnTestMatrix[0,0] = 2.0f;
            swapColumnTestMatrix[1,1] = 3.0f;
            swapColumnTestMatrix[1,0] = 4.0f;
            swapColumnTestMatrix[2,1] = 5.0f;
            swapColumnTestMatrix[2,0] = 6.0f;
            swapColumnTestMatrix[3,1] = 7.0f;
            swapColumnTestMatrix[3,0] = 8.0f;

            testMatrix1.SwapColumns(0,1);
            Logger.Assert(testMatrix1.EqualsValue(swapColumnTestMatrix, 0.00001f), "Swap columns");
        }
    }
}