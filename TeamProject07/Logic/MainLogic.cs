using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Status;
using TeamProject07.Shop;
//using TeamProject07.Dungeon;
using TeamProject07.Utils;
using static TeamProject07.Utils.Define;
using TeamProject07.Dungeon;
using TeamProject07.Inventroy;
using TeamProject07.Skills;
using TeamProject07.Characters;
using TeamProject07.Controller;

namespace TeamProject07.Logic
{
    

    internal class MainLogic
    {
        //필요한 객체 선언
        StatusMain status = new StatusMain();
        ShopMain shop = new ShopMain();
        DungeonMain dungeon = new DungeonMain();
        InvenMain inventory = new InvenMain();
        Define define = new Define();
        Skill skill = new Skill();
       
        static public Player dummy = new Player("KIm", 1, 5, 5, 20, 10000, 50, 50);
        
        MainGamePhase mainGamePhase = MainGamePhase.temp;

        //프로그램 종료 트리거
        private bool gameEndTrigger = false;

        //게임에 필요한 데이터 모음
        public static Dictionary<int, Skill> skillData;
        
        
        public void Start()
        {
            GameTitle();
            Console.WriteLine("게임 시작부 입니다.");
            Console.WriteLine("1. 타이틀 화면을 출력합니다.");
            Console.WriteLine("2. 여기서 이름, 클래스를 입력받습니다.");
            Console.WriteLine("3. 게임에 필요할 데이터를 로드 합니다. (Monster, Item)");
            Console.WriteLine("4. 아무 키나 입력 받아서 메인화면으로 넘어가도록 합니다.");

            
            Console.ReadLine();
            Shop_Init();    // 상점 초기화 + 아이템 정보 load
            dummy.LoadSkills();
        }

        public void Game()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("게임 진행부 입니다.");
                Console.WriteLine("1. 상태창");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전");
                Console.WriteLine("0. 게임종료");
                Console.WriteLine("\n개발자 도구");
                Console.WriteLine("11. 스킬정보 확인");
                Console.WriteLine("12. 모든 아이템 사기");
                Console.WriteLine("13. 플레이어 공격력 1000000");
                Console.WriteLine("14. 플레이어 치명차 확률 100%");
                Console.WriteLine("15. 플레이어 회피 확률 100%");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = CheckValidInput(0, 15);
                switch (input)
                {
                    case (int)MainGamePhase.Exit:
                        gameEndTrigger = true;
                        break;
                    case (int)MainGamePhase.Status:
                        mainGamePhase = status.test(dummy);
                        break;
                    case (int)MainGamePhase.Inventory:
                        mainGamePhase = inventory.test(dummy);
                        break;
                    case (int)MainGamePhase.Shop:
                        mainGamePhase = shop.test();
                        break;
                    case (int)MainGamePhase.Dungeon:
                        mainGamePhase = dungeon.Entrance(dummy);
                        break;
                    case 11:
                        mainGamePhase = dummy.ShowSkillProto();
                        break;
                    case 12:
                        itemcheat();
                        break;
                    case 13:
                        AttackCheat();
                        break;
                    case 14:
                        CritCheat();
                        break;
                    case 15:
                        MissCheat();
                        break;
                }

                if (gameEndTrigger == true)
                {
                    break;
                }
            }

        }

        public void End()
        {
            Console.Clear();
            Console.WriteLine("게임 종료부 입니다.");
            Console.WriteLine("아직 예정된 컨텐츠는 없습니다.");
            Console.WriteLine("추가한다면 데이터 저장을 할 예정입니다.\n");
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

        private void GameTitle()
        {
            Console.WriteLine("                       __      __       .__                                ___________          ");
            Console.WriteLine("                      /  \\    /  \\ ____ |  |   ____  ____   _____   ____   \\__    ___/___   ");
            Console.WriteLine("                      \\   \\/\\/   // __ \\|  | _/ ___\\/  _ \\ /     \\_/ __ \\    |    | /  _ \\");
            Console.WriteLine("                       \\        /\\  ___/|  |_\\  \\__(  <_> )  Y Y  \\  ___/    |    |(  <_> )");
            Console.WriteLine("                        \\__/\\  /  \\___  >____/\\___  >____/|__|_|  /\\___  >   |____| \\____/");
            Console.WriteLine("                             \\/       \\/          \\/            \\/     \\/");
            Console.WriteLine("  _________                    __           ________   ");
            Console.WriteLine(" /   _____/__________ ________/  |______    \\______ \\  __ __  ____    ____   ____  ____   ____  ");
            Console.WriteLine(" \\_____  \\\\____ \\__  \\\\_  __ \\   __\\__  \\    |    |  \\|  |  \\/    \\  / ___\\_/ __ \\/  _ \\ /    \\");
            Console.WriteLine(" /        \\  |_> > __ \\|  | \\/|  |  / __ \\_  |    `   \\  |  /   |  \\/ /_/  >  ___(  <_> )   |  \\");
            Console.WriteLine("/_______  /   __(____  /__|   |__| (____  / /_______  /____/|___|  /\\___  / \\___  >____/|___|  /");
            Console.WriteLine("        \\/|__|       \\/                 \\/          \\/           \\//_____/      \\/           \\/");
        }

        static void Shop_Init()
        {
            ItemData.Init();
            ShopData.Init();
            Shop_Normal.Init();
            Shop_Reseller.Init();
        }

        //치트, 나중에 삭제
        private void itemcheat()
        {
            foreach (Item i in ItemData.items.Values)
            {
                dummy.Inven.Add(i);
            }
            Console.WriteLine("모든 아이템 추가 완료");
            Thread.Sleep(300);
        }

        private void AttackCheat()
        {
            dummy.Attack = 1000000;
            Console.WriteLine("공격력 재조정 완료");
            Thread.Sleep(300);
        }

        private void CritCheat()
        {
            dummy.CritRate = 100;
            Console.WriteLine("치명타 확률 재조정 완료");
            Thread.Sleep(300);
        }

        private void MissCheat()
        {
            dummy.MissRate = 100;
            Console.WriteLine("회피 확률 재조정 재조정 완료");
            Thread.Sleep(300);
        }
    }
}
