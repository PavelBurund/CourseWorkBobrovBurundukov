using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rasp
{
    public partial class Form3 : Form
    {

        public Form3(TOOL Form1_tool)
        {
            InitializeComponent();
            tool = Form1_tool;
            textBoxName.Text = tool.DATA[tool.index][0];
            textBoxLastname.Text = tool.DATA[tool.index][1];
            textBoxPhone.Text = tool.DATA[tool.index][2];
            textBoxEmail.Text = tool.DATA[tool.index][3];
            textBoxAdress.Text = tool.DATA[tool.index][4];
            textBoxBirthdate.Text = tool.DATA[tool.index][5];
        }
        TOOL tool;

        private void button1_Click(object sender, EventArgs e)
        {
            string error = tool.editLead(textBoxName.Text, textBoxLastname.Text, textBoxPhone.Text, textBoxEmail.Text, textBoxAdress.Text, textBoxBirthdate.Text);
            if (error != "OK")
            {
                MessageBox.Show(error);
            }
            else
            {
                //List<List<string>> tmp = new List<List<string>>(); 
                tool.rewriteLeads();
                tool.readFile();

                Form1 frm1 = this.Owner as Form1;
                frm1.dataGridView1.Rows.Clear();

                foreach (List<string> i in tool.DATA)
                {
                    frm1.dataGridView1.Rows.Add(i[6], i[0], i[1], i[2], i[3], i[4], i[5]);
                }
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
