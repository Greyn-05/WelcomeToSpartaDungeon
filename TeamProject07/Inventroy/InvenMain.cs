using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
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
            Console.WriteLine("  보유 중인 아이템을 확인하고 관리할 수 있습니다."); // (1, 1)

            Console.SetCursorPosition(1, 2);
            Console.WriteLine("  상세 정보를 보고 싶은 아이템을 선택해주세요."); // (1, 2)

            for (int i = 5; i < 30; i++)  // (3, 50++)로 가운데 분리선 그림
            {
                Console.SetCursorPosition(50, i);
                Console.Write("  |");
            }

            // Console.SetCursorPosition(1, 3);
            // Console.Write("0. 돌아가기"); // 돌아가기 - (1, 3)
            Console.SetCursorPosition(15, 4);
            Console.WriteLine("  [아이템 목록]"); // [아이템 목록] - (15, 4)
            Console.SetCursorPosition(75, 4);
            Console.WriteLine("  [아이템 정보]"); // [아이템 정보] - (75, 4)
        }

        public Define.MainGamePhase test(Player player)
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();

            InvenDraw();

            // Keyboard.KeyDown();

            //var key = Console.ReadKey(true);

            //if (key.Key == ConsoleKey.Escape)
            //{
            //    return Define.MainGamePhase.Main;
            //}

            while (true)
            {
                int selectedItemIndex = ItemListPage(player);

                if (selectedItemIndex >= 0)
                {
                    bool backToItemList = ItemDescription(player, selectedItemIndex);
                   
                    if (backToItemList)
                    {
                        continue;
                    }
                    else
                    {
                        return Define.MainGamePhase.Main;
                    }
                }
                else
                {
                    return Define.MainGamePhase.Main;
                }
            }

            static int ItemListPage(Player player)
            {
                int currentPage = 0;   // 지금 페이지
                int itemsPerPage = 10; // 페이지당 아이템 갯수
                int totalPages = (player.Inven.Count + itemsPerPage - 1) / itemsPerPage; // 전체 페이지
                int selectedItemIndex = 0; // 선택한 아이템


                while (true)
                {
                    Console.Clear();
                    InvenDraw();

                    // 페이지 아이템 범위
                    int startPage = currentPage * itemsPerPage;
                    int endPage = Math.Min((currentPage + 1) * itemsPerPage, player.Inven.Count);

                    Console.SetCursorPosition(16, 5);
                    Console.WriteLine("페이지: " + (currentPage + 1) + "/" + totalPages);

                    // 아이템 목록
                    for (int i = startPage; i < endPage; i++)
                    {
                        Item item = player.Inven[i];
                        string equipSign = item.IsEquipped ? "[E]" : "";
                        string pageList = $"[{i + 1}] {equipSign} {item.Name}";

                        int lineIndex = 0;

                        for (int j = 0; j < pageList.Length; j += 30)
                        {
                            string substring = pageList.Substring(j, Math.Min(30, pageList.Length - j));
                            int itemY = 7 + (i - startPage) * 2 + lineIndex;

                            if (selectedItemIndex == i - startPage)
                            {
                                Console.ForegroundColor = ConsoleColor.Green; // 선택한 아이템 초록색으로
                            }

                            Console.SetCursorPosition(12, itemY);
                            Console.WriteLine(substring);

                            Console.ResetColor();
                            lineIndex++;
                        }
                    }

                    var key = Keyboard.KeyDown();

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedItemIndex = Math.Max(0, selectedItemIndex - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            selectedItemIndex = Math.Min(itemsPerPage - 1, selectedItemIndex + 1);
                            break;
                        case ConsoleKey.LeftArrow:
                            if (currentPage > 0)
                            {
                                currentPage--;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (currentPage < totalPages - 1)
                            {
                                currentPage++;
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.Enter:
                            return currentPage * itemsPerPage + selectedItemIndex;
                        case ConsoleKey.Escape:
                            return -1;
                    }
                }
            }

            static bool ItemDescription(Player player, int selectedItemIndex)
            {
                int startDescriptionY = 6; // 첫 출력 좌표 y = 6
                int option = 0;  // 0은 장착 또ㅓ는 사용 1은 해제

                while (true)
                {

                    InvenDraw();

                    if (selectedItemIndex >= 0 && selectedItemIndex < player.Inven.Count)
                    {
                        Item selectedItem = player.Inven[selectedItemIndex];

                        //세트 이름 부분 수정해야함
                        Console.SetCursorPosition(75, startDescriptionY++); // 출력위치
                        WriteLineInParts($"이름 : {selectedItem.Type}", 75, ref startDescriptionY); //타입
                        WriteLineInParts($"부위 : {selectedItem.Name}", 75, ref startDescriptionY); //이름
                        WriteLineInParts($"설명 : {selectedItem.Info}", 75, ref startDescriptionY); //설명
                        WriteLineInParts($"능력치 정보 : {selectedItem.buff} + {selectedItem.buffValue}", 75, ref startDescriptionY); //올라가는 능력치
                        WriteLineInParts("세트 효과 : ", 75, ref startDescriptionY); //세트 이름 // .set 없음 수정
                        WriteLineInParts($"가격 : {selectedItem.ItemPrice}", 75, ref startDescriptionY); //가격

                        while (true)
                        {
                            Console.SetCursorPosition(75, startDescriptionY);
                            SelectOption("장착/사용", option == 0);

                            Console.SetCursorPosition(75, startDescriptionY + 1);
                            SelectOption("해제", option == 1);

                            Console.SetCursorPosition(75, startDescriptionY + 2);
                            SelectOption("취소", option == 2);

                            var key = Console.ReadKey(true);

                            switch (key.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    option = option > 0 ? option - 1 : 2;
                                    break;
                                case ConsoleKey.DownArrow:
                                    option = option < 2 ? option + 1 : 0;
                                    break;
                                case ConsoleKey.Enter:
                                    bool isCancelled;
                                    SelectedOption(selectedItem, option, out isCancelled);
                                         if (isCancelled)
                                         {
                                            return true;
                                         }
                                    break;
                                case ConsoleKey.Escape:
                                    return false;
                            }
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(75, 6);
                        Console.WriteLine("잘못된 선택입니다");
                        return true;
                    }
                }

                static void WriteLineInParts(string text, int startX, ref int startDescriptionY) // 30글자마자 잘라서 출력
                {
                    for (int i = 0; i < text.Length; i += 30)
                    {
                        string part = text.Substring(i, Math.Min(30, text.Length - i));
                        Console.SetCursorPosition(startX, startDescriptionY++);
                        Console.WriteLine(part);
                    }
                }

                static void SelectOption(string text, bool isSelected)
                {
                    if (isSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine(text);
                    Console.ResetColor();
                }

                void SelectedOption(Item selectedItem, int option, out bool isCancelled)
                {
                    isCancelled = false;

                    switch (option)
                    {
                        case 0:
                            selectedItem.IsEquipped = !selectedItem.IsEquipped;
                            Console.SetCursorPosition(75, startDescriptionY);
                            Console.WriteLine($"{selectedItem.Name} {(selectedItem.IsEquipped ? "장착 완료!" : "")}");
                            Console.ReadKey();
                            break;
                        case 1:
                            selectedItem.IsEquipped = false;
                            Console.SetCursorPosition(75, startDescriptionY);
                            Console.WriteLine($"{selectedItem.Name} 해제 완료!");
                            Console.ReadKey();
                            break;
                        case 2:
                            isCancelled = true;
                            break;
                    }
                }
            }
        }
    }
}