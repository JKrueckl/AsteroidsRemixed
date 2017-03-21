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
    public class EnemyTwo
    {
        static readonly GraphicsPath _enemyModel = new GraphicsPath();
        static Rectangle clientScreen;
        static Random _rand = new Random();

        const float _speed = 1.5f;
        const int _shootTimer = 50;
        const int _burstWaitTimer = 100;

        int cycleNum = 0;
        int burstCycleNum = 0;
        float moveX = -2f;
        float moveY = -2f;

        public List<BulletParticle> _bullets = new List<BulletParticle>();

        public enum _shootingMode { Normal, TripleShot, ScatterShot }
        public _shootingMode _currentFireMode = _shootingMode.Normal;

        public PointF _pos;
        public float _currentRot;
        public bool _shooting = false;

        public EnemyTwo(Rectangle clientSize)
        {
            moveX += _rand.Next(0, 4);
            moveY += _rand.Next(0, 4);

            clientScreen = clientSize;

            _pos = new PointF(_rand.Next(1, clientScreen.Width - 1), _rand.Next(1, clientScreen.Height - 1));

            _enemyModel.AddLines(CreatePlayerModel());
        }

        static PointF[] CreatePlayerModel()
        {
            PointF[] shapePoints = new PointF[6];

            float mainAngle = (float)(Math.PI * 2) / 6;
            float currentAngle = 0f;

            for(int i = 0; i < 6; i++)
            {
                shapePoints[i] = new PointF(
                    (float)Math.Sin(currentAngle),
                    (float)Math.Cos(currentAngle)
                );

                currentAngle += mainAngle;
            }

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
            if (_pos.X >= clientScreen.Width || _pos.X <= 0)
                moveX *= -1;

            if (_pos.Y >= clientScreen.Height || _pos.Y <= 0)
                moveY *= -1;

            _pos.X += moveX;
            _pos.Y += moveY;

            if (_currentFireMode == _shootingMode.Normal)
                RegularShot();
            else if (_currentFireMode == _shootingMode.TripleShot)
                BurstShot();
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
