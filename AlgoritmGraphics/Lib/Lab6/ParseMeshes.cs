using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace Lib.Lab6
{
    public class ParseMeshes
    {
        #region Поля
        // Меши
        public Mesh box;
        public Mesh ChamferBox;
        public Mesh SimplePlane;
        public Mesh Sphere;

        // Файлы
        private string fileBox = "Box.obj";
        private string fileChamferBox = "ChamferBox.obj";
        private string fileSimplePlane = "SimplePlane.obj";
        private string fileSphere = "Sphere.obj";
        #endregion

        /// <summary>
        /// Определение всех объектов полей
        /// </summary>
        public ParseMeshes()
        {
            box = Parse(new Mesh(), fileBox);
            ChamferBox = Parse(new Mesh(), fileChamferBox);
            SimplePlane = Parse(new Mesh(), fileSimplePlane);
            Sphere = Parse(new Mesh(), fileSphere);
        }

        public Mesh getIndexMesh(int index)
        {
            switch (index)
            {
                case 0: return SimplePlane; 
                case 1: return box; 
                case 2: return ChamferBox; 
                case 3: return box; 
            }

            return null;
        }

        /// <summary>
        /// Запускает парсинг информации из файла в объект и возвращает его
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private Mesh Parse(Mesh mesh, string file)
        {
            mesh.load(file);
            return mesh;
        }
    }
}