namespace LinAlg.Tests
{
    public static class TestRunner
    {
        public static void RunTests()
        {
            LinAlg.Utility.Logger.Log("===== MATRIX2D TESTS: ======");
            Matrix2DTester.RunTests();
            LinAlg.Utility.Logger.Log("============================");

            LinAlg.Utility.Logger.Log("===== MATRIX CONVERSIONS TESTS: ======");
            Matrix2DConversionTester.RunTests();
            LinAlg.Utility.Logger.Log("======================================");

            LinAlg.Utility.Logger.Log("===== MATRIX DECOMPOSITION TESTS: ======");
            MatrixDecompositioTester.RunTests();
            LinAlg.Utility.Logger.Log("========================================");

            LinAlg.Utility.Logger.Log("===== MATRIX INVERSION TESTS: ======");
            MatrixInversionTester.RunTests();
            LinAlg.Utility.Logger.Log("====================================");

            LinAlg.Utility.Logger.Log("===== MATRIX OPERATIONS TESTS: ======");
            MatrixOperationTester.RunTests();
            LinAlg.Utility.Logger.Log("=====================================");
        }
    }
}