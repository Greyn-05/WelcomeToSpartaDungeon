using static TeamProject07.Utils.ShopData;
using static TeamProject07.Utils.ItemData;

namespace TeamProject07.Shop
{
    static class Shop_Reseller
    {

        static Random random = new Random();

        static public ShopInven reSellSale; // 되팔이상점
        static public List<Item> resellerInven;  // 고물상이 가진 템목록. 내가 고물상한테 판매한 템 목록, 아이템 큐로 해야하나

        static public void Init()
        {
            reSellSale = new ShopInven(ShopName.고물상);
            resellerInven = new List<Item>();

            ReLoad();
        }

        static public void ReLoad() 
        {
            for (int i = 0; i < resellerInven.Count; i++) // 될라나??
            {
                int num = random.Next(0, resellerInven.Count);
                reSellSale.Add(resellerInven[num]);
                resellerInven.Remove(resellerInven[num]);

            }
        }
    }
}