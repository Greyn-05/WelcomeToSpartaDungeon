using TeamProject07.Logic;
using static TeamProject07.Utils.ShopData;
using static TeamProject07.Utils.ItemData;
using System;

namespace TeamProject07.Shop
{
    internal class Shop
    {
        public static Random random = new Random();
        public static bool isSelected; // 아이템목록 옆에 숫자 띄울까 말까

        public static ShopInven catalog;
        public static int check = 100000000;

        public static float nego = 1; // 가격조정

        public static void Open()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            check = 100000000;

            Console.Clear();
            Console.WriteLine($"-------------{catalog.name}-------------");
            Console.WriteLine();
            Console.WriteLine($"{shopDialogue[catalog.name][LinePick.상점설명.ToString()]}");
            Console.WriteLine();

            Console.WriteLine("1.대화");
            Console.WriteLine("2.구매하기");
            Console.WriteLine("3.판매하기");

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 3))
            {
                case 1:
                    Talk();
                    break;
                case 2:
                    Buy_Open();
                    break;
                case 3:
                    Sell_Open();
                    break;
                case 0:
                    break;
            }
        }


        public static void Talk()
        {
            Console.Clear();
            Console.WriteLine($"-------------{catalog.name}-------------");
            Console.WriteLine();
            Console.WriteLine($"{shopDialogue[catalog.name]["주인이름"]} : {shopDialogue[catalog.name][LinePick.잡답.ToString()]}");

            Console.WriteLine();

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 0))
            {
                case 0:
                    Open();
                    break;
            }
        }

        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        public static void Buy_Open()
        {
            nego = (catalog.name == ShopName.고물상.ToString()) ? 2f : 1;

            Buy_Screen(LinePick.방문인사);

            Console.WriteLine("1.구매하기");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 1))
            {
                case 1:
                    Buy();
                    break;
                case 0:
                    Open();
                    break;
            }

        }

        public static void Buy()
        {
            nego = (catalog.name == ShopName.고물상.ToString()) ? 2f : 1;

            Buy_Screen(LinePick.물건볼때);

            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("구매할 아이템의 숫자를 적고 엔터를 눌러주세요");
            Console.Write(">> ");


            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (--num < 0)
                    {
                        Buy_Open();
                        break;
                    }

                    check = 100000000;

                    if (num < catalog.slots.Length)
                    {

                        if (catalog.slots[num].item != null)
                        {
                            if (((MainLogic.dummy.Gold - (int)(catalog.slots[num].item.ItemPrice * nego)) >= 0)) // 돈이 안모자를때
                            {
                                check = num;

                                if (Buy_Choice(num))    // 정말 구매하시겠습니까?
                                {
                                    MainLogic.dummy.Gold -= (int)(catalog.slots[num].item.ItemPrice * nego);
                                    MainLogic.dummy.Inven.Add(catalog.slots[num].item);

                                    int i = catalog.slots[num].item.Id;

                                    if (--catalog.slots[num].count <= 0)
                                        catalog.slots[num].item = null;


                                    check = 100000000;

                                    Buy_Screen(LinePick.구매했을때);
                                    Console.WriteLine($"소지금 : {MainLogic.dummy.Gold}");
                                    Console.WriteLine();
                                    Console.WriteLine($"※ {items[i].Name}이 인벤토리에 추가되었습니다.");
                                    Console.WriteLine();


                                    Console.WriteLine("1. 더 둘러보기");
                                    Console.WriteLine("0. 나가기");
                                    Console.WriteLine();
                                    Console.Write(">> ");

                                    switch (CheckValidInput(0, 1))
                                    {
                                        case 1:
                                            Buy();
                                            break;
                                        case 0:
                                            Buy_Open();
                                            break;
                                    }

                                }
                                else
                                {
                                    check = 100000000;
                                    Buy_Screen(LinePick.안샀을때);

                                    Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
                                    Console.WriteLine();

                                    Console.WriteLine("1. 다른 물건을 산다");
                                    Console.WriteLine("0. 나가기");
                                    Console.WriteLine();
                                    Console.Write(">> ");

                                    switch (CheckValidInput(0, 1))
                                    {
                                        case 1:
                                            Buy();
                                            break;
                                        case 0:
                                            Buy_Open();
                                            break;
                                    }

                                }
                            }
                            else // 소지금 부족
                            {
                                check = 100000000;
                                Buy_Screen(LinePick.돈이부족해);

                                Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
                                Console.WriteLine();

                                Console.WriteLine();
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.Write(">> ");

                                switch (CheckValidInput(0, 0))
                                {
                                    case 0:
                                        Buy_Open();
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("품절된 상품입니다.");
                            Console.Write("구매할 아이템의 숫자를 적고 엔터를 눌러주세요");
                            Console.Write(">> ");
                            continue;
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

        public static bool Buy_Choice(int num)
        {
            Buy_Screen(LinePick.물건골랐을때);

            // 장비 골랐을때 올라간 능력치 떠야함. 장비상점에서만
            // 산 장비를 착용하시겠습니까?

            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ( -{(int)(catalog.slots[num].item.ItemPrice * nego)}원 )");
            Console.WriteLine();
            Console.WriteLine($"{catalog.slots[num].item.Name}을 구매하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 사겠습니다.");
            Console.WriteLine("0. 안살래요");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 1))
            {
                case 1:
                    return true;
                case 0:
                    return false;
            }

            return false;
        }

        public static void Buy_Screen(LinePick state) // 구매화면
        {
            switch (state)
            {
                case LinePick.물건볼때:
                case LinePick.물건골랐을때:
                case LinePick.구매했을때:
                case LinePick.안샀을때:
                case LinePick.돈이부족해:
                case LinePick.내템봐줘:
                case LinePick.이거팔까:
                case LinePick.팔겠습니다:
                case LinePick.안팔래:
                    isSelected = true;
                    break;

                default:
                    isSelected = false;
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Clear();
            Console.WriteLine($"-------------{catalog.name}-------------");
            Console.WriteLine();
            Console.WriteLine($"{shopDialogue[catalog.name]["주인이름"]} : {shopDialogue[catalog.name][state.ToString()]}");

            Console.WriteLine();

            if (state != LinePick.전량품절)
            {
                Console.WriteLine("[           판매목록           ]");
                Console.WriteLine();

                for (int i = 0; i < catalog.slots.Length; i++)
                {
                    if (catalog.slots[i].item != null)
                    {
                        if (i == check)
                            Console.ForegroundColor = ConsoleColor.Green;


                        Console.WriteLine((isSelected ? $"{i + 1}. " : "") +
                            ((catalog.slots[i].item.Type == Utils.Define.ItemType.Equip) ? $"{catalog.slots[i].item.Part} | " : "") +
                            ($"{catalog.slots[i].item.Name} | {catalog.slots[i].item.Info} | {(int)(catalog.slots[i].item.ItemPrice * nego)}원") +
                            (isSelected && (catalog.slots[i].count > 1) ? ($" | {catalog.slots[i].count}개") : ""));



                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.WriteLine((isSelected ? $"{i + 1}. " : "") + "-----------------------------  품절  -----------------------------");
                    }
                }
            }

            Console.WriteLine();
        }


        //-■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■    공용      ■■■■■■■■■■■■■■■■■■■■■■■■■


        public static int CheckValidInput(int min, int max)
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

        //-■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■    내템 파는 함수들      ■■■■■■■■■■■■■■■■■■■■■■■■■

        static public void Sell_Open()
        {
            nego = 0.5f;


            Sell_Screen(LinePick.방문인사);

            Console.WriteLine("1. 판매하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 1))
            {
                case 1:
                    Sell();
                    break;
                case 0:
                    Open();
                    break;
            }

        }

        static public void Sell()
        {
            nego = 0.5f;

            Sell_Screen(LinePick.내템봐줘);

            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold}");
            Console.WriteLine();

            Console.WriteLine("판매할 아이템의 숫자를 적고 엔터를 눌러주세요");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (--num < 0)
                    {
                        Sell_Open();
                        break;
                    }

                    check = 100000000;

                    if (num < MainLogic.dummy.Inven.Count)
                    {
                        check = num;

                        if (Sell_Choice(num))
                        {
                            int i = MainLogic.dummy.Inven[num].Id;

                            Shop_Reseller.resellerInven.Add(items[i]); // 고물상 인벤에 아이템추가
                            Shop_Reseller.ReLoad(); // 고물상인벤 갱신


                            MainLogic.dummy.Gold += (int)(MainLogic.dummy.Inven[num].ItemPrice * nego);
                            MainLogic.dummy.Inven.Remove(MainLogic.dummy.Inven[num]);


                            check = 100000000;

                            Sell_Screen(LinePick.팔겠습니다);
                            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold}");
                            Console.WriteLine();
                            Console.WriteLine($"※ {items[i].Name}를 판매하였습니다.");
                            Console.WriteLine();

                            if (MainLogic.dummy.Inven.Count <= 0)
                            {
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.Write(">> ");

                                switch (CheckValidInput(0, 0))
                                {
                                    case 0:
                                        Sell_Open();
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("1. 더 팔기");
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.Write(">> ");

                                switch (CheckValidInput(0, 1))
                                {
                                    case 1:
                                        Sell();
                                        break;
                                    case 0:
                                        Sell_Open();
                                        break;
                                }

                            }
                        }
                        else
                        {
                            check = 100000000;
                            Sell_Screen(LinePick.안팔래);

                            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
                            Console.WriteLine();

                            Console.WriteLine("1. 다른걸 팔게요");
                            Console.WriteLine("0. 나가기");
                            Console.WriteLine();
                            Console.Write(">> ");

                            switch (CheckValidInput(0, 1))
                            {
                                case 1:
                                    Sell();
                                    break;
                                case 0:
                                    Sell_Open();
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

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 0))
            {
                case 0:
                default:
                    Sell_Open();
                    break;
            }
        }


        public static bool Sell_Choice(int num)
        {

            Sell_Screen(LinePick.이거팔까);

            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ( +{(int)(MainLogic.dummy.Inven[num].ItemPrice * nego)}원 )");
            Console.WriteLine();
            Console.WriteLine($"{MainLogic.dummy.Inven[num].Name}을 판매 하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 팝니다.");
            Console.WriteLine("0. 마음이바뀌었어요");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 1))
            {
                case 1:
                    return true;
                case 0:
                    return false;
            }

            return false;
        }

        public static void Sell_Screen(LinePick state) // 내템 파는 화면
        {
            switch (state)
            {
                case LinePick.물건볼때:
                case LinePick.물건골랐을때:
                case LinePick.구매했을때:
                case LinePick.안샀을때:
                case LinePick.돈이부족해:
                case LinePick.내템봐줘:
                case LinePick.이거팔까:
                case LinePick.팔겠습니다:
                case LinePick.안팔래:
                    isSelected = true;
                    break;

                default:
                    isSelected = false;
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Clear();
            Console.WriteLine($"-------------{catalog.name}-------------");
            Console.WriteLine();
            Console.WriteLine($"{shopDialogue[catalog.name]["주인이름"]} : {shopDialogue[catalog.name][state.ToString()]}");

            Console.WriteLine();
            Console.WriteLine("[          내 인벤토리 목록           ]");

            if (MainLogic.dummy.Inven.Count <= 0)
            {
                Console.WriteLine("      인벤토리가 비어있습니다       ");
            }
            else
            {
                Console.WriteLine();

                for (int i = 0; i < MainLogic.dummy.Inven.Count; i++)
                {
                    if (i == check)
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine((isSelected ? $"{i + 1}. " : "") +
                          ((MainLogic.dummy.Inven[i].Type == Utils.Define.ItemType.Equip) ? $"{MainLogic.dummy.Inven[i].Part} | " : "") +
                        ($"{MainLogic.dummy.Inven[i].Name} | {MainLogic.dummy.Inven[i].Info} | {(int)(MainLogic.dummy.Inven[i].ItemPrice * nego)}원"));


                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.WriteLine();
        }
    }
}