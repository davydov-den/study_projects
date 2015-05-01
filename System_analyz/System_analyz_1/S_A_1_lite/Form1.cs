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
            dataGridView1.Columns.Add("1", "Номер");
            dataGridView1.Columns.Add("2", "Эффективность варианта");
            dataGridView1.Columns.Add("3", "Предложеный вариант");
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 570;
            dataGridView3.Columns.Add("1", "Ci");
            dataGridView3.Columns.Add("2", "Vi");
            dataGridView3.Columns[0].Width = 74;
            dataGridView3.Columns[1].Width = 74;
            textBox4.Text = "3";
            trackBar1.Value = 3;
        }

        public void input_f()
        {
            input inp = new input();
            object[] ob = new object[4];
            ob[0] = comboBox1;
            ob[1] = dataGridView2;
            ob[2] = textBox2;
            ob[3] = textBox3;
            textBox3.Text = "";
            control();
            ob=inp.input_from_file(ob,n);
            comboBox1 = (ComboBox)ob[0];
            dataGridView2 = (DataGridView)ob[1];
            textBox2 = (TextBox)ob[2];
            textBox3 = (TextBox)ob[3];

            
        }

        public void click(object sender, EventArgs e) //работа с таблицей ввода
        {
            int i, j;
            i = dataGridView2.SelectedCells[0].ColumnIndex;
            j = dataGridView2.SelectedCells[0].RowIndex;
            if (i != j)
            {
                string str = dataGridView2[i, j].Value.ToString();
                if (str == "1")
                {
                    dataGridView2[i, j].Value = "0";
                    dataGridView2[j, i].Value = "1";
                }
                else
                    if (str == "0")
                    {
                        dataGridView2[i, j].Value = "1/2";
                        dataGridView2[j, i].Value = "1/2";
                    }
                    else
                    {
                        dataGridView2[i, j].Value = 1;
                        dataGridView2[j, i].Value = 0;
                    }
        
            }
        }

        private void файл7ЭлементовToolStripMenuItem_Click    (object sender, EventArgs e) //загрузить из файла
        {
            ToolStripMenuItem tool = new ToolStripMenuItem();
            tool = (ToolStripMenuItem)sender;
            n = System.Convert.ToInt32(tool.Text[0]) - 42;
            input_f();
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)  //ввод проблемы
        {
            if(textBox2.Text != "")
            {
                control();
            }
            else
            {
                MessageBox.Show("Введите решаемую проблему!");
            }
        }

        private void button1_Click(object sender, EventArgs e) //добавить новый вариант
        {
            if (n - kol_delete < 21)
            {
                if (textBox1.Text != "")
                {
                    n++;
                    int n_new2 = dataGridView2.Rows.Count;
                    string new_variant = "";
                    new_variant = textBox1.Text;
                    new_variant = new_variant.Replace('\n', ' ');
                    new_variant = new_variant.Replace('\r', ' ');
                    textBox1.Text = "";
                    dataGridView2.Columns.Add(n.ToString(), "Z" + n.ToString());
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[n_new2].HeaderCell.Value = "Z" + n.ToString();
                    dataGridView2.Columns[n_new2].Width = 30;
                    if (n > 1)
                    {
                        for (int i = 0; i < n_new2; i++)
                        {
                            dataGridView2[i, n_new2].Value = 1;
                            dataGridView2[n_new2, i].Value = 0;
                        }
                    }

                    textBox3.AppendText(Environment.NewLine + n.ToString() + ". " + new_variant);

                    string new_items = "Альтернатива №" + n.ToString();
                    comboBox1.Items.Add(new_items);
                    var results = from p in textBox3.Lines
                                  where p != String.Empty
                                  select p;
                    textBox3.Lines = results.ToArray<string>();
                    foreach (DataGridViewColumn column in dataGridView2.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    foreach (DataGridViewColumn column in dataGridView3.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {
                    MessageBox.Show("Введите что-нибудь в поле");
                }
            }
            else
            {
                MessageBox.Show("Слишком много альтернатив!");
            }
        }

        private void button2_Click(object sender, EventArgs e)  //удаление
        {
            object[] ob = new object[3];
            delete_alter del = new delete_alter();
            ob=del.delet(comboBox1.SelectedIndex,dataGridView2,textBox3,comboBox1);
            if (dataGridView2 != (DataGridView)ob[0])
                kol_delete++;
            dataGridView2=(DataGridView)ob[0];
            textBox3=(TextBox)ob[1];
            comboBox1=(ComboBox)ob[2];
            
        }

        private void button4_Click(object sender, EventArgs e)  //вычисление
        {
            if (n - kol_delete > 1)
            {
                DataGridView[] date = new DataGridView[2];
                calculate calc = new calculate();
                date = calc.start_calculate(dataGridView2, dataGridView3, textBox3,dataGridView1,kol_round);
                dataGridView1.Visible = true;
                dataGridView1 = date[0];
                dataGridView3 = date[1];
                timer2.Enabled = true;
                check_first_start = true;
            }
            else
                MessageBox.Show("Требуется минимум две альтернативы!");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Lines.Count() > 3 && groupBox4.Enabled == false || textBox2.Lines.Count() > 5 && groupBox4.Enabled == true)
                textBox2.ScrollBars = ScrollBars.Vertical;    
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
            groupBox1.Visible = false;
            timer3.Enabled = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            изменитьПроблемуToolStripMenuItem_Click(sender, e);
            kol_delete = 0;
            n = 0;
            comboBox1.Items.Clear();
            textBox3.Clear();
            textBox2.Clear();
            int kol_old = dataGridView2.Rows.Count;
            for (int j = kol_old - 1; j >= 0; j--)
            {
                dataGridView2.Rows.RemoveAt(j);
                dataGridView2.Columns.RemoveAt(j);
            }
            for (int j = dataGridView3.Rows.Count - 1; j >= 0; j--)
                dataGridView3.Rows.RemoveAt(j);
        }

        private void изменитьТочностьВыводаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            time = 0;
            timer1.Enabled = true;
            action = 1;
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(time<70)
            {
                label3.Top += action;
                label4.Top += action;
                groupBox1.Top+=action;
                groupBox2.Top+=action;
                groupBox3.Top += action;
                button3.Top += action;
                textBox2.Top += action;
                this.Height += action;
                time++;
            }
            else
            {
                if(action==1)
                {
                    groupBox6.Visible = true;
                }
                else
                {
                    groupBox6.Visible = false;
                }
                timer1.Enabled = false;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox6.Visible = false;
            time = 0;
            timer1.Enabled = true;
            kol_round = trackBar1.Value;
            action = -1;
            if(check_first_start)
            button4_Click(sender, e);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar1.Value.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(this.Height<600)
            {
                this.Height+=10;
            }
            else
            {
                groupBox1.Visible = true;
                timer2.Enabled = false;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if(this.Height>420)
            {
                this.Height -= 10;
            }
            else
            {
                timer3.Enabled = false;
            }
        }
      
    }
}
