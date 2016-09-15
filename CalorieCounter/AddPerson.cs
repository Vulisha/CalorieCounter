using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalorieCounter
{
    public partial class AddPerson : Form
    {
        public AddPerson()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "")
            {

                using (var dbCtx = new caloriedatabaseEntities1())
                {
                    Person item = new Person();
                    if (dbCtx.Person.Count() == 0)
                        item.PersonID = 1;
                    else
                        item.PersonID = dbCtx.Person.Max(i => i.PersonID) + 1;
                    item.Name = textBox1.Text;
                    item.WeightStart = Convert.ToDouble(textBox2.Text);
                    item.WeightCurrent = item.WeightStart;
                    item.Height = Convert.ToDouble(textBox4.Text);
                    dbCtx.Person.Add(item);
                    dbCtx.SaveChanges();
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Enter all fields");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(textBox2.Text, out result) && textBox2.Text != "")
            {
                MessageBox.Show("Enter integer!");
                textBox2.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(textBox4.Text, out result) && textBox4.Text != "")
            {
                MessageBox.Show("Enter integer!");
                textBox4.Text = "";
            }
        }
    }
}
