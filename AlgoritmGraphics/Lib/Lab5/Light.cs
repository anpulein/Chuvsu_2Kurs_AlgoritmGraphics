using System;
using System.IO;
using GlmSharp;
using Newtonsoft.Json;
using Tao.OpenGl;

namespace Lib.Lab5
{
    public class Light
    {
        #region Поля
        // Позиция источника света
        private vec4 position;
        
        // Фонова состовляющая источника света
        private vec4 ambient;
        
        // Диффузная состовляющая 
        private vec4 diffuse;
        
        // Зеркальная состовляющая
        private vec4 specular;
        #endregion

        public Light()
        {
            setPostion(new vec4(15.0f, 15.0f, 7.5f, 0.0f));
            setAmbient(new vec4(0.25f, 0.25f, 0.25f, 1.0f));
            setDiffuse(new vec4(0.5f, 0.5f, 0.5f, 1.0f));
            setSpecular(new vec4(0.5f, 0.5f, 0.5f, 1.0f));
        }

        public Light(vec4 position)
        {
            setPostion(position);
            setAmbient(new vec4(1.0f, 1.0f, 1.0f, 1.0f));
            setDiffuse(new vec4(0.0f, 0.0f, 0.0f, 1.0f));
            setSpecular(new vec4(1.0f, 1.0f, 1.0f, 1.0f));
        }
        public Light(float x, float y, float z) {}

        #region Задание различных параметров источников света
        public void setPostion(vec4 position) => this.position = position;
        public void setAmbient(vec4 color) => this.ambient = color;
        public void setDiffuse(vec4 color) => this.diffuse = color;
        public void setSpecular(vec4 color) => this.specular = color;
        #endregion

        /// <summary>
        /// Установка всех параметров источника света с заданным номером
        /// Данная функция должна вызываться после установки камеры,
        /// Т.к. здесь устанавливается позиция источника света
        /// </summary>
        /// <param name="LightNumber"></param>
        public void apply(int LightNumber = Gl.GL_LIGHT0)
        {
            Gl.glLightfv(LightNumber, Gl.GL_POSITION, glm.Values(this.position));
            Gl.glLightfv(LightNumber, Gl.GL_AMBIENT, glm.Values(this.ambient));
            Gl.glLightfv(LightNumber, Gl.GL_DIFFUSE, glm.Values(this.diffuse));
            Gl.glLightfv(LightNumber, Gl.GL_SPECULAR, glm.Values(this.specular));
            Gl.glEnable(LightNumber);
        }

        public void load(string path)
        {
            using (StreamReader reader = new StreamReader($"{path}"))
            {
                string json = reader.ReadToEnd();
                Root item = JsonConvert.DeserializeObject<Root>(json);

                if (item != null)
                {
                    setPostion(getVec4(item.position));
                    setAmbient(getVec4(item.ambient));
                    setDiffuse(getVec4(item.diffuse));
                    setSpecular(getVec4(item.specular));
                }
            }
            
        }
        
        private vec4 getVec4(float[] array)
        {
            return new vec4(array);
        }
        
        public class Root
        {
            public float[] position { get; set; }
            public float[] ambient { get; set; }
            public float[] diffuse { get; set; }
            public float[] specular { get; set; }
        }
    }
}