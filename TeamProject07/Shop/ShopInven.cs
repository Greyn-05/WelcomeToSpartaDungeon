using static TeamProject07.Utils.ShopData;

namespace TeamProject07
{
    internal class ShopInven
    {
        public ShopInvenSlot[] slots;

        public int slotCount = 0;
        public int maxSlot = 8; //  최대 크기

        public string name = "";

        public ShopInven(ShopName ne) 
        {
            slots = new ShopInvenSlot[maxSlot];

            for (int i = 0; i < slots.Length; i++)
            {
                ShopInvenSlot inventorySlot = new ShopInvenSlot();
                slots[i] = inventorySlot;
            }

            name = ne.ToString();

        }


        public bool Add(Item item) 
        {
            if (slotCount < maxSlot)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null && slots[i].item.Id == item.Id)
                    {
                        slots[i].count++;
                        return true;
                    }
                }

                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item == null)
                    {
                        slots[i].item = item;
                        slots[i].count++;
                        slotCount++;
                        break;
                    }
                }

                return true;
            }
            else // 인벤 공간부족
            {
                return false;
            }
        }

        public bool Delete(Item item) 
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.Id == item.Id)
                {
                    slots[i].count--;

                    if (slots[i].count <= 0)
                    {
                        slots[i].item = null;
                        slots[i].count = 0;
                        slotCount--;
                    }

                    return true;
                }
            }

            return false;

        }


        public void Mix() // id 낮은것부터 높은 순으로 정렬 실험
        {
            Array.Sort(slots, (a, b) =>
            {
                if (a.item == null)
                    return 1;
                else
                    return ((a.item.Id > b.item.Id) ? 1 : -1);
            }); // 값이 작은것부터 위에서 표시된다. null은 가장 하단 

            // Array.Sort(consumSale.slots, (a, b) => (a.item.ItemPrice > b.item.ItemPrice) ? 1 : -1);
            // Array.Sort C#
            // compareTo

        }

    }
}

