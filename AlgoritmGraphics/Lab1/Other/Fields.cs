using System;
using System.Diagnostics;
using Lib.Enum;

namespace Lab1.Other
{

    #region COLORS
    public class Color
    {
        private static int index_colors = 0;
        private static COLORS[] _colors = { COLORS.BLACK, COLORS.WHITE, COLORS.RED, COLORS.BLUE, COLORS.PURPLE };
        public static float[] rgb = GetColors(_colors[0]);

        public static void GetNextRGB()
        {
            index_colors++;
            index_colors = (index_colors % _colors.Length);
            rgb = GetColors(_colors[index_colors]);
        }
        public static void GetPrevRGB()
        {
            index_colors = (index_colors % _colors.Length);
            index_colors = index_colors == 0 ? _colors.Length - 1 : index_colors - 1;
            rgb = GetColors(_colors[index_colors]);
        }

        private static float[] GetColors(COLORS color)
        {
            switch (color)
            {
                case COLORS.BLACK: return new float[]{ 0.0f, 0.0f, 0.0f };
                case COLORS.WHITE: return new float[]{ 1.0f, 1.0f, 1.0f };
                case COLORS.RED: return new float[]{ 1.0f, 0.0f, 0.0f };
                case COLORS.BLUE: return new float[]{ 0.0f, 0.0f, 1.0f };
                case COLORS.PURPLE: return new float[]{ 1.0f, 0.0f, 1.0f };
            }

            return null;
        }
        
        
        
    }
    #endregion
}