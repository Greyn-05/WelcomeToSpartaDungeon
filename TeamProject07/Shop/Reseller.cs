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
        static Random random = new Random();


        static private List<Item> resellerInven;  // 고물상이 가진 템목록

        public static void Init()
        {
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