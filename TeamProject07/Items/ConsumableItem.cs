using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Characters;

namespace TeamProject07.Items
{
    internal class ConsumableItem : Item
    {
        public int BuffHp { get; set; }
        public int BuffAttack { get; set; }
        public int BuffDefence { get; set; }
        public int Stock { get; set; }


        public int point; // 회복수치
        
    }
}
