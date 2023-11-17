using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeamProject07.Characters;

namespace TeamProject07.Controller
{
    internal class BattleController
    {

        private BattleView _battleView = new BattleView();

        Player player = new Player("버스타조","전사",10,5,100,1500,5,5,1);
        public List<Monster> CreateMonsters { get; set; }


        public void StartDungeon(int stage)
        {

            int monsternum = 0;
            if (stage == 1) { monsternum = 4; }            //난이도
            else if (stage == 2) { monsternum = 7; }

            CreateMonsters = new List<Monster>();
            CreateMonsters.Clear();
            Random rand = new Random();
            int MonsterNumber = rand.Next(3, 4);    // 2마리~4마리
            int MonsterType;

            for (int i = 0; i < MonsterNumber; i++)
            {

                MonsterType = rand.Next(monsternum, monsternum + 3);   //몬스터 데이터 보고 조정   
                Monster monsterinfo = monsterData.ElementAt(MonsterType).Value;
                CreateMonsters.Add(monsterinfo);
                Console.WriteLine($"LV.{monsterinfo.Level} \t {monsterinfo.Name} \t HP : {monsterinfo.Hp} \t ATK : {monsterinfo.Attack},");
            }

        }
        public void InDungeon(Monster monster)
        {
            //전투 씬
            //enum switch -> 스킬쓸지 공격할지 방어?회피? 선택지
        }
        public void DungeonClear(Monster monster)
        {

            CreateMonsters.Clear();
        }

        public Dictionary<int, Monster> monsterData;

        public string path = "MonsterData.csv";
        public void LoadMosters()
        {
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


        void BattleMonster(int level)
        {

        }


        static int CheckValidInput(int min, int max)
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

        public void WinBoard(List<Monster> monster, Player player)
        {
            int rewardGold;
            int rewardExp;
            rewardGold = monster[0].Gold + monster[1].Gold + monster[2].Gold;
            rewardExp = monster[0].RewardExp + monster[1].RewardExp + monster[2].RewardExp;
            player.Gold += rewardGold;
            player.LevelUpExp += rewardExp;
            // stage++;
        }
        public void LoseBoard()
        {
            Console.WriteLine("키 입력으로  던전 입구로 돌아가기");
        }

        public void dungeonEnter()
        {
            //종욱님 그림
            DungeonSelectView();

            int firstView = (int)Select.EnterDungeon;
            firstView = CheckValidInput(1, 3);
            Select enumValue = (Select)firstView;

            switch (enumValue)
            {
                case Select.EnterDungeon:
                    battleView();
                    // 던전입장
                    break;

                case Select.UseItem:
                    Console.WriteLine("아이템사용 ");
                    break;

                case Select.exit:
                    // 메인으로 나가기
                    break;

            }
        }
        private void battleView()

        {
            int input = CheckValidInput(1, 3);
            //while (!player.Isdead&&!moster.Isdead) { }
            switch (input)
            {
                case 1:
                    //공격
                    break;
                case 2:
                    //스킬
                    break;
                case 3:
                    //도망

                    break;
            }

            if (player.IsDead)
            {
                Console.WriteLine("플레이어 사망");
                LoseBoard();

            }
            else if (CreateMonsters.Count == 0)//몹 리스트 길이 0이면
            {
                WinBoard(CreateMonsters, player);
            }
        }
        public void DungeonSelectView()
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
