using TeamProject07.Items;

namespace TeamProject07.Utils
{
    internal class ItemData
    {
        public static Dictionary<int, Item> items;
        public static string path = Define.ItemPath; //파일경로

        public static void Init()
        {

            items = new Dictionary<int, Item>();
            items.Clear();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                       

                        if (Define.ItemType.Equip == (Define.ItemType)(int.Parse(data[1])))
                        {
                            Equipment item = new Equipment();
                            item.Setting(data[0], data[3], data[4], data[5]); // Item클래스안에 들어있는 변수만 초기화 
                            item.Type = (Define.ItemType)(int.Parse(data[1]));

                            item.Part = (Define.Parts)(int.Parse(data[5]));
                            item.buffName = (Define.Buff)(int.Parse(data[6]));
                            item.point = int.Parse(data[7]);

                            item.set = (Define.SetEquip)int.Parse(data[8]);

                            items.Add(item.Id, item);
                        }
                        else
                        {
                            ConsumableItem item = new ConsumableItem();
                            item.Setting(data[0], data[3], data[4], data[5]);  // Item클래스안에 들어있는 변수만 초기화 
                            item.Type = (Define.ItemType)(int.Parse(data[1]));
                            item.point = int.Parse(data[7]);

                            items.Add(item.Id, item);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("파일을 찾을 수 없습니다.");
            }
        }

    }
}
