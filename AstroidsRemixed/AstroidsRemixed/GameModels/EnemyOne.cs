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
    public class EnemyOne
    {
        static readonly GraphicsPath _enemyModel = new GraphicsPath();

        const float _speed = 1.5f;
        const int _shootTimer = 15;
        const int _burstWaitTimer = 100;

        int cycleNum = 0;
        int burstCycleNum = 0;

        public List<BulletParticle> _bullets = new List<BulletParticle>();

        public enum _shootingMode { Normal, TripleShot, ScatterShot }
        public _shootingMode _currentFireMode = _shootingMode.Normal;

        public PointF _pos = new PointF(500, 500);
        public float _currentRot;
        public bool _shooting = false;

        public EnemyOne()
        {
            _enemyModel.AddLines(CreatePlayerModel());
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
       
        public GraphicsPath GetPath()
        {
            GraphicsPath gpClone = (GraphicsPath)_enemyModel.Clone();

            Matrix cloneMatrix = new Matrix();

            cloneMatrix.Translate(_pos.X, _pos.Y);
            cloneMatrix.Rotate(_currentRot);
            cloneMatrix.Scale(20, 20);

            gpClone.Transform(cloneMatrix);

            return gpClone;
        }

        public void Tick(PlayerInfo player)
        {
            float diffX = player._pos.X - _pos.X;
            float diffY = player._pos.Y - _pos.Y;

            float angle = (float)Math.Atan2(diffY, diffX);

            _pos.X += (float)Math.Cos(angle) * _speed;
            _pos.Y += (float)Math.Sin(angle) * _speed;

            _currentRot = (float)((angle) * (180 / Math.PI)) - 90;

            switch (_currentFireMode)
            {
                case _shootingMode.Normal: RegularShot();
                    break;
                case _shootingMode.TripleShot: BurstShot();
                    break;
                case _shootingMode.ScatterShot:
                    break;
            }
        }

        private void BurstShot()
        {
            if (cycleNum == 0 && burstCycleNum == 0)
            {
                cycleNum++;
                _shooting = true;
                Console.WriteLine("shooting");
            }
            else if (cycleNum <= _shootTimer)
            {
                _shooting = cycleNum % 2 == 0;

                cycleNum++;
                Console.WriteLine("trying to shoot");
            }
            else if (cycleNum > _shootTimer)
            {
                if (burstCycleNum == 0)
                {
                    _shooting = false;
                    burstCycleNum++;
                }

                else if (burstCycleNum <= _burstWaitTimer)
                    burstCycleNum++;

                else if (burstCycleNum > _burstWaitTimer)
                {
                    cycleNum = 0;
                    burstCycleNum = 0;
                }
            }
        }

        private void RegularShot()
        {
            if (cycleNum == 0)
            {
                cycleNum++;
                _shooting = true;
                Console.WriteLine("shooting");
            }
            else if (cycleNum <= _shootTimer)
            {
                cycleNum++;
                _shooting = false;
                Console.WriteLine("trying to shoot");
            }
            else if (cycleNum > _shootTimer)
            {
                cycleNum = 0;
                _shooting = false;
                Console.WriteLine("Gave up / reset");
            }
        }
    }
}
