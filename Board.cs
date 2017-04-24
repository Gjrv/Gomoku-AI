using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku_v1._0
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    public enum Player { AI,User }
    class Board
    {
        public Dictionary<Point, char> _game_board;
        public List<Point> _coord_AI;
        public List<Point> _coord_User;
        public Board()
        {
            _game_board = new Dictionary<Point,char>();
            _coord_AI = new List<Point>();
            _coord_User = new List<Point>();
        }

        public void Add_Point(Point p,Player player)
        {
            switch (player)
            {
                case Player.AI:
                    {
                        _game_board.Add(p, 'x');
                        break;
                    }
                case Player.User:
                    {
                        _game_board.Add(p, 'o');
                        break;
                    }
            }
        }
        public bool Check()
        {
            return true;
        }
    }
}
