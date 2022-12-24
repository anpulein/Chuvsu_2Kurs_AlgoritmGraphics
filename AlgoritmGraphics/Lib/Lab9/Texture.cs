// using DevILSharp;

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Lib.Enum;
using Tao.DevIl;
using Tao.OpenGl;

namespace Lib.Lab9
{
    // КЛАСС ДЛЯ РАБОТЫ С ТЕКСТУРОЙ
    public class Texture
    {
        [DllImport("user32.dll")]
        static extern int wsprintf([Out] StringBuilder lpOut, string lpFmt, __arglist);
        
        #region Поля
        // Идентификатор (индекс) текстурного объекта
        private int texIndex;
        #endregion

        /// <summary>
        /// Загрузка текстуры из внешнего файла
        /// </summary>
        /// <param name="filename"></param>
        public void load(string filename)
        {
            // Создаем новое изображение
            int imageId = Il.ilGenImage();
            
            // Задаем текущее изображение
            Il.ilBindImage(imageId);
            var loaded = Il.ilLoadImage(filename);
            
            if (loaded)
            {
                Il.ilConvertImage(Il.IL_RGBA, Il.IL_UNSIGNED_BYTE);
                // Получение параметров загруженной текстуры
                int weight = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int format = Il.ilGetInteger(Il.IL_IMAGE_FORMAT);
                int type = Il.ilGetInteger(Il.IL_IMAGE_TYPE);

                
                WriteableBitmap bitmap = new WriteableBitmap(weight, height, 96, 96, PixelFormats.Bgra32, null);
                bitmap.Lock();
                Il.ilCopyPixels(0, 0, 0, weight, height, 1, format, type, bitmap.BackBuffer);
                
                
                // Определяем модель памяти (параметры распаковки)
                Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
                // Выбираем активный текстурный блок
                Gl.glActiveTexture(Gl.GL_TEXTURE0);
                // Создание текстурного объекта
                Gl.glGenTextures(1, out texIndex);
                // Привязка текстурного объекта к тестурному блоку
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texIndex);
                // Загрузка изображения из оперативной памяти в текстурный объект
                Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, 4, weight, height, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, bitmap.BackBuffer);
                bitmap.Unlock();
                
                // Массив текселей в оперативной памяти больше не нужен и может быть удален
                // Генерация mipmap'ов
                Gl.glGenerateMipmapEXT(Gl.GL_TEXTURE_2D);
                
                // Установка параметров текстуры
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP);
                
                // Удаляем "Изображение"
                Il.ilDeleteImage(imageId);
            }
            else
            {
                Console.WriteLine($"Error, не найден {filename}");
            }
        }

        /// <summary>
        /// Применение текстуры (привязка к текстурному блоку и установка праметров)
        /// </summary>
        /// <param name="textureFilter"></param>
        public void apply(TextureFilter textureFilter = TextureFilter.ANISOTROPIC)
        {
            // Привязать текстурный объект к тектсурному блоку
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texIndex);
            
            // Установить параметр текстурного блока
            switch (textureFilter)
            {
                case TextureFilter.POINT:
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
                    break;
                case TextureFilter.BILINEAR:
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_NEAREST);
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
                    break;
                case TextureFilter.TRIPLINEAR:
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_NEAREST);
                    Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
                    break;
                case TextureFilter.ANISOTROPIC:
                    break;
                default:
                    break;
            }
            // Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);
        }

        /// <summary>
        /// Отключение текстурирования для всех текстурных объектов
        /// </summary>
        public static void disableAll()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
    }
}