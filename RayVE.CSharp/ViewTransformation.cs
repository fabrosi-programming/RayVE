using RayVE.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayVE.CSharp
{
    public class ViewTransformation
    {
        private readonly Point3D _position;
        private readonly Point3D _target;
        private readonly Vector3D _up;
        private readonly Vector3D _forward;
        private readonly Vector3D _left;
        private readonly Vector3D _trueUp;

        public Matrix Matrix
            => new Matrix(new[]
                {
                    _left,
                    _trueUp,
                    -_forward,
                    new Vector(0.0, 0.0, 0.0, 1.0)
                })
            * Matrix.Translation(-_position);

        public ViewTransformation(Point3D position, Point3D target, Vector3D up)
        {
            _position = position;
            _target = target;
            _up = new Vector3D(up.Normalize());
            _forward = new Vector3D((_target - _position).Normalize());
            _left = _forward.Cross(_up);
            _trueUp = _left.Cross(_forward);
        }
    }
}
