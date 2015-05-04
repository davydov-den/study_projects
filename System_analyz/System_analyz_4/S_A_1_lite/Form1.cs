using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

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
        float[,] massiv = new float[20, 20];
        ArrayList Tables = new ArrayList();
        int action = 1;
        public int kol_row;
        int kol_delete = 0;
        int n = 0;
        int kol_round = 3;
        int time = 0;
        int kol_expert = 0;
        int current_table = 0;
        int kol_alt = 0;
        bool first_expert = true;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("1", "Номер");
            dataGridView1.Columns.Add("2", "Эффективность варианта");
            dataGridView1.Columns.Add("3", "Предложеный вариант");
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 570;
            textBox4.Text = "3";
            trackBar1.Value = 3;
            dataGridView2.TopLeftHeaderCell.Value = "Эi/Zi";
            dataGridView4.Columns.Add("1", "Эксперт");
            dataGridView4.Columns.Add("2", "Ранг");
            dataGridView4.Columns[1].Width = 45;
            dataGridView4.Columns[0].Width = 200;
            dataGridView5.Columns.Add("1", "№");
            dataGridView5.Columns.Add("2", "Варианты решения");
            dataGridView5.Columns[1].Width = 200;
            dataGridView5.Columns[0].Width = 30;
            HScrollBar scroll = new HScrollBar();
            scroll.Dock = DockStyle.Bottom;
            dataGridView5.ContextMenuStrip = contextMenuStrip2;
            dataGridView4.ContextMenuStrip = contextMenuStrip1;
            textBox1.Text = "ВВедите альтернативу";//подсказка
            textBox1.ForeColor = Color.Gray;
            control();
            //  Tables.Insert(0, massiv);
        }

        public void input_f(int number)
        {
            input inp = new input();
            object[] ob = new object[6];
            ob[0] = Tables;
            ob[1] = textBox2;
            ob[2] = dataGridView5;
            ob[3] = dataGridView4;
            control();
            ob = inp.input_from_file(ob, n, number);
            Tables = (ArrayList)ob[0];
            textBox2 = (TextBox)ob[1];
            dataGridView5 = (DataGridView)ob[2];
            dataGridView4 = (DataGridView)ob[3];
            kol_expert = (int)ob[4];
            kol_alt = (int)ob[5];
        }
        private void файл7ЭлементовToolStripMenuItem_Click(object sender, EventArgs e) //загрузить из файла
        {
            ToolStripMenuItem tool = new ToolStripMenuItem();
            tool = (ToolStripMenuItem)sender;
            int number = System.Convert.ToInt32(tool.Text[0]) - 48;
            if (number == 1)
                n = 4;
            else
                n = 5;
            input_f(number);
            block_sort();

            for (int i = comboBox1.Items.Count - 1; i >= n; i--)
            {
                comboBox1.Items.RemoveAt(i);
                comboBox2.Items.RemoveAt(i);
            }
            for (int i = comboBox1.Items.Count; i < n; i++)
            {
                string str = "№" + (i + 1).ToString();
                comboBox1.Items.Add(str);
                comboBox2.Items.Add(str);
            }
            int j = dataGridView4.Rows.Count;
            for (int i = comboBox3.Items.Count - 1; i >= j; i--)
            {
                comboBox3.Items.RemoveAt(i);
                comboBox4.Items.RemoveAt(i);
            }

            for (int i = comboBox3.Items.Count; i < j; i++)
            {
                string str = "№" + (i + 1).ToString();
                comboBox3.Items.Add(str);
                comboBox4.Items.Add(str);
            }
            for (int i = comboBox5.Items.Count - 1; i >= j; i--)
                comboBox5.Items.RemoveAt(i);

            for (int i = 0; i < kol_expert; i++)
                comboBox5.Items.Add("Эксперт №" + (i + 1).ToString());

            float[,] tmp = new float[20,20];
            tmp =  (float[,])Tables[0];

            for (int i = 0; i < kol_alt;i++)
            {
                dataGridView2.Columns.Add((i+1).ToString(), "Z" + (i + 1).ToString());
                dataGridView2.Columns[dataGridView2.Columns.Count - 1].Width = 40;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].HeaderCell.Value = "Z" + (i+1).ToString();
                
                    
            }
            for (int i = 0; i < kol_alt; i++)
                for (int k = 0; k < kol_alt; k++)
                    if(k!=i)
                    dataGridView2[k, i].Value = tmp[k, i];
            //comboBox5.SelectedIndex = 0;
            current_table = 0;


        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save_problem(object sender, EventArgs e)  //ввод проблемы
        {
            if (textBox2.Text != "")
                control();
            else
                MessageBox.Show("Введите решаемую проблему!");
        }

        private void add_new_variant(object sender, EventArgs e) //добавить новый вариант
        {
            kol_alt++;
            if (dataGridView2.Columns.Count == 1 && dataGridView2.Columns[0].HeaderText == "Z0")
                dataGridView2.Columns[0].HeaderText = "Z1";
            else
            {
                dataGridView2.Columns.Add(n.ToString(), "Z" + n.ToString());
                dataGridView2.Columns[dataGridView2.Columns.Count - 1].Width = 30;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[kol_alt - 1].HeaderCell.Value = "Z" + n.ToString();
            }
            for (int i = 0; i < dataGridView5.Rows.Count - 1; i++)
            {
                dataGridView2[dataGridView2.Columns.Count - 1, i].Value = 0;
                dataGridView2[i, dataGridView2.Columns.Count - 1].Value = 1;
            }
            string str = "№" + n.ToString();
            comboBox1.Items.Add(str);
            comboBox2.Items.Add(str);
        }

        private void calculate(object sender, EventArgs e)  //вычисление
        {

            float[,] tmp = new float[20, 20];
            float[,] tmp2 = new float[20, 20];
           // tmp2 = (float[,])Tables[comboBox5.SelectedIndex];
            //Tables.Remove(comboBox5.SelectedIndex);
            for (int i = 0; i < kol_alt; i++)
                for (int j = 0; j < kol_alt; j++)
                    if (i != j)
                        tmp[i, j] = (float)System.Convert.ToDouble(dataGridView2[i, j].Value);
           // Tables.Insert(comboBox5.SelectedIndex, tmp);
         //   current_table = comboBox5.SelectedIndex;




            if (n - kol_delete > 1)
            {
                if (check_table())
                {
                    DataGridView[] date = new DataGridView[2];
                    calculate calc = new calculate();
                    date = calc.start_calculate(Tables, dataGridView5, dataGridView1, kol_round, dataGridView4,kol_alt,kol_expert);
                    dataGridView1.Visible = true;
                    dataGridView1 = date[0];
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
            //textBox2.ReadOnly = true;
            label4.Visible = true;
            textBox2.Height = 82;
        }

        private void изменитьПроблемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Height = 51;
            button3.Visible = true;
            textBox2.ReadOnly = false;
        }  //норм все

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check_first_start = false;
            if (open_window_edit)
                change_precision_calc(sender, e);
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

        } //есть косяки

        private void изменитьТочностьВыводаToolStripMenuItem_Click(object sender, EventArgs e) //норм все
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
            if (time < 75)
            {
                panel4.Top += action;
                groupBox3.Top += action;
                groupBox10.Top += action;
                label3.Top += action;
                label4.Top += action;
                groupBox1.Top += action;
                groupBox2.Top += action;
                button3.Top += action;
                button4.Top += action;
                textBox2.Top += action;
                this.Height += action;
                time++;
            }
            else
            {
                if (action == 1)
                    groupBox6.Visible = true;
                else
                    groupBox6.Visible = false;
                timer1.Enabled = false;

            }
        }

        private void change_precision_calc(object sender, EventArgs e)  //закрыть окно изменения точности вывода
        {
            open_window_edit = false;
            groupBox6.Visible = false;
            time = 0;
            timer1.Enabled = true;
            kol_round = trackBar1.Value;
            action = -1;
            if (check_first_start)
                calculate(sender, e);
        }

        private void trackBar1_Scroll(object sender, EventArgs e) //отображать текущую точность вывода
        {
            textBox4.Text = trackBar1.Value.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)//анимация выезда таблицы с результатами
        {
            if (this.Height < 600 + 75 * System.Convert.ToInt32(open_window_edit))
                this.Height += 10;
            else
            {
                groupBox1.Visible = true;
                timer2.Enabled = false;
            }
        }

        private void timer3_Tick(object sender, EventArgs e) //анимация изчезновения таблицы с результатаи
        {
            if (this.Height > 450 + 75 * System.Convert.ToInt32(open_window_edit))
                this.Height -= 10;
            else
            {
                timer3.Enabled = false;
                this.Height = 450 + 75 * System.Convert.ToInt32(open_window_edit);
            }
        }

        public bool check_table()  //проверка матрицы предпочтений на корректность
        {
            double check_sum = 0;
            for (int i = 0; i < kol_expert; i++)
            {
                float[,] tmp = (float[,])Tables[i];
                for (int j = 0; j < kol_alt; j++)
                    for (int k = 0; k < j; k++)
                        if (tmp[k, j] + tmp[j, k] != 1)
                        {
                            MessageBox.Show("В " + (i + 1).ToString() + "-оЙ таблице. Для пары альтернатив " + (j + 1).ToString() + " и " + (k + 1).ToString() + " введено некорректное соотношение", "Ошибка!");
                            return false;
                        }
            }
            return true;
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
                int iter=1;
                for(int i = pos-1;i>=0;i--)
                {
                    result += ((double)str[i] - 48)*iter;
                    iter *= 10;
                }
                iter =10;
                for (int i = pos + 1; i < str.Count(); i++)
                {
                    result += ((double)str[i] - 48) / iter;
                    iter *= 10;
                }
            }
            return result;
        }

        public void block_sort() //запретить сортировать таблицы 
        {
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dataGridView4.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == "ВВедите альтернативу")
            {
                textBox1.Text = "ВВедите альтернативу";//подсказка
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void add_new_variant_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != "ВВедите альтернативу")
            {
                n++;
                dataGridView5.Rows.Add(dataGridView5.Rows.Count + 1, textBox1.Text);
                add_new_variant(sender, e);
            }
            textBox1.Text = "ВВедите альтернативу";
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void delete_variant(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                var result = MessageBox.Show("Точно желаете удалить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    delet_change(comboBox1.SelectedIndex);
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
            }
        }

        public void delet_change(int number)
        {
            n--;
            kol_alt--;
            int i = comboBox1.Items.Count - 1;
            if (dataGridView2.Columns.Count == 1)
                dataGridView2.Columns[0].HeaderText = "Z0";
            else
            {
                dataGridView2.Columns.RemoveAt(number);
                dataGridView2.Rows.RemoveAt(number);
            }
            comboBox1.Items.RemoveAt(i);
            dataGridView5.Rows.RemoveAt(number);
            comboBox2.Items.RemoveAt(i);
            if (comboBox1.SelectedIndex > -1)
                for (i = comboBox1.SelectedIndex; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView5[0, i].Value = (i + 1).ToString() + ".";
                    dataGridView2.Columns[i].HeaderText = "Z" + (i + 1).ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = "Z" + (i + 1).ToString();
                }
        }

        public void delet_expert(int number)
        {
            int i = comboBox3.Items.Count - 1;
            dataGridView4.Rows.RemoveAt(number);
            comboBox4.Items.RemoveAt(i);
            comboBox3.Items.RemoveAt(i);
            comboBox5.Items.RemoveAt(i);
            kol_expert--;
            Tables.RemoveAt(kol_expert);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                textBox3.Text = dataGridView5[1, comboBox1.SelectedIndex].Value.ToString();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl2.Visible = true;
            tabControl3.Visible = false;

        }

        private void save_edit_variant(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                dataGridView5[1, comboBox2.SelectedIndex].Value = textBox5.Text;
                tabControl2.TabIndex = 0;
                comboBox2.SelectedIndex = -1;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
                textBox5.Text = dataGridView5[1, comboBox2.SelectedIndex].Value.ToString();
            else
                textBox5.Text = "";
        }

        private void редактироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl3.Visible = true;
            tabControl2.Visible = false;
        }

        private void change_add_variant(object sender, EventArgs e)
        {
            tabControl2.Visible = true;
            panel4.Visible = false;
        }

        private void change_add_expert(object sender, EventArgs e)
        {
            tabControl3.Visible = true;
            panel4.Visible = false;
        }

        private void add_expert(object sender, EventArgs e)  //Добавление эксперта
        {
            if (textBox6.Text != "" && textBox6.Text != "ВВедите имя эксперта")
            {
                if (numericUpDown1.Value != 0)
                {
                    add_tables();
                    kol_expert++;
                    string str = "№ " + kol_expert.ToString();
                    comboBox5.Items.Add("Эксперт " + str);
                    comboBox3.Items.Add(str);
                    comboBox4.Items.Add(str);
                    dataGridView4.Rows.Add(textBox6.Text, numericUpDown1.Value);
                    numericUpDown1.Value = 0;
                }
            }
            textBox6.Text = "ВВедите имя эксперта";
        }
        void add_tables()
        {
            for (int i = 1; i < kol_alt; i++)
            {
                for (int j = 0; j < i; j++)
                    massiv[j, i] = 1;
            }
            Tables.Insert(kol_expert, massiv);
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.Text = null;
            textBox6.ForeColor = Color.Black;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox6.Text == "ВВедите имя эксперта")
            {
                textBox6.Text = "ВВедите имя эксперта";//подсказка
                textBox6.ForeColor = Color.Gray;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                textBox7.Text = dataGridView4[0, comboBox3.SelectedIndex].Value.ToString();
                textBox8.Text = dataGridView4[1, comboBox3.SelectedIndex].Value.ToString();
            }
            else
            {
                textBox7.Text = "";
                textBox8.Text = "0.0";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                textBox10.Text = dataGridView4[0, comboBox4.SelectedIndex].Value.ToString();
                numericUpDown2.Value = System.Convert.ToDecimal(dataGridView4[1, comboBox4.SelectedIndex].Value);
            }
            else
            {
                textBox10.Text = "";
                numericUpDown2.Value = 0;
            }
        }

        private void delete_expert(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                var result = MessageBox.Show("Точно желаете удалить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    delet_expert(comboBox3.SelectedIndex);
                textBox7.Text = "";
                comboBox3.SelectedIndex = -1;
                numericUpDown2.Value = 0;

            }
        }

        private void save_change_expert(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                dataGridView4[0, comboBox4.SelectedIndex].Value = textBox10.Text;
                dataGridView4[1, comboBox4.SelectedIndex].Value = numericUpDown2.Value;
                comboBox4.SelectedIndex = -1;
            }
        }


        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox9.Text = Tables.Count.ToString();
            float[,] tmp = new float[20, 20];
            float[,] tmp2 = new float[20, 20];
            tmp2 = (float[,])Tables[comboBox5.SelectedIndex];
            
            for (int i = 0; i < kol_alt; i++)
                for (int j = 0; j < kol_alt; j++)
                    if (i != j)
                        tmp[i, j] = (float)System.Convert.ToDouble(dataGridView2[i, j].Value);
            for (int i = 0; i < kol_alt; i++)
            {
                for (int j = 0; j < kol_alt; j++)
                    dataGridView2[i, j].Value = tmp2[i, j];
                dataGridView2[i, i].Value = "";
            }
            Tables.RemoveAt(comboBox5.SelectedIndex);
            Tables.Insert(current_table, tmp);
            current_table = comboBox5.SelectedIndex;
        }


    }
}
