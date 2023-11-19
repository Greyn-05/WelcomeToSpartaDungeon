using TeamProject07.Utils;
using static TeamProject07.Utils.Define;

namespace TeamProject07
{
    internal class ShopInven
    {
        public ShopInvenSlot[] slots;

        public int slotCount = 0;
        public int maxSlot = 8; //  최대 크기

        public string name = "";

        public ShopInven(ShopName ne) //생성자
        {
            slots = new ShopInvenSlot[maxSlot];

            for (int i = 0; i < slots.Length; i++)
            {
                ShopInvenSlot inventorySlot = new ShopInvenSlot();
                slots[i] = inventorySlot;
            }

            name = ne.ToString();

        }


        public bool Add(Item item) // 소지품에 추가, 소모품은 겹쳐짐
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

        public bool Delete(Item item) // 인벤에서 같은 id템 삭제
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

