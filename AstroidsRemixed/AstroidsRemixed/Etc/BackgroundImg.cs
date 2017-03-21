using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstroidsRemixed.Etc
{
    public class BackgroundImg
    {
        // Star object
        public struct Star
        {
            public Point starPos;
            public Color starColor;
            public GraphicsPath model;
            private int starSize;

            public Star(Point pos, Color col, GraphicsPath gp, int size)
            {
                starPos = pos;
                starColor = col;
                model = gp;
                starSize = size;
            }

            public GraphicsPath GetPath()
            {
                GraphicsPath gpClone = (GraphicsPath)model.Clone();

                Matrix cloneMatrix = new Matrix();

                cloneMatrix.Translate(starPos.X, starPos.Y);
                cloneMatrix.Scale(starSize, starSize);

                gpClone.Transform(cloneMatrix);

                return gpClone;
            }
        }
        // Planet object
        public struct Planet
        {
            public Point planetPos;
            public Color planetCol;
            public GraphicsPath model;
            private int planetSize;

            public Planet(Point pos, Color col, GraphicsPath gp, int size)
            {
                planetPos = pos;
                planetCol = col;
                model = gp;
                planetSize = size;
            }

            public GraphicsPath GetPath()
            {
                GraphicsPath gpClone = (GraphicsPath)model.Clone();

                Matrix cloneMatrix = new Matrix();

                cloneMatrix.Translate(planetPos.X, planetPos.Y);
                cloneMatrix.Scale(planetSize, planetSize);

                gpClone.Transform(cloneMatrix);

                return gpClone;
            }
        }

        // Used in Dictionary for storing stars and planets
        private enum spaceObject { Planet, Star };

        // Amount of stars on screen
        const int MAXSTARS = 500;
        const int MINSTARS = 400;
        const int MAXPLANETS = 3;
        const int MINPLANETS = 0;

        // Dictionary holding stars / planets
        readonly Dictionary<spaceObject, Color[]> ColorsForObject = new Dictionary<spaceObject, Color[]>();

        static Random rnd = new Random();

        // bg has planet
        public bool hasPlanets = false;

        // canvas width / height
        int imgWidth;
        int imgHeight;

        // amount of random stars / planets
        int amountOfStars;
        int amountOfPlanets;

        // all stars / planets
        public List<Star> imgStars = new List<Star>();
        public List<Planet> imgPlanets = new List<Planet>();

        // Constructor
        public BackgroundImg(Rectangle windowSize)
        {
            imgWidth = windowSize.Width;
            imgHeight = windowSize.Height;
            amountOfStars = rnd.Next(MINSTARS, MAXSTARS + 1);
            amountOfPlanets = rnd.Next(MINPLANETS, MAXPLANETS + 1);
            hasPlanets = amountOfPlanets > 0;

            ColorsForObject.Add(spaceObject.Star, new Color[10] {
                Color.FromArgb(255, 0, 0),      // Red
                Color.FromArgb(125, 0, 0),      // Dull Red
                Color.FromArgb(0, 0, 0),        // Pure White
                Color.FromArgb(0, 0, 0),        // -
                Color.FromArgb(0, 0, 0),        // -    
                Color.FromArgb(0, 0, 0),        // -
                Color.FromArgb(175, 175, 175),  // Grey
                Color.FromArgb(175, 175, 175),  // 
                Color.FromArgb(50, 50, 50),     // Dark Grey
                Color.FromArgb(50, 50, 50)
            });

            ColorsForObject.Add(spaceObject.Planet, new Color[5] {
                Color.FromArgb(214, 210, 186),  // Moon
                Color.FromArgb(125, 0, 0),      // Faded Red
                Color.FromArgb(255, 249, 84),   // Sun
                Color.FromArgb(117, 150, 121),  // Green    
                Color.FromArgb(168, 94, 188)    // Purp
            });

            // Make planet
            while (imgPlanets.Count < amountOfPlanets)
            {
                Point randomPos = new Point(rnd.Next(0, imgWidth), rnd.Next(0, imgHeight));
                Color randomCol = ColorsForObject[spaceObject.Planet][rnd.Next(0, ColorsForObject[spaceObject.Planet].Count())];
                Rectangle planetSize = new Rectangle(new Point(0, 0), new Size(1, 1));

                GraphicsPath randomPlanetGP = new GraphicsPath();
                randomPlanetGP.AddEllipse(planetSize);

                imgPlanets.Add(new Planet(randomPos, randomCol, randomPlanetGP, rnd.Next(200, 500)));
            }
                
            // Make stars
            while(imgStars.Count < amountOfStars)
            {
                Point randomPos = new Point(rnd.Next(0, imgWidth), rnd.Next(0, imgHeight));
                Color randomCol = ColorsForObject[spaceObject.Star][rnd.Next(0, ColorsForObject[spaceObject.Star].Count())];

                GraphicsPath randomStarGP = new GraphicsPath();
                randomStarGP.AddLines(createStar());

                imgStars.Add(new Star(randomPos, randomCol, randomStarGP, rnd.Next(1, 3)));
            }
        }

        // Create a star "Square"
        private PointF[] createStar()
        {
            PointF[] points = new PointF[4];

            points[0] = new PointF(1, -1);
            points[1] = new PointF(-1, -1);
            points[2] = new PointF(-1, 1);
            points[3] = new PointF(1, 1);

            return points;
        }
    }
}
