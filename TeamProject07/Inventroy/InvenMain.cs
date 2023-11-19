using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Characters;
using TeamProject07.Utils;

namespace TeamProject07.Inventroy
{
    internal class InvenMain
    {

        // 인벤토리 전체 수정중

        public Define.MainGamePhase test(Player player)
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();

            Console.SetCursorPosition(1, 1);
            Console.WriteLine("보유 중인 아이템을 확인하고 관리할 수 있습니다."); // (1, 1)

            Console.SetCursorPosition(1, 2);
            Console.WriteLine("상세 정보를 보고 싶은 아이템을 선택해주세요."); // (1, 2)

            for (int i = 5; i < 30; i++)  // (3, 50++)로 가운데 분리선 그림
            {
                Console.SetCursorPosition(50, i);
                Console.Write("|");
            }

            Console.SetCursorPosition(15, 4);
            Console.WriteLine("[아이템 목록]"); // [아이템 목록] - (15, 4)
            Console.SetCursorPosition(75, 4);
            Console.WriteLine("[아이템 정보]"); // [아이템 정보] - (75, 4)

            int selectedItemIndex = ItemList(player);
            ItemDescription(player, selectedItemIndex);


            static void ItemList(Player player)
            {
                int startListY = 0;

                foreach (Item item in player.Inven)
                {

                    string equippedSign = item.IsEquipped ? "[E]" : "";
                    string input_str = $"[{startListY + 1}] {equippedSign} {item.Name}";

                    for (int j = 0; j < input_str.Length; j += 10)
                    {
                        string substring = input_str.Substring(j, Math.Min(10, input_str.Length - j));
                        Console.SetCursorPosition(12, 6 + startListY + j / 10); // (12, 6)에서 출력 시작해 아이템 번호가 바뀌거나, 아이템 이름이 10글자를 넘어가면 y값 바꿈
                        Console.Write($"{substring}");
                    }

                    startListY += (input_str.Length + 9) / 10;
                }

                int selectedItemIndex = CheckValidInput(0, startListY);
                return selectedItemIndex;
            }

            static void ItemDescription(Player player, int selectedItemIndex)
            {
                if (selectedItemIndex >= 0 && selectedItemIndex < player.Inven.Count)
                {
                    Item selectedItem = player.Inven[selectedItemIndex];

                    int startDescriptionY = 6;

                    //""부분 수정해야함
                    Console.SetCursorPosition(75, startDescriptionY++); // 출력위치
                    WriteLineInParts("타입", 75, ref startDescriptionY); //타입
                    WriteLineInParts("이름", 75, ref startDescriptionY); //이름
                    WriteLineInParts("설명", 75, ref startDescriptionY); //설명
                    WriteLineInParts("올라가는 능력치", 75, ref startDescriptionY); //올라가는 능력치
                    WriteLineInParts("세트 이름", 75, ref startDescriptionY); //세트 이름
                    WriteLineInParts("가격", 75, ref startDescriptionY); //가격
                }
                else
                {
                    Console.SetCursorPosition(75, 6);
                    Console.WriteLine("잘못된 선택입니다");
                }

                Console.SetCursorPosition(75, startDescriptionY);
                Console.WriteLine("0. 돌아가기");
            }
        }

        static void WriteLineInParts(string text, int startX, ref int startDescriptionY)
        {
            for (int i = 0; i < text.Length; i += 10)
            {
                string part = text.Substring(i, Math.Min(10, text.Length - i));
                Console.SetCursorPosition(startX, startDescriptionY++);
                Console.WriteLine(part);
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
    }
}