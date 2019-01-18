using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDivider
{
    class Program
    {
        static void Main()
        {
            string message = "1101010101111000101111101111101110101110111001110011100101011101011110111001011111100010111110010111111010100111100111101110011011111000101011101111101101101001111000101111101111101110010101111000101111100010111110010100111001001011101011111010101001101011111010100111001010111110101010100110100111001010101001101011001110101100111010101111";
            Console.WriteLine("Длина сообщения: " + message.Length);
            char[] chararr = message.ToCharArray();
            int[] arr = new int[chararr.Length];
            for (int i = 0; i < chararr.Length; i++)
                arr[i] = chararr[i] - 48;
            Dictionary<int, int> adict = new Dictionary<int, int>();
            int counter = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[i-1])
                    counter++;

                else
                {
                    if (adict.ContainsKey(counter))
                        adict[counter]++;
                    else
                        adict.Add(counter, 1);
                    counter = 1;
                }
                if (i == arr.Length - 1)
                    adict[counter]++;
            }
            foreach (KeyValuePair<int, int> kvp in adict.OrderBy(e => e.Key))
                Console.WriteLine(kvp.Key + " " + kvp.Value);

            int f0 = 500;
            int fa = 0;
            foreach (KeyValuePair<int, int> kvp in adict)
                fa += kvp.Value * f0 / kvp.Key;
            float fav = (float) fa/arr.Length;
            Console.WriteLine("fср = " + fav);

            if (arr.Length % 10 == 0)
            {
                Console.WriteLine("---Cкремблинг---");
                int[] brr = new int[arr.Length];
                for (int i = 0; i < arr.Length; i+=10)
                {
                    brr[i] = arr[i];
                    brr[i + 1] = arr[i + 1];
                    brr[i + 2] = arr[i + 2];
                    brr[i + 3] = arr[i + 3] ^ brr[i];
                    brr[i + 4] = arr[i + 4] ^ brr[i + 1];
                    brr[i + 5] = arr[i + 5] ^ brr[i + 2] ^ brr[i];
                    brr[i + 6] = arr[i + 6] ^ brr[i + 3] ^ brr[i + 1];
                    brr[i + 7] = arr[i + 7] ^ brr[i + 4] ^ brr[i + 2];
                    brr[i + 8] = arr[i + 8] ^ brr[i + 5] ^ brr[i + 3];
                    brr[i + 9] = arr[i + 9] ^ brr[i + 6] ^ brr[i + 4];
                }

                Dictionary<int, int> bdict = new Dictionary<int, int>();
                counter = 1;
                for (int i = 1; i < brr.Length; i++)
                {

                    if (brr[i] == brr[i - 1])
                        counter++;

                    else
                    {
                        if (bdict.ContainsKey(counter))
                            bdict[counter]++;
                        else
                            bdict.Add(counter, 1);
                        counter = 1;
                    }
                    if (i == brr.Length - 1)
                        bdict[counter]++;
                }
                foreach (KeyValuePair<int, int> kvp in bdict.OrderBy(e => e.Key))
                    Console.WriteLine(kvp.Key + " " + kvp.Value);

                f0 = 500;
                fa = 0;
                foreach (KeyValuePair<int, int> kvp in bdict)
                    fa += kvp.Value * f0 / kvp.Key;
                fav = (float)fa / arr.Length;
                Console.WriteLine("fср = " + fav);

                string path = "C:\\Users\\Admin\\Desktop\\Test.txt";
                if (!File.Exists(path))
                    File.Create(path);

                StreamWriter file = new StreamWriter(path);
                List<int> scr = brr.ToList();
                string res = "";
                foreach (int s in scr)
                {
                    res = res + s.ToString();
                }
                file.Write(res);
                file.Close();
            }
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
