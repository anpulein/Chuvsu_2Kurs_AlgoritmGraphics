using Lib.Lab9;

namespace Lib.Lab10
{
    public class Material
    {
        #region Поля
        // Наименование текстуры
        private string textureName;
        // Диффузная состовляющая
        private float[] diffuse;
        // Фоновая состовляющая
        private float[] ambient;
        // Зеркальная состовляющая
        private float[] specular;
        // Самосвечение
        private float[] emission;
        // Степень отполированности
        private float shininess;
        #endregion

        // Задание параметров материала
        public string TextureName
        {
            get => textureName;
            set => textureName = value;
        }
        public float[] Diffuse
        {
            get => diffuse;
            set => diffuse = value;
        }
        public float[] Ambient
        {
            get => ambient;
            set => ambient = value;
        }
        public float[] Specular
        {
            get => specular;
            set => specular = value;
        }
        public float[] Emission
        {
            get => emission;
            set => emission = value;
        }
        public float Shininess
        {
            get => shininess;
            set => shininess = value;
        }

        /// <summary>
        /// Установка всех параметров материала
        /// </summary>
        public virtual void apply()
        {

        }
    }
}