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
        public int AddAttack { get; set; } = 0;
        public int AddDefence { get; set; } = 0;
        public int AddHp { get; set; } = 0;
        public int AddMp { get; set; } = 0;


        /// <summary>
        /// 크리티컬 확률을 올려주는 변수 (Crit - Critical)
        /// </summary>
        public float AddCritRate { get; set; } = 0f;
        public float AddMissRate { get; set; } = 0f;



        // 세트명

    }
}
