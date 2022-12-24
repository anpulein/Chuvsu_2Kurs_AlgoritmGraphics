// Для работы с библиотекой OpenGL

using System.Diagnostics;
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;

using Lib;
using Lib.Lab9;
using LABS =  Lib.Enum.LABS;

namespace Lib.RenderProcessing
{
    public class Display
    {
        /// <summary>
        /// Функция вызывается при перерисовке окна
        /// В том числе и принудительно, по командам glutPostRedisplay
        /// </summary>
        public static void display(Data data)
        {
            switch (Data.Labs)
            {
                case LABS.LAB3: DisplayLabThree(data); break; 
                case LABS.LAB4: DisplayLabFour(data); break;
                case LABS.LAB5: DisplayLabFive(data); break;
                case LABS.LAB6: DisplayLabSix(data); break;
                case LABS.LAB7: DisplayLabSeven(data); break;
                case LABS.LAB8: DisplayLabEight(data); break;
                case LABS.LAB9: DisplayLabNine(data); break;
                case LABS.LAB10: DisplayLabTen(data); break;
            }
        }
        
        /// <summary>
        /// Для выполнения Lab3
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabThree(Data data)
        {
            // Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            
            foreach (var obj in data.graphicObjects)
            {
                // obj.setAngle(-_data.camera.getAngleX() - 180); // Доп задание 4 лабы
                obj.draw();
            }
            
            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        /// <summary>
        /// Для выполнения Lab4
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabFour(Data data)
        {
            // Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            
            foreach (var obj in data.graphicObjects)
            {
                // obj.setAngle(-_data.camera.getAngleX() - 180); // Доп задание 4 лабы
                obj.draw();
            }
            
            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        /// <summary>
        /// Для выполнения Lab5
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabFive(Data data)
        {
            /// Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // Включаем режим расчета освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            data.light.apply(Gl.GL_LIGHT0);
            
            foreach (var obj in data.graphicObjects)
            {
                // obj.setAngle(-_data.camera.getAngleX() - 180); // Доп задание 4 лабы
                obj.draw();
            }
            
            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        
        /// <summary>
        /// Для выполнения Lab6
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabSix(Data data)
        {
            /// Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.5f, 0.5f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // Включаем режим расчета освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            data.light.apply(Gl.GL_LIGHT0);
            
            data._planeGameObject?.draw();

            foreach (var obj in data._gameObjects )
            {
                obj?.draw();
            }



            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        /// <summary>
        /// Для выполнения Lab7
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabSeven(Data data)
        {
            /// Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.5f, 0.5f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // Включаем режим расчета освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            data.light.apply(Gl.GL_LIGHT0);
            
            data._newPlaneGameObject?.draw();
            data._newPlayerGameObject?.draw();

            foreach (var obj in data._newGameObjects )
            {
                obj?.draw();
            }
            

            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        /// <summary>
        /// Для выполнения Lab8
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabEight(Data data)
        {
            int mapSize = 21;
            /// Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.5f, 0.5f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // Включаем режим расчета освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            data.light.apply(Gl.GL_LIGHT0);
            
            data._newPlaneGameObject8?.draw();

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    data._newGameObject8s[i, j]?.draw();
                }
            }
            
            // data._newPlayerGameObject8?.draw();
            
            

            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        /// <summary>
        /// Для выполнения Lab9
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabNine(Data data)
        {
            int mapSize = 21;
            /// Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.5f, 0.5f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // Включаем режим расчета освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            data.light.apply(Gl.GL_LIGHT0);

            drawPlane(data);
            
            // data._newPlaneGameObject8?.draw();

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    data._newGameObject8s[i, j]?.draw();
                }
            }



            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }

        /// <summary>
        /// Для выполнения Lab10
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayLabTen(Data data)
        {
            int mapSize = 21;
            /// Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.5f, 0.5f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            // Включаем режим расчета освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            data.camera.apply();
            data.light.apply(Gl.GL_LIGHT0);

            drawPlaneTen(data);
            
            // data._newPlaneGameObject8?.draw();

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    data.gameObjects[i, j]?.draw();
                    Texture.disableAll();
                }
            }

            data.bombGameObject?.draw();
            Texture.disableAll();
        
            Gl.glDisable(Gl.GL_LIGHTING);


            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }

        public static void drawPlane(Data data)
        {
            // Выбираем активный текстурный блок
            Gl.glActiveTexture(Gl.GL_TEXTURE0);
            
            // Разрешаем текстурирование в выбранном текстурном блоке
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            
            // Привязываем текстуру к ранее выбраноому текстурному блоку
            data._planeTexture.apply();
            
            // Указываем режим наложения тексткры (GL_MODULATE)
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);
            
            // Выводим плоскать
            data._newPlaneGameObject8?.draw();
            
            // Отключаем тексткрирование (чтобы все остальные объекты выводились без текстур)
            Texture.disableAll();
        }
        
        public static void drawPlaneTen(Data data)
        {
            // Выводим плоскать
            data.planeGameObject?.draw();
            
            // Отключаем тексткрирование (чтобы все остальные объекты выводились без текстур)
            Texture.disableAll();
        }
    }
}