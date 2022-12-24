#region MyRegion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows;


using Tao.DevIl;
// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;
// Кросплатформенная библиотека, которая упрощает запрос и загрузку расширений OpenGL
using Lib;
using Lib.Enum;
using Display = Lib.RenderProcessing.Display;
using Reshape = Lib.RenderProcessing.Reshape;
using Simulation = Lib.RenderProcessing.Simulation;
#endregion

namespace Lab10
{
    public partial class Form1 : Form
    {
        public static Data _data;
        public Form1()
        {
            InitializeComponent();
            View.InitializeContexts();
            _data = new Data();
            
            Console.WriteLine("Запуск приложения!");
        }
        
        /// <summary>
        /// Функция, вызываемая при изменении размеров окна
        /// </summary>
        private void reshape(int w, int h)
        {
            Reshape.reshape(w, h);
        }
       
        /// <summary>
        /// Функция вызывается при перерисовке окна
        /// В том числе и принудительно, по командам glutPostRedisplay
        /// </summary>
        private void display()
        {
            Display.display(_data);
        }
        
        /// <summary>
        /// Функция вызывается максимально часто
        /// </summary>
        /// <param name="value"></param>
        private void simulation()
        {
            Simulation.simulation10(_data);
        }

        /// <summary>
        /// Функция обработки нажатия клавиш
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void keyboard(byte key, int x, int y)
        {
            
        }

        private void View_Load(object sender, EventArgs e)
        {
            // Инициализация библиотеки FreeGlut
            Glut.glutInit();
            // Инициализация дисплея (формат вывода)
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH | Glut.GLUT_MULTISAMPLE);
            
            // Включаем нулевой источник света
            Gl.glEnable(Gl.GL_LIGHT0);


            // Устанавливаем общую фоновую освещенность
            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, Data.globalAmbientColor);
            
            
            //----------------- Создание окна -------------------//
            // 1. Устанавливаем верхний левый угол окна
            Glut.glutInitWindowPosition(200, 200);
            // 2. Устанавливаем размер окна
            Glut.glutInitWindowSize(800, 600);
            // 3. Создаем окно
            Glut.glutCreateWindow("Lab9");

            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();

            _data.initDataLab10();
            //----------------- Установка функций обратного вызова -------------------//
            // Устанавливаем функцию, которая будет вызываться для перерисовки окна
            Glut.glutDisplayFunc(display);
            // Устанавливаем функцию, которая будет вызываться при изменении размеров окна
            Glut.glutReshapeFunc(reshape);
            // Устанавливаем функцию, которая будет вызвана через 20 мс
            Glut.glutIdleFunc(simulation);
            // Устанавливаем функцию, которая будет вызываться при нажатии на клавишу
            // Glut.glutKeyboardFunc(keyboard);
            
            // Основной цикл обработки сообщений ОС
            Glut.glutMainLoop();
        }
    }
}