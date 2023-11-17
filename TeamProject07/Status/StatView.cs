using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamProject07.Characters;
using TeamProject07.Items;

namespace TeamProject07.View
{


    internal class StatView
    {
        public void View(Player player, Equipment equip)
        {
            // 장착장비 어떻게 될지 보고 매개변수, 다시 작업, 틀만 
            // 배열로 받아오면 ++

            var table = new ConsoleTable($" ", $" {player.Name} ", $" {player.Class} ", "  ");
            table.AddRow(" 스탯 ", " 총스탯 ", " 기본스탯 ", " + 장비")
                 .AddRow(" 공 격 력 ", $"{player.Attack + equip.AddAttack} ", $" {player.Attack} ", $" ({equip.AddAttack})")
                 .AddRow(" 방 어 력 ", $"{player.Defence + equip.AddDefence}", $" {player.Defence} ", $" ({equip.AddDefence}) ")
                 .AddRow(" 체    력 ", $"{player.Hp + equip.AddHp}", $" {player.Hp} ", $" ({equip.AddHp}) ")
                 .AddRow(" 크리티컬 ", $"{player.CritRate + equip.AddCritRate}", $" {player.CritRate} ", $" ({equip.AddCritRate}) ")
                 .AddRow(" 회 피 율 ", $"{player.MissRate + equip.AddMissRate}", $" {player.MissRate} ", $" ({equip.AddMissRate}) ")
                 .AddRow(" 소지금 ", " 골드(G) ", " ", " ")
                 .AddRow("   ", $" {player.Gold} G ", "", "");
            table.Write();

            Console.WriteLine("=============");
            Console.WriteLine("= 1. 나가기 =");
            Console.WriteLine("=============");
        }
    }
}

