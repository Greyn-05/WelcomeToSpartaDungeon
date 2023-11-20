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
            Exit = 0,
            Status,
            Inventory,
            Shop,
            Dungeon,
            Main,
            temp
        }



        public enum Parts
        {
            Weapon,
            Head,
            Body,
            Hand,
            Foot,
            Accessory
        }

        public enum SkillTypes
        {
            Buff,
            DeBuff,
        }

        public enum Buff // 올려준거거 하나
        {
            atk,
            def,
            cri,
            dex,
            hp
        }
        public enum DeBuff // 낮추는거 하나
        {
            atk,
            def,
            cri,
            dex,
            hp
        }


        public enum SetEquip
        {
            세트효과없음,
            종이세트,
            나무세트,
            철세트,
            아다만세트
        }
        public enum ItemType
        {
            Equip,
            Consum
        }


        public static string ItemPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\ItemData.csv";
        public static string MonsterPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\MonsterData.csv";
        public static string ShopPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\ShopDialogue.csv";
        public static string SkillPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\CSV\\SkillData.csv";



    }
}
