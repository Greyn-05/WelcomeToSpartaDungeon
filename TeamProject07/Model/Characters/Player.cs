using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Items;
using TeamProject07.Skills;

namespace TeamProject07.Characters
{
    internal class Player : Character
    {
        public List<Item> Inven { get; set; }
        public string Class { get; set; }
        public int LevelUpExp { get; set; }
        public List<Skill> Skills { get; set; }

        public Player() { 
        }
        public Player(string name, int level, int attack, int defence, int hp, int gold, int critRate,int missRate) { 
            Name = name;
            Level = level;
            Attack = attack;
            Defence = defence;
            MaxHp = hp;
            hp = MaxHp;

            Gold = gold;
            CritRate = critRate;
            MissRate = missRate;

            Inven = new List<Item>();

        }
        public void ObtainItem(Item item)
        {
            // TODO
        }

        public void RemoveItem(Item item)
        {
            // TODO
        }
        public void LevelUp() { 
        
        }
    }
}
