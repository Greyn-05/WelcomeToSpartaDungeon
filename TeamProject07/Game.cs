using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Logic;
using TeamProject07.Shop;
using TeamProject07.Utils;

namespace TeamProject07
{
    internal class Game
    {
        static void Main(string[] args)
        {


            MainLogic mainLogic = new MainLogic();

            mainLogic.Start();

            mainLogic.Game();

            mainLogic.End();
        }
    }
}
