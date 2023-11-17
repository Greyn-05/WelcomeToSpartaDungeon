using System;
using System.Numerics;
using TeamProject07.Characters;
using TeamProject07.Items;
using TeamProject07.Logic;
using TeamProject07.Utils;

namespace TeamProject07.Shop
{
    internal class Shop
    {
        public enum Name
        {
            장비상점,
            소모품상점
        }
        static public Name name;

        static public Item[] equipSale; // 장비상점 판매목록
        static public Item[] consumSale; // 소모품상점 판매목록

        static string shopName;
        static Item[] catalog;

        static bool isBuy = false;
        static bool isSell = false;

        public static void Init() // 상점에 아이템추가 재고 추가기능 넣으려면 수정해야함
        {
            equipSale = new Item[6];
            consumSale = new Item[5];

            for (int i = 0; i < equipSale.Length; i++)
            {
                equipSale[i] = ItemData.items[1];
            }

            for (int i = 0; i < consumSale.Length; i++)
            {
                consumSale[i] = ItemData.items[13];
            }

        }
        static public void Visit(Name nam)
        {
            name = nam;

            switch (name)
            {
                case Name.장비상점:
                    catalog = equipSale;
                    break;
                case Name.소모품상점:
                    catalog = consumSale;
                   
                    break;
            }
            shopName = nam.ToString();

            Open();
        }


        //     {{ "주인이름", data[1] }, { "방문인사", data[2] }, { "물건볼때", data[3]}, { "구매했을때", data[4] },{ "안살때", data[5] },{ "작별인사", data[6]}};

        static void Open()
        {
            Console.Clear();
            Console.WriteLine($"-------------{shopName}-------------");
            Console.WriteLine();
            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["방문인사"]}");
            
            BuyScreen();

            Console.WriteLine();
            Console.WriteLine("1.구매하기");
            Console.WriteLine("2.판매하기");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");


            switch (CheckValidInput(0, 2))
            {
                case 1:
                    TakeMyMoney();
                    break;
                case 2:
                    GiveMeMoney();
                    break;
                case 0:
                    // 상점 밖 페이지 연결
                    break;
            }

        }

        static void GiveMeMoney()
        {
            isBuy = false;
            isSell = true;

            SellScreen();

            Console.WriteLine("판매할 아이템의 숫자를 적고 엔터를 눌러주세요");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

        }

        static void TakeMyMoney() // 템구매하기
        {
            isBuy = true;
            isSell = false;

            Console.Clear();
            Console.WriteLine($"-------------{shopName}-------------");
            Console.WriteLine();
            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["방문인사"]}");

            BuyScreen();

            Console.WriteLine("구매할 아이템의 숫자를 적고 엔터를 눌러주세요");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (--num < 0)
                    {
                        Open();
                        break;
                    }

                    if (num < catalog.Length && catalog[num] != null)
                    {
                        if ((MainLogic.dummy.Gold - catalog[num].ItemPrice) >= 0) // 돈이 안모자를때
                        {
                            MainLogic.dummy.Gold -= catalog[num].ItemPrice;
                            MainLogic.dummy.Inven.Add(catalog[num]);

                            int i = catalog[num].Id;

                            Console.Clear();
                            Console.WriteLine($"-------------{shopName}-------------");
                            Console.WriteLine();
                            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["구매했을때"]}");
                            BuyScreen();

                            Console.WriteLine($"※ {ItemData.items[i].Name}이 인벤토리에 추가되었습니다.");
                            Console.WriteLine($"현재 소지금 : {MainLogic.dummy.Gold} ( -{ItemData.items[i].ItemPrice}원 )");
                            Console.WriteLine();

                            Console.WriteLine("1. 더 둘러보기");
                            Console.WriteLine("0. 나가기");
                            Console.WriteLine();
                            Console.Write(">> ");

                            switch (CheckValidInput(0, 1))
                            {
                                case 1:
                                    TakeMyMoney();
                                    break;
                                case 0:
                                    Open();
                                    break;
                            }
                        }
                        else // 소지금 부족
                        {
                            Console.Clear();
                            Console.WriteLine($"-------------{shopName}-------------");
                            Console.WriteLine();
                            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["안살때"]}");
                            BuyScreen();

                            Console.WriteLine("0. 나가기");
                            Console.WriteLine();
                            Console.Write(">> ");

                            switch (CheckValidInput(0, 0))
                            {
                                case 0:
                                    Open();
                                    break;
                            }

                        }

                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

            }
        }


        static void BuyScreen() // 구매화면
        {
            Console.WriteLine();

            Console.WriteLine("[           판매목록           ]");
            Console.WriteLine();

            for (int i = 0; i < catalog.Length; i++)
            {
                if (catalog[i] != null)
                    Console.WriteLine((isBuy ? $"{i + 1}." : "") + ($"{catalog[i].Name} | {catalog[i].Info} | {catalog[i].ItemPrice}원 "));
                else
                    Console.WriteLine((isBuy ? $"{i + 1}." : "") + "-----------------------  품절  -----------------------");
            }

            Console.WriteLine();
            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
            Console.WriteLine();
        }


        static void SellScreen() // 판매 화면
        {
            Console.WriteLine();

            Console.WriteLine("[        소지 아이템 목록           ]");
            Console.WriteLine();


            for (int i = 0; i < MainLogic.dummy.Inven.Count; i++) // 장비 목록
            {
                if (MainLogic.dummy.Inven[i] != null && MainLogic.dummy.Inven[i].Type == Define.ItemType.Equip && !MainLogic.dummy.Inven[i].IsEquipped)
                {
                    Console.WriteLine((isSell ? $"{i + 1}." : "") + ($"판매가 : {MainLogic.dummy.Inven[i].ItemPrice / 2}원 | {MainLogic.dummy.Inven[i].Name}"));
                }
            }

            for (int i = 0; i < MainLogic.dummy.Inven.Count; i++) // 소모품
            {
                if (MainLogic.dummy.Inven[i] != null && MainLogic.dummy.Inven[i].Type == Define.ItemType.Consum)
                {
                    Console.WriteLine((isSell ? $"{i + 1}." : "") + $"판매가 : {MainLogic.dummy.Inven[i].ItemPrice / 2}원 | {MainLogic.dummy.Inven[i].Name}");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
            Console.WriteLine();

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
    }
}