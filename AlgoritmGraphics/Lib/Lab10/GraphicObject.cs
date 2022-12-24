using GlmSharp;
using Lib.Lab7;
using Tao.OpenGl;

namespace Lib.Lab10
{
    // КЛАСС ДЛЯ ПРЕДСТАВЛЕНИЯ ОДНОГО ГРАФИЧЕСКОГО ОБЪЕКТА
    public class GraphicObject
    {
        #region Поля
        // Позиция объекта в глобальной системе координат
        private vec3 position;
        // Угод поворота  в горизонтальной плоскости (в градусах)
        private float angle;
        // Цвет модели
        private vec3 color;
        // Матрица модели (расположение объекта) - чтоб не вычислять каждый раз
        private mat4 modelMatrix; // size - 16
        // Иcпользуемый материал
        private Material material;
        // Используемый меш
        private Mesh mesh;

        private vec3 size;
        #endregion

        // Установка и получение 
        

        public GraphicObject()
        {
            this.modelMatrix = GlmSharp.mat4.Identity;
            this.material = null;
            this.mesh = null;
        }
        
        public GraphicObject(vec3 position, Mesh mesh, Material material)
        {
            this.modelMatrix = GlmSharp.mat4.Identity;
            this.position = position;
            this.mesh = mesh;
            this.material = material;
        }

        /// <summary>
        /// Установка позиции объекта
        /// </summary>
        /// <param name="position"></param>
        public new void setPosition(vec3 position)
        {
            this.position = position;
            modelMatrix = recalculateModelMatrix(position);
            // modelMatrix = translate(modelMatrix, position);
        }
        
        /// <summary>
        /// Получение позиции объекта
        /// </summary>
        /// <returns></returns>
        public vec3 getPosition() => this.position;

        /// <summary>
        /// Установка угла поворота
        /// </summary>
        /// <param name="grad"></param>
        public new void setAngle(float grad)
        {
            this.angle = grad;
            this.modelMatrix = glm.Rotated(quat.FromMat4(modelMatrix), glm.Radians(this.angle), new vec3(0.0f, 1.0f, 0.0f)).ToMat4;
        }


        public vec3 getSize() => this.size;

        public void setSize(vec3 value)
        {
            this.size = value.x <= 0 || value.y <= 0 || value.z <= 0 ? new vec3(1.0f, 1.0f, 1.0f) : value;
            modelMatrix = scale(modelMatrix, size);
        }
        
        /// <summary>
        /// Установка используемого материала
        /// </summary>
        /// <param name="material"></param>
        public void setMaterial(Material material) => this.material = material;

        /// <summary>
        /// Установка используемого меша
        /// </summary>
        /// <param name="mesh"></param>
        public void setMesh(Mesh mesh) => this.mesh = mesh;

        /// <summary>
        /// Вывод объкта на экран
        /// </summary>
        public void draw()
        {
            material?.apply();
            Gl.glPushMatrix();
            Gl.glMultMatrixf(modelMatrix.Values1D);
            mesh?.draw();
            Gl.glPopMatrix();
        }


        //// <summary>
        /// Applies a translation transformation to matrix <paramref name="m" /> by vector <paramref name="v" />.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to translate by.</param>
        /// <returns><paramref name="m" /> translated by <paramref name="v" />.</returns>
        public static mat4 translate(mat4 m, vec3 v)
        {
            mat4 mat4 = m;
            mat4[3] = m[0] * v[0] + m[1] * v[1] + m[2] * v[2] + m[3];
            return mat4;
        }
        
        
        /// <summary>
        /// Applies a scale transformation to matrix <paramref name="m" /> by vector <paramref name="v" />.
        /// </summary>
        /// <param name="m">The matrix to transform.</param>
        /// <param name="v">The vector to scale by.</param>
        /// <returns><paramref name="m" /> scaled by <paramref name="v" />.</returns>
        public static mat4 scale(mat4 m, vec3 v)
        {
            mat4 mat4 = m;
            mat4[0] = m[0] * v[0];
            mat4[1] = m[1] * v[1];
            mat4[2] = m[2] * v[2];
            mat4[3] = m[3];
            return mat4;
        }

        private static mat4 recalculateModelMatrix(vec3 position)
        {
            mat4 mat4 = mat4.Identity;
            mat4.m30 = position.x;
            mat4.m31 = position.z;
            mat4.m32 = position.y;

            return mat4;
        }

        public override string ToString()
        {
            return $"GraphicObject ({this.position.ToString()})";
        }
    }
}