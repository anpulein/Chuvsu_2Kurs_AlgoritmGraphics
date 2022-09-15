using System;
using System.Collections.Generic;
using System.Diagnostics;
using GlmSharp;
using Lib.Enum;


namespace Lab2.Other
{

    #region COLORS
    public class Color
    {
        private static int index_colors = 0;
        private static List<COLORS> _colors = new List<COLORS>() { COLORS.WHITE, COLORS.BLUE, COLORS.RED, COLORS.YELLOW, COLORS.PURPLE };
        public static vec3 rgb = GetColors(_colors[index_colors]);

        public static void getRGB()
        {
            index_colors++;
            if (index_colors == _colors.Count) index_colors = 0;
            rgb = GetColors(_colors[index_colors]);
            Console.WriteLine($"Смена цвета на {Colors.GetColorsName(_colors[index_colors])}");
        }
        private static vec3 GetColors(COLORS color)
        {
            return Colors.GetRGB(color);
        }
        
        
        
    }
    #endregion
}