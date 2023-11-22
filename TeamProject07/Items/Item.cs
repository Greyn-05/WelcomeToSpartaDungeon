using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;

namespace TeamProject07
{
    internal class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int ItemPrice { get; set; }
        public bool IsEquipped { get; set; } = false;
        public bool IsUsed { get; set; } = false;


        public void Setting(string myId, string myName, string myComment, string myPrice)
        {
            Id = int.Parse(myId);

            Name = myName;
            Info = myComment;

            ItemPrice = int.Parse(myPrice);
        }

        public Define.ItemType Type;
        public Define.Parts Part;

        public Define.Buff buff;
        public int buffValue;
        public Define.Buff debuff;
        public int debuffValue;
    }
}
