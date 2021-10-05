namespace LinAlg.Tests
{
    using LinAlg;

    public static class MatrixDecompositioTester
    {
        public static void RunTests()
        {
            Matrix2D testMatrix = new Matrix2D(4,4);
            Matrix2D matrixL = new Matrix2D(4,4);
            Matrix2D matrixU = new Matrix2D(4,4);

            Matrix2D matrixLCalculated = new Matrix2D(4,4);
            Matrix2D matrixUCalculated = new Matrix2D(4,4);

            testMatrix[0,0] = 1.0f;
            testMatrix[0,1] = 5.0f;
            testMatrix[0,2] = 0.0f;
            testMatrix[0,3] = 0.0f;
            testMatrix[1,0] = 7.0f;
            testMatrix[1,1] = 6.0f;
            testMatrix[1,2] = 4.0f;
            testMatrix[1,3] = 4.0f;
            testMatrix[2,0] = 0.0f;
            testMatrix[2,1] = 0.0f;
            testMatrix[2,2] = 4.0f;
            testMatrix[2,3] = 4.0f;
            testMatrix[3,0] = 1.0f;
            testMatrix[3,1] = 1.0f;
            testMatrix[3,2] = 0.0f;
            testMatrix[3,3] = 1.0f;

            matrixLCalculated[0,0] = 1.0f;
            matrixLCalculated[0,1] = 0.0f;
            matrixLCalculated[0,2] = 0.0f;
            matrixLCalculated[0,3] = 0.0f;
            matrixLCalculated[1,0] = 7.0f;
            matrixLCalculated[1,1] = 1.0f;
            matrixLCalculated[1,2] = 0.0f;
            matrixLCalculated[1,3] = 0.0f;
            matrixLCalculated[2,0] = 0.0f;
            matrixLCalculated[2,1] = 0.0f;
            matrixLCalculated[2,2] = 1.0f;
            matrixLCalculated[2,3] = 0.0f;
            matrixLCalculated[3,0] = 1.0f;
            matrixLCalculated[3,1] = 0.138f;
            matrixLCalculated[3,2] = -0.138f;
            matrixLCalculated[3,3] = 1.0f;

            matrixUCalculated[0,0] = 1.0f;
            matrixUCalculated[0,1] = 5.0f;
            matrixUCalculated[0,2] = 0.0f;
            matrixUCalculated[0,3] = 0.0f;
            matrixUCalculated[1,0] = 0.0f;
            matrixUCalculated[1,1] = -29.0f;
            matrixUCalculated[1,2] = 4.0f;
            matrixUCalculated[1,3] = 4.0f;
            matrixUCalculated[2,0] = 0.0f;
            matrixUCalculated[2,1] = 0.0f;
            matrixUCalculated[2,2] = 4.0f;
            matrixUCalculated[2,3] = 4.0f;
            matrixUCalculated[3,0] = 0.0f;
            matrixUCalculated[3,1] = 0.0f;
            matrixUCalculated[3,2] = 0.0f;
            matrixUCalculated[3,3] = 1.0f;

            MatrixDecomposition.DoolittleLUDecomposition(testMatrix, ref matrixL, ref matrixU);

            LinAlg.Utility.Logger.Assert(matrixL.EqualsValue(matrixLCalculated, 0.001f), "LU Decomposition: L");
            LinAlg.Utility.Logger.Assert(matrixU.EqualsValue(matrixUCalculated, 0.001f), "LU Decomposition: U");
        }
    }
}