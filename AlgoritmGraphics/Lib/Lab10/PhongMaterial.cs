using GlmSharp;
using Lib.Enum;
using Tao.OpenGl;

namespace Lib.Lab10
{
    // КЛАС ДЛЯ РАБОТЫ С МАТЕРИАЛОМ
    public class PhongMaterial : Material
    {
        public PhongMaterial(Material material) : base()
        {
            this.TextureName = material.TextureName;
            this.Diffuse = material.Diffuse;
            this.Ambient = material.Ambient;
            this.Specular = material.Specular;
            this.Emission = material.Emission;
            this.Shininess = material.Shininess;
        }

        /// <summary>
        /// Установка всех параметров материала
        /// </summary>
        public override void apply()
        {
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT, glm.Values(getVec4(Ambient)));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, glm.Values(getVec4(Diffuse)));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, glm.Values(getVec4(Specular)));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_EMISSION, glm.Values(getVec4(Emission)));
            Gl.glMaterialf(Gl.GL_FRONT_AND_BACK, Gl.GL_SHININESS, Shininess);
        }
        
        private vec4 getVec4(float[] array)
        {
            return new vec4(array);
        }
    }
}