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
        ComboBox comboBox1=new ComboBox();
        DataGridView dataGridView2;
        TextBox textBox3;
        TextBox textBox2;
        int n;
        object[] ob = new object[4];
        public object[] input_from_file(object[] ob1,int kol_kol)
        {
            n = kol_kol;
            ob = ob1;
            comboBox1 = (ComboBox)ob1[0];
            dataGridView2 = (DataGridView)ob[1];
            textBox2=(TextBox)ob[2];
            textBox3 = (TextBox)ob[3];
            comboBox1.Items.Clear();
            for (int j = 0; j < n; j++)
                comboBox1.Items.Add("Альтернатива №" + (j + 1).ToString());
            int kol_old = dataGridView2.Rows.Count;
            for (int j = kol_old - 1; j >= 0; j--)
            {
                dataGridView2.Rows.RemoveAt(j);
                dataGridView2.Columns.RemoveAt(j);
            }
            int i = 0;
            string temp, const_error = "Пустой аргумент";
            try
            {
                string[] lines = System.IO.File.ReadAllLines("input" + n.ToString() + ".txt", Encoding.UTF8);
                foreach (string line in lines)
                {
                    if (i < n)
                    {
                        temp = line;
                        textBox3.AppendText(Environment.NewLine + (i + 1).ToString() + ". " + temp);
                    }
                    else
                        textBox2.Text = line;
                    i++;
                }
                for (; i < n; i++)
                    textBox3.AppendText(Environment.NewLine + (i + 1).ToString() + ". " + const_error);
            }
            catch
            {
                MessageBox.Show("Файл отсутствует", "Ошибка!");
            }

            for (int j = 0; j < n; j++)
            {
                dataGridView2.Columns.Add(j.ToString(), "Z" + (j + 1).ToString());
                dataGridView2.Rows.Add();
                dataGridView2.Rows[j].HeaderCell.Value = "Z" + (j + 1).ToString();
                dataGridView2.Columns[j].Width = 30;
            }

            try
            {
                string[] lines = System.IO.File.ReadAllLines("input2" + n.ToString() + ".txt", Encoding.UTF8);

                for (int k = 0; k < n; k++)
                {
                    string[] A = lines[k].Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        if (A[j] == "1" || A[j] == "0" || A[j] == "1/2")
                            dataGridView2[k, j].Value = A[j];
                        else
                        {
                            MessageBox.Show("Во входном файле заданы недопустимые символы!", "Ошибка!");
                            break;
                        }
                    }
                    dataGridView2[k, k].Value = null;
                }

            }
            catch
            {
                MessageBox.Show("Файл отсутствует или ошибка в данных", "Ошибка!");
            }
            var results = from p in textBox3.Lines
                          where p != String.Empty
                          select p;

            textBox3.Lines = results.ToArray<string>();
            textBox2.ScrollBars = ScrollBars.Vertical;
            ob[0]=comboBox1;
            ob[1]=dataGridView2;
            ob[2]=textBox2;
            ob[3]=textBox3;

            return ob;
        }
    }
}
