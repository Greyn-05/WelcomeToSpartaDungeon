using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject07.Utils
{
    internal class Define
    {
        public enum GameStages
        {
            Intro,
            Login,
            Base,
            Stat,
            Battle,
        }

        public enum MainGamePhase
        {
            Exit=0,
            Status,
            Inventory,
            Shop,
            Dungeon,
            Main
        }

        public enum Parts
        {
            LeftHand,
            RightHand,
        }

        public enum SkillTypes
        {
            Buff,
            DeBuff,
        }

        public static string ItemPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\ItemData.csv";
        public static string MonsterPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\MonsterData.csv";
        public static string ShopPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\ShopDialogue.csv";
        public static string SkillPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\SkillData.csv";
    }
}
