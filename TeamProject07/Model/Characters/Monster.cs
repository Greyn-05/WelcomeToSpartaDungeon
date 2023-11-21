using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject07.Characters
{
    internal class Monster : Character
    {
        public int RewardExp { get; set; }
        public Monster(Monster m) {
            Id = m.Id;
            Name = m.Name;
            Level = m.Level;
            Attack = m.Attack;
            Defence = m.Defence;
            Hp = m.Hp;
            Gold = m.Gold;
            CritRate = m.CritRate;
            MissRate = m.MissRate;
            RewardExp = m.RewardExp;
        }
        public Monster(string myId, string myName, string myLevel, string myAttack, string myDefence, string myHp, string myGold, string myCritRate, string myMissRate, string myExp)
        {
            Id = int.Parse(myId);
            Name = myName;
            Level = int.Parse(myLevel);
            Attack = int.Parse(myAttack);
            Defence = int.Parse(myDefence);
            Hp = int.Parse(myHp);
            Gold = int.Parse(myGold);
            CritRate = int.Parse(myCritRate);
            MissRate = int.Parse(myMissRate);
            RewardExp = int.Parse(myExp);
        }

        public void Dead()
        {
            //배틀 자체를 종료시켜야함 > 실패
            //결과창 
        }
    }
}
