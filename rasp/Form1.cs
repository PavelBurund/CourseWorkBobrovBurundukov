using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace rasp
{
    public partial class Form1 : Form
    {
        private List<List<string>> DATA = new List<List<string>>();

        public Form1()
        {
            InitializeComponent();
        }

        TOOL tool = new TOOL();

        private void Form1_Load(object sender, EventArgs e)
        {
            DATA = tool.readFile();
            dataGridView1.Rows.Clear();
            foreach (List<string> i in DATA)
            {
                dataGridView1.Rows.Add(i[6], i[0], i[1], i[2], i[3], i[4], i[5]);
            }
            dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<List<string>> tmp = new List<List<string>>();
            tmp = tool.findLeads(textBoxFindName.Text, textBoxFindLastname.Text, textBoxFindPhone.Text);
            dataGridView1.Rows.Clear();
            foreach (List<string> i in tmp)
            {
                dataGridView1.Rows.Add(i[6], i[0], i[1], i[2], i[3], i[4], i[5]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string error = tool.appendLead(textBoxAddName.Text, textBoxAddLastName.Text, textBoxAddNumber.Text, textBoxAddEmail.Text, textBoxAddAdress.Text, textBoxAddBirthDate.Text);
            if (error != "OK")
            {
                MessageBox.Show(error);
            }
            DATA = tool.readFile();
            dataGridView1.Rows.Clear();
            foreach (List<string> i in DATA)
            {
                dataGridView1.Rows.Add(i[6], i[0], i[1], i[2], i[3], i[4], i[5]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DATA = tool.readFile();
            dataGridView1.Rows.Clear();
            foreach (List<string> i in DATA)
            {
                dataGridView1.Rows.Add(i[6], i[0], i[1], i[2], i[3], i[4], i[5]);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point point = Form1.MousePosition;
            if (e.Button.ToString() == "Right")
            {
                tool.index = e.RowIndex;
                contextMenuStrip1.Show(point);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DATA.RemoveAt(tool.index);
            tool.rewriteLeads();
            DATA = tool.readFile();
            dataGridView1.Rows.Clear();
            foreach (List<string> i in DATA)
            {
                dataGridView1.Rows.Add(i[6], i[0], i[1], i[2], i[3], i[4], i[5]);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tool.index >= 0 && tool.index <= tool.DATA.Count - 1)
            {
                Form3 form3 = new Form3(tool);
                form3.ShowDialog(this);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBoxAddBirthDate_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAddBirthDate.Text == "ГГГГ-ММ_ДД")
            textBoxAddBirthDate.Text = "";
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf = "BEGIN:VCARD\n";
            buf += "VERSION:1.00\n";
            buf += $"N:{tool.DATA[tool.index][0]}{tool.DATA[tool.index][1]}\n";
 //           buf += $"FN:{tool.DATA[tool.index][1]}\n";
            buf += $"TEL;HOME;VOICE:{tool.DATA[tool.index][2]}\n";
            buf += $"EMAIL;TYPE=INTERNET:{tool.DATA[tool.index][3]}\n";
            buf += $"ADR;WORK;PREF;CHARSET=utf-8:;;{tool.DATA[tool.index][4]};;;;Россия\nLABEL;WORK;PREF:{tool.DATA[tool.index][4]}\n";
            buf += $"BDAY:{tool.DATA[tool.index][5]}\n";
            buf += "END:VCARD";

                saveFileDialog1.ShowDialog();
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName + ".vcf");
                sw.Write(buf);
                sw.Close();

        }
    }

}