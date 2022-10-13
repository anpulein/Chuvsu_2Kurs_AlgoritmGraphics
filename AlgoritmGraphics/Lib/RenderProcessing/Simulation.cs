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
    public class Simulation
    {

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState( System.Windows.Forms.Keys vKey);

        /// <summary>
        /// Функция симуляции - вызывается максимально часто
        /// Черз заранее неизвестные промежутки времени
        /// Для чего реагируется с помощью glutIdleFunc
        /// </summary>
        public static void simulation()
        {

            CalculateFrameRate();
            
            // Передвижение камеры
            // bool cameraLeft = GetAsyncKeyState(Keys.Left);
            
            // Устанавоиваем признак того, что окно нуждается в перерисовке
            Glut.glutPostRedisplay();
            
            // Установка таймера
            Glut.glutIdleFunc(simulation);
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

        // public static void getSimulationTime()
        // {
        //     Data.endSimulationTime = DateTime.Now.Ticks;
        //     var time = (int)(Data.endSimulationTime - Data.startSimulationTime);
        //     Data.startSimulationTime = Data.endSimulationTime;
        //
        //     Glut.glutSetWindowTitle($"FPS: {(time.ToString())}");
        // }
    }
}