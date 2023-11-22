using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Characters;
using TeamProject07.Utils;

namespace TeamProject07.Hotel
{
    internal class HotelMain
    {
        private BlackJack BJ = new BlackJack();
        public Define.MainGamePhase Entrance(Player player)
        {
            while(true)
            {
                bool Kill_hotel = false;
                Console.Clear();
                Console.WriteLine("호텔 아트 추가?");
                Console.WriteLine("호텔입니다.");
                Console.WriteLine("여기서는 휴식과 카지노를 할 수 있습니다.\n");

                Console.WriteLine("1. 휴식하기 (체력을 50% 회복합니다.)");
                Console.WriteLine("2. 카지노 입장 (미니게임을 통해 골드를 벌 수 있습니다.)");
                Console.WriteLine("3. 숙박하기 (저장합니다.)");

                Console.WriteLine("0. 메인화면으로 돌아가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요\n>>");

                int input = CheckValidInput(0, 4);
                switch (input)
                {
                    case 1:
                        rest(player);
                        break;
                    case 2:
                        break;
                    case 3:
                        JsonSave.Save();
                        break;
                    case 0:
                        Kill_hotel = true;
                        break;
                }

                if(Kill_hotel == true)
                {
                    break;
                }

            }
            return Define.MainGamePhase.Main;
        }
        private void casino()
        {

        }

        private void rest(Player player)
        {
            Console.WriteLine("휴식 하시겠습니까?");
            Console.WriteLine("100gold가 필요합니다.\n");

            Console.WriteLine("1. 예");
            Console.WriteLine("0. 아니오");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 1:
                    if((player.Gold < 100)||(player.Hp==player.MaxHp))
                    {
                        if(player.Gold < 100)
                        {
                            Console.WriteLine("Gold가 부족합니다.");
                            Thread.Sleep(400);
                        }
                        else
                        {
                            Console.WriteLine("이미 체력이 충분합니다.");
                            Thread.Sleep(400);
                        }
                    }
                    else
                    {
                        if(player.IsDead == true)
                        {
                            player.IsDead = false;
                        }
                        player.Gold -= 100;
                        int showhp = player.Heal((player.MaxHp / 2));
                        showHealProcess(showhp);
                    }
                    break;
                case 0:
                    break;
            }
        }
        
        private void showHealProcess(int Healrate)
        {
            for(int cnt = 0; cnt < 10; cnt++)
            {
                Console.Clear();
                Console.Write(" 휴식중");
                if(cnt%3==0)
                {
                    Console.WriteLine("...");
                }
                else if(cnt%3==1)
                {
                    Console.WriteLine("......");
                }
                else
                {
                    Console.WriteLine(".........");
                }
                Thread.Sleep(200);
            }
            Console.WriteLine($" 체력이 {Healrate}만큼 회복되었습니다.!");
            Thread.Sleep(350);
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
