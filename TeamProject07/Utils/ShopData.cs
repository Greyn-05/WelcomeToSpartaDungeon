namespace TeamProject07.Utils
{
    internal class ShopData
    {
        public enum ShopName // 상점이름
        {
            장비상점,
            소모품상점,
            고물상
        }
        public enum LinePick
        {
            // 0 상점이름
            // 1 상점주인이름

            상점설명 = 2,
            잡답,
            방문인사,
            물건볼때,
            물건골랐을때,
            구매했을때,
            안샀을때,
            돈이부족해,
            전량품절,
            작별인사,
            
            내템봐줘,
            이거팔까,
            팔겠습니다,
            안팔래,
        }


        public static string path = Define.ShopPath; //파일경로
        public static Dictionary<string, Dictionary<string, string>> shopDialogue;

        public static void Init()
        {

            shopDialogue = new Dictionary<string, Dictionary<string, string>>(); // 상점이름 / 언제대사인지 / 대사

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        Dictionary<string, string> dic = new Dictionary<string, string>();

                        dic.Add("주인이름", data[1]);

                        for (int i = 0; i < Enum.GetValues(typeof(LinePick)).Length; i++)
                        {
                            dic.Add(((LinePick)(i + 2)).ToString(), data[i + 2]);
                        }

                        shopDialogue.Add(((ShopName)(int.Parse(data[0]))).ToString(), dic);
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

