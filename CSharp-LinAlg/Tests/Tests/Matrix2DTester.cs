namespace LinAlg.Tests
{
    using System;
    using LinAlg;
    using LinAlg.Utility;

    public static class Matrix2DTester
    {
        public static void RunTests()
        {
            Matrix2D testMatrix = new Matrix2D(21,17);

            Logger.Assert(testMatrix.NumberOfLines == 21, "Number of lines");
            Logger.Assert(testMatrix.NumberOfColumns == 17, "Number of columns");
            Logger.Assert(!testMatrix.IsSquare(), "Is square");

            try
            {
                float x = testMatrix[21,10];
            }
            catch (System.Exception exception)
            {
                Logger.Assert(exception.Message == "[Matrix2D] Index out of range", "Get column index out of range");
            }

            try
            {
                float x = testMatrix[10,17];
            }
            catch (System.Exception exception)
            {
                Logger.Assert(exception.Message == "[Matrix2D] Index out of range", "Get line index out of range");
            }

            try
            {
                testMatrix[21,10] = 10.0f;
            }
            catch (System.Exception exception)
            {
                Logger.Assert(exception.Message == "[Matrix2D] Index out of range", "Set column index out of range");
            }

            try
            {
                testMatrix[10,17] = 10.0f;
            }
            catch (System.Exception exception)
            {
                Logger.Assert(exception.Message == "[Matrix2D] Index out of range", "Set line index out of range");
            }

            try
            {
                float x = testMatrix.Trace();
            }
            catch (System.Exception exception)
            {
                Logger.Assert(exception.Message == "[Matrix2D Trace] Trace only makes sense for square matrices.", "Trace non-square matrix exception");
            }

            Matrix2D testMatrix1 = testMatrix.DeepCopy();
            Logger.Assert(testMatrix.EqualsValue(testMatrix1, 0.01f), "Deep copy & Equals value");

            Matrix2D testIdentityMatrix = Matrix2D.Identity(10);
            Logger.Assert(testIdentityMatrix.NumberOfLines == 10, "Number of lines identity");
            Logger.Assert(testIdentityMatrix.NumberOfColumns == 10, "Number of columns identity");
            Logger.Assert(testIdentityMatrix.IsSquare(), "Identity is square");

            bool identityDiagonalIsValid = true;
            for (int i = 0; i < testIdentityMatrix.NumberOfLines; i++)
            {
                if (testIdentityMatrix[i,i] != 1.0f)
                {
                    identityDiagonalIsValid = false;
                }
            }
            Logger.Assert(identityDiagonalIsValid, "Identity diagonal");

            bool identityOutOfDiagonalIsValid = true;
            for (int i = 0; i < testIdentityMatrix.NumberOfLines; i++)
            {
                for (int j = 0; j < testIdentityMatrix.NumberOfColumns; j++)
                {
                    if (i != j && testIdentityMatrix[i,j] != 0.0f)
                    {
                        identityOutOfDiagonalIsValid = false;
                    }
                }
            }
            Logger.Assert(identityOutOfDiagonalIsValid, "Identity out of diagonal terms");

            testMatrix[1,1] = 10.0f;
            testMatrix[5,2] = -20.0f;
            testMatrix[3,2] = 0.25f;

            Logger.Assert(testMatrix.GetBiggestCoefficient() == 10.0f, "Get biggest coefficient");
            Logger.Assert(testMatrix.GetSmallestCoefficient() == -20.0f, "Get smallest coefficient");
            Logger.Assert(testMatrix.GetBiggestAbsoluteCoefficient() == -20.0f, "Get biggest absolute coefficient");

            Matrix2D testMatrix2 = new Matrix2D(3,3);
            testMatrix2[0,0] = 14.0f;
            testMatrix2[0,1] = 4.0f;
            testMatrix2[0,2] = 23.0f;
            testMatrix2[1,0] = 58.0f;
            testMatrix2[1,1] = 56.0f;
            testMatrix2[1,2] = 44.0f;
            testMatrix2[2,0] = 9.0f;
            testMatrix2[2,1] = 13.0f;
            testMatrix2[2,2] = 7.0f;

            Logger.Assert(MathUtility.Abs(testMatrix2.Determinant()-3190.0f) < 0.001f, "Determinant");

            Matrix2D testMatrix3 = new Matrix2D(3,3);
            testMatrix3[0,0] = 14.0f;
            testMatrix3[1,0] = 4.0f;
            testMatrix3[2,0] = 23.0f;
            testMatrix3[0,1] = 58.0f;
            testMatrix3[1,1] = 56.0f;
            testMatrix3[2,1] = 44.0f;
            testMatrix3[0,2] = 9.0f;
            testMatrix3[1,2] = 13.0f;
            testMatrix3[2,2] = 7.0f;

            Matrix2D testMatrix2Transposed = new Matrix2D(3,3);

            testMatrix2.GetTransposed(ref testMatrix2Transposed);

            Logger.Assert(testMatrix2Transposed.EqualsValue(testMatrix3, 0.000001f), "Transpose");
        }
    }
}
