using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;

namespace Lib.Lab4
{
    public class Reshape
    {
        
        /// <summary>
        /// Функция, вызываемая при изменении размеров окна
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void reshape(int w, int h)
        {
            // Установить новую область просмотра, равную всей области окна
            Gl.glViewport(0, 0, w, h);
            
            // Установить матрицу проекции с правильным аспектом
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(25.0, (float)w / h, 0.2, 70.0);
        }
    }
}