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


        public static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\" + MainLogic.player.Name + ".json";

        public static void Save()
        {

            string inv = ""; // 아이템목록

            string eqi = ""; // 착용중인 목록


            for (int i = 0; i < MainLogic.player.Inven.Count; i++)
            {
                if (i == MainLogic.player.Inven.Count - 1)
                {
                    inv += $"{MainLogic.player.Inven[i].Id}";
                    eqi += (MainLogic.player.Inven[i].IsEquipped) ? 1 : 0;

                }
                else
                {
                    inv += $"{MainLogic.player.Inven[i].Id},";
                    eqi += (MainLogic.player.Inven[i].IsEquipped) ? 1 + "," : 0 + ",";
                }

            }

            JObject characterData = new JObject(
                    new JProperty("Name", MainLogic.player.Name),
                    new JProperty("Level", MainLogic.player.Level),
                    new JProperty("Class", MainLogic.player.Class),
                    new JProperty("Attack", MainLogic.player.Attack),
                    new JProperty("Defence", MainLogic.player.Defence),
                    new JProperty("Hp", MainLogic.player.Hp),
                    new JProperty("MaxHp", MainLogic.player.MaxHp),
                    new JProperty("Mp", MainLogic.player.Mp),
                    new JProperty("MaxMp", MainLogic.player.MaxMp),
                    new JProperty("Gold", MainLogic.player.Gold),
                    new JProperty("CritRate", MainLogic.player.CritRate),
                    new JProperty("MissRate", MainLogic.player.MissRate),
                    new JProperty("LevelUpExp", MainLogic.player.LevelUpExp),
                    new JProperty("Inven", inv),
                    new JProperty("Equip", eqi)

                );

            File.WriteAllText(path, characterData.ToString());

        }


        public static void Load()
        {


            string nameTest = "ddd";


            path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\" + nameTest + ".json";



            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject json = (JObject)JToken.ReadFrom(reader);

                        MainLogic.player.Name = json["Name"].ToString();
                        MainLogic.player.Level = (int)json["Level"];
                        MainLogic.player.Class = json["Class"].ToString();
                        MainLogic.player.Attack = (int)json["Attack"];
                        MainLogic.player.Defence = (int)json["Defence"];
                        MainLogic.player.Hp = (int)json["Hp"];
                        MainLogic.player.MaxHp = (int)json["MaxHp"];
                        MainLogic.player.Mp = (int)json["Mp"];
                        MainLogic.player.MaxMp = (int)json["MaxMp"];


                        MainLogic.player.Gold = (int)json["Gold"];
                        MainLogic.player.CritRate = (int)json["CritRate"];
                        MainLogic.player.MissRate = (int)json["MissRate"];
                        MainLogic.player.LevelUpExp = (int)json["LevelUpExp"];



                        string[] equip = (json["Equip"].ToString()).Split(',');
                        string[] inven = (json["Inven"].ToString()).Split(',');

                        for (int i = 0; i < inven.Length; i++)
                        {
                            Item itme = new Item();
                            itme = (Utils.ItemData.items[int.Parse(inven[i])]);
                            itme.IsEquipped = (equip[i] == "1") ? true : false;

                            MainLogic.player.Inven.Add(itme);
                        }

                    }

                }
            }
        }
    }
}