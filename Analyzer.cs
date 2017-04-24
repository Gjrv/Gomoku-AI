using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku_v1._0
{
    class Analyzer
    {
        public Point _best_Move;
        static private List<Point> _possibleMoves = new List<Point>();
        private Board _board;
        static private Dictionary<string, int> _template = new Dictionary<string, int>()
        {
            { "xo",      1 },
            { "ox",      1 },
            { "x",      2 },
            { "x xo",    3 },
            { "ox x",    3 },
            { "xx xo",   4 },
            { "x xxo",   4 },
            { "xxo",     5 },
            { "oxx",     5 },
            { "x x",    6 },
            { "x x",     6 },
            { "x x",     6 },
            { "x xx",   7 },
            { "xx x",   7 },
            { "xx xxo",  8 },
            { "oxx xx",  8 },
            { "xxxo",    9 },
            { "oxxx",    9 },
            { "xx",    10 },
            { "xx xx", 11 },
            { "xxxxo",  12 },
            { "oxxxx",  12 },
            { "xxx",   13 },
            { "xxxx",  14 },
            { "xxxxx", 15 },
        };

        private List<Point> get_Possible(Point p)
        {
            if(_board._game_board. == 'x')
            {

            }
        }

        private void PossibleMoves(Player player)
        {
            _possibleMoves.Clear();
            List<Point> Coord;
            switch (player)
            {
                case Player.AI:
                    Coord = _board._coord_AI;
                    break;
                case Player.User:
                    Coord = _board._coord_User;
                    break;
                default: Coord = null; break;
            }
            foreach (var p in Coord)
            {
                if (!_board._game_board.ContainsKey(new Point(p.X + 1, p.Y + 1)))
                {
                    _possibleMoves.Add(new Point(p.X + 1, p.Y + 1));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X + 1, p.Y)))
                {
                    _possibleMoves.Add(new Point(p.X + 1, p.Y));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X + 1, p.Y - 1)))
                {
                    _possibleMoves.Add(new Point(p.X + 1, p.Y - 1));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X, p.Y - 1)))
                {
                    _possibleMoves.Add(new Point(p.X, p.Y - 1));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X - 1, p.Y - 1)))
                {
                    _possibleMoves.Add(new Point(p.X - 1, p.Y - 1));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X - 1, p.Y)))
                {
                    _possibleMoves.Add(new Point(p.X - 1, p.Y));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X - 1, p.Y + 1)))
                {
                    _possibleMoves.Add(new Point(p.X - 1, p.Y + 1));
                }
                if (!_board._game_board.ContainsKey(new Point(p.X, p.Y + 1)))
                {
                    _possibleMoves.Add(new Point(p.X, p.Y + 1));
                }
            }
            _possibleMoves = _possibleMoves.Distinct().ToList();
        }
        private void Analyz()
        {
            int max_score = -238590234;
            int deep = 3;
            int score = 0;
            foreach(var move in _possibleMoves)
            {
                score = MiniMax(move,deep,Player.User);
                if (score > max_score)
                {
                    max_score = score;
                    _best_Move = move;
                }
                    
            }
        }
        private int MiniMax(Point move,int deep,Player player)
        {
            if (deep == 0)
                return Mark(move);
            int max = 0;
            int min = 0;
            int totalMax = -3242432;
            int totalMin = 23412314;
            List<Point> p_m = get_Possible(move);
            if (player == Player.AI)
            {
                foreach (var tmp in p_m)
                {
                    max = MiniMax(tmp, deep - 1,Player.User);
                    if (max > totalMax)
                        totalMax = max;
                }
                return totalMax;
            }
            else 
            {
                foreach (var tmp in p_m)
                {
                    min = MiniMax(tmp, deep - 1, Player.AI);
                    if (min < totalMin)
                        totalMin = min;
                }
                return totalMax;
            }
        }
    }
}
