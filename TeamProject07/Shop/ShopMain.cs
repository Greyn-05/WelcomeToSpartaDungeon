using TeamProject07.Utils;

namespace TeamProject07.Shop
{
    internal class ShopMain
    {


        public Define.MainGamePhase test()
        {
            Define.MainGamePhase choicePhase;
            Console.Clear();
            Console.WriteLine("상점입니다.");
            Console.WriteLine("아이템을 사고팔 수 있습니다.");

            Console.WriteLine("1. 장비상점");
            Console.WriteLine("2. 소모품상점");
            Console.WriteLine("3. 고물상");
            Console.WriteLine("0. 메인화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 3);
            switch (input)
            { case 1:
                    Shop_Normal.Visit(ShopData.ShopName.장비상점);
                    break;
                case 2:
                    Shop_Normal.Visit(ShopData.ShopName.소모품상점);
                    break;
                case 3:
                    Shop_Reseller.Visit(ShopData.ShopName.고물상);
                    break;
                case 0:
                    choicePhase = Define.MainGamePhase.Main;
                    break;
                    
                    
            }
            return Define.MainGamePhase.Main;
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
