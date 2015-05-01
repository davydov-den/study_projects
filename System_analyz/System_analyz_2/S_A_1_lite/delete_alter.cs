using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace S_A_1_lite
{
    class delete_alter
    {
        DataGridView dataGridView2 = new DataGridView();
        TextBox textBox3 = new TextBox();
        ComboBox comboBox1;
        ComboBox comboBox6;
        object[] ob = new object[4];
        /// <summary>
        /// Удаление альтернативы
        /// </summary>
        /// <param name="i"></param>
        /// Номер
        /// <param name="date"></param>
        /// <param name="str"></param>
        /// <param name="combo"></param>
        /// <returns></returns>
        public object[] delet(int i,DataGridView date , TextBox str, ComboBox combo,ComboBox combo2)
        {
            textBox3.Multiline = true;
            textBox3 = str;
            dataGridView2 = date;
            comboBox1 = combo;
            comboBox6 = combo2;

            if (comboBox1.SelectedIndex > -1)
            {
                var result = MessageBox.Show("Выдействительно хотите эту альтернативу?", "Удаление альтернативы", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                    delete_alte(i);
            }
            else
            {
                MessageBox.Show("Выберите альтернативу для удаления!");
            }


            ob[0] = dataGridView2;
            ob[1] = textBox3;
            ob[2] = comboBox1;
            ob[3] = comboBox6;
            return ob;

        }

        public void delete_alte(int number)
        {
            dataGridView2.Columns.RemoveAt(number);
            textBox3.Lines[number] = "";
            var results = from p in textBox3.Lines
                          where p != textBox3.Lines[number]
                          select p;

            textBox3.Lines = results.ToArray<string>();
            comboBox1.Items.RemoveAt(number);
            comboBox6.Items.RemoveAt(number);


            
        }
    }
}
