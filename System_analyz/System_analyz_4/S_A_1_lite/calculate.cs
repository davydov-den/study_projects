using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace S_A_1_lite
{
    class calculate
    {
        int n_new,n_new2;
        double R = 0;
        public DataGridView[] start_calculate(ArrayList Tables,  DataGridView dataGridView5, DataGridView dat, int kol_round, DataGridView dataGridView4,int kol_alt,int kol_exp)
        {
            n_new2 = kol_alt;
            double[,] f_ki = new double[10, 20];
            DataGridView dataGridView3 = new DataGridView();
            DataGridView[] date = new DataGridView[2];
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1 = dat;
            n_new = kol_exp;
            int[] numbers = new int[kol_alt];
            double[] Vi = new double[kol_alt];
            double tmp = 0;
            dataGridView3.Columns.Add("1,","fdsf");
            dataGridView3.Columns.Add("2","fdsfs");

            

            while (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
                R += toDouble(dataGridView4[1, i].Value);
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3[0, i].Value = Math.Round(toDouble(dataGridView4[1, i].Value) / R,kol_round);
            }

            for (int j = 0; j < n_new;j++)
                for (int i = 0; i < kol_alt; i++)
                {
                    tmp = 0;
                    float[,] tmp2 = (float[,])Tables[j];
                    for (int k = 0; k < kol_alt; k++)
                        if(i!=k)
                            tmp += tmp2[k,i];
                    f_ki[j, i] = tmp;
                }
            for (int i = 0; i < kol_alt; i++)
            {
                tmp = 0;
                for(int j=0;j<n_new;j++)
                    tmp +=f_ki[j,i];
                Vi[i] = 1.0/(kol_alt*(kol_alt-1)*n_new)*tmp;
                if (i >= dataGridView4.Rows.Count)
                    dataGridView3.Rows.Add();
                dataGridView3[1, i].Value = Math.Round(Vi[i], kol_round);
            }
            string[] texts =  new string[kol_alt];
            string[] texts2 = new string[kol_alt];
            for (int i = 0; i < kol_alt; i++)
            {
                texts[i] = dataGridView5[1, i].Value.ToString();
                texts2[i] = dataGridView5[0, i].Value.ToString();
            }
            
            numbers = sortirovka2(texts, Vi);
            for (int i = 0; i < kol_alt; i++)
            {
                string temp = texts[numbers[i]];
                string temp2 = texts[numbers[i]];
                dataGridView1.Rows.Add();
                double prior = System.Convert.ToDouble(dataGridView3[1, numbers[i]].Value);
                dataGridView1[0, i].Value = texts2[numbers[i]];
                dataGridView1[1, i].Value = Math.Round(prior, kol_round);
                dataGridView1[2, i].Value = temp2;
            }
            date[0] = dataGridView1;
            date[1] = dataGridView3;
            return date;
        }

        public int[] sortirovka2(string[] Variant, double[] V)  //сортировка пузырьком
        {
            int[] numbers = new int[n_new2];
            int size = n_new2;
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
