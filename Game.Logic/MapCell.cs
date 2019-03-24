using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public class MapCell
    {
        public Character CharacterInCell { get; set; }
        public bool IsEmpty { get; set; }
    }
}
