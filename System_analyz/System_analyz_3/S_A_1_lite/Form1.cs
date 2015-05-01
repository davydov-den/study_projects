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
        DataGridView dataGridView6;
        int[,] massiv = new int[20, 20];

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
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("1", "№");
            dataGridView1.Columns.Add("2", "Эффективность варианта");
            dataGridView1.Columns.Add("3", "Предложеный вариант");
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 200;
            dataGridView9.Columns.Add("1", "№");
            dataGridView9.Columns.Add("2", "Эффективность варианта");
            dataGridView9.Columns.Add("3", "Предложеный вариант");
            dataGridView9.Columns[0].Width = 30;
            dataGridView9.Columns[1].Width = 60;
            dataGridView9.Columns[2].Width = 200;
            dataGridView3.Columns.Add("1", "Si");
            dataGridView3.Columns.Add("2", "Vi");
            dataGridView3.Columns[0].Width = 74;
            dataGridView3.Columns[1].Width = 74;
            textBox4.Text = "3";
            trackBar1.Value = 3;
            dataGridView2.TopLeftHeaderCell.Value = "Эi/Zi";
            dataGridView8.TopLeftHeaderCell.Value = "Эi/Zi";
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
        }

        public void input_f(int number)
        {
            input inp = new input();
            object[] ob = new object[5];
            ob[0] = dataGridView2;
            ob[1] = textBox2;
            ob[2] = dataGridView5;
            ob[3] = dataGridView4;
            ob[4] = dataGridView8;
            control();
            ob=inp.input_from_file(ob,number);
            dataGridView2 = (DataGridView)ob[0];
            textBox2 = (TextBox)ob[1];
            dataGridView5 = (DataGridView)ob[2];
            dataGridView4 = (DataGridView)ob[3];
            dataGridView8 = (DataGridView)ob[4];
            n = dataGridView2.Columns.Count;
        }
        private void файл7ЭлементовToolStripMenuItem_Click    (object sender, EventArgs e) //загрузить из файла
        {
            ToolStripMenuItem tool = new ToolStripMenuItem();
            tool = (ToolStripMenuItem)sender;
            int number = System.Convert.ToInt32(tool.Text[0])-48;
            input_f(number);
            block_sort();

            for (int i = comboBox1.Items.Count - 1; i >= n; i--)
            {
                comboBox1.Items.RemoveAt(i);
                comboBox2.Items.RemoveAt(i);
            }
            for (int i = comboBox1.Items.Count; i < n; i++)
            {
                string str = "№" + (i+1).ToString();
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

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save_problem(object sender, EventArgs e)  //ввод проблемы
        {
            if(textBox2.Text != "")
                control();
            else
                MessageBox.Show("Введите решаемую проблему!");
        }

        private void add_new_variant(object sender, EventArgs e) //добавить новый вариант
        {
            if (dataGridView2.Columns.Count == 1 && dataGridView2.Columns[0].HeaderText == "Z0")
                dataGridView2.Columns[0].HeaderText = "Z1";
            else
            {
                dataGridView2.Columns.Add(n.ToString(), "Z" + n.ToString());
                dataGridView2.Columns[dataGridView2.Columns.Count - 1].Width = 30;
            }
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
                dataGridView2[dataGridView2.Columns.Count-1,i].Value = 0;
            string str = "№"+n.ToString();
            comboBox1.Items.Add(str);
            comboBox2.Items.Add(str);
        }

        private void calculate(object sender, EventArgs e)  //вычисление
        {
            int mode=0;
            if (n - kol_delete > 1)
            {
                if (check_table(0))
                {
                    DataGridView[] date = new DataGridView[2];
                    calculate calc = new calculate();
                    date = calc.start_calculate(dataGridView2, dataGridView3, dataGridView5, dataGridView1, kol_round, dataGridView4, 0);
                    dataGridView1.Visible = true;
                    dataGridView1 = date[0];
                    dataGridView3 = date[1];
                    timer2.Enabled = true;
                    check_first_start = true;
               }
               if(true)
               { 
                    DataGridView[] date = new DataGridView[2];
                    calculate calc = new calculate();
                    date = calc.start_calculate(dataGridView8, dataGridView3, dataGridView5, dataGridView9, kol_round, dataGridView4, 1);
                    dataGridView1.Visible = true;
                    dataGridView9 = date[0];
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
            if(open_window_edit)
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

            for (int j = dataGridView3.Rows.Count - 1; j >= 1; j--)
                dataGridView3.Rows.RemoveAt(j-1);
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
            if(time<75)
            {
                panel4.Top += action;
                groupBox3.Top += action;
                groupBox10.Top += action;
                label3.Top += action;
                label4.Top += action;
                groupBox1.Top+=action;
                groupBox2.Top+=action;
                button3.Top += action;
                button4.Top += action;
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

        private void change_precision_calc(object sender, EventArgs e)  //закрыть окно изменения точности вывода
        {
            open_window_edit = false;
            groupBox6.Visible = false;
            time = 0;
            timer1.Enabled = true;
            kol_round = trackBar1.Value;
            action = -1;
            if(check_first_start)
                calculate(sender, e);
        }

        private void trackBar1_Scroll(object sender, EventArgs e) //отображать текущую точность вывода
        {
            textBox4.Text = trackBar1.Value.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)//анимация выезда таблицы с результатами
        {
            if(this.Height<600+75*System.Convert.ToInt32(open_window_edit))
                this.Height+=10;
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

        public bool check_table(int mode)  //проверка матрицы предпочтений на корректность
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                int check=find_repeat(i);
                if(check!=0)
                {
                    MessageBox.Show("В "+(i+1).ToString()+ "-ой строке повторяется число "+check.ToString());
                    return false;
                }
            }
            return true;
        }
        public int find_repeat(int number)
        {
            int position = 0;
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                for (int j = i + 1; j < dataGridView2.Columns.Count;j++)
                    if(dataGridView2[i,number].Value.ToString()==dataGridView2[j,number].Value.ToString())
                    {
                        position = System.Convert.ToInt32(dataGridView2[i,number].Value);
                        return position;
                    }
                    return position;
        }

        public bool find_nimber(int number,int stroka)
        {
            try
            {
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                    if (number == System.Convert.ToInt32(dataGridView2[i, stroka].Value))
                        return true;
            }
            catch
            {
                return false;
            }
            return false;
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
                dataGridView5.Rows.Add(dataGridView5.Rows.Count+1, textBox1.Text);
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
                string name = "Добавить";
                comboBox1.SelectedIndex = -1;
            }
        }

        public void delet_change(int number)
        {
            n--;
            int i = comboBox1.Items.Count - 1;
            if (dataGridView2.Columns.Count == 1)
                dataGridView2.Columns[0].HeaderText = "Z0";
            else
                dataGridView2.Columns.RemoveAt(number);
            comboBox1.Items.RemoveAt(i);
            dataGridView5.Rows.RemoveAt(number);
            comboBox2.Items.RemoveAt(i);
            if(comboBox1.SelectedIndex > -1)
                for (i=comboBox1.SelectedIndex; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView5[0, i].Value = (i + 1).ToString()+".";
                    dataGridView2.Columns[i].HeaderText = "Z"+(i+1).ToString();
                }
        }

        public void delet_expert(int number)
        {
            int i = comboBox3.Items.Count - 1;
            dataGridView2.Rows.RemoveAt(number);
            dataGridView4.Rows.RemoveAt(number);
            for (i = comboBox3.SelectedIndex; i < dataGridView2.Rows.Count; i++)
                dataGridView2.Rows[i].HeaderCell.Value = "Э" + (i + 1).ToString();

            comboBox4.Items.RemoveAt(i);
            comboBox3.Items.RemoveAt(i);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex!=-1)
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
                if (dataGridView2.Columns.Count == 0)
                {
                    dataGridView2.Columns.Add("1", "Z0");
                    dataGridView2.Columns[0].Width = 30;
                }
                dataGridView2.Rows.Add();
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].HeaderCell.Value = "Э" + dataGridView2.Rows.Count.ToString();
                dataGridView4.Rows.Add(textBox6.Text,numericUpDown1.Value);
                numericUpDown1.Value = 0;
                string str = "№" + dataGridView2.Rows.Count.ToString();
                comboBox3.Items.Add(str);
                comboBox4.Items.Add(str);
            }
            textBox6.Text = "ВВедите имя эксперта";
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
            }
        }

        private void save_change_expert(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                dataGridView4[0,comboBox4.SelectedIndex].Value = textBox10.Text;
                dataGridView4[1, comboBox4.SelectedIndex].Value = numericUpDown2.Value;
                comboBox4.SelectedIndex = -1;
            }
        }


        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int i=0, j;
            i++;
            i = e.ColumnIndex;
            j = e.RowIndex;
            try
            {
                if (dataGridView2.Columns.Count > 0)
                {
                    double xxx = System.Convert.ToDouble(dataGridView2[i, j].Value);
                    int data = System.Convert.ToInt32(Math.Round(xxx));
                    int max;
                    max = dataGridView2.Columns.Count;
                    
                    if (data < 1 || data > max || data !=xxx)
                    {
                        dataGridView2[i, j].Value = empty_position(j);
                        MessageBox.Show("Некорректные данные введите целое число от 1 до " + max.ToString());
                    }
                }
            }
            catch
            {
                
            }
        }

        public int empty_position(int stroka)  //поиск не занятого значения
        {
            int j;
            for(int i=1;i<=dataGridView2.Columns.Count;i++)
            {
                for (j = 0; j < dataGridView2.Columns.Count && dataGridView2[j, stroka].Value.ToString() != i.ToString(); j++);
                if (j == dataGridView2.Columns.Count)
                    return i;
            }
            return dataGridView2.Columns.Count;

               
        }

        private void dataGridView8_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0, j;
            i++;
            i = e.ColumnIndex;
            j = e.RowIndex;
            try
            {
                if (dataGridView8.Columns.Count > 0)
                {
                    double xxx = System.Convert.ToDouble(dataGridView8[i, j].Value);
                    int data = System.Convert.ToInt32(Math.Round(xxx));
                    int max=10;
                    

                    if (data < 1 || data > max || data != xxx)
                    {
                        dataGridView8[i, j].Value = empty_position(j);
                        MessageBox.Show("Некорректные данные введите целое число от 1 до " + max.ToString());
                    }
                }
            }
            catch
            {

            }
        }

    }
}
