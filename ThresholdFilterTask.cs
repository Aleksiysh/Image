using System;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);
            var res = new double[x, y];
            var t = GetT(CreatArray1(original), whitePixelsFraction);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (original[i, j] >= t)
                        res[i, j] = 1;
                    else
                        res[i, j] = 0;
                }
            }
            return res;
        }
        public static double[] CreatArray1(double[,] original)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);
            var arr = new double[x * y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    arr[i * (y) + j] = original[i, j];
                }
            }
            Array.Sort(arr);
            Array.Reverse(arr);
            return arr;
        }

        public static double GetT(double[] array1, double whitePixelsFraction)
        {
            var firstElement = (int)(whitePixelsFraction * array1.Length);
            if (firstElement == 0) return double.MaxValue;
            if (whitePixelsFraction == 1) return double.MinValue;
            if (firstElement >= 1)
            {
                if (array1.Length % 2 == 0)
                    return (array1[firstElement] + array1[firstElement - 1]) / 2;
                else return array1[firstElement - 1];
            }
            return 0;
        }
    }
}