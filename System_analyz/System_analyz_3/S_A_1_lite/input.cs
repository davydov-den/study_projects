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
        DataGridView dataGridView8;
        TextBox textBox2;
        DataGridView dataGridView4;
        DataGridView dataGridView5;
        int n;
        object[] ob = new object[5];
        public object[] input_from_file(object[] ob1,int number)
        {
            n = 0;
            ob = ob1;
            dataGridView2 = (DataGridView)ob[0];
            textBox2 = (TextBox)ob[1];
            dataGridView5 = (DataGridView)ob[2];
            dataGridView4 = (DataGridView)ob[3];
            dataGridView8 = (DataGridView)ob[4];
            int kol_old = dataGridView2.Rows.Count;
            /// Очищение таблиц
            for (int j = dataGridView2.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView2.Rows.RemoveAt(j);
                dataGridView8.Rows.RemoveAt(j);
            }
            for (int j = dataGridView2.Columns.Count - 1; j >= 0; j--)
            {
                dataGridView2.Columns.RemoveAt(j);
                dataGridView8.Columns.RemoveAt(j);
            }
            for (int j = dataGridView4.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView4.Rows.RemoveAt(j);
            }
            for (int j = dataGridView5.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView5.Rows.RemoveAt(j);
            }

            int i = 0;
            string temp;
            try
            {
                string[] lines = System.IO.File.ReadAllLines("input" + (number+2).ToString() + ".txt", Encoding.UTF8);
                n = System.Convert.ToInt32(lines[0][0])-48;
                foreach (string line in lines)
                {
                    if(i!=0)
                        if (i <= n)
                        {
                            temp = line;
                            dataGridView5.Rows.Add();
                            dataGridView5[1, i-1].Value =temp;
                            dataGridView5[0, i-1].Value = i ;
                        }
                        else
                            if (i == n+1)
                                textBox2.Text = line;
                            else
                            {
                                dataGridView4.Rows.Add();
                                dataGridView4[0,i-n-2].Value = line;
                            }
                    i++;
                }
                kol_expert = i - n-2;
            }
            catch
            {
                MessageBox.Show("Файл отсутствует", "Ошибка!");
            }

            for (int j = 0; j < n; j++)
            {
                dataGridView2.Columns.Add(j.ToString(), "Z" + (j + 1).ToString());
                dataGridView2.Columns[j].Width = 30;
                dataGridView8.Columns.Add(j.ToString(), "Z" + (j + 1).ToString());
                dataGridView8.Columns[j].Width = 30;
            }

            for (int j = 0; j < kol_expert; j++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[j].HeaderCell.Value = "Э" + (j + 1).ToString();
                dataGridView8.Rows.Add();
                dataGridView8.Rows[j].HeaderCell.Value = "Э" + (j + 1).ToString();
            }
            
                try
                {
                    string[] lines = System.IO.File.ReadAllLines("input21"+ (number+2).ToString() + ".txt", Encoding.UTF8);

                    for (int k = 0; k < kol_expert; k++)
                    {
                        string[] A = lines[k].Split(' ');
                        for (int j = 0; j < n; j++)
                                dataGridView2[j, k].Value = A[j];
                    }

                    string[] B = lines[kol_expert].Split(' ');
                    for (int k = 0; k < kol_expert; k++)
                        dataGridView4[1, k].Value = B[k];
                    string[] lines2 = System.IO.File.ReadAllLines("input22"+ (number + 2).ToString() + ".txt", Encoding.UTF8);
                    for (int k = 0; k < kol_expert; k++)
                    {
                        string[] A = lines2[k].Split(' ');
                        for (int j = 0; j < n; j++)
                            dataGridView8[j, k].Value = A[j];
                    }
                }
                catch
                {
                    MessageBox.Show("Файл отсутствует или ошибка в данных", "Ошибка!");
                }
                ob[0] = dataGridView2;
                ob[1] = textBox2;
                ob[2] = dataGridView5;
                ob[3] = dataGridView4;
                ob[4] = dataGridView8;
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

        public DataGridView copy(DataGridView x)
        {
            DataGridView y;
            y = x;
            return y;
        }
    }
}
