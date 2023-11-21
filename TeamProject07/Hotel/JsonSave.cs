using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TeamProject07.Logic;
using System;
using System.Diagnostics.CodeAnalysis;
using static TeamProject07.Utils.ShopData;

namespace TeamProject07.Hotel
{
    internal class JsonSave
    {
        // 장착중인템 상태 저장 불러오기 미구현



        public static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\SaveData.json";

        // public static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\" + MainLogic.dummy.Name + ".json";

        public static void Save()
        {

            string inv = "";

            for (int i = 0; i < MainLogic.dummy.Inven.Count; i++)
            {
                if (i == MainLogic.dummy.Inven.Count - 1)
                    inv += $"{MainLogic.dummy.Inven[i].Id}";
                else
                    inv += $"{MainLogic.dummy.Inven[i].Id},";
            }

            JObject characterData = new JObject(
                    new JProperty("Name", MainLogic.dummy.Name),
                    new JProperty("Level", MainLogic.dummy.Level),
                    new JProperty("Class", MainLogic.dummy.Class),
                    new JProperty("Attack", MainLogic.dummy.Attack),
                    new JProperty("Defence", MainLogic.dummy.Defence),
                    new JProperty("Hp", MainLogic.dummy.Hp),
                    new JProperty("MaxHp", MainLogic.dummy.MaxHp),
                    new JProperty("Gold", MainLogic.dummy.Gold),
                    new JProperty("CritRate", MainLogic.dummy.CritRate),
                    new JProperty("MissRate", MainLogic.dummy.MissRate),
                    new JProperty("LevelUpExp", MainLogic.dummy.LevelUpExp),
                    new JProperty("Inven", inv)
                );

            File.WriteAllText(path, characterData.ToString());

        }


        public static void Load() 
        {
            if (File.Exists(path)) 
            {
                using (StreamReader file = File.OpenText(path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject json = (JObject)JToken.ReadFrom(reader);

                        MainLogic.dummy.Name = json["Name"].ToString();
                        MainLogic.dummy.Level = (int)json["Level"];
                        MainLogic.dummy.Class = json["Class"].ToString();
                        MainLogic.dummy.Attack = (int)json["Attack"];
                        MainLogic.dummy.Defence = (int)json["Defence"];
                        MainLogic.dummy.Hp = (int)json["Hp"];
                        MainLogic.dummy.MaxHp = (int)json["MaxHp"];
                        MainLogic.dummy.Gold = (int)json["Gold"];
                        MainLogic.dummy.CritRate = (int)json["CritRate"];
                        MainLogic.dummy.MissRate = (int)json["MissRate"];
                        MainLogic.dummy.LevelUpExp = (int)json["LevelUpExp"];


                        string[] inven = (json["Inven"].ToString()).Split(',');

                        for (int i = 0; i < inven.Length; i++)
                            MainLogic.dummy.Inven.Add(Utils.ItemData.items[int.Parse(inven[i])]);

                    }
                }
            }
        }
    }
}
