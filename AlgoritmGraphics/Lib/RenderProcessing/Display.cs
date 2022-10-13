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
                // case LABS.LAB6: DisplayLabFive(data); 
                // case LABS.LAB7: DisplayLabThree(data); 
                // case LABS.LAB8: DisplayLabThree(data); 
                // case LABS.LAB9: DisplayLabThree(data); 
                // case LABS.LAB10: DisplayLabThree(data); 
                // case LABS.LAB11: DisplayLabThree(data); 
                // case LABS.LAB12: DisplayLabThree(data);
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
        
        
    }
}