using System;
using System.Reactive;
using GlmSharp;

namespace Lib.Enum
{
   
    public enum TextureFilter
    {
        POINT, // Точечная               
        BILINEAR, // Билинейная
        TRIPLINEAR, // Трилинейная
        ANISOTROPIC // Анизторопная
    }

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
        LAB3,
        LAB4,
        LAB5,
        LAB6,
        LAB7,
        LAB8,
        LAB9,
        LAB10,
    }
    
    /// <summary>
    /// Определение типов игровых объектов
    /// </summary>
    public enum GameObjectType
    {
        /// <summary>
        /// Пустой
        /// </summary>
        EMPTY,
        /// <summary>
        /// Легкий игровой объект
        /// </summary>
        LIGHT_OBJECT,
        /// <summary>
        /// Тяжелый игровой объект
        /// </summary>
        HEAVY_OBJECT,
        /// <summary>
        /// Граничный игровой объект
        /// </summary>
        BORDER_OBJECT,
        /// <summary>
        /// Игровой объект для представления игрока
        /// </summary>
        PLAYER,
        /// <summary>
        /// Игровой объект для представления бомбы
        /// </summary>
        BOMB,
        /// <summary>
        /// Игровой объект для представления монстров
        /// </summary>
        MONSTER, 
        /// <summary>
        /// Игровое поле
        /// </summary>
        SIMPLE_PLANE
    }

    public enum MoveDirection { STOP, LEFT, RIGHT, UP, DOWN, WAIT }

    public enum GameMaterialType
    {
        PHONG_MATERIAL,
        PHONG_MATERIAL_WITH_TEXTURE
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

    public class Objects
    {
        public static GameObjectType GetGameObjectType(string typeName)
        {
            switch (typeName)
            {
                case "Empty": return GameObjectType.EMPTY;
                case "LIGHT_OBJECT": return GameObjectType.LIGHT_OBJECT;
                case "HEAVY_OBJECT": return GameObjectType.HEAVY_OBJECT;
                case "BORDER_OBJECT": return GameObjectType.BORDER_OBJECT;
                case "PLAYER": return GameObjectType.PLAYER;
                case "BOMB": return GameObjectType.BOMB;
                case "MONSTER": return GameObjectType.MONSTER;
                case "SIMPLE_PLANE": return GameObjectType.SIMPLE_PLANE;
            }

            return GameObjectType.LIGHT_OBJECT;
        }
    }
}