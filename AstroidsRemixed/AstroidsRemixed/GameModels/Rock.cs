using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstroidsRemixed.GameModels
{
    public class Rock
    {
        // Max and min amount of sides on a rock shape
        public const int minSideCount = 4;
        public const int maxSideCountExclusive = 14;

        public enum State { small = 25, medium = 40, large =  65, boss = 200 }
        public int _tileSize;

        // current rotation of shape
        public float _rotation;

        // how fast the shape will rotate ( -3f to 3f )
        public float _rotationDelta = -3f;

        // how fast the shape will move | BOTH : ( -2.5f to 2.5f )
        public float _xSpeed = -2.5f;
        public float _ySpeed = -2.5f;

        // static random number generator
        public static Random _rand = new Random();

        // this shape got hit by another one
        public bool IsMarkedForDeath { get; set; }

        // position in world space
        public PointF Pos { get; set; }

        // Alpha (Used mostly for invuln frames)
        public int _alpha;

        public GraphicsPath _model = new GraphicsPath();

        /// <summary>
        /// Constructor for a basic shape
        /// </summary>
        /// <param name="position"> position in world space</param>
        public Rock(PointF position, State size)
        {
            _tileSize = (int)size;

            _model.AddLines(MakeNewShape(_rand.Next(minSideCount, maxSideCountExclusive), _tileSize / 2, _tileSize, _tileSize));

            Pos = position;
            _rotation = 0;

            _rotationDelta += _rand.Next(7);

            _xSpeed += _rand.Next(6);
            _ySpeed += _rand.Next(6);

            _alpha = 0;
        }

        /// <summary>
        /// Abstract method used to get the Graphics path of the shape
        /// </summary>
        /// <returns> current shapes graphics path </returns>
        public GraphicsPath GetPath(Size clientSize)
        {
            GraphicsPath gpClone = (GraphicsPath)_model.Clone();

            Matrix cloneMatrix = new Matrix();

            cloneMatrix.Translate(Pos.X, Pos.Y);
            cloneMatrix.Rotate(_rotation);

            gpClone.Transform(cloneMatrix);

            if(Pos.X > clientSize.Width - _tileSize)
            {
                GraphicsPath xClone = (GraphicsPath)_model.Clone();

                Matrix xMatrix = new Matrix();

                xMatrix.Translate(Pos.X - clientSize.Width, Pos.Y);
                xMatrix.Rotate(_rotation);

                xClone.Transform(xMatrix);

                gpClone.AddPath(xClone, false);
            }
            else if(Pos.X < 0 + _tileSize)
            {
                GraphicsPath xClone = (GraphicsPath)_model.Clone();

                Matrix xMatrix = new Matrix();

                xMatrix.Translate(Pos.X + clientSize.Width, Pos.Y);
                xMatrix.Rotate(_rotation);

                xClone.Transform(xMatrix);

                gpClone.AddPath(xClone, false);
            }

            if (Pos.Y > clientSize.Height - _tileSize)
            {
                GraphicsPath yClone = (GraphicsPath)_model.Clone();

                Matrix yMatrix = new Matrix();

                yMatrix.Translate(Pos.X, Pos.Y - clientSize.Height);
                yMatrix.Rotate(_rotation);

                yClone.Transform(yMatrix);

                gpClone.AddPath(yClone, false);
            }
            else if (Pos.Y < 0 + _tileSize)
            {
                GraphicsPath yClone = (GraphicsPath)_model.Clone();

                Matrix yMatrix = new Matrix();

                yMatrix.Translate(Pos.X, Pos.Y + clientSize.Height);
                yMatrix.Rotate(_rotation);

                yClone.Transform(yMatrix);

                gpClone.AddPath(yClone, false);
            }

            return gpClone;
        }

        /// <summary>
        /// Render the shape based on what type it is onto the world space
        /// </summary>
        /// <param name="bg"> current buffer </param>
        public virtual void Render(BufferedGraphics bg, Size clientSize)
        {
            bg.Graphics.FillPath(new SolidBrush(Color.FromArgb(_alpha, Color.White)), GetPath(clientSize));

            // Draw maximum size the shape can be (visual TILESIZE)
            //bg.Graphics.DrawEllipse(new Pen(Color.LimeGreen), new RectangleF(Pos.X - _tileSize, Pos.Y - _tileSize, _tileSize * 2, _tileSize * 2));
        }

        /// <summary>
        /// Apply changes
        /// </summary>
        /// <param name="clientSize"> world space size </param>
        public void Tick(Size clientSize)
        {
            // Change the shapes movement direction
            if (Pos.X > clientSize.Width)
                Pos = new PointF(0, Pos.Y);

            else if (Pos.X < 0)
                Pos = new PointF(clientSize.Width, Pos.Y);

            if (Pos.Y > clientSize.Height)
                Pos = new PointF(Pos.X, 0);

            else if (Pos.Y < 0)
                Pos = new PointF(Pos.X, clientSize.Height);

            // Apply speed
            Pos = new PointF(Pos.X + _xSpeed, Pos.Y + _ySpeed);

            // Rotate shape by delta
            _rotation += _rotationDelta;

            // Increment up to 254
            if(_alpha < 254)
                _alpha += 2;
            // Get to 255 to prevent the renderer from having to do a alpha calculation ? Might be b.s. but it makes you think right
            else if(_alpha == 254)
            {
                _alpha++;
            }
        }

        /// <summary>
        /// Create a polygon
        /// </summary>
        /// <param name="numberOfSides"> Number of sides the polygon should have </param>
        /// <param name="minRadius"> minimum radius of point from center </param>
        /// <param name="maxRadius"> maximum radius of point from center </param>
        /// <returns> Point array that can be used for creating shapes </returns>
        public static PointF[] MakeNewShape(int numberOfSides, int minRadius, int maxRadius, int size)
        {
            // shape points
            PointF[] shapePoints = new PointF[numberOfSides];

            // theta to increment by
            float theta = (float)(Math.PI * 2) / numberOfSides;
            float currentAngle = 0f;

            for (int i = 0; i < numberOfSides; i++)
            {
                // generate point with varience of min -> max
                shapePoints[i] = new PointF(
                    (float)Math.Cos(currentAngle + size) * _rand.Next(minRadius, maxRadius + 1),
                    (float)Math.Sin(currentAngle + size) * _rand.Next(minRadius, maxRadius + 1));

                // increase angle by theta to ensure correct amount of sides
                currentAngle += theta;
            }

            return shapePoints;
        }
    }
}
