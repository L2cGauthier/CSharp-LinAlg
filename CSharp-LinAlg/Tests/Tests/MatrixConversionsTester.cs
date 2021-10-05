namespace LinAlg.Tests
{
#if UNITY_5_3_OR_NEWER 
   using UnityEngine; 
#endif 

    public static class Matrix2DConversionTester
    {
        public static void RunTests()
        {
            Matrix2D testMatrix = new Matrix2D(4,4);
            Matrix2D testMatrixComparison = new Matrix2D(4,4);

            testMatrix[0,0] = 7.0f;
            testMatrix[0,1] = 3.0f;
            testMatrix[0,2] = 2.0f;
            testMatrix[0,3] = 7.0f;
            testMatrix[1,0] = 9.0f;
            testMatrix[1,1] = 1.0f;
            testMatrix[1,2] = 0.0f;
            testMatrix[1,3] = 0.0f;
            testMatrix[2,0] = 2.0f;
            testMatrix[2,1] = 7.0f;
            testMatrix[2,2] = 9.0f;
            testMatrix[2,3] = 1.0f;
            testMatrix[3,0] = 4.0f;
            testMatrix[3,1] = 9.0f;
            testMatrix[3,2] = 8.0f;
            testMatrix[3,3] = 6.0f;

            float[][] matrixJaggedArray = new float[][]
            {
                new float []{7.0f, 3.0f, 2.0f, 7.0f},
                new float []{9.0f, 1.0f},                         // Miss 2 terms that should be replaced by 0s
                new float []{2.0f, 7.0f, 9.0f, 1.0f, 8.0f, 7.0f}, // 2 last terms should be ignored 
                new float []{4.0f, 9.0f, 8.0f, 6.0f},
            };
 
            testMatrixComparison = Matrix2DConversions.JaggedArrayToMatrix(matrixJaggedArray, testMatrixComparison.NumberOfLines, testMatrixComparison.NumberOfColumns);

            LinAlg.Utility.Logger.Assert(testMatrix.EqualsValue(testMatrixComparison, 0.0001f), "Jagged array conversion");

#if UNITY_5_3_OR_NEWER 

            // Rotation: theta rad around x axis
            float theta = 67.0f * Mathf.Deg2Rad; // in rad
            Matrix2D rotationMatrix = new Matrix2D(3,3);

            rotationMatrix[0,0] = 1.0f;
            rotationMatrix[0,1] = 0.0f;
            rotationMatrix[0,2] = 0.0f;
            rotationMatrix[1,0] = 0.0f;
            rotationMatrix[1,1] = Mathf.Cos(theta);
            rotationMatrix[1,2] = -Mathf.Sin(theta);
            rotationMatrix[2,0] = 0.0f;
            rotationMatrix[2,1] = Mathf.Sin(theta);
            rotationMatrix[2,2] = Mathf.Cos(theta);

            LinAlg.Utility.Logger.Assert(Mathf.Abs(rotationMatrix.RotationMatrixToQuaternion().eulerAngles.x - (theta * Mathf.Rad2Deg)) < 0.0001f, "Rotation matrix to quaternion (check1)");
            LinAlg.Utility.Logger.Assert(Mathf.Abs(rotationMatrix.RotationMatrixToQuaternion().eulerAngles.y ) < 0.0001f, "Rotation matrix to quaternion (check2)");
            LinAlg.Utility.Logger.Assert(Mathf.Abs(rotationMatrix.RotationMatrixToQuaternion().eulerAngles.z ) < 0.0001f, "Rotation matrix to quaternion (check3)");

            // ======================================================

            theta = 48.0f; // in deg
            Quaternion rotation = Quaternion.Euler(0.0f, theta, 0.0f);
            Matrix2D rotationMatrix1 = new Matrix2D(3,3);

            Matrix2DConversions.QuaternionToRotationMatrix(rotation, ref rotationMatrix1);

            LinAlg.Utility.Logger.Assert(Mathf.Abs(rotationMatrix1.RotationMatrixToQuaternion().eulerAngles.y - theta ) < 0.0001f, "Quaternion to rotation matrix, to quaternion (check1)");
            LinAlg.Utility.Logger.Assert(Mathf.Abs(rotationMatrix1.RotationMatrixToQuaternion().eulerAngles.x ) < 0.0001f, "Quaternion to rotation matrix, to quaternion (check2)");
            LinAlg.Utility.Logger.Assert(Mathf.Abs(rotationMatrix1.RotationMatrixToQuaternion().eulerAngles.z ) < 0.0001f, "Quaternion to rotation matrix, to quaternion (check3)");

#endif 
        }

    }
} 