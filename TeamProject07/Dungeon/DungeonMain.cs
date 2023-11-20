using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Characters;
using TeamProject07.Controller;
using TeamProject07.Logic;
using TeamProject07.Shop;
using TeamProject07.Utils;
using static TeamProject07.Utils.Define;

namespace TeamProject07.Dungeon
{
    internal class DungeonMain
    {
        Define.MainGamePhase choicePhase = Define.MainGamePhase.temp;

        DungeonEntrance Dungeon = new DungeonEntrance();
        enum DungeonEntranceSelect
        {
            exit = 0,
            EnterDungeon,
            UseItem = 2,
        }


        public Define.MainGamePhase Entrance(Player player)
        {

            while (!player.IsDead)//&& !CreateMonsters[0].IsDead
            {
                

                DungeonEntranceView();
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                
                int input = CheckValidInput(0, 2);
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        DungeonDifSelect(player);
                        Dungeon.PlayerPhase();
                        // 배틀부분반복
                        Console.WriteLine();
                        break;
                    case 2:
                        choicePhase = Define.MainGamePhase.Main;
                        break;

                }
/*                if(choicePhase == Define.MainGamePhase.Main)
                {
                    break;
                }*/
            }
            
            return Define.MainGamePhase.Main;
        }
        
        private void DungeonDifSelect(Player player)
        {
            DungeonSelectView();
            Console.WriteLine();
            Console.WriteLine("입장할 던전을 선택하세요.");
            Dungeon.LoadMosters();
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 1:
                    Console.Clear();
                    Dungeon.StartDungeon(input);

                    break;
                case 2:
                    Console.Clear();
                    Dungeon.StartDungeon(input);

                    break;
                case 3:
                    Console.Clear();
                    Dungeon.StartDungeon(input);

                    break;
                case 0:
                    break;
            }
        }

        private void UseItem(Player player)
        {
            Console.WriteLine("인벤토리 출력");
            Console.WriteLine("사용할 아이템 선택");
            Console.WriteLine("아이템 효과 보여주기??");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 0:
                    break;

            }
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

        private void DungeonEntranceView()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("  ┏   ┓             ◆ ;");
            Console.WriteLine(" |      |          └┼┐ == ");
            Console.WriteLine("|        |         ┌│  ==");
            Console.WriteLine("==================================================");
            Console.WriteLine("======== 던전에 가기 전 준비를 해주세요. =========\n\n");
            Console.WriteLine("==================");
            Console.WriteLine("= 1. 던전 입장   =");
            Console.WriteLine("= 2. 소모품 사용 =");
            Console.WriteLine("= 0. 나가기      =");
            Console.WriteLine("==================");
        }

        private void DungeonSelectView()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("  ┏   ┓             ◆ ;");
            Console.WriteLine(" |      |          └┼┐ == ");
            Console.WriteLine("|        |         ┌│  ==");
            Console.WriteLine("==================================================");
            Console.WriteLine("======== 던전에 가기 전 준비를 해주세요. =========\n\n");
            Console.WriteLine("=========================");
            Console.WriteLine("= 1. 던전 1 (난이도 하) =");
            Console.WriteLine("= 2. 던전 2 (난이도 중) =");
            Console.WriteLine("= 3. 던전 3 (난이도 상) =");
            Console.WriteLine("= 0. 나가기             =");
            Console.WriteLine("=========================");
        }


    }


}
