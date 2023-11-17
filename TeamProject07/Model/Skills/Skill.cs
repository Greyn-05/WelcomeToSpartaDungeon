using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;

namespace TeamProject07.Skills
{
    internal class Skill
    {
        public static Dictionary<int, Skill> skillData;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Mp { get; set; }
        /*public Skill(string myId, string myName, string myDamage, string myMp)
        {
            Id = int.Parse(myId);
            Name = myName;
            Damage = int.Parse(myDamage);
            Mp = int.Parse(myMp);
        }*/
        public void LoadSkills()
        {
            skillData = new Dictionary<int, Skill>();
            skillData.Clear();

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
                        skill.Damage= int.Parse(data[2]);
                        skill.Damage = int.Parse(data[3]);

                        skillData.Add(skill.Id, skill);
                    }
                }
            }
        }

        public Define.MainGamePhase ShowSkillProto()
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();
            Console.WriteLine("Load된 스킬정보");
            foreach (KeyValuePair<int, Skill> s in skillData )
            {
                Console.WriteLine($"key : {s.Key}, ID : {s.Value.Id}, Name : {s.Value.Name},Damage : {s.Value.Damage}, MP : {s.Value.Mp}");
            }
            Console.WriteLine("\n0. 메인화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    choicePhase = Define.MainGamePhase.Main;
                    break;


            }
            return Define.MainGamePhase.Main;

        }

        private int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

    }
}
