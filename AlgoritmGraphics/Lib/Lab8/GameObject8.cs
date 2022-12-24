using System;
using System.Windows.Media;
using GlmSharp;
using Lib.Enum;
using Lib.Lab7;
using Lib.RenderProcessing;
using Tao.FreeGlut;

namespace Lib.Lab8
{
    /// <summary>
    /// Класс для представлени игрового объекта
    /// </summary>
    public class GameObject8 : GameObject
    {
        #region Поля

        // Состояние объекта (заданное направление перемещения)
        private MoveDirection sost;
        // Прогресс в перемещении (от 0.0 до 1.0)
        private float progress;
        // Скорость пермещения
        private float speed;
        // 
        private MoveDirection _moveState;
        private MoveDirection _prevState;
        #endregion

        public GameObject8() : base()
        {
            _moveState = MoveDirection.STOP;
            _prevState = MoveDirection.STOP;
            progress = 0.0f;
            speed = 0.25f;
        }

        /// <summary>
        /// Начать движения в выбранном направлении с указанной скоростью
        /// Скорость передвижения определяется кол-вом клеток в секунду
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        public void move(MoveDirection direction, float speed = 0.25f)
        {
            if (!isMoving())
            {
                _moveState = direction;
                this.speed = speed;
            }
        }

        /// <summary>
        /// Проверка на то, что объект в настоящее время движется
        /// </summary>
        /// <returns></returns>
        public bool isMoving() => _moveState != MoveDirection.STOP;


        public bool isMovingDone() => progress >= 1.0f;

        public void setPrevState(MoveDirection direction) => _prevState = direction;

        public MoveDirection getPrevDirection() => this._prevState;
        
        /// <summary>
        /// Симуляция игрового объекта (плавное перемещение объекта)
        /// Метод вызывается непрерывно в функции simulation
        /// </summary>
        /// <param name="sec"></param>
        public void simulate(float sec)
        {
            // 0.0100499997
            if (isMoving())
            {
                var position = graphicObject.getPosition();
                var distance = 0f;
                if (progress + (sec * speed) >= 1.0f)
                {
                    distance = 1.0f - progress;
                    progress = 1.0f;
                }
                else
                {
                    distance = speed * sec;
                    progress += distance;
                }
                switch (_moveState)
                {
                    case MoveDirection.UP:
                        graphicObject.setPosition(new vec3(position.x, position.y - distance, position.z));
                        break;
                    case MoveDirection.DOWN:
                        graphicObject.setPosition(new vec3(position.x, position.y + distance, position.z));
                        break;
                    case MoveDirection.LEFT:
                        graphicObject.setPosition(new vec3(position.x - distance, position.y, position.z));
                        break;
                    case MoveDirection.RIGHT:
                        graphicObject.setPosition(new vec3(position.x + distance, position.y, position.z));
                        break;
                }
                Console.WriteLine(distance);
                Console.WriteLine($"progress: {progress.ToString()}, position:{getPosition().x.ToString()}; {getPosition().y.ToString()}; {getPosition().z.ToString()}");
            }
            
            if (isMovingDone())
            {
                var newPosition = getPosition();
                switch (_moveState) {
                    case MoveDirection.UP:
                        setPosition(new vec3(newPosition.x, newPosition.y - 1, newPosition.z));
                        break;
                    case MoveDirection.DOWN:
                        setPosition(new vec3(newPosition.x, newPosition.y + 1, newPosition.z));
                        break;
                    case MoveDirection.LEFT:
                        setPosition(new vec3(newPosition.x - 1, newPosition.y, newPosition.z));
                        break;
                    case MoveDirection.RIGHT:
                        setPosition(new vec3(newPosition.x + 1, newPosition.y, newPosition.z));
                        break;
                }
                
                _moveState = MoveDirection.STOP;
                progress = 0.0f;
            }
        }
        
    }
}