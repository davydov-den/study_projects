using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace S_A_1_lite
{
    class input
    {
        int kol_expert;
        double result_d;
        DataGridView dataGridView2;
        TextBox textBox2;
        DataGridView dataGridView4;
        DataGridView dataGridView5;
        int n;
        object[] ob = new object[5];
        public object[] input_from_file(object[] ob1,int kol_kol,int number)
        {
            n = kol_kol;
            ob = ob1;
            dataGridView2 = (DataGridView)ob[0];
            textBox2 = (TextBox)ob[1];
            dataGridView5 = (DataGridView)ob[2];
            dataGridView4 = (DataGridView)ob[3]; 

            int kol_old = dataGridView2.Rows.Count;
            /// Очищение таблиц
            for (int j = dataGridView2.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView2.Rows.RemoveAt(j);
            }
            for (int j = dataGridView2.Columns.Count - 1; j >= 0; j--)
            {
                dataGridView2.Columns.RemoveAt(j);
            }
            for (int j = dataGridView4.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView4.Rows.RemoveAt(j);
            }
            for (int j = dataGridView5.Rows.Count - 1; j >= 1; j--)
            {
                dataGridView5.Rows.RemoveAt(j-1);
            }


            int i = 0;
            string temp;
            try
            {
                string[] lines = System.IO.File.ReadAllLines("input" + (number+2).ToString() + ".txt", Encoding.UTF8);
                foreach (string line in lines)
                {
                    if (i < 6)
                    {
                        temp = line;
                        dataGridView5.Rows.Add();
                        dataGridView5[0, i].Value =(i + 1).ToString() + ". " + temp;
                    }
                    else
                        if (i == 6)
                            textBox2.Text = line;
                        else
                        {
                            dataGridView4.Rows.Add();
                            dataGridView4[0,i-6-1].Value = line;
                        }
                    i++;
                }
                kol_expert = i - n-1;
            }
            catch
            {
                MessageBox.Show("Файл отсутствует", "Ошибка!");
            }
            n = 6;
            kol_expert = 4;
            for (int j = 0; j < n; j++)
            {
                dataGridView2.Columns.Add(j.ToString(), "Z" + (j + 1).ToString());
                dataGridView2.Columns[j].Width = 30;
            }

            for (int j = 0; j < kol_expert; j++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[j].HeaderCell.Value = "Э" + (j + 1).ToString();
            }

                try
                {
                    string[] lines = System.IO.File.ReadAllLines("input2" + (number+2).ToString() + ".txt", Encoding.UTF8);

                    for (int k = 0; k < kol_expert; k++)
                    {
                        string[] A = lines[k].Split(' ');
                        for (int j = 0; j < n; j++)
                            if (toDouble(A[j]))
                                dataGridView2[j, k].Value = result_d;
                            else
                                dataGridView2[j, k].Value = "###";
                    }

                    string[] B = lines[kol_expert].Split(' ');
                    for (int k = 0; k < kol_expert; k++)
                        dataGridView4[1, k].Value = B[k];
                }
                catch
                {
                    MessageBox.Show("Файл отсутствует или ошибка в данных", "Ошибка!");
                }
                ob[0] = dataGridView2;
                ob[1] = textBox2;
                ob[2] = dataGridView5;
                ob[3] = dataGridView4;

            return ob;
        }

        public bool toDouble(string str)
        {
            try
            {
                if (str.Length == 1 && (str[0] == '0' || str[0] == '1') || str[0] == '0' && (str[1] == '.' || str[1] == ','))
                {
                    result_d = 0;
                    for (int i = 2; i < str.Length; i++)
                        result_d += ((double)str[i] - 48) / Math.Pow(10.0, (i - 1) * 1.0);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
