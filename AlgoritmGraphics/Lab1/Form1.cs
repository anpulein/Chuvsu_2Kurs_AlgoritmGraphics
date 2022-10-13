#region Подключение библиотек
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;

using Lab1.Other;
using Color = Lab1.Other.Color;

#endregion

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            View.InitializeContexts();
        }

        /// <summary>
        /// Функция, вызываемая при изменении размеров окна
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        private void reshape(int w, int h)
        {
            // Установить новую область просмотра, равную всей области окна
            Gl.glViewport(0, 0, w, h);
            
            // Установить матрицу проекции с правильным аспектом
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(25.0, (float)w / h, 0.2, 70.0);
        }
        
        /// <summary>
        /// Функция вызывается при перерисовке окна
        /// В том числе и принудительно, по командам glutPostRedisplay
        /// </summary>
        private void display()
        {
            // Очищает буфер цвета и буфер глубины
            Gl.glClearColor(0.22f, 0.88f, 0.11f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            // Включаем тест глубины
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            
            // Устанавливаем камеру
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(5, 5, 7.5, 0, 0, 0, 0, 1, 0);
            
            // Выводим объект - красный (1,0,0) чайник
            Gl.glColor3f(Color.rgb[0], Color.rgb[1], Color.rgb[2]);
            Glut.glutWireTeapot(1.0f);
            
            // Смена переднего и заднего буферов
            Glut.glutSwapBuffers();
        }
        
        /// <summary>
        /// Функция вызывается каждые 20 мс
        /// </summary>
        /// <param name="value"></param>
        private void simulation(int value)
        {
            // Устанавливаем признак того, что окно нуждается в перерисовке
            Glut.glutPostRedisplay();
            // Эта же функция будет вызвана еще раз через 20 мс
            Glut.glutTimerFunc(20, simulation, 0);
        }

        /// <summary>
        /// Функция обработки нажатия клавиш
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void keyboard(byte key, int x, int y)
        {
            switch ((char)key)
            {
                case 'd': Color.GetNextRGB(); break;
                case 'a': Color.GetPrevRGB(); break;
            }
            Glut.glutPostRedisplay();
            Console.WriteLine($"Key code is {(char)key}");
        }
        
        private void View_Load(object sender, EventArgs e)
        {
            
            // Инициализация библиотеки FreeGlut
            Glut.glutInit();
            // Инициализация дисплея (формат вывода)
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH | Glut.GLUT_MULTISAMPLE);
            
            //----------------- Создание окна -------------------//
            // 1. Устанавливаем верхний левый угол окна
            Glut.glutInitWindowPosition(200, 200);
            // 2. Устанавливаем размер окна
            Glut.glutInitWindowSize(800, 600);
            // 3. Создаем окно
            Glut.glutCreateWindow("Lab_1");
            
            //----------------- Установка функций обратного вызова -------------------//
            // Устанавливаем функцию, которая будет вызываться для перерисовки окна
            Glut.glutDisplayFunc(display);
            // Устанавливаем функцию, которая будет вызываться при изменении размеров окна
            Glut.glutReshapeFunc(reshape);
            // Устанавливаем функцию, которая будет вызвана через 20 мс
            Glut.glutTimerFunc(20, simulation, 0);
            // Устанавливаем функцию, которая будет вызываться при нажатии на клавишу
            Glut.glutKeyboardFunc(keyboard);
            
            // Основной цикл обработки сообщений ОС
            Glut.glutMainLoop();
        }

        
    }
}