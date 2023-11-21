using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeamProject07.Characters;
using TeamProject07.Dungeon;
using TeamProject07.Utils;
using static TeamProject07.Utils.Define;
using TeamProject07.Skills;
namespace TeamProject07.Controller
{
    enum DungeonEntranceSelect
    {

        exit = 0,
        EnterDungeon,
        UseItem = 2,
    }
    
    internal class DungeonEntrance
    {
        
        public void InDungeon(Monster monster)
        {
            //전투 씬
            //enum switch -> 스킬쓸지 공격할지 방어?회피? 선택지
        }

        void BattleMonster(int level)
        {

        }



/*        public Define.MainGamePhase Entrance(Player player)
        {
            Console.Clear();
            //종욱님 그림
            DungeonEntranceView();
            Define.MainGamePhase choice = MainGamePhase.Main;
            int firstView = (int)DungeonEntranceSelect.EnterDungeon;
            firstView = CheckValidInput(0, 2);
            DungeonEntranceSelect enumValue = (DungeonEntranceSelect)firstView;

            switch (enumValue)
            {
                case DungeonEntranceSelect.EnterDungeon:
                    battleView(player);
                    // 던전입장
                    break;

                case DungeonEntranceSelect.UseItem:
                    Console.WriteLine("아이템사용 ");
                    break;

                case DungeonEntranceSelect.exit:
                    choice = Define.MainGamePhase.Main;
                    // 메인으로 나가기
                    break;

            }
            return choice;
        }*/


