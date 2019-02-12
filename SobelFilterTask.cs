using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] gg, double[,] sx)
        {
            var sy = GetTranponMatrix(sx);
            var width = gg.GetLength(0);
            var height = gg.GetLength(1);
            var result = new double[width,  height];
            if (width == 1)
            {
                result[0, 0] = Math.Sqrt(sx[0, 0] * gg[0, 0] * sx[0, 0] * gg[0, 0] +
                    sx[0, 0] * gg[0, 0] * sx[0, 0] * gg[0, 0]);
            }
            var lenSx = sx.GetLength(0);
            for (int x = (int)(lenSx / 2); x < width - (int)(lenSx / 2); x++)
                for (int y = (int)(lenSx / 2); y < height - (int)(lenSx / 2); y++)
                {
                    result[x, y] = CalculatePixel(gg, sx, sy, x, y);
                }
            return result;
        }

        static double[,] GetTranponMatrix(double[,] array) 
        {
            var x = array.GetLength(1);
            var y = array.GetLength(0);
            var result = new double[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    result[i, j] = array[j, i];
            return result;
        }

        public static double CalculatePixel(double[,] g, double[,] sx, double[,] sy, int x, int y)
        {
            var gx = 0.0;
            var gy = 0.0;
            var lenSx = sx.GetLength(0);
            for (int k = 0; k < lenSx; k++)
                for (int l = 0; l < lenSx; l++)
                {
                    gx += sx[l, k] * g[x + (l - (int)(lenSx / 2)), y + (k - (int)(lenSx / 2))];
                    gy += sy[l, k] * g[x + (l - (int)(lenSx / 2)), y + (k - (int)(lenSx / 2))];
                }
            return Math.Sqrt(gx * gx + gy * gy);
        }
    }
}
