using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstroidsRemixed.GameModels
{
    public class RockBoss : Rock
    {
        public int _health = 20;
        public RockBoss(PointF position, State size) : base(position, size) { }

        public override void Render(BufferedGraphics bg, Size clientSize)
        {
            bg.Graphics.FillPath(new SolidBrush(Color.White), GetPath(clientSize));

            // Draw maximum size the shape can be (visual TILESIZE)
            bg.Graphics.DrawRectangle(new Pen(Color.LimeGreen), new Rectangle((int)Pos.X - _health * 5, (int)Pos.Y + _tileSize, _health * 10, 10));
            bg.Graphics.DrawString(_health.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.LimeGreen), new PointF(Pos.X, Pos.Y + _tileSize + 14));
        }
    }
}
