namespace LinAlg.Utility
{
    public static class MathUtility
    {
        public static float Lerp(float from, float to, float interpolationValue)
        {
            return from + interpolationValue * (to - from);
        }

        public static float LerpClamped(float from, float to, float interpolationValue)
        {
            return from + interpolationValue.Clamp(0.0f, 1.0f) * (to - from);
        }

        public static float InverseLerp(float from, float to, float value)
        {
            return  (value - from)/(to-from);
        }

        public static float InverseLerpClamped(float from, float to, float value)
        {
            return  (value.Clamp(from, to) - from)/(to-from);
        }

        public static float Min(float a, float b)
        {
            return (a>b) ? b : a ;
        }

        public static float Max(float a, float b)
        {
            return (a>b) ? a : b ;
        }

        public static float Abs(float value)
        {
            return (value < 0.0f) ? -value : value;
        }

        public static float Clamp(float value, float min, float max)
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
    }
}
