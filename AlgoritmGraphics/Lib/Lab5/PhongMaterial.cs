using System;
using System.IO;
using GlmSharp;
using Newtonsoft.Json;
using Tao.OpenGl;

namespace Lib.Lab5
{
    public class PhongMaterial
    {
        #region Поля
        
        // Фоновая состовляющая
        private vec4 ambient;
        
        // Диффузная состовляющая 
        private vec4 diffuse;
        
        // Зеркальная состовляющая
        private vec4 specular;
        
        // Самосвечение
        private vec4 emission;
        
        // Степень отполированности
        private float shininess;

        #endregion

        public PhongMaterial() {}

        #region Задание различных параметров материала
        public void setAmbient(vec4 color) => this.ambient = color;
        public void setDiffuse(vec4 color) => this.diffuse = color;
        public void setSpecular(vec4 color) => this.specular = color;
        public void setEmmission(vec4 color) => this.emission = color;
        public void setShininess(float p) => this.shininess = p;
        #endregion

        /// <summary>
        /// Загрузка параметров материала из внешнего текстового файла
        /// </summary>
        /// <param name="filename"></param>
        public void load(string filename)
        {
            using (StreamReader r = new StreamReader($"{Data.pathMaterial}\\{filename}"))
            {
                string json = r.ReadToEnd();
                Material item = JsonConvert.DeserializeObject<Material>(json);
                
                this.setAmbient(new vec4(item.ambient));
                this.setDiffuse(new vec4(item.diffuse));
                this.setSpecular(new vec4(item.specular));
                this.setEmmission(new vec4(item.emission));
                this.setShininess(item.shininess);
            }
        }

        /// <summary>
        /// Установка всех параметров материала
        /// </summary>
        public void apply()
        {
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT, glm.Values(this.ambient));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, glm.Values(this.diffuse));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, glm.Values(this.specular));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_EMISSION, glm.Values(this.emission));
            Gl.glMaterialf(Gl.GL_FRONT_AND_BACK, Gl.GL_SHININESS, this.shininess);
        }
        
        private class Material
        {
            public float[] ambient { get; set; }
            public float[] diffuse { get; set; }
            public float[] specular { get; set; }
            public float[] emission { get; set; }
            public float shininess { get; set; }

        }

    }
}