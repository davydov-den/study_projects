using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace S_A_1_lite
{
    class calculate
    {
        int n_new;
        public DataGridView[] start_calculate(DataGridView dataGridView2, DataGridView dataGridView3,TextBox textBox3,DataGridView dat,int kol_round)
        {
            string str_format = "{0:0.";
            for (int k = 0; k < kol_round; k++)
                str_format += "#";
            str_format += "}";
            string str_output;
            DataGridView[] date = new DataGridView[2];
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1 = dat;
            n_new = dataGridView2.Rows.Count;
            int[] numbers = new int[n_new];
            double[] Vi = new double[n_new];
            Queue<double> Ci = new Queue<double>();
            double R = 0;
            double tmp = 0;
            string str;
            while (dataGridView3.Rows.Count != 0)
                dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);
            while (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);

            for (int i = 0; i < n_new; i++)
            {
                dataGridView3.Rows.Add();
                tmp = 0;
                for (int j = 0; j < n_new; j++)
                {
                    if (i != j)
                    {
                        str = dataGridView2[j, i].Value.ToString();
                        if (str == "1/2")
                            tmp += 0.5;
                        else
                            tmp += System.Convert.ToDouble(str);
                    }
                }
                dataGridView3[0, i].Value = tmp.ToString();
                Ci.Enqueue(tmp);
                R += tmp;
            }
            for (int i = 0; i < n_new; i++)
            {
                Vi[i] = Ci.Dequeue() * 1.0 / R;
                str_output=String.Format(str_format, Vi[i]);
                dataGridView3[1, i].Value = Math.Round(Vi[i],kol_round);
            }

            numbers = sortirovka2(textBox3.Lines, Vi);
            for (int i = 0; i < n_new; i++)
            {
                string temp = textBox3.Lines[numbers[i]];
                string temp2 = textBox3.Lines[numbers[i]].Substring(2, textBox3.Lines[numbers[i]].Length - 2);
                dataGridView1.Rows.Add();
                double prior=System.Convert.ToDouble(dataGridView3[1, numbers[i]].Value);
                dataGridView1[0, i].Value = temp[0];
                dataGridView1[1, i].Value = Math.Round(prior,kol_round);
                dataGridView1[2, i].Value = temp2;
            }
            date[0] = dataGridView1;
            date[1] = dataGridView3;
            return date;
        }

        public int[] sortirovka2(string[] Variant, double[] V)  //сортировка пузырьком
        {
            int[] numbers = new int[n_new];
            int size = n_new;
            double temp;
            int tmp;
            for (int i = 0; i < size; i++)
                numbers[i] = i;

            for (int i = 0; i < size; i++)
            {
                for (int j = size - 1; j > i; j--)
                {
                    if (V[j] > V[j - 1])
                    {
                        tmp = numbers[j];
                        numbers[j] = numbers[j - 1];
                        numbers[j - 1] = tmp;
                        temp = V[j];
                        V[j] = V[j - 1];
                        V[j - 1] = temp;
                    }
                }
            }
            return numbers;
        }


    }
}
