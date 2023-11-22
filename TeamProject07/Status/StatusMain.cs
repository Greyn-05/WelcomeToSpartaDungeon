using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Characters;
using TeamProject07.Inventroy;
using TeamProject07.Items;
using TeamProject07.Utils;

namespace TeamProject07.Status
{
    internal class StatusMain
    {
        public Define.MainGamePhase test(Player player)
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();
            player.TotalStats();
            Console.WriteLine("플레이어의 정보를 표시합니다");
            Console.WriteLine("\n");
            Console.WriteLine($"LEVEL {player.Level} ");
            Console.WriteLine($"직업 : {player.Class} ");
            Console.WriteLine($"공격력 : {player.Attack} + {player.EquipStats.AddAttack}");
            Console.WriteLine($"방어력 : {player.Defence} + {player.EquipStats.AddDefence}");
            Console.WriteLine($"치명타 확률 : {player.CritRate} + {player.EquipStats.AddCritRate} %");
            Console.WriteLine($"회피 확률 : {player.MissRate} + {player.EquipStats.AddMissRate} %");
            Console.WriteLine($"체력 : {player.Hp} / {player.MaxHp} + {player.EquipStats.AddHp} ");
            Console.WriteLine($"마나 : {player.Mp} / {player.MaxMp} + {player.EquipStats.AddMp} ");
            Console.WriteLine($"골  드 : {player.Gold} G");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("0. 메인화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            if (InvenMain.SetItemCheck())
            {
                Console.WriteLine($"   세트효과 발동중 : {player.set}");
            }
            Console.WriteLine($"   골  드 : {player.Gold} G");

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