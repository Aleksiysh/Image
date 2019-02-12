using System.Linq;
using System;
using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            var x = original.GetLength(0);
            var y = original.GetLength(1);
            var res = new double[x, y];
            var inside = new List<double> { };
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    inside.Add(original[i, j]);
                    if (j > 0)
                        inside.Add(original[i, j - 1]);
                    if (i > 0)
                        inside.Add(original[i - 1, j]);
                    if (j > 0 && i > 0)
                        inside.Add(original[i - 1, j - 1]);
                    if (j < y - 1)
                        inside.Add(original[i, j + 1]);
                    if (i < x - 1)
                        inside.Add(original[i + 1, j]);
                    if (j < y - 1 && i < x - 1)
                        inside.Add(original[i + 1, j + 1]);
                    if (i > 0 && j < y - 1)
                        inside.Add(original[i - 1, j + 1]);
                    if (i < x - 1 && j > 0)
                        inside.Add(original[i + 1, j - 1]);
                    res[i, j] = GetMedian(inside);
                    inside.Clear();
                }
            }
            return res;
        }

        public static double GetMedian(List<double> inside)
        {
            var tmpArr = inside.ToArray();

            Array.Sort(tmpArr);
            if (tmpArr.Length % 2 != 0)
                return tmpArr[(tmpArr.Length) / 2];
            else if (tmpArr.Length > 1)
                return (tmpArr[tmpArr.Length / 2] + tmpArr[tmpArr.Length / 2 - 1]) / 2;
            else return tmpArr[0];
        }
    }
}