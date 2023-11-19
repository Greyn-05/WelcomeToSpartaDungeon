using static TeamProject07.Utils.ShopData;

namespace TeamProject07.Shop
{
    internal class Shop_Reseller : Shop // 되팔이하는 상점
    {

        static public ShopInven reSellSale; // 되팔이상점
        static public List<Item> resellerInven;  // 고물상이 가진 템목록. 내가 고물상한테 판매한 템 목록, 아이템 큐로 해야하나

        public static void Init()
        {
            reSellSale = new ShopInven(ShopName.고물상);
            resellerInven = new List<Item>();

            ReLoad();
        }

        public static void ReLoad() 
        {
            for (int i = 0; i < resellerInven.Count; i++) // 될라나??
            {
                int num = random.Next(0, resellerInven.Count);
                reSellSale.Add(resellerInven[num]);
                resellerInven.Remove(resellerInven[num]);

            }
        }
        static public void Visit(ShopName nam)
        {
            switch (nam)
            {
                case ShopName.고물상:
                    catalog = reSellSale;
                    break;
            }

            Open();
        }
    }
}