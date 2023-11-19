using TeamProject07.Logic;
using TeamProject07.Utils;

namespace TeamProject07.Shop
{
    internal class Shop
    {
 
        static private Random random = new Random();
        static private string shopName;
        static private ShopInventorySlot[] catalog;
        static private bool isBuy = false;

        static public Define.ShopName name;

        static public ShopInventorySlot[] equipSale; // 장비상점 판매목록
        static public ShopInventorySlot[] consumSale; // 소모품상점 판매목록

        public static void Init()
        {
            equipSale = new ShopInventorySlot[6]; //판매창 목록 갯수
            consumSale = new ShopInventorySlot[6];

            for (int i = 0; i < equipSale.Length; i++)
            {
                ShopInventorySlot inventorySlot = new ShopInventorySlot();
                equipSale[i] = inventorySlot;
            }

            for (int i = 0; i < consumSale.Length; i++)
            {
                ShopInventorySlot inventorySlot = new ShopInventorySlot();
                consumSale[i] = inventorySlot;
            }

            ReLoad(); // 상점 재고채우기
        }

        public static void ReLoad() // 상점에 물건 채우기. 재고추가할거면 실행 여러번
        {

            for (int i = 0; i < equipSale.Length; i++)
            {
                equipSale[i].item = ItemData.items[random.Next(0, 12)];
                equipSale[i].count = random.Next(1, 3);
            }

            for (int i = 0; i < consumSale.Length; i++)
            {

                consumSale[i].item = ItemData.items[random.Next(12, 18)];
                consumSale[i].count = random.Next(10, 20);
            }
        }

        static public void Visit(Define.ShopName nam)
        {
            name = nam;

            switch (name)
            {
                case Define.ShopName.장비상점:
                    catalog = equipSale;
                    break;
                case Define.ShopName.소모품상점:
                    catalog = consumSale;
                    break;
            }
            shopName = nam.ToString();

            Open();
        }

 
        static void Open()
        {
            isBuy = false;

            Console.Clear();
            Console.WriteLine($"-------------{shopName}-------------");
            Console.WriteLine();
            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["방문인사"]}");

            BuyScreen();
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
            isBuy = true;

            Console.Clear();
            Console.WriteLine($"-------------{shopName}-------------");
            Console.WriteLine();
            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["물건볼때"]}");

            BuyScreen();
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

                    if (num < catalog.Length)
                    {

                        if (catalog[num].item != null)
                        {
                            if ((MainLogic.dummy.Gold - catalog[num].item.ItemPrice) >= 0) // 돈이 안모자를때
                            {
                                MainLogic.dummy.Gold -= catalog[num].item.ItemPrice;
                                MainLogic.dummy.Inven.Add(catalog[num].item);

                                int i = catalog[num].item.Id;

                                if (--catalog[num].count <= 0)
                                    catalog[num].item = null;


                                Console.Clear();
                                Console.WriteLine($"-------------{shopName}-------------");
                                Console.WriteLine();
                                Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["구매했을때"]}");
                                BuyScreen();
                                Console.WriteLine($"소지금 : {MainLogic.dummy.Gold} ( -{ItemData.items[i].ItemPrice}원 )");
                                Console.WriteLine();

                                Console.WriteLine($"※ {ItemData.items[i].Name}이 인벤토리에 추가되었습니다.");

                                isBuy = false;
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
                                isBuy = false;
                                BuyScreen();
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

        static void BuyScreen() // 구매화면
        {

            Console.WriteLine();

            Console.WriteLine("[           판매목록           ]");
            Console.WriteLine();

            for (int i = 0; i < catalog.Length; i++)
            {
                if (catalog[i].item != null)
                    Console.WriteLine((isBuy ? $"{i + 1}." : "") + ($"{catalog[i].item.Name} | {catalog[i].item.Info} | {catalog[i].item.ItemPrice}원") + (isBuy && (catalog[i].count > 0) ? ($" | {catalog[i].count}개") : ""));
                else
                    Console.WriteLine((isBuy ? $"{i + 1}." : "") + "-----------------------------  품절  -----------------------------");
            }



            Console.WriteLine();
        }


        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
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