using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;

namespace TeamProject07.Inventroy
{
    internal class InvenMain
    {

        // 인벤토리 전체 수정중

        public Define.MainGamePhase test()
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();
            Console.WriteLine("보유 중인 아이템을 확인하고 관리할 수 있습니다.");
            Console.WriteLine("\n");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("\n");
            Console.WriteLine("[장비 아이템]");

            foreach (Item item in player.inven) // 장비 아이템 리스트
            {
                int i = 0;
                i++;

                string equippedSign = item.IsEquipped ? "[E]" : "";
                string input = $"[{i}] {item.myName} {equippedSign}";
                string output = InsertLineBreakEveryTwentyCharacters(input); // 20글자마다 줄바꿈 호출
                Console.WriteLine(output);
            }

            Console.WriteLine("[소모 아이템]");

            foreach (Item item in player.inven) // 소모 아이템 리스트
            {
                int i = 0;
                i++;

                string input = $"[{i}] {item.myName}";
                string output = InsertLineBreakEveryTwentyCharacters(input); // 20글자마다 줄바꿈
                Console.WriteLine(output);
            }

            static string InsertLineBreakEveryTwentyCharacters(string text) // 20글자마다 줄바꿈 메소드
            {
                string result = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (i > 0 && i % 20 == 0)
                        result += "\n";
                    result += text[i];
                }
                return result;
            }


            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    choicePhase = Define.MainGamePhase.Main;
                    break;


            }
            return Define.MainGamePhase.Main;
        }

        Console.WriteLine("[설명]");

        for (int i = 0; i<player.inven.Count; i++)
        {
            //string input = "종류 : ";
            //string input = "세트명 : ";
            //string input = "장비부위 : ";
            //string input = "능력치 : ";
            //string input = "설명 : ";
            //string input = "착용직업 : ";
            //string input = "가격 : ";

            // 변수이름 전부 수정해야함

            string output = InsertLineBreakEveryTwentyCharacters(input);
        Console.WriteLine(output);
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
