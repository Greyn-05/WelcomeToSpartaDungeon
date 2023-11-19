using TeamProject07.Logic;
using static TeamProject07.Utils.Define;
using static TeamProject07.Utils.ShopData;
using static TeamProject07.Utils.ItemData;

namespace TeamProject07.Shop
{
    internal class Shop
    {
        public enum State
        {
            방문인사,
            물건볼때,
            물건골랐을때,
            구매했을때,
            안살때,
            작별인사
        }

        static private Random random = new Random();
        static private bool isBuyMode;

        static private ShopInven catalog;

        static public ShopInven equipSale; // 장비상점 판매목록
        static public ShopInven consumSale; // 소모품상점 판매목록


        public static void Init()
        {
            equipSale = new ShopInven(ShopName.장비상점);
            consumSale = new ShopInven(ShopName.고물상);

          ReLoad(); // 상점 재고채우기
        }

        public static void ReLoad() // 상점에 물건 채우기. 재고추가할거면 실행 여러번
        {
            for (int i = 0; i < 20; i++)
            {
                equipSale.Add(items[random.Next(0, 12)]);
            }
            for (int i = 0; i < 100; i++)
            {
                consumSale.Add(items[random.Next(12, 18)]);
            }
        }



        static public void Visit(ShopName nam)
        {
            switch (nam)
            {
                case ShopName.장비상점:
                    catalog = equipSale;
                    break;
                case ShopName.소모품상점:
                    catalog = consumSale;
                    break;
            }

            Open();
        }

        static void Open()
        {
            BuyScreen(State.방문인사);

            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("1.구매하기");
            Console.WriteLine("0.나가기");
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


        static void TakeMyMoney() // 템구매
        {
            BuyScreen(State.물건볼때);

            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
            Console.WriteLine();

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
                        Open();
                        break;
                    }

                    if (num < catalog.slots.Length)
                    {

                        if (catalog.slots[num].item != null)
                        {
                            if ((MainLogic.dummy.Gold - catalog.slots[num].item.ItemPrice) >= 0) // 돈이 안모자를때
                            {

                                // 정말 구매하시겠습니까?

                                if (LastChoice(num))
                                {
                                    MainLogic.dummy.Gold -= catalog.slots[num].item.ItemPrice;
                                    MainLogic.dummy.Inven.Add(catalog.slots[num].item);

                                    int i = catalog.slots[num].item.Id;

                                    if (--catalog.slots[num].count <= 0)
                                        catalog.slots[num].item = null;


                                    BuyScreen(State.구매했을때);
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
                                            TakeMyMoney();
                                            break;
                                        case 0:
                                            Open();
                                            break;
                                    }

                                }
                                else
                                {
                                    
                                    BuyScreen(State.안살때);

                                    Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
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
                            }
                            else // 소지금 부족
                            {
                                BuyScreen(State.안살때);

                                Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ");
                                Console.WriteLine();

                                Console.WriteLine();
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

        static bool LastChoice(int num)
        {
            BuyScreen(State.물건골랐을때);


            // 장비 골랐을때 올라간 능력치 떠야함. 장비상점에서만,.
            // 산 장비를 착용하시겠습니까?
            Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ( -{catalog.slots[num].item.ItemPrice}원 )");
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

        static void BuyScreen(State state) // 구매화면
        {
            switch (state)
            {
                case State.물건볼때:
                case State.물건골랐을때:
                case State.구매했을때:
                case State.안살때:
                    isBuyMode = true;
                    break;

                case State.방문인사:
                case State.작별인사:
                default:
                    isBuyMode = false;
                    break;
            }


            Console.Clear();
            Console.WriteLine($"-------------{catalog.name}-------------");
            Console.WriteLine();
            Console.WriteLine($"{shopDialogue[catalog.name]["주인이름"]} : {shopDialogue[catalog.name][state.ToString()]}");

            Console.WriteLine();

            Console.WriteLine("[           판매목록           ]");
            Console.WriteLine();

            for (int i = 0; i < catalog.slots.Length; i++)
            {
                if (catalog.slots[i].item != null)
                    Console.WriteLine((isBuyMode ? $"{i + 1}." : "") + ($"{catalog.slots[i].item.Name} | {catalog.slots[i].item.Info} | {catalog.slots[i].item.ItemPrice}원") + (isBuyMode && (catalog.slots[i].count > 1) ? ($" | {catalog.slots[i].count}개") : ""));
                else
                    Console.WriteLine((isBuyMode ? $"{i + 1}." : "") + "-----------------------------  품절  -----------------------------");
            }

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