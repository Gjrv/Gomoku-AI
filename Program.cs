using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku_v1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Board game = new Board();
            Point p = new Point();
            Analyzer analyze = new Analyzer();
            while(true)
            {
                p.X = Console.Read();
                p.Y = Console.Read();
                game.Add_Point(p, Player.User);

                game.Add_Point(analyze._best_Move, Player.AI);

                game.Check();
            }
        }
    }
}
