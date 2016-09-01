//using System;
//using System.Collections.Generic;

//namespace FizzWare.NBuilder
//{
//    public class Generate
//    {
//        private readonly Random r = new Random((int)DateTime.Now.Ticks);

//        private static Generate __instance;

//        private bool tracking;
//        private List<int> trackedValues = new List<int>();

//        private static Generate _instance
//        {
//            get
//            {
//                __instance = __instance ?? new Generate();
//                return __instance;
//            }
//        }

//        private Generate()
//        {
//        }

//        private int GenerateRandomInteger(int min, int max)
//        {
//            int value = r.Next(min, max);

//            if (tracking)
//            {
//                // loop round until the value is unique
//                while (trackedValues.Contains(value))
//                    value = r.Next(min, max);

//                // add it to the list of values that have been provided
//                trackedValues.Add(value);
//            }

//            return value;
//        }
        
//        public static int RandomInt(int min, int max)
//        {
//            return _instance.GenerateRandomInteger(min, max);
//        }

//        public static decimal RandomDecimal(int min, int max)
//        {
//            int rand1 = _instance.GenerateRandomInteger(min, max);
//            int rand2 = _instance.GenerateRandomInteger(min, max);

//            string f = string.Format("{0}.{1}", rand1, rand2);

//            decimal dec = Convert.ToDecimal(f);

//            return dec;
//        }

//        public static void TrackValues()
//        {
//            _instance.tracking = true;
//            _instance.trackedValues = new List<int>();
//        }

//        public static void StopTracking()
//        {
//            _instance.tracking = false;
//        }
//    }
//}