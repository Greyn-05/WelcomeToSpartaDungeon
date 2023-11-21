using System;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Newtonsoft.Json;
using TeamProject07.Characters;
using TeamProject07.Logic;

namespace TeamProject07.Hotel
{
    internal class JsonSave
    {
        public static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\SaveData.json";

        public static void Save()
        {
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
                    new JProperty("LevelUpExp", MainLogic.dummy.LevelUpExp)
                );


            List<int> inven = new List<int>();


            for (int i = 0; i < MainLogic.dummy.Inven.Count; i++)
            {
                inven.Add(MainLogic.dummy.Inven[i].Id);
            }

            characterData.Add("Inven", JArray.FromObject(inven));

            File.WriteAllText(path, characterData.ToString());


        }


        void Load()
        {
            if (File.Exists(path)) // 세이브 파일이 존재할때만
            {

                using (StreamReader file = File.OpenText(path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject json = (JObject)JToken.ReadFrom(reader);

                        //      config.Ip = json["레벨"].ToString();
                        //      config.Port = (int)json["Port"];
                        //     config.LogPath = json["LogPath"].ToString();
                        //     config.LogKeepDate = (int)json["LogKeepDate"];
                    }
                }

            }
        }
    }
}
