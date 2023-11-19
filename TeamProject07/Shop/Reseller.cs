using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Logic;
using TeamProject07.Utils;

namespace TeamProject07.Shop
{
    internal class Reseller // 되팔이하는 상점
    {
        static public Define.ShopName name;

        static Random random = new Random();

        static private string shopName;
        static private Item[] catalog;  // 유저가 보는 판매목록


        static private List<Item> resellerInven;  // 고물상이 가진 템목록


        static private bool isBuy = false;
        static private bool isSell = false;

        public static void Init() // 상점 열때마다 재고 추가시키려면 아래에 넣을것
        {
            resellerInven = new List<Item>();
            catalog = new Item[6];

            for (int i = 0; i < 7; i++)
            {
             //   catalog[i] = resellerInven[random.Next(0, resellerInven.Count)];
            }
        }
        static public void Visit(Define.ShopName nam) // 상점 진입  ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        {
            name = nam;
            shopName = nam.ToString();
            Open();
        }


        //     {{ "주인이름", data[1] }, { "방문인사", data[2] }, { "물건볼때", data[3]}, { "구매했을때", data[4] },{ "안살때", data[5] },{ "작별인사", data[6]}};

        static void Open() // 상점 첫페이지  ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        {
            Console.Clear();
            Console.WriteLine($"-------------{shopName}-------------");
            Console.WriteLine();
            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["방문인사"]}");

           // BuyScreen();

            Console.WriteLine();
            Console.WriteLine("1.구매하기");
            Console.WriteLine("2.판매하기");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (CheckValidInput(0, 2))
            {
                case 1:
                 //   TakeMyMoney();
                    break;
                case 2:
                      GiveMeMoney();
                    break;
                case 0:
                    // 나가기
                    break;
            }
        }

        static void NoItem()
        {


         //  resellerInven.Count

        }






        static void GiveMeMoney()
        {
            isBuy = false;
            isSell = true;

            Console.Clear();
            Console.WriteLine($"-------------{shopName}-------------");
            Console.WriteLine();
            Console.WriteLine($"{ShopData.shopDialogue[shopName]["주인이름"]} : {ShopData.shopDialogue[shopName]["방문인사"]}");

            SellScreen();

            Console.WriteLine("판매할 아이템의 숫자를 적고 엔터를 눌러주세요");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

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