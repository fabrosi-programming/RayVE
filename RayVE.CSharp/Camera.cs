using System;
using System.Collections.Generic;
using System.Text;

namespace RayVE.CSharp
{
    public class Camera
    {
        private ViewTransformation _viewTransformation;

        public Camera(ViewTransformation viewTransformation)
        {
            _viewTransformation = viewTransformation;
        }
    }
}