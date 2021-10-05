namespace LinAlg.Utility
{

#if UNITY_5_3_OR_NEWER
    using UnityEngine;
#else
    using System;
#endif

    public static class Logger
    {
        public static void Log(string s)
        {
#if UNITY_5_3_OR_NEWER
            Debug.Log(s);
#else
            System.Console.WriteLine(s);
#endif
        }

        public static void Assert(bool assertion, string label)
        {
            if(assertion)
            {
                Log("<color=blue>" + label + ": OK</color>");
            }
            else
            {
                Log("<color=red>" + label + ": NOK</color>");
            }
        }
    }
}
