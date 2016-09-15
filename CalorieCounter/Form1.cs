using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace CalorieCounter
{
    public partial class Form1 : Form
    {
        int id = 0;
        public Form1()
        {
            InitializeComponent();
            startCalorie();
            button3.Hide();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection person = this.listView1.SelectedItems;
            foreach (ListViewItem item in person)
            {
                id = int.Parse(item.SubItems[0].Text);
            }

            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            button2.Visible = true;
            button3.Show();
            button4.Show();
            using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
            {
                var now = DateTime.Now.Date;
                //var blogs = context.Meal.SqlQuery("SELECT * FROM dbo.Meal WHERE PersonID=" + id.ToString() + "AND DATEDIFF(day,GETDATE(),Datetime) <= 7 ").ToList();
                //var blogs2 = context.Meal.SqlQuery("SELECT * FROM dbo.Meal WHERE PersonID=" + id.ToString() + "AND DATEDIFF(day,GETDATE(),Datetime) <= 30 ").ToList();
              
                
                var today = DateTime.Now;
                var weekago = today.AddDays(-7);
                var blogs1 = from c in context.Meal
                             where c.PersonID == id
                             where c.Datetime <= today
                             where c.Datetime >= weekago
                             select c;
                var blogs = blogs1.ToList();
                weekago = today.AddDays(-30);
                var blogs3 = from c in context.Meal
                             where c.PersonID == id
                             where c.Datetime <= today
                             where c.Datetime >= weekago
                             select c;
                var blogs2 = blogs1.ToList();

                label3.Text = "Last Week Calorie";
                label5.Text = "Last Month Calorie";
                int total = 0;
                if (blogs.Count() > 0)
                {
                    foreach (var qw in blogs)
                    {
                        total += qw.Calories;
                    }
                }
                label4.Text = total.ToString();
                total = 0;
                if (blogs2.Count() > 0)
                {
                    foreach (var mq in blogs2)
                    {
                        total += mq.Calories;
                    }
                }
                label6.Text = total.ToString();
            }

        }
        private void startCalorie()
        {
            using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
            {
                updateListViewItem(context);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search myForm = new Search(Convert.ToInt32(id));
            myForm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPerson myForm = new AddPerson();
            myForm.ShowDialog();
            listView1.Clear();
                using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
                {
                    updateListViewItem(context);
                }
        }
        public void restartFoam()
        {
            this.Update();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void updateListViewItem(caloriedatabaseEntities1 context)
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            var query = from c in context.Person select c;

            listView1.Columns.Add("ID", 30);
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("WeightStart", 70);
            listView1.Columns.Add("WeightCurrent", 100);
            listView1.Columns.Add("Height", 70);
            string[] arr = new string[5];
            ListViewItem itm;

            foreach (var q in query)
            {
                arr[0] = (q.PersonID).ToString();
                arr[1] = q.Name;
                arr[2] = (q.WeightStart).ToString();
                arr[3] = (q.WeightCurrent).ToString();
                arr[4] = (q.Height).ToString();

                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }
            this.Refresh();
            this.Invalidate();
            this.Update();
        }
     
        private void button3_Click(object sender, EventArgs e)
        {
            EditPerson myForm = new EditPerson(Convert.ToInt32(id));
            myForm.ShowDialog();
            if (myForm.DialogResult == DialogResult.OK) { 
                listView1.Clear();
                using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
                {
                    updateListViewItem(context);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewDetails myForm = new ViewDetails(Convert.ToInt32(id));
            myForm.ShowDialog();
        }
       
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var loc = listView1.HitTest(e.Location);
                if (loc.Item != null)
                    contextMenuStrip2.Show(listView1, e.Location);
            }
        }

        private void viewDetailManu(object sender, EventArgs e)
        {
            ViewDetails myForm = new ViewDetails(Convert.ToInt32(id));
            myForm.ShowDialog();
        }

        private void updateManu(object sender, EventArgs e)
        {
            EditPerson myForm = new EditPerson(Convert.ToInt32(id));
            myForm.ShowDialog();
            if (myForm.DialogResult == DialogResult.OK)
            {
                listView1.Clear();
                using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
                {
                    updateListViewItem(context);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (caloriedatabaseEntities1 context = new caloriedatabaseEntities1())
            {
                var query =
                from ord in context.Person
                where ord.PersonID == id
                select ord;
                Person person = new Person();
                foreach (var ord in query)
                    person = ord;

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete "+person.Name, "Delete?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    context.Person.Remove(person);
                    context.SaveChanges();
                    updateListViewItem(context);
                }



            }
        }
    }
}