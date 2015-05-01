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
        double R = 0;
        public DataGridView[] start_calculate(DataGridView dataGridView2, DataGridView dataGridView3, DataGridView dataGridView5, DataGridView dat, int kol_round, DataGridView dataGridView4)
        {
            DataGridView[] date = new DataGridView[2];
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1 = dat;
            n_new = dataGridView2.Columns.Count;
            int[] numbers = new int[n_new];
            double[] Vi = new double[n_new];
            double tmp = 0;
            while (dataGridView3.Rows.Count != 0)
                dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);

            while (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
                R += toDouble(dataGridView4[1, i].Value);
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[0, i].Value = Math.Round(toDouble(dataGridView4[1, i].Value) / R,kol_round);
            }

            for (int i = 0; i < n_new; i++)
            {
                tmp = 0;
                for (int j = 0; j < dataGridView4.Rows.Count; j++)
                    tmp += toDouble(dataGridView2[i, j].Value) * toDouble(dataGridView3[0, j].Value);
                Vi[i] = tmp;
                if(i>=dataGridView4.Rows.Count)
                    dataGridView3.Rows.Add();
                dataGridView3[1, i].Value = Math.Round(Vi[i], kol_round);
            }

          //  numbers = sortirovka2(textBox3.Lines, Vi);
            for (int i = 0; i < n_new; i++)
            {
               // string temp = textBox3.Lines[numbers[i]];
               // string temp2 = textBox3.Lines[numbers[i]].Substring(2, textBox3.Lines[numbers[i]].Length - 2);
                dataGridView1.Rows.Add();
                double prior = System.Convert.ToDouble(dataGridView3[1, numbers[i]].Value);
              //  dataGridView1[0, i].Value = temp[0];
                dataGridView1[1, i].Value = Math.Round(prior, kol_round);
               // dataGridView1[2, i].Value = temp2;
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


        public double toDouble(object ob)
        {
            string str;
            double result = 0 ;
            try
            {
                str = (string)ob;
            }
            catch
            {
                str = ob.ToString();
            }
            result = (double)str[0]-48;
            for (int i = 2; i < str.Length;i++)
                result += ((double)str[i]-48) / Math.Pow(10.0,(i-1)*1.0);
            return result;
        }

    }
}
