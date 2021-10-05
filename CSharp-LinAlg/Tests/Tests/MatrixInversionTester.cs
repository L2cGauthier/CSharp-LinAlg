namespace LinAlg.Tests
{

    using LinAlg.Utility;
    using LinAlg;

    public static class MatrixInversionTester
    {
        public static void RunTests()
        {
            Matrix2D testMatrix = new Matrix2D(3,3);
            Matrix2D testMatrixInverse = new Matrix2D(3,3);

            testMatrix[0,0] = 1.0f;
            testMatrix[0,1] = 2.0f;
            testMatrix[0,2] = 3.0f;
            testMatrix[1,0] = 3.0f;
            testMatrix[1,1] = 2.0f;
            testMatrix[1,2] = 1.0f;
            testMatrix[2,0] = 2.0f;
            testMatrix[2,1] = 1.0f;
            testMatrix[2,2] = 3.0f;

            Matrix2D testMatrixCalculatedInverse = new Matrix2D(3,3);

            testMatrixCalculatedInverse[0,0] = -5.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[0,1] = 3.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[0,2] = 4.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[1,0] = 7.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[1,1] = 3.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[1,2] = -8.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[2,0] = 1.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[2,1] = -3.0f * (1.0f / 12.0f);
            testMatrixCalculatedInverse[2,2] = 4.0f * (1.0f / 12.0f);

            testMatrix.Invert(ref testMatrixInverse, 0.0001f);

            Logger.Assert(testMatrixInverse.EqualsValue(testMatrixCalculatedInverse, 0.0001f), "Inversion (gaussian elimination)");
        }
    }
}