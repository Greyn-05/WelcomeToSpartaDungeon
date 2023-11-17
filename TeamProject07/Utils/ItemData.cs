using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.ComponentModel.Design;
using System.Drawing;
using TeamProject07.Utils;
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


                        if (data[1] == "장비")
                        {
                            Equipment item = new Equipment();
                            item.Setting(data[0], data[2], data[3], data[4]); // Item클래스안에 들어있는 변수만 초기화 
                            item.Type = Define.ItemType.Equip;
                            item.Part = (Define.Parts)(int.Parse(data[5]));
                            item.buffName = (Define.Buff)(int.Parse(data[6]));
                            item.point = int.Parse(data[7]);

                            item.set = (Define.SetEquip)int.Parse(data[8]);

                            items.Add(item.Id, item);
                        }
                        else
                        {
                            ConsumableItem item = new ConsumableItem();
                            item.Setting(data[0], data[2], data[3], data[4]); // Item클래스안에 들어있는 변수만 초기화 
                            item.Type = Define.ItemType.Consum;
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
