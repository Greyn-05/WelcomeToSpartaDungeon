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
       
       static public Player dummy = new Player("KIm", 1, 5, 5, 10, 10000, 50, 50);
        
        MainGamePhase mainGamePhase = MainGamePhase.Main;

        //프로그램 종료 트리거
        private bool gameEndTrigger = false;

        //게임에 필요한 데이터 모음
        public static Dictionary<int, Skill> skillData;
        
        
        public void Start()
        {
            Console.WriteLine("게임 시작부 입니다.");
            Console.WriteLine("1. 타이틀 화면을 출력합니다.");
            Console.WriteLine("2. 여기서 이름, 클래스를 입력받습니다.");
            Console.WriteLine("3. 게임에 필요할 데이터를 로드 합니다. (Monster, Item)");
            Console.WriteLine("4. 아무 키나 입력 받아서 메인화면으로 넘어가도록 합니다.");

            
            Console.ReadLine();
            skill.LoadSkills(); //스킬정보 로드
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
                Console.WriteLine("12. 아이템정보 확인");
                Console.WriteLine("13. 상점 정보 확인");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = CheckValidInput(0, 11);
                switch (input)
                {
                    case (int)MainGamePhase.Exit:
                        gameEndTrigger = true;
                        break;
                    case (int)MainGamePhase.Status:
                        mainGamePhase = status.test();
                        break;
                    case (int)MainGamePhase.Inventory:
                        mainGamePhase = inventory.test();
                        break;
                    case (int)MainGamePhase.Shop:
                        mainGamePhase = shop.test();
                        break;
                    case (int)MainGamePhase.Dungeon:
                        mainGamePhase = dungeon.test();
                        break;
                    case 11:
                        mainGamePhase = skill.ShowSkillProto();
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
    }
}
