namespace TeamProject07.Utils
{
    internal class ShopData
    {
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


                        Dictionary<string, string> dic = new Dictionary<string, string>
                               {{ "주인이름", data[1] }, { "방문인사", data[2] }, { "물건볼때", data[3]}, { "구매했을때", data[4] },{ "안살때", data[5] },{ "작별인사", data[6]},{"물건골랐을때", data[7] } };

                        switch (data[0])
                        {
                            case "장비":
                                shopDialogue.Add(Define.ShopName.장비상점.ToString(), dic);
                                break;
                            case "소모품":
                                shopDialogue.Add(Define.ShopName.소모품상점.ToString(), dic);
                                break;
                            case "고물상":
                                shopDialogue.Add(Define.ShopName.고물상.ToString(), dic);
                                break;
                            default:
                                Console.WriteLine("상점데이터에 문제가 생겼습니다");
                                break;
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

