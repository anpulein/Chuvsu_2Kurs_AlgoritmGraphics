using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lib.Enum;
using Lib.Lab7;
using Lib.Lab8;
using Lib.Lab9;
using Microsoft.FSharp.Core;
using Newtonsoft.Json;

namespace Lib.Lab10
{
    // КЛАСС ДЛЯ СОЗДАНИЯ ИГРОВЫХ ОБЪЕКТОВ
    public class GameObjectFactory
    {
        #region Поля
        /// <summary>
        /// Меши для каждого типа объекта
        /// </summary>
        private Dictionary<GameObjectType, Mesh> _mapMeshe;
        /// <summary>
        /// Материалы для каждого типа объекта
        /// </summary>
        private Dictionary<GameObjectType, Material> _mapMaterial;
        #endregion


        public GameObjectFactory(string path)
        {
            Init(path);
        }

        /// <summary>
        /// Инициализация фабрики
        /// Загрузка мешей и установка параметров материала
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Init(string path)
        {
            _mapMeshe = new Dictionary<GameObjectType, Mesh>();
            _mapMaterial = new Dictionary<GameObjectType, Material>();

            using (StreamReader reader = new StreamReader($"{path}"))
            {
                string json = reader.ReadToEnd();
                Root item = JsonConvert.DeserializeObject<Root>(json);
                var array = new int[] { 4, 5, 6 };
                Material material;
                Texture texture;
                
                if (item != null)
                {
                    foreach (var gameObject in item?.gameObjectDescription)
                    {
                        var objectType = (int)Objects.GetGameObjectType(gameObject.type);
                        if (array.Contains(objectType))
                        {
                            material = new PhongMaterial(gameObject.material);
                            _mapMaterial.Add(Objects.GetGameObjectType(gameObject.type), material);
                        }
                        else
                        {
                            texture = new Texture();
                            texture.load(gameObject.material.TextureName);
                            material = new PhongMaterialWithTexture(gameObject.material, texture);
                            _mapMaterial.Add(Objects.GetGameObjectType(gameObject.type), material);
                        }
                        _mapMeshe.Add(Objects.GetGameObjectType(gameObject.type), new Mesh(gameObject.mesh));
                    }

                    return true;
                }
            }

            return false;
        }
        

        /// <summary>
        /// Создание нового объекта заданного типа
        /// </summary>
        /// <param name="type"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GameObject Create(GameObjectType type, int x, int y)
        {
            var mesh = _mapMeshe[type];
            var material = _mapMaterial[type];

            GraphicObject graphicObject = new GraphicObject();
            graphicObject.setMesh(mesh);
            graphicObject.setMaterial(material);

            var gameObject = new GameObject();
            gameObject.setGraphicObject(graphicObject);
            gameObject.setPosition(x, y);
            
            return gameObject;
        }
        
        
        private class GameObjectDescription
        {
            public string type { get; set; }
            public string mesh { get; set; }
            public Material material { get; set; }
        }
        
        private class Root
        {
            public GameObjectDescription[] gameObjectDescription;
        }
    }
}