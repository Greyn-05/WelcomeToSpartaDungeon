using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject07.Hotel
{
    internal class WoodCarving
    {
        static int requestCount = 5; //의뢰받은 나무장작 수
        static int crushCount = 0;

        static int maxKeyPressCount = 10; // 장작내구도
        static int keyPressCount = 0;


        static Stopwatch stopwatch = new Stopwatch();
        static void PartTime_WoodCarving() // 호출하면 장작패기 시작
        {
            Console.WriteLine("장작패기 알바를 시작합니다");
            Console.WriteLine(" 아무키나 누르면 시작됩니다 ");

            Console.ReadKey(true);
            Console.Clear();

            stopwatch.Start();

            Console.WriteLine("키를 연타해서 장작을 뽀개세요!!");


            for (int i = 0; i < requestCount; i++)
            {
                Console.Write('■');
            }
            Console.WriteLine();

            while (stopwatch.ElapsedMilliseconds < 3000)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey();
                    keyPressCount++;

                    if (keyPressCount >= maxKeyPressCount)
                    {
                        keyPressCount = 0;
                        crushCount++;

                        Console.WriteLine();
                        for (int i = 0; i < requestCount - crushCount; i++)
                        {
                            Console.Write('■');
                        }

                        Console.WriteLine();
                        Console.WriteLine($"남은 장작 수  : {requestCount - crushCount}개");
                    }
                }

                if (crushCount >= requestCount)
                    break;

            }

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine($"알바가 끝났습니다. 총 {crushCount}개 장작을 팼습니다.");

            Thread.Sleep(10000);
        }
    }
}