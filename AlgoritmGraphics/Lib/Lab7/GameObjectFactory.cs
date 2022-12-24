using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Media3D;
using GlmSharp;
using Lib.Lab8;
using Newtonsoft.Json;
using Json = Newtonsoft.Json;
using GameObjectType = Lib.Enum.GameObjectType;
using Objects = Lib.Enum.Objects;

namespace Lib.Lab7
{
    /// <summary>
    /// Класс для создания игровых объектов
    /// </summary>
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

        // Инициализация фабрики (Загрузка)
        public void Init(string path)
        {
            _mapMeshe = new Dictionary<GameObjectType, Mesh>();
            _mapMaterial = new Dictionary<GameObjectType, Material>();

            using (StreamReader reader = new StreamReader($"{path}"))
            {
                string json = reader.ReadToEnd();
                Root item = JsonConvert.DeserializeObject<Root>(json);

                if (item != null)
                {
                    foreach (var gameObject in item?.gameObjectDescription)
                    {
                        _mapMaterial.Add(Objects.GetGameObjectType(gameObject.type), gameObject.material);
                        _mapMeshe.Add(Objects.GetGameObjectType(gameObject.type), new Mesh(gameObject.mesh));
                    }
                }
            }
        }

        public GameObject Creat(GameObjectType type, int x, int y)
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
        
        public GameObject8 Creat8(GameObjectType type, int x, int y)
        {

            var mesh = _mapMeshe[type];
            var material = _mapMaterial[type];

            GraphicObject graphicObject = new GraphicObject();
            graphicObject.setMesh(mesh);
            graphicObject.setMaterial(material);

            var gameObject = new GameObject8();
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