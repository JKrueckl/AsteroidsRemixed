using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using InputInterfaceLib;

namespace AstroidsRemixed.GameModels
{
    public class PlayerInfo
    {   
        static readonly GraphicsPath _playerModel = new GraphicsPath();
        static readonly GraphicsPath _playerModelBoost = new GraphicsPath();

        const int _shootTimer = 8;
        int cycleNum = 0;

        public enum _shootingMode { Normal, TripleShot, ScatterShot }
        public _shootingMode _currentFireMode = _shootingMode.Normal;

        public List<ExhaustParticle> _particles = new List<ExhaustParticle>();
        public List<BulletParticle> _bullets = new List<BulletParticle>();

        float _rotation = 0f;
        float _vel = 0f;

        public PointF _pos = new PointF(500, 500);
        public float _currentRot = 180f;
        public bool _boosting = false;
        public bool _shooting = false;
        public bool _gotHit = false;
        public int _alpha = 0;

        static PlayerInfo()
        {
            _playerModel.AddLines(CreatePlayerModel());
            _playerModelBoost.AddLines(CreatePlayerModelBoost());
        }

        static PointF[] CreatePlayerModel()
        {
            PointF[] shapePoints = new PointF[4];

            float mainAngle = (float)(Math.PI * 2) / 3;

            // how far the middle point goes into the center 
            float shipTailAngleCenter = mainAngle / 2f;
            // how deep the point goes into the ship
            float shipTailAngleDepth = 0.4f;

            shapePoints[0] = new PointF(
                    (float)Math.Sin(mainAngle),
                    (float)Math.Cos(mainAngle)
            );

            shapePoints[1] = new PointF(
                    (float)Math.Sin(mainAngle * 2 - shipTailAngleCenter),
                    (float)Math.Cos(mainAngle * 2 + shipTailAngleDepth)
            );

            shapePoints[2] = new PointF(
                    (float)Math.Sin((mainAngle * 2)),
                    (float)Math.Cos((mainAngle * 2))
            );

            shapePoints[3] = new PointF(
                    (float)Math.Sin((mainAngle * 3)),
                    (float)Math.Cos((mainAngle * 3))
            );

            return shapePoints;
        }

        static PointF[] CreatePlayerModelBoost()
        {
            PointF[] shapePoints = new PointF[5];

            shapePoints[0] = new PointF(-1f, -1f);
            shapePoints[1] = new PointF(-0.5f, -0.75f);
            shapePoints[2] = new PointF(0f, -1f);
            shapePoints[3] = new PointF(0.5f, -0.75f);
            shapePoints[4] = new PointF(1f, -1f);

            return shapePoints;
        }

        public GraphicsPath GetPath()
        {
            GraphicsPath gpClone = (GraphicsPath)_playerModel.Clone();

            Matrix cloneMatrix = new Matrix();

            cloneMatrix.Translate(_pos.X, _pos.Y);
            cloneMatrix.Rotate(_currentRot);
            cloneMatrix.Scale(20,20);

            gpClone.Transform(cloneMatrix);

            return gpClone;
        }

        public GraphicsPath GetPathForLifeSymbol(float xCord, float yCord)
        {
            GraphicsPath gpClone = (GraphicsPath)_playerModel.Clone();

            Matrix cloneMatrix = new Matrix();

            cloneMatrix.Translate(xCord, yCord);
            cloneMatrix.Rotate(180);
            cloneMatrix.Scale(20, 20);

            gpClone.Transform(cloneMatrix);

            return gpClone;
        }

        public void ResetOnHit()
        {
            _rotation = 0f;
            _vel = 0f;

            _pos = new PointF(500, 500);
            _currentRot = 180f;
            _boosting = false;
            _shooting = false;
            _gotHit = false;
            _alpha = 0;
        }

        public void Tick(InputInterface ii, Rectangle screen)
        {
            _rotation = (ii.Left ? -4f : 0) + (ii.Right ? 4f : 0);

            _boosting = ii.Up;

            if (ii.Shoot && cycleNum == 0)
            {
                cycleNum++;
                _shooting = true;
                Console.WriteLine("shooting");
            }
            else if (ii.Shoot && cycleNum <= _shootTimer)
            {
                cycleNum++;
                _shooting = false;
                Console.WriteLine("trying to shoot");
            }           
            else if (cycleNum > _shootTimer || !ii.Shoot)
            {
                cycleNum = 0;
                _shooting = false;
                Console.WriteLine("Gave up / reset");
            }              

            if (ii.Up)
                _vel = 8f;
            else if (_vel > 0)
                _vel -= (_vel * 0.05f);

            if (_pos.X < screen.Width && _pos.X > 0)
                _pos.X += -(float)Math.Sin((_currentRot * Math.PI) / 180) * _vel;
            else if (_pos.X > screen.Width)
                _pos.X = _pos.X - 2;
            else
                _pos.X = _pos.X + 2;

            if (_pos.Y < screen.Height && _pos.Y > 0)
                _pos.Y += (float)Math.Cos((_currentRot * Math.PI) / 180) * _vel;
            else if (_pos.Y > screen.Height)
                _pos.Y = _pos.Y - 2;
            else
                _pos.Y = _pos.Y + 2;

            if (_currentRot >= 360 || _currentRot <= -360)
                _currentRot = 0;
            else
                _currentRot += _rotation;

            if (_alpha < 254)
            {
                _alpha += 2;
            }
            else if(_alpha == 254)
            {
                _alpha++;
            }        
        }
    }
}
