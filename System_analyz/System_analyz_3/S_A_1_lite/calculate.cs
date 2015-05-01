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
        int n,m;
        public DataGridView[] start_calculate(DataGridView dataGridView2, DataGridView dataGridView3, DataGridView dataGridView5, DataGridView dat, int kol_round, DataGridView dataGridView4,int mode)
        {
            DataGridView[] date = new DataGridView[2];
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1 = dat;
            n = dataGridView2.Columns.Count;
            double[] Vi = new double[n];
            m= dataGridView2.Rows.Count;
            int[] numbers = new int[n];
            while (dataGridView3.Rows.Count != 0)
                dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);

            while (dataGridView1.Rows.Count != 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);

            if(mode==0)
            {
                int[,] K= new int[m,n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        K[i, j] = n - System.Convert.ToInt32(dataGridView2[j,i].Value);
                int[] Li = new int[n];
                int L=0;
                for(int i=0;i<n;i++)
                {
                    Li[i]=0;
                    for (int j = 0; j < m; j++)
                        Li[i] += K[j,i];
                    L += Li[i];
                    dataGridView3.Rows.Add();
                    dataGridView3[0, i].Value = Math.Round(1.0 * Li[i], kol_round);
                }
                for (int i = 0; i < n; i++)
                {
                    Vi[i] = (Li[i] * 1.0) / L;
                    if(i>=m)
                        dataGridView3.Rows.Add();
                    dataGridView3[1, i].Value = Math.Round(Vi[i], kol_round);
                }
            }
            else
            {
                int[] Si = new int[n];
                for (int i = 0; i < m; i++)
                {
                    Si[i] = 0;
                    for (int j = 0; j < n; j++)
                        Si[i] += System.Convert.ToInt32(dataGridView2[j,i].Value);
                    dataGridView3.Rows.Add();
                    dataGridView3[0,i].Value = Math.Round(1.0*Si[i], kol_round);
                }

                double[,] Ri = new double[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        Ri[i,j] = 1.0*System.Convert.ToInt32(dataGridView2[j,i].Value)/Si[i];
                for(int j=0;j<n;j++)
                {
                    Vi[j]=0;
                    for (int i = 0; i < m; i++)
                        Vi[j] += 1.0 * Ri[i,j] / m;
                    if (j >= m)
                        dataGridView3.Rows.Add();
                    dataGridView3[1, j].Value = Math.Round(Vi[j], kol_round);
                }
                   

            }


            string[] texts =  new string[dataGridView5.Rows.Count];
            string[] texts2 = new string[dataGridView5.Rows.Count];
            for (int i = 0; i < dataGridView5.Rows.Count; i++)
            {
                texts[i] = dataGridView5[1, i].Value.ToString();
                texts2[i] = dataGridView5[0, i].Value.ToString();
            }
            
            numbers = sortirovka2(texts, Vi);
            for (int i = 0; i < n; i++)
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
            int[] numbers = new int[n];
            int size = n;
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
