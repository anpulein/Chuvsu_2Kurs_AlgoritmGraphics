using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;
using Lib.Enum;
using Lib.Lab7;
using Lib.Lab8;

namespace Lib.RenderProcessing
{
    public class Simulation
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);
        
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey); 
        private static Data data;
        

        /// <summary>
        /// Получение времени симуляции в секундах
        /// </summary>
        /// <returns></returns>
        public static double getSimulate()
        {
            var start = Data.startTime;
            long stop;
            long frequency;
            QueryPerformanceFrequency(out frequency);
            QueryPerformanceCounter(out stop);
            float time = ((float)(stop - start) * 7) / frequency;
            Data.startTime = stop;
            return time;
        }

        /// <summary>
        /// Симуляция камеры
        /// </summary>
        /// <param name="simulationTime"></param>
        public static void cameraSimulation(double simulationTime)
        {
            
        }

        /// <summary>
        /// Перемещения главногогероя (распределение сообщений)
        /// </summary>
        public static void movePlayer(Data data)
        {
            bool moveUp = Convert.ToBoolean(GetAsyncKeyState(Keys.W)); // W
            bool moveDown = Convert.ToBoolean(GetAsyncKeyState(Keys.S)); // S
            bool moveLeft = Convert.ToBoolean(GetAsyncKeyState(Keys.A)); // A
            bool moveRight = Convert.ToBoolean(GetAsyncKeyState(Keys.D)); // D
            bool plantBomb = Convert.ToBoolean(GetAsyncKeyState(Keys.Space)); // Space


            if (Data.Labs == LABS.LAB9 || Data.Labs  == LABS.LAB8)
            {
                if (moveUp) movePlayerInstructions(data, 0, -1, 0, -1, MoveDirection.UP);
                else if (moveDown) movePlayerInstructions(data, 0, 1, 0, 1, MoveDirection.DOWN);
                else if (moveLeft) movePlayerInstructions(data, -1, 0, -1, 0, MoveDirection.LEFT);
                else if (moveRight) movePlayerInstructions(data, 1, 0, 1, 0, MoveDirection.RIGHT);
            }
            else if (Data.Labs == LABS.LAB10)
            {
                if (moveUp) movePlayerInstructionsTen(data, 0, -1, 0, -1, MoveDirection.UP);
                else if (moveDown) movePlayerInstructionsTen(data, 0, 1, 0, 1, MoveDirection.DOWN);
                else if (moveLeft) movePlayerInstructionsTen(data, -1, 0, -1, 0, MoveDirection.LEFT);
                else if (moveRight) movePlayerInstructionsTen(data, 1, 0, 1, 0, MoveDirection.RIGHT);

                if (plantBomb && data.bombGameObject == null)
                {
                    moveBombInstructions(data);
                }
            }
        }

        /// <summary>
        /// Движение объекта
        /// </summary>
        /// <param name="xO"></param>
        /// <param name="yO"></param>
        /// <param name="xOB"></param>
        /// <param name="yOB"></param>
        /// <param name="direction"></param>
        public static void movePlayerInstructions(Data data, int xO, int yO, int xOB, int yOB, MoveDirection direction)
        {
            var player = data._newPlayerGameObject8;
            var passibilityMap = data._passibilityMap;
            var gameObjects = data._newGameObject8s;
            ivec3 position = new ivec3(Data.posPlayerX, Data.posPlayerY, 0);
            if (passibilityMap[position.x + xO, position.y + yO] == (int)GameObjectType.EMPTY &&
                !player.isMoving())
            {
                player.move(direction);
                passibilityMap[position.x + xO, position.y + yO] = (int)GameObjectType.PLAYER;
                passibilityMap[position.x, position.y] = 0;
                gameObjects[position.x + xO, position.y + yO] = gameObjects[position.x, position.y];
                gameObjects[position.x, position.y] = null;
                Data.posPlayerX = position.x + xO;
                Data.posPlayerY = position.y + yO;
            }
            else if (passibilityMap[position.x + xO, position.y + yO] == (int)GameObjectType.LIGHT_OBJECT &&
                     passibilityMap[position.x + xO + xOB, position.y + yO + yOB] == (int)GameObjectType.EMPTY && !player.isMoving())
            {
                gameObjects[position.x + xO, position.y + yO].move(direction, 0.25f);
                player.move(direction);
                passibilityMap[position.x + xO + xOB, position.y + yO + yOB] = (int)GameObjectType.LIGHT_OBJECT;
                passibilityMap[position.x + xO, position.y + yO] = 0;
                gameObjects[position.x + xO + xOB, position.y + yO + yOB] = gameObjects[position.x + xO, position.y + yO];
                gameObjects[position.x + xO, position.y + yO] = null;
                passibilityMap[position.x + xO, position.y + yO] = (int)GameObjectType.PLAYER;
                passibilityMap[position.x, position.y] = 0;
                gameObjects[position.x + xO, position.y + yO] = gameObjects[position.x, position.y];
                gameObjects[position.x, position.y] = null;
                
                Data.posPlayerX = position.x + xO;
                Data.posPlayerY = position.y + yO;
            }
            
            
            data._passibilityMap = passibilityMap;
            data._newGameObject8s = gameObjects;
        }
        
        public static void movePlayerInstructionsTen(Data data, int xO, int yO, int xOB, int yOB, MoveDirection direction)
        {
            var player = data.playerGameObject;
            var passibilityMap = data._passibilityMap9;
            var gameObjects = data.gameObjects;
            ivec3 position = new ivec3(Data.posPlayerX, Data.posPlayerY, 0);
            if (passibilityMap[position.x + xO, position.y + yO] == (int)GameObjectType.EMPTY &&
                !player.isMoving())
            {
                player.move(direction);
                passibilityMap[position.x + xO, position.y + yO] = (int)GameObjectType.PLAYER;
                passibilityMap[position.x, position.y] = 0;
                gameObjects[position.x + xO, position.y + yO] = gameObjects[position.x, position.y];
                gameObjects[position.x, position.y] = null;
                Data.posPlayerX = position.x + xO;
                Data.posPlayerY = position.y + yO;
            }
            else if (passibilityMap[position.x + xO, position.y + yO] == (int)GameObjectType.LIGHT_OBJECT &&
                     passibilityMap[position.x + xO + xOB, position.y + yO + yOB] == (int)GameObjectType.EMPTY && !player.isMoving())
            {
                gameObjects[position.x + xO, position.y + yO].move(direction, 0.25f);
                player.move(direction);
                passibilityMap[position.x + xO + xOB, position.y + yO + yOB] = (int)GameObjectType.LIGHT_OBJECT;
                passibilityMap[position.x + xO, position.y + yO] = 0;
                gameObjects[position.x + xO + xOB, position.y + yO + yOB] = gameObjects[position.x + xO, position.y + yO];
                gameObjects[position.x + xO, position.y + yO] = null;
                passibilityMap[position.x + xO, position.y + yO] = (int)GameObjectType.PLAYER;
                passibilityMap[position.x, position.y] = 0;
                gameObjects[position.x + xO, position.y + yO] = gameObjects[position.x, position.y];
                gameObjects[position.x, position.y] = null;
                
                Data.posPlayerX = position.x + xO;
                Data.posPlayerY = position.y + yO;
            }
            
            
            data._passibilityMap9 = passibilityMap;
            data.gameObjects = gameObjects;
        }

        
        public static void moveMonsterInstructions(Data data, GameObject8 monster)
        {
            Random randomPos = new Random();
            var speed = 0.5f;
            var passibilityMap = data._passibilityMap;
            var gameObjects = data._newGameObject8s;

            bool UP = false;
            bool LEFT = false;
            bool DOWN = false;
            bool RIGHT = false;

            var emptyType = (int)GameObjectType.EMPTY;
            var playerType = (int)GameObjectType.PLAYER;
            var monsterType = (int)GameObjectType.MONSTER;

            if (!monster.isMoving())
            {
                List<MoveDirection> directions = new List<MoveDirection>();
                ivec3 mPos = (ivec3)monster.getPosition();

                if ((passibilityMap[mPos.x, mPos.y - 1] == emptyType || passibilityMap[mPos.x, mPos.y - 1] == playerType) && monster.getPrevDirection() != MoveDirection.DOWN)
                {
                    directions.Add(MoveDirection.UP);
                    if (passibilityMap[mPos.x, mPos.y - 1] == playerType)
                    {
                        passibilityMap[mPos.x, mPos.y - 1] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x, mPos.y - 1];
                        gameObjects[mPos.x, mPos.y - 1] = null;
                    }
                } 
                if ((passibilityMap[mPos.x - 1, mPos.y] == emptyType || passibilityMap[mPos.x - 1, mPos.y] == playerType) && monster.getPrevDirection() != MoveDirection.RIGHT)
                {
                    directions.Add(MoveDirection.LEFT);
                    if (passibilityMap[mPos.x - 1, mPos.y] == playerType)
                    {
                        passibilityMap[mPos.x - 1, mPos.y] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x - 1, mPos.y];
                        gameObjects[mPos.x - 1, mPos.y] = null;
                    }
                } 
                if ((passibilityMap[mPos.x, mPos.y + 1] == emptyType || passibilityMap[mPos.x, mPos.y + 1] == playerType) && monster.getPrevDirection() != MoveDirection.UP)
                {
                    directions.Add(MoveDirection.DOWN);
                    if (passibilityMap[mPos.x, mPos.y + 1] == playerType)
                    {
                        passibilityMap[mPos.x, mPos.y + 1] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x, mPos.y + 1];
                        gameObjects[mPos.x, mPos.y + 1] = null;
                    }
                } 
                if ((passibilityMap[mPos.x + 1, mPos.y] == emptyType || passibilityMap[mPos.x + 1, mPos.y] == playerType) && monster.getPrevDirection() != MoveDirection.LEFT)
                {
                    directions.Add(MoveDirection.RIGHT);
                    if (passibilityMap[mPos.x + 1, mPos.y] == playerType)
                    {
                        passibilityMap[mPos.x + 1, mPos.y] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x + 1, mPos.y];
                        gameObjects[mPos.x + 1, mPos.y] = null;
                    }
                }

                int size = directions.Count;
                if (size != 0)
                {
                    int random = randomPos.Next(size);
                    monster.move(directions[random], speed);
                    switch (directions[random])
                    {
                        case MoveDirection.UP:
                            passibilityMap[mPos.x, mPos.y - 1] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x, mPos.y - 1] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        case MoveDirection.LEFT:
                            passibilityMap[mPos.x - 1, mPos.y] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x + 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        case MoveDirection.DOWN:
                            passibilityMap[mPos.x, mPos.y + 1] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x, mPos.y + 1] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        case MoveDirection.RIGHT:
                            passibilityMap[mPos.x + 1, mPos.y] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x + 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (monster.getPrevDirection())
                    {
                        case MoveDirection.UP:
                            if (passibilityMap[mPos.x, mPos.y + 1] == emptyType)
                            {
                                monster.move(MoveDirection.DOWN, speed);
                                passibilityMap[mPos.x, mPos.y + 1] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x, mPos.y + 1] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        case MoveDirection.LEFT:
                            if (passibilityMap[mPos.x, mPos.y + 1] == emptyType)
                            {
                                monster.move(MoveDirection.RIGHT, speed);
                                passibilityMap[mPos.x + 1, mPos.y] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x + 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        case MoveDirection.DOWN:
                            if (passibilityMap[mPos.x, mPos.y - 1] == emptyType)
                            {
                                monster.move(MoveDirection.UP, speed);
                                passibilityMap[mPos.x, mPos.y - 1] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x, mPos.y - 1] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        case MoveDirection.RIGHT:
                            if (passibilityMap[mPos.x, mPos.y + 1] == emptyType)
                            {
                                monster.move(MoveDirection.LEFT, speed);
                                passibilityMap[mPos.x - 1, mPos.y] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x - 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            
            data._passibilityMap = passibilityMap;
            data._newGameObject8s = gameObjects;
        }
        
        public static void moveMonsterInstructions10(Data data, Lib.Lab10.GameObject monster)
        {
            Random randomPos = new Random();
            var speed = 0.5f;
            var passibilityMap = data._passibilityMap9;
            var gameObjects = data.gameObjects;

            bool UP = false;
            bool LEFT = false;
            bool DOWN = false;
            bool RIGHT = false;

            var emptyType = (int)GameObjectType.EMPTY;
            var playerType = (int)GameObjectType.PLAYER;
            var monsterType = (int)GameObjectType.MONSTER;

            if (!monster.isMoving())
            {
                List<MoveDirection> directions = new List<MoveDirection>();
                ivec3 mPos = (ivec3)monster.getPosition();

                if ((passibilityMap[mPos.x, mPos.y - 1] == emptyType || passibilityMap[mPos.x, mPos.y - 1] == playerType) && monster.getPrevDirection() != MoveDirection.DOWN)
                {
                    directions.Add(MoveDirection.UP);
                    if (passibilityMap[mPos.x, mPos.y - 1] == playerType)
                    {
                        passibilityMap[mPos.x, mPos.y - 1] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x, mPos.y - 1];
                        gameObjects[mPos.x, mPos.y - 1] = null;
                    }
                } 
                if ((passibilityMap[mPos.x - 1, mPos.y] == emptyType || passibilityMap[mPos.x - 1, mPos.y] == playerType) && monster.getPrevDirection() != MoveDirection.RIGHT)
                {
                    directions.Add(MoveDirection.LEFT);
                    if (passibilityMap[mPos.x - 1, mPos.y] == playerType)
                    {
                        passibilityMap[mPos.x - 1, mPos.y] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x - 1, mPos.y];
                        gameObjects[mPos.x - 1, mPos.y] = null;
                    }
                } 
                if ((passibilityMap[mPos.x, mPos.y + 1] == emptyType || passibilityMap[mPos.x, mPos.y + 1] == playerType) && monster.getPrevDirection() != MoveDirection.UP)
                {
                    directions.Add(MoveDirection.DOWN);
                    if (passibilityMap[mPos.x, mPos.y + 1] == playerType)
                    {
                        passibilityMap[mPos.x, mPos.y + 1] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x, mPos.y + 1];
                        gameObjects[mPos.x, mPos.y + 1] = null;
                    }
                } 
                if ((passibilityMap[mPos.x + 1, mPos.y] == emptyType || passibilityMap[mPos.x + 1, mPos.y] == playerType) && monster.getPrevDirection() != MoveDirection.LEFT)
                {
                    directions.Add(MoveDirection.RIGHT);
                    if (passibilityMap[mPos.x + 1, mPos.y] == playerType)
                    {
                        passibilityMap[mPos.x + 1, mPos.y] = emptyType;
                        passibilityMap[19, 1] = playerType;
                        gameObjects[19, 1] = gameObjects[mPos.x + 1, mPos.y];
                        gameObjects[mPos.x + 1, mPos.y] = null;
                    }
                }

                int size = directions.Count;
                if (size != 0)
                {
                    int random = randomPos.Next(size);
                    monster.move(directions[random], speed);
                    switch (directions[random])
                    {
                        case MoveDirection.UP:
                            passibilityMap[mPos.x, mPos.y - 1] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x, mPos.y - 1] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        case MoveDirection.LEFT:
                            passibilityMap[mPos.x - 1, mPos.y] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x + 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        case MoveDirection.DOWN:
                            passibilityMap[mPos.x, mPos.y + 1] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x, mPos.y + 1] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        case MoveDirection.RIGHT:
                            passibilityMap[mPos.x + 1, mPos.y] = monsterType;
                            passibilityMap[mPos.x, mPos.y] = emptyType;
                            // gameObjects[mPos.x + 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                            // gameObjects[mPos.x, mPos.y] = null;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (monster.getPrevDirection())
                    {
                        case MoveDirection.UP:
                            if (passibilityMap[mPos.x, mPos.y + 1] == emptyType)
                            {
                                monster.move(MoveDirection.DOWN, speed);
                                passibilityMap[mPos.x, mPos.y + 1] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x, mPos.y + 1] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        case MoveDirection.LEFT:
                            if (passibilityMap[mPos.x, mPos.y + 1] == emptyType)
                            {
                                monster.move(MoveDirection.RIGHT, speed);
                                passibilityMap[mPos.x + 1, mPos.y] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x + 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        case MoveDirection.DOWN:
                            if (passibilityMap[mPos.x, mPos.y - 1] == emptyType)
                            {
                                monster.move(MoveDirection.UP, speed);
                                passibilityMap[mPos.x, mPos.y - 1] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x, mPos.y - 1] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        case MoveDirection.RIGHT:
                            if (passibilityMap[mPos.x, mPos.y + 1] == emptyType)
                            {
                                monster.move(MoveDirection.LEFT, speed);
                                passibilityMap[mPos.x - 1, mPos.y] = monsterType;
                                passibilityMap[mPos.x, mPos.y] = emptyType;
                                // gameObjects[mPos.x - 1, mPos.y] = gameObjects[mPos.x, mPos.y];
                                // gameObjects[mPos.x, mPos.y] = null;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            
            data._passibilityMap9 = passibilityMap;
            data.gameObjects = gameObjects;
        }

        public static void moveBombInstructions(Data data)
        {
            var passibilityMap = data._passibilityMap9;
            var gameObjects = data.gameObjects;
            ivec3 position = (ivec3)data.playerGameObject.getPosition();
            data.bombGameObject = data.gameObjectFactory.Create(GameObjectType.BOMB, position.x, position.y);
            passibilityMap[position.x, position.y] = (int)GameObjectType.BOMB;
            // gameObjects[position.x, position.y] = bomb;
            data.bombGameObject.move(MoveDirection.WAIT, data.bombTimeout);
            
            data._passibilityMap9 = passibilityMap;
            data.gameObjects = gameObjects;
        }

        public static void moveBomb(Data data)
        {
            if (data.bombGameObject != null && !data.bombGameObject.isMoving()) moveBombTimeOverInstructions(data);
        }
        
        public static void moveBombTimeOverInstructions(Data data)
        {
            ivec3 bombPosition = (ivec3)data.bombGameObject.getPosition();
            bool[] bombDir = new bool[] { false, false, false, false };
            var mapSize = 21;

            for (int i = 1; i <= 2; i++)
            {
                // if((bombPosition.y + i >= 0 && ))
            }
        }

        
        private static void gameObjectsSimulation(float simulationTime, Data data, int mapSize)
        {
            // data._newPlayerGameObject8.simulate(simulationTime);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    data._newGameObject8s[i, j]?.simulate(simulationTime);
                }
            }
        }
        
        private static void gameObjectsSimulationTen(float simulationTime, Data data, int mapSize)
        {
            // data._newPlayerGameObject8.simulate(simulationTime);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    data.gameObjects[i, j]?.simulate(simulationTime);
                }
            }
        }
        
        
        /// <summary>
        /// Функция симуляции - вызывается максимально часто
        /// Черз заранее неизвестные промежутки времени
        /// Для чего реагируется с помощью glutIdleFunc
        /// 1 - 7 лаб
        /// </summary>
        public static void simulation()
        {
            CalculateFrameRate();
            Console.WriteLine("Yws");
            // Устанавоиваем признак того, что окно нуждается в перерисовке
            Glut.glutPostRedisplay();
            
            // Установка таймера
            Glut.glutIdleFunc(simulation);
        }

        private static void Simulate8()
        {
            Simulation.simulation8(data);
        } 
        
        private static void Simulate10()
        {
            Simulation.simulation10(data);
        } 

        public static void simulation8(Data datae)
        {
            CalculateFrameRate();

            data = datae;

            var simTimePassed = getSimulate();
            cameraSimulation(Data.EndSimulationTime);
            gameObjectsSimulation((float)simTimePassed, data, 21);
            movePlayer(data);


            if (Data.Labs == LABS.LAB9)
            {
                foreach (var monster in data._monsterGameObject)
                {
                    moveMonsterInstructions(data, monster);
                }
            }
            
            // Устанавоиваем признак того, что окно нуждается в перерисовке
            Glut.glutPostRedisplay();
            
            // Установка таймера
            Glut.glutIdleFunc(Simulate8);
        }
        
        public static void simulation10(Data datae)
        {
            CalculateFrameRate();

            data = datae;

            var simTimePassed = getSimulate();
            cameraSimulation(Data.EndSimulationTime);
            gameObjectsSimulationTen((float)simTimePassed, data, 21);
            movePlayer(data);

            foreach (var monster in data.monsterGameObject)
            {
                moveMonsterInstructions10(data, monster);
            }
            
            // Устанавоиваем признак того, что окно нуждается в перерисовке
            Glut.glutPostRedisplay();
            
            // Установка таймера
            Glut.glutIdleFunc(Simulate10);
        }
        
        private static void CalculateFrameRate()
        {
            float currentTime = Glut.glutGet(Glut.GLUT_ELAPSED_TIME);
            ++Data.FramesPerSecond;
            if (currentTime - Data.EndSimulationTime > 1000)
            {
                Data.EndSimulationTime = currentTime;
                Data.FPS = (int)(Data.FramesPerSecond);
                Data.FramesPerSecond = 0;
            }

            Glut.glutSetWindowTitle($"FPS: {Data.FPS.ToString()}");
        }
        
    }
}