// Для работы с библиотекой OpenGL

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;
using Lib.Enum;
using Lib.Lab5;
using Lib.Lab6;
using Lib.Lab7;
using Lib.Lab8;
using Lib.Lab9;
using Camera = Lib.Lab4.Camera;
using GameObject = Lib.Lab6.GameObject;
using GraphicObject = Lib.Lab4.GraphicObject;
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
        
        // Меши
        private ParseMeshes _meshes;

        // Общая фоновая освещенность
        public static float[] globalAmbientColor = {0.2f, 0.2f, 0.2f, 1.0f};
        
        // Путь к файлам
        public string path;
        
        // Путь к материалам
        public static string pathMaterial =
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\";
        
        // Путь к Мешам
        public static string pathMesh =
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\";
        
        // Путь к объектам для атрисовки
        public static string pathObject = 
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\Lab7\\materials\\GameObjectsDescription.json";


        #region Графические объекты
        // Карта проходимости
        public int[,] _passibilityMap =
        {
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
            {3,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,2,0,0,0,3},
            {3,0,2,1,2,0,2,0,2,2,2,1,2,0,2,0,2,0,2,2,3},
            {3,0,2,0,2,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,3},
            {3,0,1,0,2,2,1,2,2,0,2,0,2,2,2,1,2,0,2,0,3},
            {3,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,2,0,3},
            {3,0,2,2,1,1,2,0,2,0,2,2,2,2,2,0,2,2,2,0,3},
            {3,0,2,0,0,0,2,0,2,0,0,0,0,0,2,0,0,0,0,0,3},
            {3,0,2,0,2,2,2,0,2,0,2,2,1,2,2,2,1,2,2,0,3},
            {3,0,0,0,2,0,0,0,2,0,2,0,0,0,0,0,0,0,1,0,3},
            {3,2,2,2,2,0,2,2,2,0,2,0,2,2,2,2,2,2,2,0,3},
            {3,0,0,0,2,0,0,0,1,0,2,0,0,0,2,0,0,0,0,0,3},
            {3,0,2,0,2,2,2,0,2,1,2,0,2,2,2,0,2,2,2,2,3},
            {3,0,2,0,0,0,2,0,0,0,2,0,0,0,2,0,2,0,0,0,3},
            {3,2,2,2,2,0,2,2,2,0,2,2,2,0,1,0,2,2,2,0,3},
            {3,0,0,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,2,0,3},
            {3,0,2,0,2,1,2,0,2,0,2,2,2,0,2,2,2,0,2,0,3},
            {3,0,1,0,1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,3},
            {3,0,2,1,2,0,2,2,2,2,2,0,2,0,2,0,2,2,2,2,3},
            {3,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,3},
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3}
        };

        private int row = 21;
        private int col = 21;
        
        // список игровых объектов расположенных на карте
        public List<Lab6.GameObject> _gameObjects;

        // Графический объект для плоскости (частный случай)
        public Lib.Lab6.GraphicObject _planeGraphicObject;
        
        // Игровой объект для карты (частный случай)
        public Lib.Lab6.GameObject _planeGameObject;

        // Конввер создания игровых объектов
        public Lib.Lab7.GameObjectFactory _gameObjectFactory;
        
        // список игровых объектов расположенных на карте
        public List<Lab7.GameObject> _newGameObjects;
        
        // Игровой объект для карты (частный случай)
        public Lib.Lab7.GameObject _newPlaneGameObject;
        
        // Игровой персонаж (частный случай)
        public Lib.Lab7.GameObject _newPlayerGameObject;

        public static float simTick = 0.0f;

        #endregion

        #region Лаборатоная работа 8

        public static Int64 startTime;
        public static int framesValue;
        
        // Конввер создания игровых объектов
        public Lib.Lab7.GameObjectFactory _gameObjectFactory8;
        
        // список игровых объектов расположенных на карте
        public List<Lab8.GameObject8> _newGameObjects8;
        
        // Игровой объект для карты (частный случай)
        public Lib.Lab8.GameObject8 _newPlaneGameObject8;
        
        // Игровой персонаж (частный случай)
        public Lib.Lab8.GameObject8 _newPlayerGameObject8;

        public GameObject8 [,] _newGameObject8s;

        public static int posPlayerX;
        public static int posPlayerY;
        #endregion

        #region Лабораторная работа 9

        // Карта проходимости
        public int[,] _passibilityMap9 =
        {
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
            {3,6,0,0,0,0,1,0,0,0,0,0,0,0,0,0,2,0,0,6,3},
            {3,0,2,1,2,0,2,0,2,2,2,1,2,0,2,0,2,0,2,2,3},
            {3,0,2,0,2,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,3},
            {3,0,1,0,2,2,1,2,2,0,2,0,2,2,2,1,2,0,2,0,3},
            {3,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,2,0,3},
            {3,0,2,2,1,1,2,0,2,0,2,2,2,2,2,0,2,2,2,0,3},
            {3,0,2,0,0,0,2,0,2,0,0,0,0,0,2,0,0,0,0,0,3},
            {3,0,2,0,2,2,2,0,2,0,2,2,1,2,2,2,1,2,2,0,3},
            {3,0,0,0,2,0,0,0,2,0,2,0,0,0,0,0,0,0,1,0,3},
            {3,2,2,2,2,0,2,2,2,0,2,0,2,2,2,2,2,2,2,0,3},
            {3,0,0,0,2,0,0,0,1,0,2,0,0,0,2,0,0,0,0,0,3},
            {3,0,2,0,2,2,2,0,2,1,2,0,2,2,2,0,2,2,2,2,3},
            {3,0,2,0,0,0,2,0,0,0,2,0,0,0,2,0,2,0,0,0,3},
            {3,2,2,2,2,0,2,2,2,0,2,2,2,0,1,0,2,2,2,0,3},
            {3,0,0,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,2,0,3},
            {3,0,2,0,2,1,2,0,2,0,2,2,2,0,2,2,2,0,2,0,3},
            {3,0,1,0,1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,3},
            {3,0,2,1,2,0,2,2,2,2,2,0,2,0,2,0,2,2,2,2,3},
            {3,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,6,3},
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3}
        };
        
        // Путь к объектам для атрисовки
        public static string pathObject9 = 
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\Lab9\\GameObjectsDescription.json";
        
        // Путь к текстуре
        public static string pathTexture = 
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\Lab9\\textures\\plane.jpg";
        
        public int countMonster = 3;
        public List<Lab8.GameObject8> _monsterGameObject;

        public static int[,] monsterPosXY =
        {
            { 1, 1 },
            { 1, 19 },
            { 19, 19 }
        };

        public Texture _planeTexture;
        #endregion

        #region Лабораторная работа 10

        // Путь к объектам для атрисовки
        public static string pathObject10 = 
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\Lab10\\GameObjectsDescription.json";
        public static string pathLight = 
            "D:\\Projects\\Chuvsu\\Chuvsu_2Kurs_AlgoritmGraphics\\AlgoritmGraphics\\Lib\\Lab10\\materials\\light-1.json";
        
        public List<Lab10.GameObject> monsterGameObject;
        
        // Конввер создания игровых объектов
        public Lab10.GameObjectFactory gameObjectFactory;
        
        // // список игровых объектов расположенных на карте
        // public List<Lab10.GameObject> GameObjects;
        
        // Игровой объект для карты (частный случай)
        public Lab10.GameObject planeGameObject;
        
        // Игровой персонаж (частный случай)
        public Lab10.GameObject playerGameObject;
        
        // Бомба (частный случай)
        public Lab10.GameObject bombGameObject;

        public float bombTimeout = 0.5f;

        public Lab10.GameObject [,] gameObjects;
        #endregion

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
        
        public void initDataLab6()
        {
            Console.WriteLine("Инициализация данных нужных для Lab6!");
            Labs = LABS.LAB6;
            camera = new Camera(new vec3(10f, 45f, 17.5f));
            light = new Light();
            _material = new ParseMaterial();
            _meshes = new ParseMeshes();
            _planeGraphicObject = new Lab6.GraphicObject();
            _planeGameObject = new Lab6.GameObject();
            _gameObjects = new List<GameObject>();
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (_passibilityMap[i, j] != 0)
                    {
                        int type = _passibilityMap[i, j];
                        Lib.Lab6.GraphicObject graphicObject = new Lab6.GraphicObject();
                        graphicObject.setMesh(_meshes.getIndexMesh(type));
                        graphicObject.setMaterial(_material.getIndexMaterial(type));

                        var gameObject = new GameObject();
                        gameObject.setGraphicObject(graphicObject);
                        gameObject.setPosition(i, j);
                        _gameObjects.Add(gameObject);
                    }
                }
            }
            
            _planeGraphicObject.setMaterial(_material.material1);
            _planeGraphicObject.setMesh(_meshes.SimplePlane);
            _planeGameObject.setGraphicObject(_planeGraphicObject);
            _planeGameObject.setPosition(new vec3(10, 10, 0));
        }
        
        public void initDataLab7()
        {
            Console.WriteLine("Инициализация данных нужных для Lab7!");
            Labs = LABS.LAB7;
            camera = new Camera(new vec3(10f, 45f, 17.5f));
            light = new Light();
            _newPlaneGameObject = new Lab7.GameObject();
            _newPlayerGameObject = new Lab7.GameObject();
            _gameObjectFactory = new GameObjectFactory(pathObject);
            _newGameObjects = new List<Lab7.GameObject>();
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (_passibilityMap[i, j] != 0)
                    {
                        int type = _passibilityMap[i, j];
                        switch (type)
                        {
                            case 1:
                                _newGameObjects.Add(_gameObjectFactory.Creat(GameObjectType.LIGHT_OBJECT, i, j));
                                break;
                            case 2:
                                _newGameObjects.Add(_gameObjectFactory.Creat(GameObjectType.HEAVY_OBJECT, i, j));
                                break;
                            case 3:
                                _newGameObjects.Add(_gameObjectFactory.Creat(GameObjectType.BORDER_OBJECT, i, j));
                                break;
                        }
                    }
                }
            }
            
            _newPlaneGameObject = _gameObjectFactory.Creat(GameObjectType.SIMPLE_PLANE, 10, 10);
            _newPlayerGameObject = _gameObjectFactory.Creat(GameObjectType.PLAYER, 19, 1);
        }
        
        public void initDataLab8()
        {
            Console.WriteLine("Инициализация данных нужных для Lab8!");
            Labs = LABS.LAB8;
            camera = new Camera(new vec3(10f, 45f, 17.5f));
            light = new Light();
            _newPlaneGameObject8 = new Lab8.GameObject8();
            _newPlayerGameObject8 = new Lab8.GameObject8();
            _gameObjectFactory8 = new GameObjectFactory(pathObject);
            _newGameObject8s = new GameObject8 [row, col];
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (_passibilityMap[i, j] != 0)
                    {
                        int type = _passibilityMap[i, j];
                        switch (type)
                        {
                            case 0:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.EMPTY, i, j);
                                break;
                            case 1:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.LIGHT_OBJECT, i, j);
                                break;
                            case 2:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.HEAVY_OBJECT, i, j);
                                break;
                            case 3:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.BORDER_OBJECT, i, j);
                                break;
                        }
                    }
                }
            }

            int playerPosx = 19;
            int playerPosy = 1;
            _newPlaneGameObject8 = _gameObjectFactory8.Creat8(GameObjectType.SIMPLE_PLANE, 10, 10);
            _newPlayerGameObject8 = _gameObjectFactory8.Creat8(GameObjectType.PLAYER, 19, 1);
            posPlayerX = playerPosx;
            posPlayerY = playerPosy;
            
            _newGameObject8s[playerPosx, playerPosy] = _newPlayerGameObject8;
        }
        
        
        public void initDataLab9()
        {
            Console.WriteLine("Инициализация данных нужных для Lab9!");
            Labs = LABS.LAB9;
            camera = new Camera(new vec3(10f, 45f, 17.5f));
            light = new Light();
            _newPlaneGameObject8 = new Lab8.GameObject8();
            _newPlayerGameObject8 = new Lab8.GameObject8();
            _gameObjectFactory8 = new GameObjectFactory(pathObject9);
            _newGameObject8s = new GameObject8 [row, col];
            _monsterGameObject = new List<GameObject8>();
            _planeTexture = new Texture();
            
            Gl.glLightModelf(Gl.GL_LIGHT_MODEL_AMBIENT, globalAmbientColor[0]);


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (_passibilityMap9[i, j] != 0)
                    {
                        int type = _passibilityMap9[i, j];
                        switch (type)
                        {
                            case 0:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.EMPTY, i, j);
                                break;
                            case 1:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.LIGHT_OBJECT, i, j);
                                break;
                            case 2:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.HEAVY_OBJECT, i, j);
                                break;
                            case 3:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.BORDER_OBJECT, i, j);
                                break;
                            case 6:
                                _newGameObject8s[i, j] = _gameObjectFactory8.Creat8(GameObjectType.MONSTER, i, j);
                                _monsterGameObject.Add(_newGameObject8s[i, j]);
                                break;
                        }
                    }
                }
            }
            

            int playerPosx = 19;
            int playerPosy = 1;
            _newPlaneGameObject8 = _gameObjectFactory8.Creat8(GameObjectType.SIMPLE_PLANE, 10, 10);
            _newPlayerGameObject8 = _gameObjectFactory8.Creat8(GameObjectType.PLAYER, playerPosx, playerPosy);
            posPlayerX = playerPosx;
            posPlayerY = playerPosy;
            
            _newGameObject8s[playerPosx, playerPosy] = _newPlayerGameObject8;
            
            _planeTexture.load(pathTexture);
        }
        
        public void initDataLab10()
        {
            Console.WriteLine("Инициализация данных нужных для Lab10!");
            Labs = LABS.LAB10;
            camera = new Camera(new vec3(10f, 45f, 17.5f));
            light = new Light();
            planeGameObject = new Lab10.GameObject();
            playerGameObject = new Lab10.GameObject();
            gameObjectFactory = new Lab10.GameObjectFactory(pathObject10);
            gameObjects = new Lab10.GameObject [row, col];
            monsterGameObject = new List<Lab10.GameObject>();
            _planeTexture = new Texture();
            light.load(pathLight);
            
            Gl.glLightModelf(Gl.GL_LIGHT_MODEL_AMBIENT, globalAmbientColor[0]);


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (_passibilityMap9[i, j] != 0)
                    {
                        int type = _passibilityMap9[i, j];
                        switch (type)
                        {
                            case 0:
                                gameObjects[i, j] = gameObjectFactory.Create(GameObjectType.EMPTY, i, j);
                                break;
                            case 1:
                                gameObjects[i, j] = gameObjectFactory.Create(GameObjectType.LIGHT_OBJECT, i, j);
                                break;
                            case 2:
                                gameObjects[i, j] = gameObjectFactory.Create(GameObjectType.HEAVY_OBJECT, i, j);
                                break;
                            case 3:
                                gameObjects[i, j] = gameObjectFactory.Create(GameObjectType.BORDER_OBJECT, i, j);
                                break;
                            case 6:
                                gameObjects[i, j] = gameObjectFactory.Create(GameObjectType.MONSTER, i, j);
                                monsterGameObject.Add(gameObjects[i, j]);
                                break;
                        }
                    }
                }
            }
            

            int playerPosx = 19;
            int playerPosy = 1;
            planeGameObject = gameObjectFactory.Create(GameObjectType.SIMPLE_PLANE, 10, 10);
            playerGameObject = gameObjectFactory.Create(GameObjectType.PLAYER, playerPosx, playerPosy);
            posPlayerX = playerPosx;
            posPlayerY = playerPosy;
            
            gameObjects[playerPosx, playerPosy] = playerGameObject;
        }
    }
}