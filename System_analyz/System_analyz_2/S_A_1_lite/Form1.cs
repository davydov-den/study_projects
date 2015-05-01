using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace S_A_1_lite
{

    public partial class Form1 : Form
    {
        /// <summary>
        /// Открыто окно изменения точность вывода
        /// </summary>
        bool open_window_edit = false;  

        /// <summary>
        /// Открыто окно добавления/удаления альтернатив/ экспертов
        /// </summary>
        bool open_window = false;

        /// <summary>
        /// Проверка на первый запуск
        /// </summary>
        bool check_first_start = false;


        int action=1;
        public int kol_row;
        int kol_delete = 0;
        int n = 0;
        int kol_round = 3;
        int time = 0;
        int n_expert = 0;
        public Form1()
        {
            InitializeComponent();
                dataGridView1.Columns.Add("1", "Номер");
            dataGridView1.Columns.Add("2", "Эффективность варианта");
            dataGridView1.Columns.Add("3", "Предложеный вариант");
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 570;
            dataGridView3.Columns.Add("1", "Si");
            dataGridView3.Columns.Add("2", "Vi");
            dataGridView3.Columns[0].Width = 74;
            dataGridView3.Columns[1].Width = 74;
            textBox4.Text = "3";
            trackBar1.Value = 3;
            dataGridView2.TopLeftHeaderCell.Value = "Эi/Zi";
            dataGridView4.Columns.Add("1", "Эксперт");
            dataGridView4.Columns.Add("2", "Ранг");
            dataGridView4.Columns[1].Width = 45;
            dataGridView4.Columns[0].Width = 200;
            dataGridView5.Columns.Add("1", "Варианты решения");
            dataGridView5.Columns[0].Width = 200;
            HScrollBar scroll = new HScrollBar();
            scroll.Dock = DockStyle.Bottom;
            
        }

        public void input_f(int number)
        {
            input inp = new input();
            object[] ob = new object[5];
            ob[0] = dataGridView2;
            ob[1] = textBox2;
            ob[2] = dataGridView5;
            ob[3] = dataGridView4;
            control();
            ob=inp.input_from_file(ob,n,number);
            dataGridView2 = (DataGridView)ob[0];
            textBox2 = (TextBox)ob[1];
            dataGridView5 = (DataGridView)ob[2];
            dataGridView4 = (DataGridView)ob[3]; 
        }
        private void файл7ЭлементовToolStripMenuItem_Click    (object sender, EventArgs e) //загрузить из файла
        {
            ToolStripMenuItem tool = new ToolStripMenuItem();
            tool = (ToolStripMenuItem)sender;
            int number = System.Convert.ToInt32(tool.Text[0])-48;
            if (number == 1)
                n = 4;
            else
                n = 5;
            input_f(number);
            block_sort();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)  //ввод проблемы
        {
            if(textBox2.Text != "")
                control();
            else
                MessageBox.Show("Введите решаемую проблему!");
        }

        private void button1_Click(object sender, EventArgs e) //добавить новый вариант
        {
            dataGridView2.Columns.Add(n.ToString(), "Z" + n.ToString());
            dataGridView2.Columns[dataGridView2.Columns.Count].Width = 30;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
                dataGridView2[dataGridView2.Columns.Count, i].Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)  //удаление
        {
            object[] ob = new object[4];
            delete_alter del = new delete_alter();
         //   ob=del.delet(comboBox1.SelectedIndex,dataGridView2,textBox3,comboBox1,comboBox6);
            if (dataGridView2 != (DataGridView)ob[0])
                kol_delete++;
            dataGridView2=(DataGridView)ob[0];
            //textBox3=(TextBox)ob[1];
           // comboBox1=(ComboBox)ob[2];
            //comboBox6 = (ComboBox)ob[3];
        }

        private void button4_Click(object sender, EventArgs e)  //вычисление
        {
            if (n - kol_delete > 1)
            {
                if (check_table())
                {
                    DataGridView[] date = new DataGridView[2];
                    calculate calc = new calculate();
                    date = calc.start_calculate(dataGridView2, dataGridView3, dataGridView5, dataGridView1, kol_round, dataGridView4);
                    dataGridView1.Visible = true;
                    dataGridView1 = date[0];
                    dataGridView3 = date[1];
                    timer2.Enabled = true;
                    check_first_start = true;
               }
            }
            else
                MessageBox.Show("Требуется минимум две альтернативы!");
        }

        void control()
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            button3.Visible = false;
            label3.Visible = false;
            textBox2.ReadOnly = true;
            label4.Visible = true;
            textBox2.Height = 82;
        }

        private void изменитьПроблемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Height = 51;
            button3.Visible = true;
            textBox2.ReadOnly = false;
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check_first_start = false;
            if(open_window_edit)
                button5_Click(sender, e);
            groupBox1.Visible = false;
            timer3.Enabled = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            изменитьПроблемуToolStripMenuItem_Click(sender, e);
            kol_delete = 0;
            n = 0;
            textBox2.Clear();
            int kol_old = dataGridView2.Rows.Count;
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

            for (int j = dataGridView5.Rows.Count - 1; j >= 0; j--)
            {
                dataGridView5.Rows.RemoveAt(j);
            }

            for (int j = dataGridView3.Rows.Count - 1; j >= 1; j--)
                dataGridView3.Rows.RemoveAt(j-1);
        }

        private void изменитьТочностьВыводаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!open_window_edit)
            {
                time = 0;
                timer1.Enabled = true;
                action = 1;
                open_window_edit = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)   //таймер для анимации сдвига вниз/вверх при открытии/закрытии окна изменения точности вывода
        {
            if(time<75)
            {
                groupBox10.Top += action;
                label3.Top += action;
                label4.Top += action;
                groupBox1.Top+=action;
                groupBox2.Top+=action;
                button3.Top += action;
                textBox2.Top += action;
                this.Height += action;
                time++;
            }
            else
            {
                if(action==1)
                    groupBox6.Visible = true;
                else
                    groupBox6.Visible = false;
                timer1.Enabled = false;

            }
        }

        private void button5_Click(object sender, EventArgs e)  //закрыть окно изменения точности вывода
        {
            open_window_edit = false;
            groupBox6.Visible = false;
            time = 0;
            timer1.Enabled = true;
            kol_round = trackBar1.Value;
            action = -1;
            if(check_first_start)
                button4_Click(sender, e);
        }

        private void trackBar1_Scroll(object sender, EventArgs e) //отображать текущую точность вывода
        {
            textBox4.Text = trackBar1.Value.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)//анимация выезда таблицы с результатами
        {
            if(this.Height<710+75*System.Convert.ToInt32(open_window_edit))
                this.Height+=10;
            else
            {
                groupBox1.Visible = true;
                timer2.Enabled = false;
            }
        }

        private void timer3_Tick(object sender, EventArgs e) //анимация изчезновения таблицы с результатаи
        {
            if (this.Height > 530 + 75 * System.Convert.ToInt32(open_window_edit))
                this.Height -= 10;
            else
            {
                timer3.Enabled = false;
                this.Height = 524 + 75 * System.Convert.ToInt32(open_window_edit);
            }
        }

        private void button7_Click(object sender, EventArgs e) //добавление эксперта
        {
            
          
        }

        private void button6_Click(object sender, EventArgs e) //удаления эксперта
        {
            
        }

      /*  private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) //показать данные выбранного эксперта
        {
            if(comboBox4.SelectedIndex!=-1)
            {
                numericUpDown2.Value = System.Convert.ToDecimal(dataGridView4[1,comboBox4.SelectedIndex].Value);
                textBox6.Text = dataGridView4[0, comboBox4.SelectedIndex].Value.ToString();
            }
        }*/

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e) //редактирование выбранной альтернативы
        {
           
        }

        private void button10_Click(object sender, EventArgs e) //сохранить редактирование альтернативы
        {
            
        }

    /*   private void button9_Click(object sender, EventArgs e) // сохранить редактирования данных эксперта
        {
            if (comboBox4.SelectedIndex != -1)
            {
                dataGridView4[0, comboBox4.SelectedIndex].Value = textBox6.Text;
                dataGridView4[1, comboBox4.SelectedIndex].Value = numericUpDown2.Value;
                comboBox4.SelectedIndex = -1;
                numericUpDown2.Value = 0;
                textBox6.Text = "";
            }
        }
        */
        public bool check_table()  //проверка матрицы предпочтений на корректность
        {
            double check_sum = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                check_sum = 0;
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    if (toDouble(dataGridView2[j,i].Value) >= 0 && toDouble(dataGridView2[j,i].Value) <= 1) //проверка чтобы не ввести что-нибудь левое
                        check_sum += toDouble(dataGridView2[j,i].Value);
                    else
                    {
                        MessageBox.Show("В " + (j+1).ToString()+ "-ом столбце " + (i+1).ToString() + "-ой строки введено некорректное число","Ошибка!");
                        return false;
                    }
                if(check_sum!=1) //проверка суммы оценок каждого эксперта
                {
                    MessageBox.Show("У " + (i + 1).ToString() + "-ого эксперта сумма оценок равна = " + check_sum.ToString(), "Сумма оценок должна быть = 1!");
                    return false;
                }
                check_sum = 0;
            }
            return true;
        }

        public double toDouble(object ob) //приведение строки с числом в число (ибо стандартное косячит)
        {
            string str;
            double result = 0;
            try
            {
                str = (string)ob;
            }
            catch
            {
                str = ob.ToString();
            }
            result = (double)str[0] - 48;
            for (int i = 2; i < str.Length; i++)
                result += ((double)str[i] - 48) / Math.Pow(10.0, (i - 1) * 1.0);
            return result;
        }

        public void block_sort() //запретить сортировать таблицы 
        {
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView4.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

      
    }
}
