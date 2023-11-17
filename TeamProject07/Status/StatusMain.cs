using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;

namespace TeamProject07.Status
{
    internal class StatusMain
    {
        public Define.MainGamePhase test()
        {
            
            Define.MainGamePhase choicePhase;
            Console.Clear();
            Console.WriteLine("상태창입니다.");
            Console.WriteLine("플레이어의 상태를 확인할 수 있습니다.");
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
