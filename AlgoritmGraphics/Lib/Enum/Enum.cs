using System;
using GlmSharp;

namespace Lib.Enum
{
    /// <summary>
    /// Перечесление используемых цветов
    /// </summary>
    public enum COLORS
    {
        BLACK, 
        WHITE,
        RED,
        GREEN,
        BLUE,
        PURPLE,
        YELLOW,
        ORANGE,
    }

    /// <summary>
    /// Перечесление всех лаб
    /// </summary>
    public enum LABS
    {
        LAB1,
        LAB2,
        LAB3,
        LAB4,
        LAB5,
        LAB6,
        LAB7,
        LAB8,
        LAB9,
        LAB10,
        LAB11,
        LAB12,
    }

    // Класс для обработки цветов
    public class Colors
    {
        public static vec3 GetRGB(COLORS color)
        {
            switch (color)
            {
                case COLORS.BLACK: return new vec3( 0.0f, 0.0f, 0.0f);
                case COLORS.WHITE: return new vec3( 1.0f, 1.0f, 1.0f);
                case COLORS.RED: return new vec3(1.0f, 0.0f, 0.0f);
                case COLORS.GREEN: return new vec3(0.0f, 1.0f, 0.0f);
                case COLORS.BLUE: return new vec3(0.0f, 0.0f, 1.0f);
                case COLORS.PURPLE: return new vec3(1.0f, 0.0f, 1.0f);
                case COLORS.YELLOW: return new vec3(1.0f, 1.0f, 0.0f);
                case COLORS.ORANGE: return new vec3(1.0f,0.5f, 0.0f);
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
                case COLORS.GREEN: return "Green";
                case COLORS.BLUE: return "Blue";
                case COLORS.PURPLE: return "Purple";
                case COLORS.YELLOW: return "Yellow";
                case COLORS.ORANGE: return "Orange";
            }

            return String.Empty;
        }
    }
}