using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using text_rpg.Utils;

namespace text_rpg.Items
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
