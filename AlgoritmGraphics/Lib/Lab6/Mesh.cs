using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Forms;
using GlmSharp;
using Tao.FreeGlut;
using Tao.OpenGl;
using glm = GlmNet.glm;

namespace Lib.Lab6
{
    /// <summary>
    /// Работа с Мешем
    /// Загружает массив вершин из внешнего obj - файла
    /// Выводит полигоны на экран 
    /// </summary>
    public class Mesh
    {

        #region Поля
        // Массив вершин полигональной сетки
        private List<Vertex> _vertices;
        #endregion

        public Mesh()
        {
            _vertices = new List<Vertex>();
        }

        /// <summary>
        /// Загрузка меша из файла с указанным именем
        /// </summary>
        /// <param name="filename"></param>
        public void load(string filename)
        {
            #region Локальные поля
            // Вектор для хранения геометрических координат
            List<vec3> vertexList = new List<vec3>();
            
            // Вектор для хранения нормалей
            List<vec3> normalList = new List<vec3>();
            
            // Вектор для хранения текстурных коодинат
            List<vec2> textCoordList = new List<vec2>();
            
            // // Вектор для хранения индексов атрибутов, для построения вершин
            List<ivec3> fPoints = new List<ivec3>();
            #endregion

            string[] lines = File.ReadAllLines($"{Data.pathMesh}\\{filename}");
            foreach (var line in lines)
            {
                // Если строка пустая, то идем жальше
                if(line.Length == 0) continue;
                
                // Обработка строк
                if (line.ToLower().StartsWith("vn")) 
                {
                    normalList.Add(ParseNormal(line));
                }
                else if (line.ToLower().StartsWith("vt"))
                {
                    textCoordList.Add(ParseTextureCoords(line));
                }
                else if (line.ToLower().StartsWith("v"))
                {
                    vertexList.Add(ParseVertex(line));
                }
                else if (line.ToLower().StartsWith("f"))
                {
                    fPoints.AddRange(ParsePoints(line));
                }
            } 

            for (int i = 0; i < fPoints.Count; i++)
            {
                var one = vertexList[fPoints[i].x - 1];
                var two = textCoordList[fPoints[i].y - 1];
                var tree = normalList[fPoints[i].z - 1];
                
                _vertices.Add(new Vertex(one, two, tree));
            }
        }

        /// <summary>
        /// Вывод меша (передача всех вершин в OpenGl)
        /// </summary>
        public void draw()
        {
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);

            
            var stride = 0;

            var array = _vertices.ToArray();


            float[] vertex = new float[array.Length * 3];
            float[] textCoord = new float[array.Length * 2];
            float[] normal = new float[array.Length * 3];

            int v = 0;
            int t = 0;
            int n = 0;

            for (int i = 0; i < _vertices.Count; i++)
            {
                vertex[v++] = _vertices[i].coord[0];
                vertex[v++] = _vertices[i].coord[1];
                vertex[v++] = _vertices[i].coord[2];
                    
                textCoord[t++] = _vertices[i].textCoord[0];
                textCoord[t++] = _vertices[i].textCoord[1];
                    
                normal[n++] = _vertices[i].normal[0];
                normal[n++] = _vertices[i].normal[1];
                normal[n++] = _vertices[i].normal[2]; 
            }
                
            Gl.glVertexPointer(3, Gl.GL_FLOAT, stride, vertex);
            Gl.glTexCoordPointer(2, Gl.GL_FLOAT, stride, textCoord);
            Gl.glNormalPointer(Gl.GL_FLOAT, stride, normal);
            

            var k = array.Select(s => s.coord).Count();
            
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, k );
            
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
        }


        /// <summary>
        /// Обработка строки с вершинами
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private vec3 ParseVertex(string line)
        {
            var vx = line.Split(' ')
                .Skip(2)
                .Select(s => (float)Double.Parse(s))
                .ToArray();
            
            return new vec3(vx[0],vx[1],vx[2]);
        }
        
        /// <summary>
        /// Обработка строки с нормалью
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private vec3 ParseNormal(string line)
        {
            var vx = line.Split(' ')
                .Skip(1)
                .Select(s => (float)Double.Parse(s))
                .ToArray();
            
            return new vec3(vx[0], vx[1], vx[2]);
        }
        
        /// <summary>
        /// Обработка строки с текстурами
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private vec2 ParseTextureCoords(string line)
        {
            var vx = line.Split(' ')
                .Skip(1)
                .Take(2)
                .Select(s => (float)Double.Parse(s))
                .ToArray();

            return new vec2(vx[0], vx[1]);
        }
        
        /// <summary>
        /// Обработка строки атбрибутов для вершин
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private List<ivec3> ParsePoints(string line)
        {
            var lines = line
                .Split(' ')
                .Skip(1)
                .Take(3)
                .ToArray();
            var vertex = new List<ivec3>();

            foreach (var vr in lines)
            {
                var vx = vr
                    .Split('/')
                    .Select(s => Int32.Parse(s))
                    .ToArray();
                
                vertex.Add(new ivec3(vx[0], vx[1], vx[2]));
            }
            
            return vertex;
        }
    }
}