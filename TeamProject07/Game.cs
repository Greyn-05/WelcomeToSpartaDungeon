using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Logic;
using TeamProject07.Utils;

namespace TeamProject07
{
    internal class Game
    {
        static void Main(string[] args)
        {
            ItemData.Init();
            ShopData.Init();
            Shop.Shop.Init();
            Shop.Reseller.Init();


            MainLogic mainLogic = new MainLogic();

            mainLogic.Start();

            mainLogic.Game();

            mainLogic.End();
        }
    }
}
