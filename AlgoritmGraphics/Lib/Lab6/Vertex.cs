using GlmSharp;

namespace Lib.Lab6
{
    /// <summary>
    /// Структура, описывающая одну вершину полиногальной сетки
    /// каждая вершина имеет неометрические координаты
    /// вектор нормали и текстурные координаты
    /// </summary>
    public struct Vertex
    {
        #region Поля
        // Геометрические координаты
        public vec3 coord;
        
        // Вектор нормали
        public vec3 normal;
        
        // Текстурные координаты нулевого текстурного блока
        public vec2 textCoord;
        #endregion

        public Vertex(vec3 coord, vec2 textCoord, vec3 normal)
        {
            this.coord = coord;
            this.textCoord = textCoord;
            this.normal = normal;
        }
    }
}