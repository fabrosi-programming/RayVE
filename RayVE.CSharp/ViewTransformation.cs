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
                    new Vector(_left.X, _left.Y, _left.Z, 0.0),
                    new Vector(_trueUp.X, _trueUp.Y, _trueUp.Z, 0.0),
                    new Vector(-_forward.X, -_forward.Y, -_forward.Z, 0.0),
                    new Vector(0.0, 0.0, 0.0, 1.0)
                })
            * Matrix.Translation(-new Vector(_position.X, _position.Y, _position.Z));

        public ViewTransformation(Point3D position, Point3D target, Vector3D up)
        {
            _position = position;
            _target = target;
            _up = up.Normalize();
            _forward = (_target - _position).Normalize();
            _left = _forward.Cross(_up);
            _trueUp = _left.Cross(_forward);
        }
    }
}
