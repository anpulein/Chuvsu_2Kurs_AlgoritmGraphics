using System;
using System.Drawing.Printing;
using System.Linq;

// Для работы с библиотекой OpenGL
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
using GlmSharp;
using Lib.Enum;

namespace Lab4.Other
{
    public class GraphicObject
    {

        #region Поля

        // Позиция объекта в глобальной системе координат
        private vec3 position;
        // Угол поворота в горизонтальной плоскости
        private float angle;
        // Цвет модели
        private vec3 color;     
        // Наименование цвета модели
        private string nameColor;
        // Матрица модели (расположение объекта) - чтоб не вычеслять каждый раз
        private mat4 modelMatrix;

        #endregion
        
        /// <summary>
        /// Конструктор
        /// </summary>
        public GraphicObject(vec3 position, float angle, COLORS color)
        {
            this.position = position;
            this.angle = angle;
            this.color = Colors.GetRGB(color);
            this.nameColor = Colors.GetColorsName(color);
            Print();
        }
        
        /// <summary>
        /// Установка позиции объекта
        /// </summary>
        /// <param name="position"></param>
        public void setPosition(vec3 position) => this.position = position;


        /// <summary>
        /// Получение позиции объекта
        /// </summary>
        /// <returns></returns>
        public vec3 getPosition()
        {
            return this.position;
        }

        // Поворот осуществляется в горизонтальной плоскости //
        // Вокруг оси Oy по часовой стрелке //

        /// <summary>
        /// Установка угла поворота в градусах
        /// </summary>
        /// <param name="grad"></param>
        public void setAngle(float grad) => this.angle = angle;

        /// <summary>
        /// Получение угла поворота в градусах
        /// </summary>
        /// <returns></returns>
        public float getAngle()
        {
            return this.angle;
        }

        /// <summary>
        /// Установка текущего цвета объекта
        /// </summary>
        /// <param name="color"></param>
        public void setColor(COLORS color) => this.color = Colors.GetRGB(color);

        /// <summary>
        /// Получение текущего цвета объекта
        /// </summary>
        /// <returns></returns>
        public vec3 getColor()
        {
            return this.color;
        }

        /// <summary>
        /// Вывод объекта в форму
        /// </summary>
        public void draw()
        {
            Gl.glColor3f(this.color.x, this.color.y, this.color.z);
            Gl.glPushMatrix();
            recalculateModelMatrix();
            Gl.glMultMatrixf(this.modelMatrix.Values1D);
            Gl.glRotatef(this.angle,0, 1, 0);
            Glut.glutWireTeapot(1.0f);
            Gl.glPopMatrix();
        }

        private void Print() => Console.WriteLine("Создание нового объекта GraphicObject { " + this + "\n}");

        public override string ToString()
        {
            return $"\nposition = ({this.position.ToString()})," +
                   $"\nangle = {this.angle.ToString()}," +
                   $"\ncolor = ({this.nameColor})";
        }

        /// <summary>
        /// Расчет матрицы modelMatrix на основе position и angle
        /// </summary>
        private void recalculateModelMatrix()
        {
            // Позиция объекта (начало системы координат)
            this.modelMatrix[3, 0] = this.position.x;
            this.modelMatrix[3, 1] = this.position.y;
            this.modelMatrix[3, 2] = this.position.z;
            this.modelMatrix[3, 3] = 1.0f;
            
            // Ось Oz
            this.modelMatrix[2, 0] = 0.0f;
            this.modelMatrix[2, 1] = 0.0f;
            this.modelMatrix[2, 2] = 1.0f;
            this.modelMatrix[2, 3] = 0.0f;
            
            // Ось Oy
            this.modelMatrix[1, 0] = 0.0f;
            this.modelMatrix[1, 1] = 1.0f;
            this.modelMatrix[1, 2] = 0.0f;
            this.modelMatrix[1, 3] = 0.0f;
            
            // Ось Ox
            this.modelMatrix[0, 0] = 1.0f;
            this.modelMatrix[0, 1] = 0.0f;
            this.modelMatrix[0, 2] = 0.0f;
            this.modelMatrix[0, 3] = 0.0f;
        }

    }
}