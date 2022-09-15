using System;
using GlmSharp;

namespace Lib.Enum
{
    public enum COLORS
    {
        BLACK, 
        WHITE,
        RED,
        BLUE,
        PURPLE,
        YELLOW,
    }

    public class Colors
    {
        public static vec3 GetRGB(COLORS color)
        {
            switch (color)
            {
                case COLORS.BLACK: return new vec3( 0.0f, 0.0f, 0.0f);
                case COLORS.WHITE: return new vec3( 1.0f, 1.0f, 1.0f);
                case COLORS.RED: return new vec3(1.0f, 0.0f, 0.0f);
                case COLORS.BLUE: return new vec3(0.0f, 0.0f, 1.0f);
                case COLORS.PURPLE: return new vec3(1.0f, 0.0f, 1.0f);
                case COLORS.YELLOW: return new vec3(1.0f, 1.0f, 0.0f);
            }

            return vec3.NaN;
        }
        
        public static string GetColorsName(COLORS color)
        {
            switch (color)
            {
                case COLORS.BLACK: return "Black";
                case COLORS.WHITE: return "White";
                case COLORS.RED: return "Red";
                case COLORS.BLUE: return "Blue";
                case COLORS.PURPLE: return "Purple";
                case COLORS.YELLOW: return "Yellow";
            }

            return String.Empty;
        }
    }
}