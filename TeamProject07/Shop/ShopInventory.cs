namespace TeamProject07
{
    internal class ShopInventory
    {
        public ShopInventorySlot[] slots;

        public int slotCount = 0;
        public int maxSlot = 10; //  최대 크기

        public ShopInventory() //생성자
        {
            slots = new ShopInventorySlot[maxSlot];

            for (int i = 0; i < slots.Length; i++)
            {
                ShopInventorySlot inventorySlot = new ShopInventorySlot();
                slots[i] = inventorySlot;
            }
        }
    

        public bool Add(Item item) // 소지품에 추가, 소모품은 겹쳐짐
        {
            if (slotCount < maxSlot)
            {
                if (item.Type == Utils.Define.ItemType.Consum)
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].item != null && slots[i].item.Id == item.Id)
                        {
                            slots[i].count++;
                            return true;
                        }
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

