using GlmSharp;
using Tao.OpenGl;

namespace Lib.Lab7
{
    public class Material
    {
        public float[] ambient { get; set; }
        public float[] diffuse { get; set; }
        public float[] specular { get; set; }
        public float[] emission { get; set; }
        public float shininess { get; set; }


        private vec4 getVec4(float[] array)
        {
            return new vec4(array);
        }
        
        public void apply()
        {
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT, glm.Values(getVec4(this.ambient)));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, glm.Values(getVec4(this.diffuse)));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, glm.Values(getVec4(this.specular)));
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_EMISSION, glm.Values(getVec4(this.emission)));
            Gl.glMaterialf(Gl.GL_FRONT_AND_BACK, Gl.GL_SHININESS, this.shininess);
        }
    }
}