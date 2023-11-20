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

        static void InvenDraw()
        {
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("보유 중인 아이템을 확인하고 관리할 수 있습니다."); // (1, 1)

            Console.SetCursorPosition(1, 2);
            Console.WriteLine("상세 정보를 보고 싶은 아이템을 선택해주세요."); // (1, 2)

            for (int i = 5; i< 30; i++)  // (3, 50++)로 가운데 분리선 그림
            {
                Console.SetCursorPosition(50, i);
                Console.Write("|");
            }

            Console.SetCursorPosition(1, 3);
            Console.Write("0. 돌아가기"); // 돌아가기 - (1, 3)
            Console.SetCursorPosition(15, 4);
            Console.WriteLine("[아이템 목록]"); // [아이템 목록] - (15, 4)
            Console.SetCursorPosition(75, 4);
            Console.WriteLine("[아이템 정보]"); // [아이템 정보] - (75, 4)
        }

        public Define.MainGamePhase test(Player player)
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();

            InvenDraw();

            int selectedItemIndex = ItemListPage(player);
            ItemDescription(player, selectedItemIndex);

            return Define.MainGamePhase.Main;
        }

        static int ItemListPage(Player player)
        {
            int currentPage = 0;
            int itemsPerPage = 10;
            int totalPages = (player.Inven.Count + itemsPerPage - 1) / itemsPerPage;

            while (true)
            {
                Console.Clear();
                InvenDraw();
                int startPage = currentPage * itemsPerPage;
                int endPage = Math.Min((currentPage + 1) * itemsPerPage, player.Inven.Count);

                for (int i = startPage; i < endPage; i++)
                {
                    Item item = player.Inven[i];
                    string equipSign = item.IsEquipped ? "[E]" : "";
                    string pageList = $"[{i + 1}] {equipSign} {item.Name}";

                    for (int j = 0; j < pageList.Length; j += 30)
                    {
                        string substring = pageList.Substring(j, Math.Min(30, pageList.Length - j));
                        Console.SetCursorPosition(12, 7 + (i - startPage) * 2 + j / 30);
                        Console.WriteLine(substring);
                    }
                }

                Console.SetCursorPosition(8, 28);
                Console.WriteLine("[이전 페이지는 A, 다음 페이지는 D]");
                Console.SetCursorPosition(16, 5);
                Console.WriteLine("페이지: " + (currentPage + 1) + "/" + totalPages);

                string nextPage = Console.ReadLine().ToUpper();
                if (nextPage == "A" && currentPage > 0)
                {
                    currentPage--;
                }
                else if (nextPage == "D" && currentPage < totalPages - 1)
                {
                    currentPage++;
                }
                else
                {
                    if (int.TryParse(nextPage, out var selectedIndex))
                    {
                        if (selectedIndex > 0 && selectedIndex <= player.Inven.Count)
                        {
                            return selectedIndex - 1;
                        }
                    }
                }
            }
        }

        static void ItemDescription(Player player, int selectedItemIndex)
        {
            int startDescriptionY = 6;

            if (selectedItemIndex >= 0 && selectedItemIndex < player.Inven.Count)
            {
                Item selectedItem = player.Inven[selectedItemIndex];

                //""부분 수정해야함
                Console.SetCursorPosition(75, startDescriptionY++); // 출력위치
                WriteLineInParts($"{selectedItem.Type}", 75, ref startDescriptionY); //타입
                WriteLineInParts($"{selectedItem.Name}", 75, ref startDescriptionY); //이름
                WriteLineInParts($"{selectedItem.Info}", 75, ref startDescriptionY); //설명
                WriteLineInParts($"{selectedItem.buff}", 75, ref startDescriptionY); //올라가는 능력치 buff? buffvalue? 수정
                WriteLineInParts("세트 이름", 75, ref startDescriptionY); //세트 이름 // .set 없음 수정
                WriteLineInParts($"{selectedItem.ItemPrice}", 75, ref startDescriptionY); //가격

                Console.SetCursorPosition(75, startDescriptionY++);
                Console.WriteLine("1. 장착/해제");
                Console.WriteLine("0. 뒤로 가기");

                int input = CheckValidInput(0, 1);

                if (input == 1)
                {
                    selectedItem.IsEquipped = !selectedItem.IsEquipped;
                    Console.SetCursorPosition(75, startDescriptionY);
                    Console.WriteLine($"{selectedItem.Name} {(selectedItem.IsEquipped ? "장착 완료!" : "해제 완료!")}");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.SetCursorPosition(75, 6);
                Console.WriteLine("잘못된 선택입니다");
            }
        }

        static void WriteLineInParts(string text, int startX, ref int startDescriptionY) // 10글자마자 잘라서 출력
        {
            for (int i = 0; i < text.Length; i += 30)
            {
                string part = text.Substring(i, Math.Min(30, text.Length - i));
                Console.SetCursorPosition(startX, startDescriptionY++);
                Console.WriteLine(part);
            }
        }

        private static int CheckValidInput(int min, int max)
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