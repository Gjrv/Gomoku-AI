using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoky
{
    class Table
    {
        public Dictionary<Point, char> table;
        public List<Point> CoordUser;
        public List<Point> CoordAI;
   
        public Table()
        {
            table = new Dictionary<Point, char>();
            CoordUser = new List<Point>();
            CoordAI = new List<Point>();
        }


    }
}
