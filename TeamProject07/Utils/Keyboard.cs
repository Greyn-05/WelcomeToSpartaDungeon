using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;

namespace TeamProject07.Utils
{
    internal class Keyboard
    {

        private static int nowX = 1;
        private static int nowY = 6;

        public static ConsoleKey KeyDown()
        {
            bool running = true;

            while (running)
            {
                Console.SetCursorPosition(45, 1); // 좌표 수정
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("ESC : 나가기");
                Console.ResetColor();
                KeyboardPointer(nowX, nowY);

                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        nowY = Math.Max(1, nowY - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        nowY++ ;
                        break;
                    case ConsoleKey.LeftArrow:
                        nowX = Math.Max(1, nowX - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        nowX++;
                        break;
                    case ConsoleKey.Enter:
                        return keyInfo.Key;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                    default:
                        continue;
                }

                return keyInfo.Key;
            }

            return ConsoleKey.Escape;
        }

        public static void KeyboardPointer(int x, int y)
        {
            // Console.SetCursorPosition(x, y);
            // Console.Write("▶");
        }
    }
}
