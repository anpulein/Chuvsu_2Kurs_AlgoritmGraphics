
using GlmSharp;
using Tao.OpenGl;
using glm = GlmSharp.glm;

namespace Lib.Lab4
{
    public class Camera
    {

        #region Поля
        
        private const float MIN_ANGLE_VALUE = 5;
        private const float MAX_ANGLE_VALUE = 85;

        // Радиус и углы поворота
        private float radius;
        private float angleX;
        private float angleY;
        
        // Позиция  
        private float eyeX;
        private float eyeY;
        private float eyeZ;
        
        // Центральный массива (centerX, centerY, centerZ)
        private vec3 target = new vec3(10, 0, 10);
        
        // Верхний массив (upX, upY, upZ)
        private vec3 ups = new vec3(0, 1, 0);
        
        // Позиция камеры
        private vec3 position;
        #endregion
        
        public Camera()
        {
        }

        public Camera(vec3 position)
        {
            setPosition(position);
        }
        
        public Camera(float x, float y, float z)
        {
            // Позиция
            eyeX = x;
            eyeY = y;
            eyeZ = z;
            
            position = new vec3(x, y, z);

        }

        public void setPosition(vec3 position)
        {
            // Определяем радиус - расстояние от начала системы координат до заданной позиции
            this.radius = glm.Distance(new vec3(0, 0,0), position);
            // Определеям вертикальный угол
            // Это угол между вектором из начала координат к наблюдателю (v1)
            // И проекцией этого вектора на горизонтальную плоскость (v2)
            // Для определения угла используется скалярное произведение нормальзированных векторов 
            var v1 = position; 
            var v2 = new vec3(v1.x, 0, v1.z); 
            float cosY = glm.Dot(glm.Normalized(v1), glm.Normalized(v2)); 
            this.angleY = glm.Degrees(glm.Cos(cosY));
            
            // Аналогичным образом определяем горизонтальый угол:
            // Это угол между проекцией (v2) и единичным вектором вдоль оси Ox
            float cosX = glm.Dot(glm.Normalized(v2), new vec3(1, 0, 0)); 
            this.angleX = glm.Degrees(glm.Cos(cosX));
            
            // Пересчитываем позицию (для корректировок ошибок округления)
            recalculatePosition();
            this.position = position;
        }

        public vec3 getPosition() => this.position;

        public float getAngleX() => this.angleX;
        public float getAngleY() => this.angleY;
        public float getRadius() => this.radius;

        #region Функции для перемещения камеры
        /// <summary>
        /// Перемещение влево/вправо
        /// </summary>
        /// <param name="degree"></param>
        public void rotateLeftRight(float degree)
        {
            angleX += degree;
            recalculatePosition();
        }

        /// <summary>
        /// Перемещение вверх/вниз
        /// </summary>
        /// <param name="degree"></param>
        public void rotateUpDown(float degree)
        {
            angleY = norm_value(angleY + degree);
            recalculatePosition();
        }
        
        /// <summary>
        /// Приближение/отдаление
        /// </summary>
        /// <param name="degree"></param>
        public void ZoomInOut(float degree)
        {
            radius = norm_value(radius + degree);
            recalculatePosition();
        }
        #endregion

        /// <summary>
        /// Функция для установки матрицы камеры
        /// </summary>
        public void apply()
        {
            Glu.gluLookAt(position.x, position.y, position.z, target.x, target.y, target.z, ups.x, ups.y, ups.z);
        }

        /// <summary>
        /// Пересчет позиции камеры после поворотов
        /// </summary>
        private void recalculatePosition()
        {
            eyeX = radius * glm.Cos(glm.Radians(angleX)) * glm.Sin(glm.Radians(angleY)) + target.x;
            eyeZ = radius * glm.Sin(glm.Radians(angleX)) * glm.Sin(glm.Radians(angleY)) + target.y;
            eyeY = radius * glm.Cos(glm.Radians(angleY)) + target.z;
            position = new vec3(eyeX, eyeY, eyeZ);
        }

        private float norm_value(float value)
        {
            if (value < MIN_ANGLE_VALUE) return MIN_ANGLE_VALUE;
            if (value > MAX_ANGLE_VALUE) return MAX_ANGLE_VALUE;
            return value;
        }

    }
}