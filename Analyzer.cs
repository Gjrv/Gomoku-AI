using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoky
{
    delegate bool BoolPoint(Point s1, Point s2);
    class Analyzer
    {
        static private List <Point> _possibleMoves = new List<Point>();
        static public Dictionary<string, int> _template = new Dictionary<string, int>()
        {
            { " xo",     1 },
            { " ox ",     1 },
            { " x ",      2 },
            { " x xo",   3 },
            { "ox x ",   3 },
            { " xx xo",  4 },
            { " x xxo",  4 },
            { " xxo",    5 },
            { "oxx ",    5 },
            { " x x ",    6 },
            { " x xx ",   7 },
            { " xx x ",   7 },
            { " xx xxo", 8 },
            { "oxx xx ", 8 },
            { " xxxo",   9 },
            { "oxxx ",   9 },
            { " xx ",    10 },
            { " xx xx ", 11 },
            { " xxxxo", 12 },
            { "oxxxx ", 12 },
            { " xxx ", 13 },
            { " xxxx ", 14 },
            { "xxxxx", 15 },
        };

        //Хранит ссылку на объкт стола
        private Table _table;

        //Ходы с оцекой для ИИ и Игрока
        private Dictionary<Point, int> _movesRatingAI;
        private Dictionary<Point, int> _movesRatingUser;

        //Структура перечисления для определения хода
        private enum Player { AI, User }
        //Анализирует оценку для всевозможных ходов
        private void MovesAnalyzer(/*Dictionary<Point, int>  MovesRating*/)
        {
            string GetHorizontalTemplate(Point move)
            {

                Point tmp = new Point(move.X + 1, move.Y);
                string answere = "x";
                int space = 0;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.X++;
                        continue;
                    }

                    answere += _table.table[tmp];
                    tmp.X++;
                }
                if (space != 2)
                    answere += "o";
                space = 0;
                tmp.X = move.X - 1;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.X--;
                        continue;
                    }

                    answere.PadLeft(answere.Length + 1, _table.table[tmp]);
                    tmp.X--;
                }
                if (space != 2)
                    answere.PadLeft(answere.Length + 1, 'o');

                return answere;
            }
            string GetVericalTemplate(Point move)
            {

                Point tmp = new Point(move.X, move.Y + 1);
                string answere = "x";
                int space = 0;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.Y++;
                        continue;
                    }

                    answere += _table.table[tmp];
                    tmp.Y++;
                }
                if (space != 2)
                    answere += "o";
                space = 0;
                tmp.Y = move.Y - 1;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.Y--;
                        continue;
                    }

                    answere.PadLeft(answere.Length + 1, _table.table[tmp]);
                    tmp.Y--;
                }
                if (space != 2)
                    answere.PadLeft(answere.Length + 1, 'o');

                return answere;
            }
            string GetDiagonalTemplate(Point move)
            {

                Point tmp = new Point(move.X + 1, move.Y + 1);
                string answere = "x";
                int space = 0;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.X++;
                        tmp.Y++;
                        continue;
                    }

                    answere += _table.table[tmp];
                    tmp.X++;
                    tmp.Y++;
                }
                if (space != 2)
                    answere += "o";
                space = 0;
                tmp.X = move.X - 1;
                tmp.Y = move.Y - 1;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.X--;
                        tmp.Y--;
                        continue;
                    }

                    answere.PadLeft(answere.Length + 1, _table.table[tmp]);
                    tmp.X--;
                    tmp.Y--;
                }
                if (space != 2)
                    answere.PadLeft(answere.Length + 1, 'o');

                return answere;
            }
            string GetMainDiagonalTemplate(Point move)
            {

                Point tmp = new Point(move.X - 1, move.Y + 1);
                string answere = "x";
                int space = 0;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.X--;
                        tmp.Y++;
                        continue;
                    }

                    answere += _table.table[tmp];
                    tmp.X--;
                    tmp.Y++;
                }
                if (space != 2)
                    answere += "o";
                space = 0;
                tmp.X = move.X + 1;
                tmp.Y = move.Y - 1;
                while (_table.table[tmp] != 'o')
                {
                    if (!_table.table.ContainsKey(tmp))
                    {
                        space++;
                        if (space == 2)
                            break;
                        answere += " ";
                        tmp.X++;
                        tmp.Y--;
                        continue;
                    }

                    answere.PadLeft(answere.Length + 1, _table.table[tmp]);
                    tmp.X++;
                    tmp.Y--;
                }
                if (space != 2)
                    answere.PadLeft(answere.Length + 1, 'o');

                return answere;
            }
            foreach (var move in _possibleMoves)
            {
                string HorizontalMask = GetHorizontalTemplate(move);
                string VerticalMask   = GetVericalTemplate(move);
                string MainDiagMask   = GetMainDiagonalTemplate(move);
                string DiagMask       = GetDiagonalTemplate(move);

                int Horizontal = HorizontalRaiting(HorizontalMask);
                int Vertical   = VerticalRaiting(VerticalMask);
                int MainDiag   = MainDiagRaiting(MainDiagMask);
                int Diag       = DiagRaiting(DiagMask);


            }
        }

        

        private void GetPossibleMoves(Player player)
        {
            List<Point> Coord = new List<Point>();
            switch (player)
            {
                case Player.AI:
                    {
                        Coord = _table.CoordAI;
                        break;
                    }
                case Player.User:
                    {
                        Coord = _table.CoordUser;
                        break;
                    }
                default:
                    {
                        Coord.Clear();
                        break;
                    }
            }
            foreach(var p in Coord)
            {  
                if (!_table.table.ContainsKey(new Point(p.X + 1, p.Y + 1)))
                {
                    _possibleMoves.Add(new Point(p.X + 1, p.Y + 1));
                }
                if (!_table.table.ContainsKey(new Point(p.X + 1, p.Y)))
                {
                    _possibleMoves.Add(new Point(p.X + 1, p.Y));
                }
                if (!_table.table.ContainsKey(new Point(p.X + 1, p.Y - 1)))
                {
                    _possibleMoves.Add(new Point(p.X + 1, p.Y - 1));
                }
                if (!_table.table.ContainsKey(new Point(p.X, p.Y -1)))
                {
                    _possibleMoves.Add(new Point(p.X , p.Y - 1));
                }
                if (!_table.table.ContainsKey(new Point(p.X - 1, p.Y - 1)))
                {
                    _possibleMoves.Add(new Point(p.X - 1, p.Y - 1));
                }
                if (!_table.table.ContainsKey(new Point(p.X - 1 , p.Y)))
                {
                    _possibleMoves.Add(new Point(p.X - 1, p.Y));
                }
                if (!_table.table.ContainsKey(new Point(p.X - 1, p.Y + 1)))
                {
                    _possibleMoves.Add(new Point(p.X - 1, p.Y+1));
                }
                if (!_table.table.ContainsKey(new Point(p.X, p.Y + 1)))
                {
                    _possibleMoves.Add(new Point(p.X, p.Y + 1));
                }
            }

            _possibleMoves = _possibleMoves.Distinct().ToList();

        }




        public Analyzer(Table table)
        {
            _table = table;
        }
    }
}