        public List<Monster> CreateMonsters { get; set; }
        public Dictionary<int, Monster> monsterData;
        int MonsterNumber;
        public void LoadMonsters()
        {

            string path = MonsterPath;
            monsterData = new Dictionary<int, Monster>();
            monsterData.Clear();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        string[] data = line.Split(',');

                        Monster monster = new Monster(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9]);
                        monsterData.Add(monster.Id, monster);

                    }
                }
            }
        }
        public void StartDungeon(int stage)
        {

            int monsternum = 101;
            if (stage == 2) { monsternum = 201; }            //난이도
            else if (stage == 3) { monsternum = 301; }


            CreateMonsters = new List<Monster>();
            CreateMonsters.Clear();
            Random rand = new Random();
            MonsterNumber = rand.Next(3, 5);    // 3마리~4마리
            int MonsterType;

            for (int i = 0; i < MonsterNumber; i++)
            {

                MonsterType = rand.Next(monsternum, monsternum + 3);   //몬스터 데이터 보고 조정   
                Monster monsterinfo = monsterData[MonsterType];

                Monster m = new Monster(monsterinfo);

                CreateMonsters.Add(m);
                //Console.WriteLine($"LV.{monsterinfo.Level} \t {monsterinfo.Name} \t HP : {monsterinfo.Hp} \t ATK : {monsterinfo.Attack},");
            }
        }

        public void DungeonClear(Monster monster)
        {
            CreateMonsters.Clear();
        }
        public int CheckValidInput(int min, int max)
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

        public void PlayerPhase(Player player)
        {
            Console.Clear();
            
            FirstStageMonsterView();
            Console.WriteLine("\n\t전투가 시작됩니다!!");

            int killMonsterNum = 0;
            while (!player.IsDead)
            {
                Console.WriteLine($"\n\t   {player.Name} 체력 :{player.Hp} \n");
                for (int i = 0; i < CreateMonsters.Count; i++)
                {
                     
                    if (CreateMonsters[i].IsDead == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{i}. {CreateMonsters[i].Name} 사망");
                        Console.ResetColor();
                    }
                    else
                        Console.WriteLine($"{i + 1}. LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");
                }
                Console.WriteLine("\n공격할 몬스터를 선택하세요.");
                Console.WriteLine("0.도망가기");
                int monsterChoice = CheckValidInput(0, MonsterNumber);
                Console.Clear();
                if (monsterChoice == 0) {
                    Run();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                }
                Console.WriteLine($"\n\t   {player.Name} 체력 :{player.Hp} \n");
                //선택된 몬스터의 글자 색이 바뀌는 코드가 추가되면 좋겠어요
                for (int i = 0; i < MonsterNumber; i++)
                {
                    if (CreateMonsters[i].IsDead == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{i+1}. {CreateMonsters[i].Name} 사망");
                        Console.ResetColor();
                    }
                    else if (monsterChoice == i+1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{i+1}. LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i+1}. LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");

                    }

                }
                Console.WriteLine("\n공격할 스킬을 선택하세요.\n");
                Console.WriteLine($"번호 \t이름\t데미지");
                for (int i = 1; i <= player.Skills.Count; i++) { 
                
                Console.WriteLine($"{i}. \t{player.Skills[i].Name} \t{player.Skills[i].Damage}");

                }
                int skillChoice = CheckValidInput(1, player.Skills.Count);
                Console.Clear();
                Console.WriteLine($"\n\t   {player.Name} 체력 :{player.Hp} \n");
                if (CreateMonsters[monsterChoice-1].IsDead == false)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"\n{player.Name} "); Console.ResetColor(); Console.Write("가 공격합니다. ");
                    int damageValue =CreateMonsters[monsterChoice - 1].TakeDamage(player, player.Skills[skillChoice].Damage);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"\n{CreateMonsters[monsterChoice -1].Name} "); Console.ResetColor(); Console.Write("가 ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" {player.Name} "); Console.ResetColor(); Console.Write("에게");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {damageValue} "); Console.ResetColor(); Console.Write($"의 피해를 받았습니다!\n\n");
                    Thread.Sleep(4000);
                    Console.Clear();
                    if (CreateMonsters[monsterChoice-1].IsDead==true)
                    {
                        killMonsterNum++;
                    }
                    if (MonsterNumber == killMonsterNum)
                    {
                        WinBoard(CreateMonsters, player);
                        
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다. 턴이 소모됩니다.");
                }
                monsterPhase(player);
            }
            

        }

        public void monsterPhase(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n몬스터 "); Console.ResetColor(); Console.Write("가 공격합니다. ");
            for (int i = 0; i < CreateMonsters.Count; i++)
            {
                if (!player.IsDead)
                {
                    if (!CreateMonsters[i].IsDead)
                    {
                        int damageValue = player.TakeDamage(CreateMonsters[i], 0);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"\n{player.Name} "); Console.ResetColor(); Console.Write("이/가");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" {CreateMonsters[i].Name} "); Console.ResetColor(); Console.Write("에게");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($" {damageValue} "); Console.ResetColor(); Console.WriteLine($"의 피해를 받았습니다!\n\n");

                        Console.WriteLine($"\t   {player.Name} 체력 :{player.Hp} \n");
                        Thread.Sleep(3000);
                        
                    }
                }
                else {
                    Console.Clear();
                    Console.Write($" {player.Name} 가 사망하였습니다. ");
                    //비석 그림
                    Thread.Sleep(4000);
                    break;
                }
            }
            Console.Clear();

        }

        public void FirstStageMonsterView()
        {
            if ( CreateMonsters == null ) { return; }
            for (int i = 0; i < CreateMonsters.Count; i++)
            {
                if (CreateMonsters[i].Name == "슬라임")
                {
                    Console.WriteLine("\t강한 몬스터가 등장했습니다.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                                  \r\n                                                  \r\n                                                  \r\n                                                  \r\n                                          +#***   \r\n                                         +#+++*%  \r\n                       ..=@@@@@@..       +#+++*%  \r\n                       @%        =@:.....+-       \r\n   :   :.              %                :+.=-     \r\n  +-+:+:-            .%@@@+.          ..=@:#+..   \r\n   +. +.          ..*++++++%@:.       ::::.::::   \r\n  -:=% +.       -@%+++++++++++*@*     @@@@:#@@@   \r\n             .@*+++++++++++++++++#+.    -@:#=     \r\n           :#*+++++++++++++++++++++%+             \r\n         :**+##+++++++++++++++++%%+++@.           \r\n         #*+++#@#++++++++++++%%@*++++*@           \r\n         %++++++#@#+++++++#@**+++++++++#          \r\n       .@++++++++++++++++++++++++++++++#          \r\n       .%*+++++++++***%*++++++++++++++*#          \r\n         #*+++++**##+++*%%**+++++++++*#           \r\n          :##*++++++++++++++++++++*##*.           \r\n             =###################*-               \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == "고스트")
                {
                    Console.WriteLine("\t강한 몬스터가 등장했습니다.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                                                  \r\n                                                  \r\n                                                  \r\n                                                  \r\n                                          +#***   \r\n                                         +#+++*%  \r\n                       ..=@@@@@@..       +#+++*%  \r\n                       @%        =@:.....+-       \r\n   :   :.              %                :+.=-     \r\n  +-+:+:-            .%@@@+.          ..=@:#+..   \r\n   +. +.          ..*++++++%@:.       ::::.::::   \r\n  -:=% +.       -@%+++++++++++*@*     @@@@:#@@@   \r\n             .@*+++++++++++++++++#+.    -@:#=     \r\n           :#*+++++++++++++++++++++%+             \r\n         :**+##+++++++++++++++++%%+++@.           \r\n         #*+++#@#++++++++++++%%@*++++*@           \r\n         %++++++#@#+++++++#@**+++++++++#          \r\n       .@++++++++++++++++++++++++++++++#          \r\n       .%*+++++++++***%*++++++++++++++*#          \r\n         #*+++++**##+++*%%**+++++++++*#           \r\n          :##*++++++++++++++++++++*##*.           \r\n             =###################*-               \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == " 골램 ")
                {
                    
                    
                }
                
                Console.Clear();
                if (CreateMonsters[i].Name == "슬라임")
                {
                    Console.WriteLine("\t강한 몬스터가 등장했습니다.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                           =+.    \r\n                                        =#%*+#=.  \r\n      -    .-                    -=**==.%*++++@:  \r\n    *=:*-.=+.-                 -+.     +*#####*.  \r\n      *-:*.=*                 =:                  \r\n    .-* :.*:                 %*                   \r\n    *.-%:#::#:              .*=                   \r\n    :%    :*          .*#::#+                     \r\n                   .:*@=:.                        \r\n                ..:#+++++%@-.                     \r\n             ..=@+++++++++++#-.            . .    \r\n           .:%++++++++++++++++#:.        ..% %..  \r\n           :@++++++++++++++++++@+                 \r\n          :@++++++++++++++++++++*%-      .:% %::  \r\n        :%**@%+++++++++++++++%#+++%:       : :    \r\n        #*+++#@#+++++++++++%@#*++++@-             \r\n        @+++++*@%++++++++#%#*+++++++%:            \r\n      .*#++++++*##+++++#%%#+++++++++%:            \r\n      :@++++++++++++++++++++++++++++%:            \r\n      .*#++++++++*###*+++++++++++++#*.            \r\n        @+++++++*%+++#%*+++++++++++@-             \r\n        :#*++++*%++++++#%*+++++++*#:              \r\n          :%*+++++++++++++++++*%%:                \r\n            :@@@@@@@@@@@@@@@@@+                   \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == "고스트")
                {
                    Console.WriteLine("\t강한 몬스터가 등장했습니다.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                                           =+.    \r\n                                        =#%*+#=.  \r\n      -    .-                    -=**==.%*++++@:  \r\n    *=:*-.=+.-                 -+.     +*#####*.  \r\n      *-:*.=*                 =:                  \r\n    .-* :.*:                 %*                   \r\n    *.-%:#::#:              .*=                   \r\n    :%    :*          .*#::#+                     \r\n                   .:*@=:.                        \r\n                ..:#+++++%@-.                     \r\n             ..=@+++++++++++#-.            . .    \r\n           .:%++++++++++++++++#:.        ..% %..  \r\n           :@++++++++++++++++++@+                 \r\n          :@++++++++++++++++++++*%-      .:% %::  \r\n        :%**@%+++++++++++++++%#+++%:       : :    \r\n        #*+++#@#+++++++++++%@#*++++@-             \r\n        @+++++*@%++++++++#%#*+++++++%:            \r\n      .*#++++++*##+++++#%%#+++++++++%:            \r\n      :@++++++++++++++++++++++++++++%:            \r\n      .*#++++++++*###*+++++++++++++#*.            \r\n        @+++++++*%+++#%*+++++++++++@-             \r\n        :#*++++*%++++++#%*+++++++*#:              \r\n          :%*+++++++++++++++++*%%:                \r\n            :@@@@@@@@@@@@@@@@@+                   \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == " 골램 ")
                {
                   
                }

                Console.Clear();

            }
        }

        public void WinBoard(List<Monster> monster, Player player)
        {
            int rewardGold= 0;
            int rewardExp= 0;
            for (int i = 0; i < monster.Count; i++)
            {
                rewardGold = rewardGold + monster[i].Gold ;
                rewardExp = rewardGold + monster[i].RewardExp;
            }
            player.Gold += rewardGold;
            player.LevelUpExp += rewardExp;
            Console.WriteLine($"\n\t\t{player.Name}님 승리하셨습니다!\n");
            
            Console.WriteLine($"\t획득한 골드 : "); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{rewardGold}"); Console.ResetColor();
            Console.WriteLine($"\t획득한 EXP  : "); Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"{rewardExp}\n\n"); Console.ResetColor();
            Console.WriteLine($"\t{player.Name} 정보");
            Console.WriteLine($"\tGold  : {player.Gold}");
            Console.WriteLine($"\tLevel : {player.Level} , EXP : {player.LevelUpExp}");
            
            Console.WriteLine("\n\n\t\t0.돌아가기");
            switch (CheckValidInput(0, 0))
            {
                case 0
                :
                    //dungeonEnter();
                    break;
            }
            // stage++;
        }

        public void DungeonEntranceView()
        {
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

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("===================== RUN ========================");
            Console.WriteLine("==================================================");
            Console.WriteLine("  ┏   ┓             ; ◆ ");
            Console.WriteLine(" |      |        ==  ┌┼┘  ");
            Console.WriteLine("|        |       ==   │┒  ");
            Console.WriteLine("==================================================");
        }
    }
}
