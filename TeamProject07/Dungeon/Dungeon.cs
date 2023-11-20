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
        public Player player = new Player("1", 1, 1, 1, 1, 1, 1, 1);
        
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
        public void LoadMosters()
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

            int monsternum = 1;
            if (stage == 2) { monsternum = 4; }            //난이도
            else if (stage == 3) { monsternum = 7; }

            CreateMonsters = new List<Monster>();
            CreateMonsters.Clear();
            Random rand = new Random();
            int MonsterNumber = rand.Next(3, 4);    // 3마리~4마리
            int MonsterType;

            for (int i = 0; i < MonsterNumber; i++)
            {

                MonsterType = rand.Next(monsternum, monsternum + 3);   //몬스터 데이터 보고 조정   
                Monster monsterinfo = monsterData.ElementAt(MonsterType).Value;
                CreateMonsters.Add(monsterinfo);
                Console.WriteLine($"LV.{monsterinfo.Level} \t {monsterinfo.Name} \t HP : {monsterinfo.Hp} \t ATK : {monsterinfo.Attack},");
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
            for (int i = 0; i < CreateMonsters.Count; i++)
            {
                Console.WriteLine($"LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack},");
            }
            while (!player.IsDead && MonsterNumber != 0)//&& !CreateMonsters[0].IsDead
            {


                Console.WriteLine("\n전투가 시작됩니다!!");
                Console.WriteLine("공격할 몬스터를 선택하세요.");
                int monsterChoice = CheckValidInput(0, MonsterNumber);
                Console.Clear();
                //선택된 몬스터의 글자 색이 바뀌는 코드가 추가되면 좋겠어요
                for (int i = 0; i < MonsterNumber; i++)
                {
                    if (monsterChoice == i + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"LV.{CreateMonsters[i].Level} \t {CreateMonsters[i].Name} \t HP : {CreateMonsters[i].Hp} \t ATK : {CreateMonsters[i].Attack}");
                    }
                }
                Console.WriteLine("공격할 스킬을 선택하세요.");
                int skillChoice = CheckValidInput(1, player.Skills.Count);
                Console.Clear();
                // int skillChoice = CheckValidInput(1, player.Skills.Count);
                /*for (int i = 0; i < MonsterNumber; i++)
                {
                    switch (skillChoice)
                    {
                        case 1:
                            //스킬 받는피해 코드
                            break;
                    }
                }*/
                if (CreateMonsters[monsterChoice].IsDead != false)
                {
                    CreateMonsters[monsterChoice].TakeDamage(player, player.Skills[skillChoice - 1].Damage);
                }
                else
                {
                    Console.WriteLine("올바른 몬스터를 선택하십시오");
                }
            }

        }

        public void WinBoard(List<Monster> monster, Player player)
        {
            int rewardGold;
            int rewardExp;

            rewardGold = monster[0].Gold + monster[1].Gold + monster[2].Gold;
            rewardExp = monster[0].RewardExp + monster[1].RewardExp + monster[2].RewardExp;
            player.Gold += rewardGold;
            player.LevelUpExp += rewardExp;
            Console.WriteLine("승리하셨습니다!");
            Console.WriteLine($"획득한 골드 : {0}", rewardGold);
            Console.WriteLine($"획득한 경험치 : {0}", rewardExp);
            Console.WriteLine("0.돌아가기");
            switch (CheckValidInput(0, 0))
            {
                case 0
                :
                    //dungeonEnter();
                    break;
            }
            // stage++;
        }
        public void LoseBoard()
        {
            Console.WriteLine("실패하셨습니다!");
            Console.WriteLine("0.돌아가기");
            switch (CheckValidInput(0, 0))
            {
                case 0
                :
                    //dungeonEnter();
                    break;
            }
            Console.WriteLine("키 입력으로  던전 입구로 돌아가기");
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
    }
}
