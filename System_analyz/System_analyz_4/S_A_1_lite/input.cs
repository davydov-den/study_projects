using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace S_A_1_lite
{
    class input
    {
        int kol_expert;
        double result_d;
        ArrayList Tables;
        TextBox textBox2;
        DataGridView dataGridView4;
        DataGridView dataGridView5;
        int n;
        object[] ob = new object[5];
        public object[] input_from_file(object[] ob1, int kol_kol, int number)
        {
            n = kol_kol;
            ob = ob1;
            Tables = (ArrayList)ob[0];
            textBox2 = (TextBox)ob[1];
            dataGridView5 = (DataGridView)ob[2];
            dataGridView4 = (DataGridView)ob[3];

            for (int j = dataGridView4.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView4.Rows.RemoveAt(j);
            }
            for (int j = dataGridView5.Rows.Count - 1; j >= 1; j--)
            {
                dataGridView5.Rows.RemoveAt(j - 1);
            }
            string[] requirements = System.IO.File.ReadAllLines("requirements.txt", Encoding.UTF8);
            int kol_alt;
            string[] req = requirements[0].Split(' ');
            kol_expert = System.Convert.ToInt32(req[0]);
            kol_alt = System.Convert.ToInt32(req[1]);
            n = kol_alt;
            int i = 0;
            string temp;
            try
            {
                string[] lines = System.IO.File.ReadAllLines("input_Data.txt", Encoding.UTF8);
                foreach (string line in lines)
                {
                    if (i < n)
                    {
                        temp = line;
                        dataGridView5.Rows.Add();
                        dataGridView5[1, i].Value = temp;
                        dataGridView5[0, i].Value = i + 1;
                    }
                    else
                        if (i == n)
                            textBox2.Text = line;
                        else
                        {
                            dataGridView4.Rows.Add();
                            dataGridView4[0, i - n - 1].Value = line;
                        }
                    i++;
                }

                lines = System.IO.File.ReadAllLines("input.txt", Encoding.UTF8);
                string[] exp = lines[0].Split(' ');
                for (int j = 0; j < kol_expert; j++)
                    dataGridView4[1, j].Value = toDouble(exp[j]).ToString();
                for (int j = 0; j < kol_expert; j++)
                {
                    float[,] massiv = new float[20, 20];
                    for (int k = 0; k < kol_alt; k++)
                    {
                        string[] A = lines[j * kol_alt + k+1].Split(' ');
                        for (int l = 0; l < kol_alt; l++)
                        {
                            massiv[l, k] = (float)toDouble(A[l]);
                        }
                    }
                    Tables.Insert(j, massiv);

                }
            }
            catch
            {
                MessageBox.Show("Файл отсутствует", "Ошибка!");
            }



            ob[0] = Tables;
            ob[1] = textBox2;
            ob[2] = dataGridView5;
            ob[3] = dataGridView4;
            ob[4] = kol_expert;
            ob[5] = kol_alt;
            return ob;
        }

        public double toDouble(object ob) //приведение строки с числом в число (ибо стандартное косячит)
        {
            string str;
            double result = 0;
            int pos;
            try
            {
                str = (string)ob;
            }
            catch
            {
                str = ob.ToString();
            }
            pos = str.IndexOf('.');
            if (pos == -1)
            {
                result = Convert.ToDouble(str);
            }
            else
            {
                int iter = 1;
                for (int i = pos - 1; i >= 0; i--)
                {
                    result += ((double)str[i] - 48) * iter;
                    iter *= 10;
                }
                iter = 10;
                for (int i = pos + 1; i < str.Count(); i++)
                {
                    result += ((double)str[i] - 48) / iter;
                    iter *= 10;
                }
            }
            return result;
        }
    }
}
