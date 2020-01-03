using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace InfoSnifferForm.Class
{
    public class StringUtil
    {
        /// <summary>
        /// 此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将
        /// 四个字节数组存储在object数组中。
        /// 参数：length，代表需要产生的汉字个数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRegionChineseCharacter(int length)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
            StringBuilder sb = new StringBuilder();

            /**/
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
            每个汉字有四个区位码组成
            区位码第1位和区位码第2位作为字节数组第一个元素
            区位码第3位和区位码第4位作为字节数组第二个元素
            */
            for (int i = 0; i < length; i++)
            {
                //区位码第1位
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中
                byte[] str_r = new byte[] { byte1, byte2 };

                sb.Append(gb.GetString(str_r));

            }


            return sb.ToString();

        }

        public static string GenerateRandomString(int length)
        {
            byte[] random = new Byte[length / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);

            StringBuilder sb = new StringBuilder(length);
            int i;
            for (i = 0; i < random.Length; i++)
            {
                sb.Append(String.Format("{0:X2}", random[i]));
            }
            return sb.ToString();
        }
    }
}
