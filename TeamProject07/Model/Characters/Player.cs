using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Items;
using TeamProject07.Skills;
using TeamProject07.Utils;

namespace TeamProject07.Characters
{
    internal class Player : Character
    {
        public List<Item> Inven { get; set; }
        public string Class { get; set; }
        public int LevelUpExp { get; set; }
        public int Mp { get; set; }

        public int MaxMp;

        public Dictionary<int, Skill> Skills;

        public Equipment EquipStats { get; set; }
        public ConsumableItem ConsumStats { get; set; }

        public Player(string name, int level, int attack, int defence, int hp, int mp, int gold, int critRate, int missRate)
        {
            Name = name;
            Level = level;
            Attack = attack;
            Defence = defence;
            Hp = hp;
            MaxHp = hp;
            Mp = mp;
            MaxMp = mp;
            Gold = gold;
            CritRate = critRate;
            MissRate = missRate;

            Inven = new List<Item>();
            EquipStats = new Equipment();
            ConsumStats = new ConsumableItem();
        }

        public int TotalAttack => Attack + EquipStats.AddAttack;
        public int TotalDefence => Defence + EquipStats.AddDefence;
        public int TotalHp => MaxHp + EquipStats.AddHp;
        public int TotalMp => MaxMp + EquipStats.AddMp;
        public float TotalCritRate => CritRate + EquipStats.AddCritRate;
        public float TotalMissRate => MissRate + EquipStats.AddMissRate;

        public void TotalStats()
        {
            EquipStats.AddAttack = 0;
            EquipStats.AddDefence = 0;
            EquipStats.AddHp = 0;
            EquipStats.AddMp = 0;
            EquipStats.AddCritRate = 0;
            EquipStats.AddMissRate = 0;
            ConsumStats.BuffHp = 0;
            ConsumStats.BuffMp = 0;

            foreach (Item item in Inven)
            {
                if (item.IsEquipped)
                {
                    switch (item.buff)
                    {
                        case Define.Buff.atk:
                            EquipStats.AddAttack += item.buffValue;
                            break;
                        case Define.Buff.def:
                            EquipStats.AddDefence += item.buffValue;
                            break;
                        case Define.Buff.cri:
                            EquipStats.AddCritRate += item.buffValue;
                            break;
                        case Define.Buff.miss:
                            EquipStats.AddMissRate += item.buffValue;
                            break;
                        case Define.Buff.maxHp:
                            EquipStats.AddHp += item.buffValue;
                            break;
                        case Define.Buff.maxMp:
                            EquipStats.AddMp += item.buffValue;
                            break;
                        default:
                            break;
                    }
                }
                else if (item.IsUsed)
                {
                    switch(item.buff)
                    {
                        case Define.Buff.hp:
                            ConsumStats.BuffHp += item.buffValue;
                            break;
                        case Define.Buff.mp:
                            ConsumStats.BuffMp += item.buffValue;
                            break;
                        default:
                            break;
                    }
                }
            }


           // if (SetItemCheck()) // 세트효고ㅡㅏ있으면 세트 추가
           
            }


        }

        public void EquipItem(Item item)
        {
            item.IsEquipped = !item.IsEquipped;
            TotalStats();
        }

        public void LoadSkills()
        {
            Skills = new Dictionary<int, Skill>();
            Skills.Clear();

            if (File.Exists(Define.SkillPath))
            {
                using (StreamReader sr = new StreamReader(new FileStream(Define.SkillPath, FileMode.Open)))
                {
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        //Skill skill = new Skill(data[0], data[1], data[2], data[3]);
                        Skill skill = new Skill();
                        skill.Id = int.Parse(data[0]);
                        skill.Name = data[1];
                        skill.Damage = int.Parse(data[2]);
                        skill.Mp = int.Parse(data[3]);

                        Skills.Add(skill.Id, skill);
                    }
                }
            }
        }

        public Define.MainGamePhase ShowSkillProto()
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();
            Console.WriteLine("Load된 스킬정보");
            foreach (KeyValuePair<int, Skill> s in Skills)
            {
                Console.WriteLine($"key : {s.Key}, ID : {s.Value.Id}, Name : {s.Value.Name}, Damage : {s.Value.Damage}, MP : {s.Value.Mp}");
            }
            Console.WriteLine("아무키나 입력");
            Console.ReadLine();
            return Define.MainGamePhase.Main;
        }

        //public void LevelUp()
        //{

        //}
    }
}