using System.Data;
using GlmSharp;

namespace Lib.Lab6
{
    /// <summary>
    /// Клас для представления игрового объекта
    /// </summary>
    public class GameObject
    {

        #region Поля
        // Логические координаты игрового объекта
        private vec3 postiton;

        // Графический объект (Для вывода на экран)
        private GraphicObject graphicObject;
        #endregion


        /// <summary>
        /// Установка используемого графического объекта
        /// Происходит копирование переданного объекта для последующего использования
        /// </summary>
        /// <param name="graphicObject"></param>
        public void setGraphicObject(GraphicObject graphicObject) => this.graphicObject = graphicObject;

        /// <summary>
        /// Установка логических координат (два ререгруженные метода для удобства)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setPosition(int x, int y)
        {
            this.postiton = new vec3(x, y, 0.0f);
            if (graphicObject != null) graphicObject.setPosition(this.postiton);
        }
        
        public void setPosition(int x, int y, int z)
        {
            this.postiton = new vec3(x, y, z);
            if (graphicObject != null) graphicObject.setPosition(this.postiton);
        }
        
        public void setPosition(float x, float y, float z)
        {
            this.postiton = new vec3(x, y, z);
            if (graphicObject != null) graphicObject.setPosition(this.postiton);
        }
        
        public void setPosition(vec2 position)
        {
            this.postiton = new vec3(position.x, position.y, 0.0f);
            if (graphicObject != null) graphicObject.setPosition(this.postiton);
        }
        
        public void setPosition(vec3 position)
        {
            this.postiton = position;
            if (graphicObject != null) graphicObject.setPosition(this.postiton);
        }

        /// <summary>
        /// Получение текущих логических координат
        /// </summary>
        /// <returns></returns>
        public vec3 getPosition() => this.postiton;

        public vec3 getGraphicPosition() => this.graphicObject.getPosition();

        /// <summary>
        /// Вывод игрового объекта на экран
        /// </summary>
        public void draw()
        {
            this.graphicObject?.draw();
        }
    }
}