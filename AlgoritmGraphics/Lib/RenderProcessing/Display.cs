// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;

using Lib;
using Lib.Enum;

namespace Lib.Lab4
{
    public class Display
    {
        /// <summary>
        /// Функция вызывается при перерисовке окна
        /// В том числе и принудительно, по командам glutPostRedisplay
        /// </summary>
        public static void display(Data _data)
        {
            // Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            if (Data.Labs == LABS.LAB5)
            {
                // Включаем режим расчета освещения
                Gl.glEnable(Gl.GL_LIGHTING);
            }

            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            _data.camera.apply();
            if (Data.Labs == LABS.LAB5)
            {
                _data.light.apply(Gl.GL_LIGHT0);
            }
                


            foreach (var obj in _data.graphicObjects)
            {
                // obj.setAngle(-_data.camera.getAngleX() - 180); // Доп задание 4 лабы
                obj.draw();
            }
            
            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
    }
}