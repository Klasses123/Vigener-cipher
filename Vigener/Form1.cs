using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vigener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly static string Alfavit = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        static int[] mas;
        static List<SymbRepeat> ListRepeat2 = new List<SymbRepeat>();
        static List<SymbRepeat> ListRepeat3 = new List<SymbRepeat>();
        static List<SymbRepeat> ListRepeat4 = new List<SymbRepeat>();

        private static bool KeyCheck(string key)
        {
            if (key.Length != 0)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (Convert.ToInt32(key[i]) < 1072 || Convert.ToInt32(key[i]) > 1103)
                    {
                        return false;
                    }
                }
            }
            else return false;
            return true;
        }

        public static char[] ClearInput(string str)
        {
            str = str.ToLower();
            str = str.Replace('ё', 'е');
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(str[i]) >= 1072 && Convert.ToInt32(str[i]) <= 1103)
                {
                    j++;
                }
            }
            char[] mass = new char[j];
            j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(str[i]) >= 1072 && Convert.ToInt32(str[i]) <= 1103)
                {
                    mass[j] = str[i];
                    j++;
                }
            }
            return mass;
        }

        public static int[] KeyChange(string key, bool EnDe)
        {
            int[] IntKey = new int[key.Length];
            //true - Зашифровка; false - Расшифровка
            for (int i = 0; i < IntKey.Length; i++)
            {
                if (EnDe == true)
                {
                    IntKey[i] = key[i] - 1072;
                }
                else
                {
                    IntKey[i] = -(key[i] - 1072);
                }
            }
            return IntKey;
        }

        public static char[] Crypt(char[] str, int[] IntKey)
        {
            int a = 0;
            int sch = 0;
            for (int i = 0; i < str.Length; i++, sch++)
            {
                if (sch == IntKey.Length)
                {
                    sch = 0;
                }
                a = str[i] + IntKey[sch];
                if (a < 1072)
                {
                    a += 32;
                }
                else if (a > 1103)
                {
                    a -= 32;
                }
                str[i] = Convert.ToChar(a);
            }
            return str;
        }

        public static string print(char[] str)
        {
            int a = 0;
            string strin = "";
            for (int i = 0; i < str.Length; i++, a++)
            {
                if (a == 6)
                {
                    a = 0;
                    strin += " ";
                }
                strin += str[i];
            }
            return strin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1 - ключ, 2 - входной, 3 - результат(текстбоксы)
            if (KeyCheck(textBox1.Text))
            {
                char[] str = Crypt(ClearInput(textBox2.Text), KeyChange(textBox1.Text, true));
                textBox3.Text = print(str);
            }
            else
            {
                textBox3.Text = "Некорректный ввод данных!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (KeyCheck(textBox1.Text))
            {
                textBox3.Text = print(Crypt(ClearInput(textBox2.Text), KeyChange(textBox1.Text, false)));
            }
            else
            {
                textBox3.Text = "Некорректный ввод данных!";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        public static string Break(char[] mass)
        {
            string key = "";
            FindRepeat6(mass);
            FindRepeat4(mass);
            FindRepeat5(mass);
            int[] GCDrepeat3 = PovtorNOD(ListRepeat3);
            int[] GCDrepeat2 = PovtorNOD(ListRepeat2);
            int[] GCDrepeat4 = PovtorNOD(ListRepeat4);
            int[] GCDrepeat = new int[GCDrepeat2.Length + GCDrepeat3.Length + GCDrepeat4.Length];
            int index = 0;
            for (int i = 0; i < GCDrepeat2.Length; i++, index++)
            {
                GCDrepeat[index] = GCDrepeat2[i];
            }
            for (int i = 0; i < GCDrepeat3.Length; i++, index++)
            {
                GCDrepeat[index] = GCDrepeat3[i];
            }
            for (int i = 0; i < GCDrepeat4.Length; i++, index++)
            {
                GCDrepeat[index] = GCDrepeat4[i];
            }
            GCDrepeat = Without1(GCDrepeat);
            int Length = GetLength(GCDrepeat);

            string[] SubStrings = Substringer(Length, mass);

            key = GetKey(SubStrings);
            return key;
        }

        public static string GetKey(string[] SubStrings)
        {
            string key = "";
            double[] chastota = new double[32];
            chastota[0] = 0.062;//а
            chastota[1] = 0.014;//б
            chastota[2] = 0.038;//в
            chastota[3] = 0.013;//г
            chastota[4] = 0.025;//д
            chastota[5] = 0.072;//е
            chastota[6] = 0.007;//ж
            chastota[7] = 0.016;//з
            chastota[8] = 0.062;//и
            chastota[9] = 0.010;//й
            chastota[10] = 0.028;//к
            chastota[11] = 0.035;//л
            chastota[12] = 0.026;//м
            chastota[13] = 0.053;//н
            chastota[14] = 0.090;//о
            chastota[15] = 0.023;//п
            chastota[16] = 0.040;//р
            chastota[17] = 0.045;//с
            chastota[18] = 0.053;//т
            chastota[19] = 0.021;//у
            chastota[20] = 0.002;//ф
            chastota[21] = 0.002;//х
            chastota[22] = 0.003;//ц
            chastota[23] = 0.012;//ч
            chastota[24] = 0.006;//ш
            chastota[25] = 0.003;//щ
            chastota[26] = 0.007;//ъ
            chastota[27] = 0.016;//ы
            chastota[28] = 0.007;//ь
            chastota[29] = 0.003;//э
            chastota[30] = 0.006;//ю
            chastota[31] = 0.018;//я

            for (int k = 0; k < SubStrings.Length; k++)
            {
                int[] masi = new int[32];
                for (int i = 0; i < SubStrings[k].Length; i++)
                {
                    for (int j = 0; j < Alfavit.Length; j++)
                    {
                        if (SubStrings[k][i] == Alfavit[j])
                        {
                            masi[j] = masi[j] + 1;
                        }
                    }
                }

                int sch = 0;
                for (int i = 0; i < SubStrings[k].Length; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if (SubStrings[k][i] == Alfavit[j])
                        {
                            sch++;
                            break;
                        }
                    }
                }

                double[] Try_Dec = new double[32];

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        int bbb = j + i;
                        if (bbb > 31)
                        {
                            bbb -= 32;
                        }
                        Try_Dec[i] += Math.Pow((Math.Round(((double)masi[bbb] / sch), 4) - chastota[j]), 2);
                    }
                }
                int preKey = 0;
                double min = 1000000;
                for (int i = 0; i < 32; i++)
                {
                    if (min > Try_Dec[i])
                    {
                        min = Try_Dec[i];
                        preKey = i;
                    }
                }
                key += Alfavit[preKey];
            }
            return key;
        }

        public static string[] Substringer(int a, char[] str)
        {
            string[] SubStrings = new string[a];
            a = 0;
            for (int i = 0; i < str.Length; i++, a++)
            {
                if (a == SubStrings.Length)
                {
                    a = 0;
                }
                SubStrings[a] += str[i];
            }
            return SubStrings;
        }

        public static int GetLength(int[] GCD)
        {
            var most = GCD.GroupBy(x => x).OrderByDescending(x => x.Count()).First();
            int a = 0;
            a = most.Key;
            List<int> variation = new List<int>();
            variation.Add(a);
            for (int i = 0; i < GCD.Length; i++)
            {
                bool boo = true;
                foreach (int INT in variation)
                {
                    if (GCD[i] == INT)
                    {
                        boo = false;
                    }
                }
                if (boo)
                {
                    variation.Add(GCD[i]);
                }
            }

            return a;
        }

        public static int[] Without1(int[] GCD)
        {
            int sch = 0;
            int[] mas;
            for (int i = 0; i < GCD.Length; i++)
            {
                if (GCD[i] != 1 && GCD[i] != 2)
                {
                    sch++;
                }
            }
            mas = new int[sch];
            sch = 0;
            for (int i = 0; i < GCD.Length; i++)
            {
                if (GCD[i] != 1 && GCD[i] != 2)
                {
                    mas[sch] = GCD[i];
                    sch++;
                }
            }
            return mas;
        }

        static int GCD(params int[] n)
        {
            int min = n.Min(), i;
            for (i = min; i > 0; i--)
            {
                bool reminderIsZero = true;
                for (int j = 0; j < n.Length; j++)
                    reminderIsZero &= n[j] % i == 0;
                if (reminderIsZero) break;
            }
            return i;
        }


        //метод для поиска НОД расстояний между символами
        private static int[] PovtorNOD(List<SymbRepeat> symbs)
        {
            int sch = 0;

            foreach (SymbRepeat obj in symbs)
            {
                sch++;
            }
            mas = new int[sch];
            int[] GCDrepeat = new int[sch];
            sch = 0;
            foreach (SymbRepeat obj in symbs)
            {
                GCDrepeat[sch] = GCD(obj.SR);
                sch++;
            }
            return GCDrepeat;
        }

        public static void FindRepeat6(char[] mass)
        {
            List<string> ThreeS = new List<string>();
            ListRepeat3 = new List<SymbRepeat>();

            for (int i = 0; i < mass.Length - 11; i++)
            {
                string str = "";
                str += mass[i];
                str += mass[i + 1];
                str += mass[i + 2];
                str += mass[i + 3];
                str += mass[i + 4];
                str += mass[i + 5];
                bool boo = false;
                foreach (string CHECK in ThreeS)
                {
                    if (str == CHECK)
                    {
                        boo = true;
                        break;
                    }
                }
                if (!boo)
                {
                    int sch = 0;
                    int[] Repeat = new int[2];
                    for (int j = i + 3; j < mass.Length - 5; j++)
                    {
                        if (i != j && (j - i) > 0)
                        {
                            string str1 = "";
                            str1 += mass[j];
                            str1 += mass[j + 1];
                            str1 += mass[j + 2];
                            str1 += mass[j + 3];
                            str1 += mass[j + 4];
                            str1 += mass[j + 5];
                            if (str == str1)
                            {
                                Repeat[sch] = j - i;
                                sch++;
                            }
                            if (sch == 2)
                            {
                                ListRepeat3.Add(new SymbRepeat { SR = Repeat });
                                ThreeS.Add(str);
                                sch = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void FindRepeat3(char[] mass)
        {
            //лист для проверки повторений(3-х символьных повторов), чтобы заного не считать их повторения в тексте;
            List<string> ThreeS = new List<string>();
            ListRepeat3 = new List<SymbRepeat>();

            //поиск 3х-симв повторений в текстеи запись их в список лист, а расстояний между ними в свойство класса SymbRepeat;
            //но при условии, если они встречаются в тексте 4 раза, или, соответственно, больше!
            //все объекты данного класса записываются в ListRepeat3;
            for (int i = 0; i < mass.Length - 5; i++)
            {
                string str = "";
                str += mass[i];
                str += mass[i + 1];
                str += mass[i + 2];
                bool boo = false;
                //проверка на наличие такой 3-ки символов в листе повторов;
                foreach (string CHECK in ThreeS)
                {
                    if (str == CHECK)
                    {
                        boo = true;
                        break;
                    }
                }
                if (!boo)
                {
                    int sch = 0;
                    //массив для передачи свойству объекта SymbRepeat расстояния между повторами, 
                    //чтобы в дальнейшем найти НОД;
                    int[] Repeat = new int[4];
                    for (int j = i + 3; j < mass.Length - 2; j++)
                    {
                        if (i != j && (j - i) > 0)
                        {
                            string str1 = "";
                            str1 += mass[j];
                            str1 += mass[j + 1];
                            str1 += mass[j + 2];
                            if (str == str1)
                            {
                                Repeat[sch] = j - i;
                                sch++;
                            }
                            if (sch >= 4)
                            {
                                ListRepeat3.Add(new SymbRepeat { SR = Repeat });
                                ThreeS.Add(str);
                                sch = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void FindRepeat4(char[] mass)
        {
            List<string> ThreeS = new List<string>();
            ListRepeat2 = new List<SymbRepeat>();

            for (int i = 0; i < mass.Length - 7; i++)
            {
                string str = "";
                str += mass[i];
                str += mass[i + 1];
                bool boo = false;
                foreach (string CHECK in ThreeS)
                {
                    if (str == CHECK)
                    {
                        boo = true;
                        break;
                    }
                }
                if (!boo)
                {
                    int sch = 0;
                    int[] Repeat = new int[2];
                    for (int j = i + 3; j < mass.Length - 3; j++)
                    {
                        if (i != j && (j - i) > 0)
                        {
                            string str1 = "";
                            str1 += mass[j];
                            str1 += mass[j + 1];
                            if (str == str1)
                            {
                                Repeat[sch] = j - i;
                                sch++;
                            }
                            if (sch >= 2)
                            {
                                ListRepeat2.Add(new SymbRepeat { SR = Repeat });
                                ThreeS.Add(str);
                                sch = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void FindRepeat5(char[] mass)
        {
            List<string> ThreeS = new List<string>();
            ListRepeat4 = new List<SymbRepeat>();

            for (int i = 0; i < mass.Length - 9; i++)
            {
                string str = "";
                str += mass[i];
                str += mass[i + 1];
                str += mass[i + 2];
                str += mass[i + 3];
                str += mass[i + 4];
                bool boo = false;

                foreach (string CHECK in ThreeS)
                {
                    if (str == CHECK)
                    {
                        boo = true;
                        break;
                    }
                }
                if (!boo)
                {
                    int sch = 0;
                    int[] Repeat = new int[2];
                    for (int j = i + 3; j < mass.Length - 4; j++)
                    {
                        if (i != j && (j - i) > 0)
                        {
                            string str1 = "";
                            str1 += mass[j];
                            str1 += mass[j + 1];
                            str1 += mass[j + 2];
                            str1 += mass[j + 3];
                            str1 += mass[j + 4];
                            if (str == str1)
                            {
                                Repeat[sch] = j - i;
                                sch++;
                            }
                            if (sch >= 2)
                            {
                                ListRepeat4.Add(new SymbRepeat { SR = Repeat });
                                ThreeS.Add(str);
                                sch = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string key = Break(ClearInput(textBox2.Text));
            textBox1.Text = key;
            textBox3.Text = print(Crypt(ClearInput(textBox2.Text), KeyChange(textBox1.Text, false)));
        }
    }
}
