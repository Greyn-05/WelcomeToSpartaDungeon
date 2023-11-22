using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;

namespace TeamProject07.Items
{
    internal class Equipment : Item
    {
        public int AddAttack { get; set; } 
        public int AddDefence { get; set; }
        public int AddHp { get; set; }
        public int AddMp { get; set; }


        /// <summary>
        /// 크리티컬 확률을 올려주는 변수 (Crit - Critical)
        /// </summary>
        public float AddCritRate { get; set; }
        public float AddMissRate { get; set; }



        // 세트명

    }
}
