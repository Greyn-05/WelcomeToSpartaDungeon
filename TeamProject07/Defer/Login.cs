using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject07.Utils;
using TeamProject07.Characters;
using System.IO;
using System.Numerics;
using System.Diagnostics;

namespace TeamProject07.Defer
{
    /*internal class LoginController
    {
        public string _id = "bus";
        private string _password = "123";
        
        private LoginView _loginView = new LoginView();
        public Player PlayerData = new Player();

        public string path = "PlayerData.csv";
        
        public void Load()
        {
           
            PlayerData = new Player();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    string line = sr.ReadLine();
                    string[] data = line.Split(',');
                    PlayerData = new Player(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5]), int.Parse(data[6]), int.Parse(data[7]), int.Parse(data[8]));
                    sr.Close();
                }
            }
        }

        public void Save()
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write(PlayerData.Name+","+PlayerData.Class+","+ PlayerData.Level + "," + PlayerData.Attack + "," + PlayerData.Defence + "," + PlayerData.Hp + "," + PlayerData.Gold + "," +
                PlayerData.CritRate + "," + PlayerData.MissRate);
            writer.Close();
        }

        public void Login()
        {

            Console.WriteLine("아이디(ID)를 입력해 주세요");
            String _InputId = Console.ReadLine();
            if (_id != null && _InputId == _id)
            {
                Console.WriteLine("비밀번호(PassWord)를 입력해 주세요");
                String _InputpassWord = Console.ReadLine();
                if (_InputpassWord != null && _password == _InputpassWord)
                {
                    Load();
                    MainController.getInst().CurrentStage = Define.GameStages.Base;
                }
            }
            else {
                Console.Clear();
                Console.WriteLine("다시 입력해주세요.");
                Login();
            }

            _loginView.View(_id, PlayerData);
        }
    }*/
}
