
using TeamProject07.Characters;
using static TeamProject07.Utils.Define;
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
            else if (stage == 4) { monsternum = 400; }

            CreateMonsters = new List<Monster>();
            CreateMonsters.Clear();
            Random rand = new Random();
            MonsterNumber = rand.Next(3, 5);    // 3마리~4마리
            int MonsterType;
            Monster m;
            for (int i = 0; i < MonsterNumber; i++)
            {
                if (monsternum == 400)
                {
                    MonsterType = 400;
                    MonsterNumber = 1;
                    Monster monsterinfo = monsterData[MonsterType];
                    m = new Monster(monsterinfo);
                } else { 
                MonsterType = rand.Next(monsternum, monsternum + 3);   //몬스터 데이터 보고 조정   
                Monster monsterinfo = monsterData[MonsterType];

                m = new Monster(monsterinfo);
                }

                CreateMonsters.Add(m);
                //Console.WriteLine($"LV.{monsterinfo.Level} \t {monsterinfo.Name} \t HP : {monsterinfo.Hp} \t ATK : {monsterinfo.Attack},");
            }
        }
        public void PlayerPhase(Player player)
        {
            Console.Clear();
            
            StageMonsterView();
            Console.WriteLine("\n\t전투가 시작됩니다!!");

            int killMonsterNum = 0;
            while (!player.IsDead)
            {
                Console.WriteLine($"\n\t   {player.Name} 체력 :{player.Hp}    공격력 : {player.Attack} \n");
                for (int i = 0; i < CreateMonsters.Count; i++)
                {
                     
                    if (CreateMonsters[i].IsDead == true)
                    {
                        RedText($"{CreateMonsters[i].Name} 사망");
                    }
                    else
                        Console.WriteLine($"{i + 1}. LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");
                }
                Console.WriteLine("\n0.도망가기");
                Console.WriteLine("공격할 몬스터를 선택하세요.");
                
                int monsterChoice = CheckValidInput(0, MonsterNumber);
                Console.Clear();
                if (monsterChoice == 0) {
                    Run();
                    Thread.Sleep(200);
                    Console.Clear();
                    break;
                }
                //Console.WriteLine($"\n\t   {player.Name} 체력 :{player.Hp} \n");
                //선택된 몬스터의 글자 색이 바뀌는 코드가 추가되면 좋겠어요
                for (int i = 0; i < MonsterNumber; i++)
                {
                    if (CreateMonsters[i].IsDead == true)
                    {
                        RedText($"{i + 1}. {CreateMonsters[i].Name} 사망");
                    }
                    else if (monsterChoice == i+1)
                    {
                        Bluetext($"{i + 1}. LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");
                    }
                    else
                    {
                        Console.WriteLine($"{i+1}. LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");

                    }

                }
                Console.WriteLine($"\n\n\t   {player.Name} 체력 :{player.Hp}    마나 : {player.Mp}");
                Console.WriteLine("\n공격할 스킬을 선택하세요.\n");
                Console.WriteLine($"번호 \t이름\t 공+ (스킬데미지)\t소모MP");
                for (int i = 1; i <= player.Skills.Count; i++) { 
                
                Console.WriteLine($"{i}. \t{player.Skills[i].Name} \t {player.Attack} + ({player.Skills[i].Damage})          \t{player.Skills[i].Mp}");

                }
                int skillChoice = CheckValidInput(1, player.Skills.Count);
                Console.Clear();
                if(player.Mp <player.Skills[skillChoice].Mp) 
                {
                    Console.WriteLine("MP가 부족합니다. 평타로 공격합니다.");
                    Thread.Sleep(1500);
                    skillChoice = 1;
                }

                if (CreateMonsters[monsterChoice-1].IsDead == false)
                {
                    player.Mp -= player.Skills[skillChoice].Mp;
                    BluetextNo($"\n{player.Name} "); Console.Write("가 공격합니다. ");
                    int damageValue =CreateMonsters[monsterChoice - 1].TakeDamage(player, player.Skills[skillChoice].Damage);
                    RedTextNo($"\n{CreateMonsters[monsterChoice - 1].Name} "); Console.Write("가 ");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {damageValue} "); Console.ResetColor(); Console.Write($"의 피해를 받았습니다!\n");
                    RedTextNo($"\n{CreateMonsters[monsterChoice - 1].Name} "); Console.Write($"남은Hp : {CreateMonsters[monsterChoice - 1].Hp}\n\n");
                    Thread.Sleep(1700);
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
            RedTextNo($"\n몬스터 "); Console.Write("가 공격합니다. ");
            for (int i = 0; i < CreateMonsters.Count; i++)
            {
                if (!player.IsDead)
                {
                    if (!CreateMonsters[i].IsDead)
                    {
                        int damageValue = player.TakeDamage(CreateMonsters[i], 0);
                        BluetextNo($"\n{player.Name} "); Console.Write("이/가");
                        RedTextNo($" {CreateMonsters[i].Name} "); Console.Write("에게");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($" {damageValue} "); Console.ResetColor(); Console.WriteLine($"의 피해를 받았습니다!\n\n");

                        Console.WriteLine($"\t   {player.Name} 체력 :{player.Hp} \n");
                        Thread.Sleep(300);

                    }
                }
                if (player.IsDead)
                {
                    Console.Clear();
                    Console.Write($" {player.Name} 가 치명상을 입었습니다. 휴식이 필요합니다. ");
                    //비석 그림
                    Thread.Sleep(1500);
                    break;
                }
            }
            Thread.Sleep(500);
            Console.Clear();

        }
        public void StageMonsterView()
        {
            if ( CreateMonsters == null ) { return; }
                viewMonster();
        }
        private void viewMonster()
        {
            for (int i = 0; i < CreateMonsters.Count; i++)
            {
                if (CreateMonsters[i].Name == "슬라임")
                {
                    RedText("\t슬라임이 나타났습니다!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                                  \r\n                                                  \r\n                                                  \r\n                                                  \r\n                                          +#***   \r\n                                         +#+++*%  \r\n                       ..=@@@@@@..       +#+++*%  \r\n                       @%        =@:.....+-       \r\n   :   :.              %                :+.=-     \r\n  +-+:+:-            .%@@@+.          ..=@:#+..   \r\n   +. +.          ..*++++++%@:.       ::::.::::   \r\n  -:=% +.       -@%+++++++++++*@*     @@@@:#@@@   \r\n             .@*+++++++++++++++++#+.    -@:#=     \r\n           :#*+++++++++++++++++++++%+             \r\n         :**+##+++++++++++++++++%%+++@.           \r\n         #*+++#@#++++++++++++%%@*++++*@           \r\n         %++++++#@#+++++++#@**+++++++++#          \r\n       .@++++++++++++++++++++++++++++++#          \r\n       .%*+++++++++***%*++++++++++++++*#          \r\n         #*+++++**##+++*%%**+++++++++*#           \r\n          :##*++++++++++++++++++++*##*.           \r\n             =###################*-               \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == "고스트")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t강한 몬스터가 등장합니다.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("       .=-                                \r\n      :=%                                 \r\n     .-=##                                \r\n   :=+-==.                                \r\n   *--==.                                 \r\n .-#---+.                                 \r\n.+=-=+-+:       ..:=--:..                 \r\n.*++..++#     .:=+:.  .+++..              \r\n.+=+*%===    .*-.*%*:  ..:*:              \r\n  -****:   .=*.%@@@@- +@@*.+.             \r\n          .=- :@@@@+  *@@@=.%.       .:.  \r\n          =:     ..***:.*#:.==       -*.  \r\n         -=    .=%@@@@@.    .*      .*=+- \r\n        .+-    .=#@@%@@.    .*     =+-+=  \r\n        -+            ..    .*    .:*-+:  \r\n       -:                   .*    -+--=+. \r\n      :=.                   .*    :=--+=  \r\n     .=+.                   .*   .*=***++:\r\n     :+.                    .*   .+=+=++=.\r\n    -=                      .*    .:---.  \r\n   :#:                      .*            \r\n   -* ..-%=-:.              .*            \r\n   +#===. ..+.              .*            \r\n            .#+   .*#*=*=   .*            \r\n              .=++.     .+=..*            \r\n                         .=++=            ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == " 골램  ")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t고스트가 나타났습니다!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                          \r\n                                          \r\n                                          \r\n              ..............              \r\n          .:==+************@=+.           \r\n        ..=%*----++++++++----#*=.         \r\n       .:##---*@*--------*@*---##         \r\n       :*#=--=%------------@---=#:        \r\n       :*+----%%----------=@----+*        \r\n       :*%=----%%=------=%------+*        \r\n        :*+------=%%%%%%%=-----=#         \r\n         :*=-------------------@*         \r\n          :#=-----------------@%          \r\n        .:#@@@%-------------@@@%.         \r\n       :*@@%-----+@@@@@@@@#***@@@*        \r\n     :*%+=#=-------+%*********#%-*@+      \r\n   .+%+-=%@@@@@@@@@@@@@@@@@@@@@@#-+@@-    \r\n  .+*=#@%#*****#@+++++++++@-----+@#--#-   \r\n .+#---=##*****#@+++++++++@-----+#---=%:  \r\n .+*--=#%##%+++++++++#%*******#@*#*---#:  \r\n +@%#+=##*#%---------*#+++++++*@+##--*%@: \r\n +*-=+*##*#%---------*#+++++++*@+#%#*+-%: \r\n-*=---=#*+++*@*******##%%########%#----+*.\r\n:==+####+---=%+++++++++%#********#%###+==.\r\n  ....:*#####@%%%%%%%%%@%%%%%%%%%@*....   \r\n      :*#++++++++*#----------@+++#*       \r\n      :**++++++++*#----------@+++#*       \r\n      .=+++++++++++++++++++++*++++-       ");
                    Console.ResetColor();
                    Thread.Sleep(400);

                }
                else if (CreateMonsters[i].Name == "드래곤")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    for (int j = 0; j < 15; j++)
                    {
                        Console.WriteLine("\t보스(드래곤) 출현"); Thread.Sleep(30);
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                                                  \r\n                                                  \r\n                         @@     @@                \r\n                    @   @@      @##%              \r\n                  %@%%%@%@   @%##%@@              \r\n                 %*## @ @@%@%#*%%@      @@        \r\n               %#%**%     %*#*%#%@     #%         \r\n             ##%#*#%*    ******%%@    %*#%        \r\n          %##%%**#      #*#*###%%%  @#*#%%@       \r\n        %##%%%***@     *****@ @#%@@@@ #*%%%       \r\n        #%%%%#****%% @***#***# %%@   #*%%%%%      \r\n      %#%%%%%%##*********##****####%#*%%%%%%%     \r\n      #%%%%%%%%%%%%%#***********#**##%%%%%%%%     \r\n     #%%%%%%%%%%%%%%%%**#*****#*#%%%%%%%%%%%%     \r\n    ##%%%%%%%%%%%%%%%%%#********%%%%%%%%%%%%%     \r\n    %%%%%%%%%%%%@@     @********#@@%%%%%%%%%%     \r\n    %%%%%%%%%%@@ %#*************@   @%%%%%%%%     \r\n    %%%%%%%%@%##********##*****%    @  @%%%#      \r\n    %%%%%%@ %#********#******#%        @%%%%      \r\n    %%%@    #**#***********##%           %%       \r\n    %%%    @*******##*###****##         @@        \r\n     %%     #****%%       %***#         @@        \r\n     @@    @**#*##@      @**#@                    \r\n      @    #**%%%%%%%%%@@#*#%@%                   \r\n          @*++*++@       #####*@                  \r\n            %                                     \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(1500);

                }

                Console.Clear();
                if (CreateMonsters[i].Name == "슬라임")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t슬라임이 나타났습니다!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                           =+.    \r\n                                        =#%*+#=.  \r\n      -    .-                    -=**==.%*++++@:  \r\n    *=:*-.=+.-                 -+.     +*#####*.  \r\n      *-:*.=*                 =:                  \r\n    .-* :.*:                 %*                   \r\n    *.-%:#::#:              .*=                   \r\n    :%    :*          .*#::#+                     \r\n                   .:*@=:.                        \r\n                ..:#+++++%@-.                     \r\n             ..=@+++++++++++#-.            . .    \r\n           .:%++++++++++++++++#:.        ..% %..  \r\n           :@++++++++++++++++++@+                 \r\n          :@++++++++++++++++++++*%-      .:% %::  \r\n        :%**@%+++++++++++++++%#+++%:       : :    \r\n        #*+++#@#+++++++++++%@#*++++@-             \r\n        @+++++*@%++++++++#%#*+++++++%:            \r\n      .*#++++++*##+++++#%%#+++++++++%:            \r\n      :@++++++++++++++++++++++++++++%:            \r\n      .*#++++++++*###*+++++++++++++#*.            \r\n        @+++++++*%+++#%*+++++++++++@-             \r\n        :#*++++*%++++++#%*+++++++*#:              \r\n          :%*+++++++++++++++++*%%:                \r\n            :@@@@@@@@@@@@@@@@@+                   \r\n                                                  ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == "고스트")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t고스트가 나타났습니다!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                          \r\n                              .*.         \r\n                             .#**:        \r\n                             .:*-*.       \r\n                             .:+-=..      \r\n                             .+*-+*-.     \r\n                ..::::.        *+:%==.    \r\n              .**.   .:++:.   .+=+==.     \r\n             -*:... .+#*----.             \r\n.--.        :=.+@@@.:%@@@#.:=.            \r\n  -%%-      -:-@@@@.   =*:  -=.           \r\n .+=#.      +.#@%:.:%@@#.   ..*.          \r\n .==-=.     *     +@@@@@%:   .-:          \r\n.-#=-+:     *    :#%#+....     ++.        \r\n.=+--*-     *:                  =:        \r\n :+--+%*.   -:                  :#:.      \r\n.++#+=+-*   -:                   .@-      \r\n.*=+++*+-   -:                    .+:     \r\n .-*##*.    -:.                    :=.    \r\n            :=.                     :#.   \r\n            :=.                      :+   \r\n            :=.             .:*+===#=:#:  \r\n            :+.  :*=-+=.   .++.    ....   \r\n            .+- -*.   .:###*..            \r\n             .+=:.                        ");
                    Console.ResetColor();
                    Thread.Sleep(400);
                }
                else if (CreateMonsters[i].Name == " 골램  ")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t골렘이 나타났습니다.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  .========:.                 .:========:.\r\n .++=-----*+.                 .+*------#*:\r\n :#-------++.                 .++-------#-\r\n :#-------++.-*+++++++++++*%*+.++-------#-\r\n :##%#####%%*=---*#######=---++%#+######+.\r\n .=+-----+#=--*%*--------+%#---#+------#-.\r\n  -*----=#=---#*----------=%---*%=-----#-.\r\n  :*=---=#=---=#*--------=*#----#%+===+#-.\r\n   =*++**%#----=**++++++##=----=*+==+#%=. \r\n   .**=--=#*------=======------##----=#-. \r\n    +#=---##=-----------------#*+*++*%=.  \r\n    .*+-+*+-*@#=------------*@#----*+..   \r\n     -%#---**--+*#%#*****%%%%%%=-+@+.     \r\n       =%##*-------+%*********#%%-.       \r\n         -%%*******#%%%%%%%###%@=.        \r\n        .*#*****%*+++++++*%----=*:..:.    \r\n      ..:#######%#********%=====#=.....   \r\n      ..*##%#+=======*%*******#%*@: ...   \r\n       .##*%*--------+#+++++++##+%:       \r\n       .%##%#++++++++#%*******#%*@:       \r\n       .#=---##++++++++##********@::.     \r\n       .#=---##++++++++##********@:       \r\n       .%%%%%%%%%%%##########@%%%@:       \r\n       .#*+++++++*#=--------=%+++%:       \r\n       .###########**********%###%:       ");
                    Console.ResetColor();
                    Thread.Sleep(400);
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
            
            Console.Write($"\t획득한 골드 : "); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"{rewardGold}\n"); Console.ResetColor();
            Console.Write($"\t획득한 EXP  : "); Console.ForegroundColor = ConsoleColor.Blue; Console.Write($"{rewardExp}\n\n"); Console.ResetColor();
            LevelUp(player);
            Console.WriteLine($"\n\t{player.Name} 정보");
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
        public void LevelUp(Player player) {
            int levelUpPoint = player.Level* player.Level * 100;
            int UpHp;
            int UpDefence ;
            int UpAttack;
            while (player.LevelUpExp > levelUpPoint)
            {
                player.Level++;
                // 수치 조정
                UpHp = player.Level * 5;
                UpDefence = 2;
                UpAttack =  player.Level * 1;
                player.MaxHp += UpHp;
                player.Hp = player.MaxHp;
                player.MaxMp += 10;
                player.Mp = player.MaxMp;
                player.Defence += UpDefence;
                player.Attack += UpAttack;

                player.LevelUpExp -= levelUpPoint;
                Console.WriteLine($"레벨업! Level : {player.Level} 가 되었습니다.");
                Console.WriteLine($"체력 + {UpHp}  공격력 + {UpAttack} 방어력 + {UpDefence}");
                Thread.Sleep(400);
                levelUpPoint = player.Level * player.Level * 100;
            }
        }

        
        public void Run()
        {
            Console.Clear();
            Console.WriteLine("===================== RUN ========================");
            Console.WriteLine("==================================================");
            Console.WriteLine("  ┏   ┓           ; ◆ ");
            Console.WriteLine(" |      |        ==┌┼┘  ");
            Console.WriteLine("|        |       == │┒  ");
            Console.WriteLine("==================================================");
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
        public void RedText(String s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{s}");
            Console.ResetColor();
        }
        public void RedTextNo(String s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{s}");
            Console.ResetColor();
        }
        public void Bluetext(String s)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{s}");
            Console.ResetColor();
        }
        public void BluetextNo(String s)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{s}");
            Console.ResetColor();
        }
    }
}
