using System.Collections.Generic;
using System.Windows;
using System.Numerics;


namespace Bv2Av
{

    public partial class MainWindow : Window
    {
        static string[] chars = { "f", "Z", "o", "d", "R", "9", "X", "Q", "D", "S", "U", "m", "2", "1", "y", "C", "k", "r", "6", "z", "B", "q", "i", "v", "e", "Y", "a", "h", "8", "b", "t", "4", "x", "s", "W", "p", "H", "n", "J", "E", "7", "j", "L", "5", "V", "G", "3", "g", "u", "M", "T", "K", "N", "P", "A", "w", "c", "F" };
        List<string> list = new List<string>();
        static int[] s = { 11, 10, 3, 8, 4, 6 };
        static BigInteger xor = BigInteger.Parse("177451812");
        static BigInteger add = BigInteger.Parse("8728348608");
        static BigInteger a58 = BigInteger.Parse("58");
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            foreach (string i in chars)
            {
                list.Add(i);
            }
        }

        private void BV2av_Click(object sender, RoutedEventArgs e)
        {
            string Bv = BVText.Text;
            if (Bv.Length != 12 || Bv.Substring(0, 2) != "BV")
            {
                Msg.Text = "请输入正确格式的BV号~";
                return;
            }
            BigInteger av = new BigInteger();
            int index = 0;
            foreach (int i in s)
            {
                av = av + (BigInteger.Parse(list.IndexOf(Bv[i].ToString()).ToString()) * BigInteger.Pow(a58, index));
                index++;
            } 
            avText.Text = "av" + ((av - add) ^ xor).ToString();
        }

        private void Av2BV_Click(object sender, RoutedEventArgs e)
        {
            BigInteger avbig;
            try
            {
                avbig = (BigInteger.Parse(avText.Text.Substring(2)) ^ xor) + add;
            }
            catch
            {
                Msg.Text = "请输入正确格式的av号~";
                return;
            }            
            string Bv = "BV1  4 1 7  ";
            int index = 0;
            foreach (int i in s) {
                Bv = Bv.Remove(i, 1);
                Bv = Bv.Insert(i, chars[int.Parse((avbig / BigInteger.Pow(a58, index) % a58).ToString())]);
                index++;
            }
            BVText.Text = Bv;


        } 
    }
}