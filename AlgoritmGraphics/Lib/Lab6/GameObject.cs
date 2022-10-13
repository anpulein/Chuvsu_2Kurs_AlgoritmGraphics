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
        private ivec2 postiton;

        // Графический объект (Для вывода на экран)
        private GraphicObject graphicObject;
        #endregion

        /// <summary>
        /// Установка используемого графического объекта
        /// Происходит копирование переданного объекта для последующего использования
        /// </summary>
        /// <param name="graphicObject"></param>
        public void setGraphicObject(GraphicObject graphicObject)
        {
            
        }

        /// <summary>
        /// Установка логических координат (два ререгруженные метода для удобства)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setPosition(int x, int y)
        {
            
        }
        
        public void setPosition(ivec2 position)
        {
            
        }

        /// <summary>
        /// Получение текущих логических координат
        /// </summary>
        /// <returns></returns>
        public ivec2 getPosition() => this.postiton;

        /// <summary>
        /// Вывод игрового объекта на экран
        /// </summary>
        public void draw()
        {
            
        }
    }
}