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
        public Define.DeBuff debuff;
        public int point;

    }
}

/*
 무기
머리
몸
장갑
발
장신구

스탯
올려준거거 하나
낮추는거 하나
 
 */
