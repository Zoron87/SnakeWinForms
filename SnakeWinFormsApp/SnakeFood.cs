using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeWinFormsApp
{
    public class SnakeFood
    {
        Random random = new Random();
        Form form;
        PictureBox snakeFood;

        public int X { get; set; }
        public int Y { get; set; }

        public SnakeFood(Form form)
        {
            this.form = form;

            snakeFood = new PictureBox();
            snakeFood.BackColor = Color.Yellow;
            snakeFood.Size = new Size(Settings.cellSize - 1 , Settings.cellSize - 1);
            Generate();
        }

        public Point GetLocation()
        {
            return snakeFood.Location;
        }

        public void Generate()
        {
            var rnd = random.Next(Settings.cellSize, Settings.gameFieldHeight);
            var locationMultipleCellSizeX = rnd - rnd % Settings.cellSize;

            rnd = random.Next(Settings.cellSize, Settings.gameFieldWidth);
            var locationMultipleCellSizeY = rnd - rnd % Settings.cellSize;

            snakeFood.Location = new Point(Settings.offsetX + locationMultipleCellSizeX + 1, Settings.offsetY + locationMultipleCellSizeY + 1);

            form.Controls.Add(snakeFood);
        }
    }
}
