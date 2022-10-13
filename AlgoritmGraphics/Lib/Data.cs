// Для работы с библиотекой OpenGL

using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;

using Lib.Lab4;
using Lib.Enum;
using Lib.Lab5;
using Camera = Lib.Lab4.Camera;
using Light = Lib.Lab5.Light;

namespace Lib
{
    public class Data
    {
        public const int FRAME_RATE = 20;
        
        // Конечно время симуляции
        public static float EndSimulationTime = 0.0f;
        public static float FramesPerSecond = 0.0f;
        public static int FPS = 0;
        
        // Поворот объекта
        public static float rotateObject = 2f;
        
        // Запущенная лабораторная
        public static LABS Labs;
        
        // Список графических объектов
        public List<GraphicObject> graphicObjects;
        
        // Список материалов для объектов
        public List<PhongMaterial> phongMaterials;
        
        // Используемая камера
        public Camera camera;
        
        // Источник света
        public Light light;
        
        // Материалы
        private ParseMaterial _material;

        // Общая фоновая освещенность
        public static float[] globalAmbientColor = {0.2f, 0.2f, 0.2f, 1.0f};
        
        // Путь к материалам
        public static string pathMaterial =
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\Lab5\\materials";

        // Функция для инициализация всех общих данных (камера объекты и т.д)
        public void initDataLab4()
        {
            Console.WriteLine("Инициализация данных нужных для Lab4!");
            Labs = LABS.LAB4;
            
            graphicObjects = new List<GraphicObject>();
            camera = new Camera(new vec3(10, 15, 17.5f));

            graphicObjects.Add(new GraphicObject(new vec3(4, 0, 0), 
                180, 
                COLORS.RED));
            graphicObjects.Add(new GraphicObject(new vec3(-4, 0, 0), 
                0, 
                COLORS.BLUE));
            graphicObjects.Add(new GraphicObject(new vec3(0, 0, 4), 
                90, 
                COLORS.WHITE));
            graphicObjects.Add(new GraphicObject(new vec3(0, 0, -4), 
                -90, 
                COLORS.GREEN));
            
            camera.setPosition(new vec3(10f, 15f, 17.5f));
        }

        public void initDataLab5()
        {
            Console.WriteLine("Инициализация данных нужных для Lab5!");
            Labs = LABS.LAB5;
            graphicObjects = new List<GraphicObject>();
            camera = new Camera(new vec3(10, 15, 17.5f));
            light = new Light();
            _material = new ParseMaterial();
            
            graphicObjects.Add(new GraphicObject(new vec3(4, 0, 0), 
                180, 
                COLORS.RED,
                _material.material1));
            graphicObjects.Add(new GraphicObject(new vec3(-4, 0, 0), 
                0, 
                COLORS.BLUE,
                _material.material2));
            graphicObjects.Add(new GraphicObject(new vec3(0, 0, 4), 
                90, 
                COLORS.WHITE,
                _material.material3));
            graphicObjects.Add(new GraphicObject(new vec3(0, 0, -4), 
                -90, 
                COLORS.GREEN,
                _material.material4));
            
            camera.setPosition(new vec3(10f, 15f, 17.5f));
        }
    }
}