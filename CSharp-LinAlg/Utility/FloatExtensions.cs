namespace LinAlg.Utility
{
    public static class FloatExtensions
    {
        public static float Clamp(this float value, float min, float max)
        {
            if (min > max) 
            {
                float buffer = min;
                min = max;
                max = buffer;
            }

            if (value < min) { return min; }
            if (value > max) { return max; }
            return value;
        }

        public static float Remap(this float value, float originFrom, float originTo, float targetFrom, float targetTo)
        {
            float interpolant = MathUtility.InverseLerp(originFrom, originTo, value);
            return MathUtility.Lerp(targetFrom, targetTo, interpolant);
        }

        public static float RemapClamped(this float value, float originFrom, float originTo, float targetFrom, float targetTo)
        {
            float interpolant = MathUtility.InverseLerpClamped(originFrom, originTo, value);
            return MathUtility.Lerp(targetFrom, targetTo, interpolant);
        }
    }
}
