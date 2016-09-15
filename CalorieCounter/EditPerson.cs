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
    public partial class EditPerson : Form
    {
        int id;
        public EditPerson(int myId)
        {
            InitializeComponent();
            id = myId;
        }

        private void EditPerson_Load(object sender, EventArgs e)
        {
            using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
            {
                var query = from c in context.Person
                            where c.PersonID == id
                            select c;
                foreach (var q in query)
                {
                    textBox1.Text = q.Name;
                    textBox3.Text = q.WeightCurrent.ToString();

                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "")
            {
                using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
                {
                    var query =
                    from ord in context.Person
                    where ord.PersonID == id
                    select ord;

                    // Execute the query, and change the column values
                    // you want to change.
                    foreach (var ord in query)
                    {
                        ord.Name = textBox1.Text;
                        ord.WeightCurrent = Convert.ToDouble(textBox3.Text);
                    }
                    context.SaveChanges();
                    this.Close();

                }
            }
            else
                MessageBox.Show("Enter all fields");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(textBox3.Text, out result) && textBox3.Text != "")
            {
                MessageBox.Show("Enter only integer value!");
                //er.SetError(this, "Only Integers are allowed");
                textBox3.Text = "";
            }
        }
    }
}
