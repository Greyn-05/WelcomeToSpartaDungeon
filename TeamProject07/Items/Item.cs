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
        public Define.ItemType Type;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int ItemPrice { get; set; }

        public bool IsEquipped { get; set; } = false;

        public void Setting(string myId, string myName, string myComment, string myPrice)
        {
            Id = int.Parse(myId);

            Name = myName;
            Info = myComment;

            ItemPrice = int.Parse(myPrice);
        }

    }
}
