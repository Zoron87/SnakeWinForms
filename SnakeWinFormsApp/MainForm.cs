using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace SnakeWinFormsApp
{
    public partial class MainForm : Form
    {
        Snake snake;
        SnakeFood snakeFood;

        private int rowCount;
        private int colCount;

        Directions direction = Directions.Right;

        public MainForm()
        {
            InitializeComponent();
            this.KeyDown += MainForm_KeyPress;

            rowCount = (int)(Settings.gameFieldHeight / Settings.cellSize);
            colCount = (int)(Settings.gameFieldWidth / Settings.cellSize);

            snake = new Snake(this);
            snakeFood = new SnakeFood(this);

            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Interval = 500;
            mainTimer.Start();
        }

        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            snake.Move(direction);
            CheckEatFood(snakeFood);

            if (snake.CheckEatYourSelf())
            {
                mainTimer.Stop();
                if (MessageBox.Show("Вы съели сами себя! \r\n Игра завершена. \r\n Попробовать еще раз?", "GameOver", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    Application.Restart();
            }

            if (snake.CheckGameBorder())
            {
                mainTimer.Stop();
                if (MessageBox.Show("Вы вышли за границы поля! \r\n Игра завершена. \r\n Попробовать еще раз?", "GameOver", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    Application.Restart();
            }

            scoreLabel.Text = "Score: " + snake.GetTailCount.ToString();

            CheckWinGame();
        }

        private void CheckEatFood(SnakeFood snakeFood)
        {
            if (snake.GetLocation.X == snakeFood.GetLocation().X && snake.GetLocation.Y == snakeFood.GetLocation().Y)
            {
                snake.AddTail();
                snakeFood.Generate();
            }
        }

        private void CheckWinGame()
        {
            if (snake.GetTailCount >= 200)
                if (MessageBox.Show("Вы победили! \r\n Игра завершена. \r\n Сыграть еще раз?", "GameOver", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    Application.Restart();
        }

        private void MainForm_KeyPress(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    direction = Directions.Right;
                    break;

                case "Left":
                    direction = Directions.Left;
                    break;

                case "Up":
                    direction = Directions.Up;
                    break;

                case "Down":
                    direction = Directions.Down;
                    break;
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawGrid(Settings.offsetX, Settings.offsetY, Settings.cellSize, rowCount, colCount, Pens.Black);
        }
    }

    public static class DrawingExtension
    {
        public static void DrawGrid(this Graphics g, int offsetX, int offsetY, int cellSize, int rowCount, int colCount, Pen pen)
        {
            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    g.DrawRectangle(pen, offsetX + i * cellSize, offsetY + j * cellSize, cellSize, cellSize);
                }
            }
        }
    }
}