using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstroidsRemixed.GameModels
{
    public class ExhaustParticle
    {
        private GraphicsPath _model = new GraphicsPath();
        static Random _rnd = new Random();
        static Color[] flameColors = new Color[7] { Color.FromArgb(244, 83, 66),
                                                    Color.FromArgb(244, 143, 65),
                                                    Color.FromArgb(40, 39, 38),
                                                    Color.FromArgb(71, 70, 69),
                                                    Color.Red,
                                                    Color.Red,
                                                    Color.Red
                                                   };

        public PointF location = new PointF();
        public bool KILLMENOW = false;
        public Color myColor = flameColors[_rnd.Next(0, 7)];

        private float moveX;
        private float moveY;
        private float fadeaway = 5;

        public float _currentRot = 0;
        public float _rotation = -3 + _rnd.Next(0, 6);

        public ExhaustParticle(PlayerInfo pi)
        {
            location = pi._pos;

            moveX = (float)Math.Sin(((pi._currentRot + (-10 + _rnd.Next(0, 21))) * Math.PI) / 180) * 5;
            moveY = -1 * (float)Math.Cos(((pi._currentRot + (-10 + _rnd.Next(0, 21))) * Math.PI) / 180) * 5;

            _model.AddLines(CreateModel());
        }

        private PointF[] CreateModel()
        {
            PointF[] points = new PointF[4];

            points[0] = new PointF(1, -1);
            points[1] = new PointF(-1, -1);
            points[2] = new PointF(-1, 1);
            points[3] = new PointF(1, 1);

            return points;
        }

        public GraphicsPath GetPath()
        {
            GraphicsPath gpClone = (GraphicsPath)_model.Clone();

            Matrix cloneMatrix = new Matrix();

            cloneMatrix.Translate(location.X, location.Y);
            cloneMatrix.Rotate(_currentRot);
            cloneMatrix.Scale(fadeaway, fadeaway);

            gpClone.Transform(cloneMatrix);

            return gpClone;
        }

        public void Tick()
        {
            location.X += moveX;
            location.Y += moveY;

            if (fadeaway > 0)
                fadeaway -= 0.1f;
            else
                KILLMENOW = true;

            _currentRot += _rotation;
        }
    }
}
