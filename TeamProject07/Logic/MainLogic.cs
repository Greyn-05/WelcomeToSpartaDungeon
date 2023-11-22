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
using TeamProject07.Hotel;

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
        HotelMain hotel = new HotelMain();
        Skill skill = new Skill();

        static public Player player;


        //static public Player player;
        MainGamePhase mainGamePhase = MainGamePhase.temp;

        //프로그램 종료 트리거
        private bool gameEndTrigger = false;

        //게임에 필요한 데이터 모음
        public static Dictionary<int, Skill> skillData;


        public void Start()
        {
            Shop_Init();

            GameTitle();
            //Console.WriteLine("게임 시작부 입니다.");
            //Console.WriteLine("1. 타이틀 화면을 출력합니다.");
            //Console.WriteLine("2. 여기서 이름, 클래스를 입력받습니다.");
            //Console.WriteLine("3. 게임에 필요할 데이터를 로드 합니다. (Monster, Item)");
            //Console.WriteLine("4. 아무 키나 입력 받아서 메인화면으로 넘어가도록 합니다.");
               // 상점 초기화 + 아이템 정보 load
            int selectNorL = SelectNewOrLoad();   // New로 할 것인지 Load로 할 것인지 선택
            if (selectNorL == 1) // New > 새로운 플레이어를 생성
            {
                string TplayerName = InputName();
                string TplayerClass = InputClass();
                if (TplayerClass == "Warrior")
                {
                    player = new Player(TplayerName, TplayerClass, 1, 15, 15, 150, 100, 1000, 20, 20);
                    player.LoadSkills(Define.SkillPathW); // 플레이어 스킬 정보 load
                }
                else if (TplayerClass == "Sorcerer")
                {
                    player = new Player(TplayerName, TplayerClass, 1, 10, 5, 100, 200, 1000, 20, 20);
                    player.LoadSkills(Define.SkillPathS); // 플레이어 스킬 정보 load
                }
            }
            else if (selectNorL == 2)    // Load > 기존의 플레이어 정보를 가져온다.
            {
                player = new Player("", "", 1, 10, 5, 100, 200, 1000, 20, 20);
                string choosenPath = ChooseSaveFile();
                //Console.WriteLine(choosenPath);
                JsonSave.Load(choosenPath);
                if(player.Class == "Warrior")
                {
                    player.LoadSkills(Define.SkillPathW);
                }
                else if (player.Class == "Sorcerer")
                {
                    player.LoadSkills(Define.SkillPathS);
                }

            }

        }

        public void Game()
        {
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine();
                }
                Console.WriteLine("   게임 진행부 입니다.");
                Console.WriteLine();
                Console.WriteLine("   1. 상태창");
                Console.WriteLine("   2. 인벤토리");
                Console.WriteLine("   3. 상점");
                Console.WriteLine("   4. 던전");
                Console.WriteLine("   5. 호텔");
                Console.WriteLine("   0. 게임종료");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\n   개발자 도구");
                Console.WriteLine();
                Console.WriteLine("   11. 스킬정보 확인");
                Console.WriteLine("   12. 모든 아이템 사기");
                Console.WriteLine("   13. 플레이어 공격력 1000000");
                Console.WriteLine("   14. 플레이어 치명차 확률 100%");
                Console.WriteLine("   15. 플레이어 회피 확률 100%");
                Console.WriteLine("   16. 플레이어 체력을 1로");
                Console.WriteLine();
                Textbox();
                Console.WriteLine("      원하시는 행동을 입력해주세요.");
                DrawWindowHigh();
                DrawWindowLow();
                int input = CheckValidInput(0, 16);

                switch (input)
                {
                    case (int)MainGamePhase.Exit:
                        gameEndTrigger = true;
                        break;
                    case (int)MainGamePhase.Status:
                        mainGamePhase = status.test(player);
                        break;
                    case (int)MainGamePhase.Inventory:
                        mainGamePhase = inventory.test(player);
                        break;
                    case (int)MainGamePhase.Shop:
                        mainGamePhase = shop.test();
                        break;
                    case (int)MainGamePhase.Dungeon:
                        mainGamePhase = dungeon.Entrance(player);
                        break;
                    case (int)MainGamePhase.Hotel:
                        mainGamePhase = hotel.Entrance(player);
                        break;
                    case 11:
                        mainGamePhase = player.ShowSkillProto();
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
                    case 16:
                        PlayerHPto1();
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
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("   게임을 종료합니다 !");
            Console.WriteLine("    ________      ________      _____ ______       _______      \r\n|\\   ____\\    |\\   __  \\    |\\   _ \\  _   \\    |\\  ___ \\     \r\n\\ \\  \\___|    \\ \\  \\|\\  \\   \\ \\  \\\\\\__\\ \\  \\   \\ \\   __/|    \r\n \\ \\  \\  ___   \\ \\   __  \\   \\ \\  \\\\|__| \\  \\   \\ \\  \\_|/__  \r\n  \\ \\  \\|\\  \\   \\ \\  \\ \\  \\   \\ \\  \\    \\ \\  \\   \\ \\  \\_|\\ \\ \r\n   \\ \\_______\\   \\ \\__\\ \\__\\   \\ \\__\\    \\ \\__\\   \\ \\_______\\\r\n    \\|_______|    \\|__|\\|__|    \\|__|     \\|__|    \\|_______|\r\n                                                             \r\n                                                             \r\n                                                             \r\n ________      ___      ___  _______       ________          \r\n|\\   __  \\    |\\  \\    /  /||\\  ___ \\     |\\   __  \\         \r\n\\ \\  \\|\\  \\   \\ \\  \\  /  / /\\ \\   __/|    \\ \\  \\|\\  \\        \r\n \\ \\  \\\\\\  \\   \\ \\  \\/  / /  \\ \\  \\_|/__   \\ \\   _  _\\       \r\n  \\ \\  \\\\\\  \\   \\ \\    / /    \\ \\  \\_|\\ \\   \\ \\  \\\\  \\|      \r\n   \\ \\_______\\   \\ \\__/ /      \\ \\_______\\   \\ \\__\\\\ _\\      \r\n    \\|_______|    \\|__|/        \\|_______|    \\|__|\\|__|     ");
            DrawWindowHigh();
            DrawWindowLow();
            Console.ReadLine();
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

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                          __      __       .__                                ___________          ");
            Console.WriteLine("                         /  \\    /  \\ ____ |  |   ____  ____   _____   ____   \\__    ___/___   ");
            Console.WriteLine("                         \\   \\/\\/   // __ \\|  | _/ ___\\/  _ \\ /     \\_/ __ \\    |    | /  _ \\");
            Console.WriteLine("                          \\        /\\  ___/|  |_\\  \\__(  <_> )  Y Y  \\  ___/    |    |(  <_> )");
            Console.WriteLine("                           \\__/\\  /  \\___  >____/\\___  >____/|__|_|  /\\___  >   |____| \\____/");
            Console.WriteLine("                                \\/       \\/          \\/            \\/     \\/");
            Console.WriteLine("     _________                    __           ________   ");
            Console.WriteLine("    /   _____/__________ ________/  |______    \\______ \\  __ __  ____    ____   ____  ____   ____  ");
            Console.WriteLine("    \\_____  \\\\____ \\__  \\\\_  __ \\   __\\__  \\    |    |  \\|  |  \\/    \\  / ___\\_/ __ \\/  _ \\ /    \\");
            Console.WriteLine("    /        \\  |_> > __ \\|  | \\/|  |  / __ \\_  |    `   \\  |  /   |  \\/ /_/  >  ___(  <_> )   |  \\");
            Console.WriteLine("   /_______  /   __(____  /__|   |__| (____  / /_______  /____/|___|  /\\___  / \\___  >____/|___|  /");
            Console.WriteLine("           \\/|__|       \\/                 \\/          \\/           \\//_____/      \\/           \\/");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("   Press Anykey to Start");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            DrawWindowHigh();
            DrawWindowLow();
            Console.ReadLine();


        }

        private string ChooseSaveFile()
        {
            string choosenPath = "";
            int cnt = 0;
            int cnt2 = 1;
            Console.Clear();
            String FolderName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\";

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".json") == 0)
                {
                    cnt++;
                    String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                    String FullFileName = File.FullName;
                    DateTime FileSaveTime = File.CreationTime;
                    

                    Console.Write($"{cnt}. {FileNameOnly}    \t Save time : ");
                    Console.WriteLine($"{FileSaveTime} \n ");
                    
                }
            }
            if(cnt==0)
            {
                Console.WriteLine("세이브 파일이 없습니다!");
                Environment.Exit(0);
            }
            int input = CheckValidInput(1, cnt);

            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".json") == 0)
                {
                    if (cnt2 == input)
                    {
                        String FileNameOnlyC = File.Name.Substring(0, File.Name.Length - 4);
                        choosenPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\" + FileNameOnlyC + "json";
                    }
                    cnt2++;
                }
            }
            return choosenPath;
        }
        private int SelectNewOrLoad()
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("   1. NEW Game");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   2. Load Game");
            Textbox();
            DrawWindowHigh();
            DrawWindowLow();

            switch (CheckValidInput(1, 2))
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
            }
            return 1;
        }

        private string InputName()
        {
            string playerName;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine();
                }
                Console.Write("   플레이어 이름을 알려주세요.\n>> ");
                Textbox();
                DrawWindowHigh();
                DrawWindowLow();
                playerName = Console.ReadLine();
                if ((playerName != null) || (playerName != ""))
                {
                    break;
                }
            }
            return playerName;
        }

        private string InputClass()
        {
            string playerClass = "";
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("   플레이어 직업을 선택하세요.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("   1. Warrior");
            Console.WriteLine();
            Console.WriteLine("   2. Sorcerer");
            Console.WriteLine();
            Textbox();
            DrawWindowHigh();
            DrawWindowLow(); ;
            switch (CheckValidInput(1, 2))
            {
                case 1:
                    playerClass = "Warrior";
                    break;
                case 2:
                    playerClass = "Sorcerer";
                    break;
            }
            return playerClass;
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
                player.Inven.Add(i);
            }
            Console.WriteLine("모든 아이템 추가 완료");
            Thread.Sleep(300);
        }

        private void AttackCheat()
        {
            player.Attack = 1000000;
            Console.WriteLine("공격력 재조정 완료");
            Thread.Sleep(300);
        }

        private void CritCheat()
        {
            player.CritRate = 100;
            Console.WriteLine("치명타 확률 재조정 완료");
            Thread.Sleep(300);
        }

        private void MissCheat()
        {
            player.MissRate = 100;
            Console.WriteLine("회피 확률 재조정 완료");
            Thread.Sleep(300);
        }

        private void PlayerHPto1()
        {
            player.Hp = 1;
            Console.WriteLine("플레이어 체력 재조정 완료");
            Thread.Sleep(300);
        }

        public static void DrawWindowHigh()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            for (int i = 0; i < 116; i++)
            {
                Console.Write("─");
            }
            Console.Write("┐");

            for (int cnt = 1; cnt <= 20; cnt++)
            {
                Console.SetCursorPosition(0, cnt);
                Console.Write("│");
            }
            for (int cnt = 1; cnt <= 20; cnt++)
            {
                Console.SetCursorPosition(117, cnt);
                Console.Write("│");
            }
        }
        public static void DrawWindowLow()
        {

            for (int cnt = 21; cnt <= 37; cnt++)
            {
                Console.SetCursorPosition(0, cnt);
                Console.Write("│");
            }
            for (int cnt = 21; cnt <= 37; cnt++)
            {
                Console.SetCursorPosition(117, cnt);
                Console.Write("│");
            }

            Console.Write("\n└");
            for (int i = 0; i < 116; i++)
            {
                Console.Write("─");
            }
            Console.Write("┘");
            Console.SetCursorPosition(3, 29);
        }
        
        public static void Textbox()
        {
            Console.SetCursorPosition(0, 27);
            for (int i = 2; i < 117; i++)
            {
                Console.SetCursorPosition(i, 27);
                Console.Write("=");
            }
        }
    }
}
