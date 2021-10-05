namespace LinAlg
{

#if UNITY_5_3_OR_NEWER
    using UnityEngine;
#endif

    public static class Matrix2DConversions
    {
    
        public static Matrix2D JaggedArrayToMatrix(float[][] sourceArray, int destinationMatrixNumberOfLines, int destinationMatrixNumberOfColumns)
        {
            // Dimension check on each row would be expensive
            // Instead, let's force the user to pass destination matrix dimensions. If out of the jagged array range, insert a 0, and ignore number outside the passed range.

            Matrix2D destinationMatrix = new Matrix2D(destinationMatrixNumberOfLines, destinationMatrixNumberOfColumns);

            for (int i = 0; i < destinationMatrix.NumberOfLines; i++)
            {
                for (int j = 0; j < destinationMatrix.NumberOfColumns; j++)
                {
                    try
                    {
                        destinationMatrix[i,j] = sourceArray[i][j];
                    }
                    catch (System.IndexOutOfRangeException exception)
                    {
                        LinAlg.Utility.Logger.Log("[Matrix2D GetMatrixFromJaggedArray] Coefficient at (" + i + ", " + j +") in destination matrix is out of jagged array range. Inserting a 0.\n" + exception);
                        destinationMatrix[i,j] = 0.0f;
                    }
                }
            }

            return destinationMatrix;
        }

#if UNITY_5_3_OR_NEWER

        public static Quaternion RotationMatrixToQuaternion(this Matrix2D rotationMatrix)
        {
            // The matrix needs to be a pure rotation for the quaternion to make sense
            // i.e. the matrix needs to be orthogonal orthogonal ==> det and eigenvalues are 1, or equivalently, Matrix * Transposed(Matrix) = Identity

            float matrixTrace = rotationMatrix.Trace();
            Quaternion result = new Quaternion();

            if (matrixTrace > 0) 
            { 
                float S = Mathf.Sqrt(matrixTrace + 1.0f) * 2.0f;
                result.w = 0.25f * S;
                result.x = (rotationMatrix[2,1] - rotationMatrix[1,2]) / S;
                result.y = (rotationMatrix[0,2] - rotationMatrix[2,0]) / S; 
                result.z = (rotationMatrix[1,0] - rotationMatrix[0,1]) / S; 
            } 
            else if ((rotationMatrix[0,0] > rotationMatrix[1,1])&(rotationMatrix[0,0] > rotationMatrix[2,2])) 
            { 
                float S = Mathf.Sqrt(1.0f + rotationMatrix[0,0] - rotationMatrix[1,1] - rotationMatrix[2,2]) * 2.0f;
                result.w = (rotationMatrix[2,1] - rotationMatrix[1,2]) / S;
                result.x = 0.25f * S;
                result.y = (rotationMatrix[0,1] + rotationMatrix[1,0]) / S; 
                result.z = (rotationMatrix[0,2] + rotationMatrix[2,0]) / S; 
            } 
            else if (rotationMatrix[1,1] > rotationMatrix[2,2]) 
            { 
                float S = Mathf.Sqrt(1.0f + rotationMatrix[1,1] - rotationMatrix[0,0] - rotationMatrix[2,2]) * 2.0f;
                result.w = (rotationMatrix[0,2] - rotationMatrix[2,0]) / S;
                result.x = (rotationMatrix[0,1] + rotationMatrix[1,0]) / S; 
                result.y = 0.25f * S;
                result.z = (rotationMatrix[1,2] + rotationMatrix[2,1]) / S; 
            } 
            else 
            { 
                float S = Mathf.Sqrt(1.0f + rotationMatrix[2,2] - rotationMatrix[0,0] - rotationMatrix[1,1]) * 2.0f;
                result.w = (rotationMatrix[1,0] - rotationMatrix[0,1]) / S;
                result.x = (rotationMatrix[0,2] + rotationMatrix[2,0]) / S;
                result.y = (rotationMatrix[1,2] + rotationMatrix[2,1]) / S;
                result.z = 0.25f * S;
            }

            return result;
        }

        public static Matrix2D QuaternionToRotationMatrix(Quaternion quaternion, ref Matrix2D resultMatrix3x3)
        {
            float sqw = quaternion.w * quaternion.w;
            float sqx = quaternion.x * quaternion.x;
            float sqy = quaternion.y * quaternion.y;
            float sqz = quaternion.z * quaternion.z;

            if (quaternion == quaternion.normalized)
            {
                // Quaternion is normalized
                resultMatrix3x3[0,0] = sqx - sqy - sqz + sqw;
                resultMatrix3x3[1,1] = -sqx + sqy - sqz + sqw;
                resultMatrix3x3[2,2] = -sqx - sqy + sqz + sqw;
                
                float tmp1 = quaternion.x * quaternion.y;
                float tmp2 = quaternion.z * quaternion.w;
                resultMatrix3x3[1,0] = 2.0f * (tmp1 + tmp2);
                resultMatrix3x3[0,1] = 2.0f * (tmp1 - tmp2);
                
                tmp1 = quaternion.x * quaternion.z;
                tmp2 = quaternion.y * quaternion.w;
                resultMatrix3x3[2,0] = 2.0f * (tmp1 - tmp2);
                resultMatrix3x3[0,2] = 2.0f * (tmp1 + tmp2);

                tmp1 = quaternion.y * quaternion.z;
                tmp2 = quaternion.x * quaternion.w;
                resultMatrix3x3[2,1] = 2.0f * (tmp1 + tmp2);
                resultMatrix3x3[1,2] = 2.0f * (tmp1 - tmp2);   
            }
            else
            {
                // Quaternion isn't normalized
                float invs = 1.0f / (sqx + sqy + sqz + sqw);
                resultMatrix3x3[0,0] = ( sqx - sqy - sqz + sqw) * invs;
                resultMatrix3x3[1,1] = (-sqx + sqy - sqz + sqw) * invs;
                resultMatrix3x3[2,2] = (-sqx - sqy + sqz + sqw) * invs;
                
                float tmp1 = quaternion.x * quaternion.y;
                float tmp2 = quaternion.z * quaternion.w;
                resultMatrix3x3[1,0] = 2.0f * (tmp1 + tmp2) * invs;
                resultMatrix3x3[0,1] = 2.0f * (tmp1 - tmp2) * invs;
                
                tmp1 = quaternion.x * quaternion.z;
                tmp2 = quaternion.y * quaternion.w;
                resultMatrix3x3[2,0] = 2.0f * (tmp1 - tmp2) * invs;
                resultMatrix3x3[0,2] = 2.0f * (tmp1 + tmp2) * invs;

                tmp1 = quaternion.y * quaternion.z;
                tmp2 = quaternion.x * quaternion.w;
                resultMatrix3x3[2,1] = 2.0f * (tmp1 + tmp2) * invs;
                resultMatrix3x3[1,2] = 2.0f * (tmp1 - tmp2) * invs;   
            }

            return resultMatrix3x3;
        }

#endif

    }
}
