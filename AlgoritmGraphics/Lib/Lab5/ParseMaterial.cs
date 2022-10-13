using System.Windows.Media.Media3D;

namespace Lib.Lab5
{
    public class ParseMaterial
    {
        #region Поля
        // Материалы
        public PhongMaterial material1;
        public PhongMaterial material2;
        public PhongMaterial material3;
        public PhongMaterial material4;
        
        // Файлы
        private string file1 = "material_1.json";
        private string file2 = "material_2.json";
        private string file3 = "material_3.json";
        private string file4 = "material_4.json";
        #endregion

        public ParseMaterial()
        {
            material1 = Parse(new PhongMaterial(), file1);
            material2 = Parse(new PhongMaterial(), file2);
            material3 = Parse(new PhongMaterial(), file3);
            material4 = Parse(new PhongMaterial(), file4);
        }

        private PhongMaterial Parse(PhongMaterial material, string file)
        {
            material.load(file);
            return material;
        }
        
    }
}