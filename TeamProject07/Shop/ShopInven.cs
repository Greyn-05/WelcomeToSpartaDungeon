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
    }
}

