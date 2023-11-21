using static TeamProject07.Utils.ShopData;
using static TeamProject07.Utils.ItemData;

namespace TeamProject07.Shop
{
    static class Shop_Normal
    {

        static Random random = new Random();

        static public ShopInven equipSale; // 장비상점 판매목록
        static public ShopInven consumSale; // 소모품상점 판매목록

        static public void Init()
        {
            equipSale = new ShopInven(ShopName.장비상점);
            consumSale = new ShopInven(ShopName.소모품상점);

        }

        static public void ReLoad() // 상점에 물건 채우기. 재고추가할거면 실행 여러번
        {

            equipSale.ClearArray();
            consumSale.ClearArray();

            for (int i = 0; i < 20; i++)
            {
                equipSale.Add(items[random.Next(0, 24)]);
            }
            for (int i = 0; i < 150; i++)
            {
                consumSale.Add(items[random.Next(24, 33)]);
            }

        }
    }
}
