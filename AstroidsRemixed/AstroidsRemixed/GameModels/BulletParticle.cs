using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstroidsRemixed.GameModels
{
    public class BulletParticle
    {
        public static Color myColor = Color.Green;
        static Random _rnd = new Random();

        private GraphicsPath _model = new GraphicsPath();

        private float moveX;
        private float moveY;

        public PointF location = new PointF();
        public float _currentRot = 0;
        public bool KILLMENOW = false;

        public BulletParticle(PlayerInfo pi)
        {
            location = pi._pos;
            _currentRot = pi._currentRot + 90;

            moveX = -(float)Math.Sin(((pi._currentRot + (-2 + _rnd.Next(0, 5))) * Math.PI) / 180) * 20;
            moveY = (float)Math.Cos(((pi._currentRot + (-2 + _rnd.Next(0, 5))) * Math.PI) / 180) * 20;

            _model.AddLines(CreateModel());
        }

        public BulletParticle(EnemyOne pi)
        {
            location = pi._pos;
            _currentRot = pi._currentRot + 90;

            moveX = -(float)Math.Sin(((pi._currentRot + (-2 + _rnd.Next(0, 5))) * Math.PI) / 180) * 20;
            moveY = (float)Math.Cos(((pi._currentRot + (-2 + _rnd.Next(0, 5))) * Math.PI) / 180) * 20;

            _model.AddLines(CreateModel());
        }

        public BulletParticle(EnemyTwo pi, int offset)
        {
            location = pi._pos;
            _currentRot = pi._currentRot + offset + 90;

            moveX = -(float)Math.Sin(((offset * Math.PI) / 180)) * 10;
            moveY = (float)Math.Cos(((offset * Math.PI) / 180)) * 10;

            _model.AddLines(CreateModel());
        }

        static PointF[] CreateModel()
        {
            PointF[] points = new PointF[4];

            points[0] = new PointF(2, -0.5f);
            points[1] = new PointF(-2, -0.5f);
            points[2] = new PointF(-2, 0.5f);
            points[3] = new PointF(2, 0.5f);

            return points;
        }

        public GraphicsPath GetPath()
        {
            GraphicsPath gpClone = (GraphicsPath)_model.Clone();

            Matrix cloneMatrix = new Matrix();

            cloneMatrix.Translate(location.X, location.Y);
            cloneMatrix.Rotate(_currentRot);
            cloneMatrix.Scale(3, 3);

            gpClone.Transform(cloneMatrix);

            return gpClone;
        }

        public void Tick()
        {
            KILLMENOW = (location.X > 4000 || location.X < 0 || location.Y > 4000 || location.Y < 0);

            location.X += moveX;
            location.Y += moveY;
        }
    }
}
