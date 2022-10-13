// Для работы с библиотекой OpenGL

using System.Collections.Generic;
using Tao.OpenGl;
// Для работы с библиотекой FreeGlut
using Tao.FreeGlut;
// Для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;
// Для работы со специальными типами данных в трехмерной графики
using GlmSharp;

namespace Lib.Lab4
{
    public class Data
    {
        private List<GraphicObject> _graphicObjects;
        private Camera _camera;
    }
}