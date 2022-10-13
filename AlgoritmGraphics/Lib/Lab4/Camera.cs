
using GlmSharp;
using glm = GlmSharp.glm;

namespace Lab4.Other
{
    public class Camera
    {

        #region Поля

        // Радиус и углы поворота
        private float radius;
        private float angleX;
        private float angleY;
        
        // Позиция камеры
        private vec3 position;
        #endregion
        
        public Camera()
        {
        }

        public Camera(vec3 position)
        {
            this.position = position;
        }
        
        public Camera(float x, float y, float z)
        {
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

        #region Функции для перемещения камеры
        /// <summary>
        /// Перемещение влево/вправо
        /// </summary>
        /// <param name="degree"></param>
        public void rotateLeftRight(float degree)
        {
            
        }
        
        /// <summary>
        /// Перемещение вверж/вниз
        /// </summary>
        /// <param name="degree"></param>
        public void rotateUpDown(float degree)
        {
            
        }
        
        /// <summary>
        /// Приближение/отдаление
        /// </summary>
        /// <param name="degree"></param>
        public void ZoomInOut(float degree)
        {
            
        }
        #endregion

        /// <summary>
        /// Функция для установки матрицы камеры
        /// </summary>
        public void apply()
        {
            
        }

        /// <summary>
        /// Пересчет позиции камеры после поворотов
        /// </summary>
        private void recalculatePosition()
        {
            
        }

    }
}