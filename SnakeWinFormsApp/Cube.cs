using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeWinFormsApp
{
    public class Cube
    {
        PictureBox cube;

        private int x;
        private int y;

        private int offsetForBorderX;
        private int offsetForBorderY;

        public int X 
        {
            get 
            { return x; }
            set
            { 
                x = value;
                offsetForBorderX = x + 1;
            }
        }

        public int Y {
            get
            { return y; }
            set
            {
                y = value;
                offsetForBorderY = y + 1;
            }
        }
        

        public Cube(Form form, int x, int y, Color color)
        {
            this.X = x;
            this.Y = y;

            cube = new PictureBox();
            cube.Size = new Size(Settings.cellSize - 1 , Settings.cellSize - 1);
            cube.BackColor = color;
            cube.Location = new Point(offsetForBorderX, offsetForBorderY);
            form.Controls.Add(cube);
        }

        public Point GetLocation()
        {
            return cube.Location;
        }

        public void Move()
        {
            cube.Location = new Point(offsetForBorderX, offsetForBorderY);
        }
    }
}
