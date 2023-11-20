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
        public Dictionary<int, Skill> Skills;

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
                        skill.Damage = int.Parse(data[3]);

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
