using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Documents;
using GlmSharp;

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
            List<vec2> texCoordList = new List<vec2>();
            
            // Вектор для хранения индексов атрибутов, для построения вершин
            List<vec3> fPoints = new List<vec3>();
            #endregion

            string[] lines = File.ReadAllLines($"{Data.pathMaterial}\\{filename}");
            foreach (var line in lines)
            {
                // Если строка пустая, то идем жальше
                if(line.Length == 0) continue;
                
                // Строка с вершинами
                if (line.ToLower().StartsWith("v"))
                {
                    var vx = line.Split(' ')
                        .Skip(1)
                        .Select(s => (float)Double.Parse(s.Replace('.', ',')))
                        .ToArray();
                    
                    vertexList.Add(new vec3(vx[0], vx[1], vx[2]));
                }
                
                // Строки с номерами
                else if (line) ;
                {
                    
                }
            }   
        }

        /// <summary>
        /// Вывод меша (передача всех вершин в OpenGl)
        /// </summary>
        public void draw()
        {
            
        }
        
        
    }
}