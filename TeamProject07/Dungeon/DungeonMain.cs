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
using TeamProject07.Skills;
using System.Threading;

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
            Dungeon.LoadMonsters();
            if (player.IsDead==true)
            {
                Console.Clear();
                Console.WriteLine("   \n\t휴식 후 다시오세요");
                MainLogic.DrawWindowLow();
                Thread.Sleep(700);
                choicePhase = Define.MainGamePhase.Main;
                return choicePhase;
            }
            DungeonEntranceView();
            MainLogic.Textbox();
            Console.WriteLine("     던전에 들어가시겠습니까?");
            MainLogic.DrawWindowHigh();
            MainLogic.DrawWindowLow();
            int input = CheckValidInput(0, 1);
            
            switch (input)
            {
                case 0:
                    Dungeon.Run();
                    MainLogic.DrawWindowHigh();
                    MainLogic.DrawWindowLow();
                    Thread.Sleep(700);
                    choicePhase = Define.MainGamePhase.Main;
                    break;
                case 1:
                    Console.Clear();
                    choicePhase = Define.MainGamePhase.temp;
                    choicePhase = DungeonDifSelect(player);
                    if (choicePhase == Define.MainGamePhase.Main)
                    {
                        break;
                    }
                    Console.WriteLine();
                    Dungeon.PlayerPhase(player);
                    Console.WriteLine();
                    //Thread.Sleep(1000);  
                    break;
            }
            
            return choicePhase;
        }

        public Define.MainGamePhase DungeonDifSelect(Player player)
        {
            DungeonSelectView();

            MainLogic.Textbox();
            Console.WriteLine("     입장할 던전을 선택하세요.");
            MainLogic.DrawWindowHigh();
            MainLogic.DrawWindowLow();


            int input = CheckValidInput(0, 4);
            Console.Clear();
            switch (input)
            {
                case 0:
                    choicePhase = Define.MainGamePhase.Main;
                    Dungeon.Run();
                    MainLogic.DrawWindowHigh();
                    MainLogic.DrawWindowLow();
                    Thread.Sleep(700);
                    Console.Clear();
                    break;
                case 1:
                    Dungeon.StartDungeon(input);
                    break;
                case 2:
                    Dungeon.StartDungeon(input);
                    break;
                case 3:
                    Dungeon.StartDungeon(input);
                    break;
                case 4:
                    Dungeon.StartDungeon(input);
                    break;
            }
            return choicePhase;
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

                Console.WriteLine("   잘못된 입력입니다.");

            }
        }

        private void DungeonEntranceView()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   ==================================================");
            Console.WriteLine("     ┏   ┓             ◆ ;");
            Console.WriteLine("    |      |          └┼┐ == ");
            Console.WriteLine("   |        |         ┌│  ==");
            Console.WriteLine("   ==================================================\n\n");
            Console.WriteLine("   ==================");
            Console.WriteLine("   = 1. 던전 입장   =");
            Console.WriteLine("   = 0. 나가기      =");
            Console.WriteLine("   ==================");

        }

        

        private void DungeonSelectView()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   ==================================================");
            Console.WriteLine("     ┏   ┓             ◆ ;");
            Console.WriteLine("    |      |          └┼┐ == ");
            Console.WriteLine("   |        |         ┌│  ==");
            Console.WriteLine("   ==================================================\n\n");
            Console.WriteLine("   ==================");
            Console.WriteLine("   = 1. 초원 필드   =");
            Console.WriteLine("   = 2. 묘지 필드   =");
            Console.WriteLine("   = 3. 사원 필드   =");
            Dungeon.RedText("   = 4. 용의 은신처 =");
            Console.WriteLine("   = 0. 나가기      =");
            Console.WriteLine("   ==================");

        }
    }
}
