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
            Console.WriteLine("0. 메인화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
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
