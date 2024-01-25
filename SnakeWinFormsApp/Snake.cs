using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SnakeWinFormsApp
{
    public class Snake
    {
        Form form;
        Cube tail, head;
        List<Cube> snake = new List<Cube>();

        private int moveX;
        private int moveY;

        public Point GetLocation
        {
            get
            { return head.GetLocation(); }
        }

        public int GetTailCount
        {
            get
            { 
                int tailCountWithoutHead = snake.Count - 1;
                return tailCountWithoutHead; }
        }

        public Snake(Form form)
        {
            this.form = form;

            snake.Add(new Cube(form, Settings.offsetX, Settings.offsetY, Color.DarkGreen));
            head = snake[0];
        }

        public void Move(Directions direction)
        {
            for (int i = snake.Count; i >= 2; i--)
            {
                snake[i - 1].X = snake[i - 2].X;
                snake[i - 1].Y = snake[i - 2].Y;
                snake[i - 1].Move();
            }

            switch (direction)
            {
                case Directions.Up:
                    moveY = -1;
                    moveX = 0;
                    break;
                case Directions.Down:
                    moveY = 1;
                    moveX = 0;
                    break;
                case Directions.Right:
                    moveX = 1;
                    moveY = 0;
                    break;
                case Directions.Left:
                    moveX = -1;
                    moveY = 0;
                    break;
            }

            head.X = head.X + Settings.cellSize * moveX;
            head.Y = head.Y + Settings.cellSize * moveY;
            head.Move();
        }

        public void AddTail()
        {
            int snakeCount = snake.Count;
            tail = new Cube(form, snake[snakeCount - 1].X + moveX * Settings.cellSize, snake[snakeCount - 1].Y + moveY * Settings.cellSize, Color.Green);
            snake.Add(tail);
        }

        public bool CheckEatYourSelf()
        {
            for (int i = 1; i < snake.Count - 1; i++)
            {
                if (snake[0].GetLocation() == snake[i].GetLocation())
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckGameBorder()
        {
            if (head.GetLocation().X >= Settings.gameFieldWidth || head.GetLocation().X <= 0
                || head.GetLocation().Y >= Settings.gameFieldHeight || head.GetLocation().Y <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
