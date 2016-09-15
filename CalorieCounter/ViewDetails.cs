using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalorieCounter
{
    public partial class ViewDetails : Form
    {
        int id;
        public ViewDetails(int myId)
        {
            InitializeComponent();
            id = myId;
        }

        private void ViewDetails_Load(object sender, EventArgs e)
        {
            caloriedatabaseEntities1 DbCtx = new caloriedatabaseEntities1();
            var current = from c in DbCtx.Person
                             where c.PersonID == id
                             select c;
            Person person = current.First();
            labelName.Text = person.Name;
            labelWC.Text = person.WeightCurrent.ToString()+" kg";
            labelWS.Text = person.WeightStart.ToString() + " kg";
            labelHeight.Text = person.Height.ToString() + " cm";


            double VitC=0,Iron=0,Coles=0;
            //mine start

            double proteinP = 0, fiberP = 0, totalCarbP = 0, sodiumP = 0, colesterolP = 0, totalSatFatP = 0, totalFatP = 0, vitaminAP = 0, calcium = 0, thiamin = 0, riboflavin = 0, niacin = 0, vitaminB12 = 0, folate = 0, phosphorus = 0, magnesium = 0, zinc = 0, vitaminEP = 0, vitaminB6P = 0;


            //mine end
        
            var today = DateTime.Now;
            var weekago = today.AddDays(-7);
            var blogs1 = from c in DbCtx.Meal
                         where c.PersonID == id
                         where c.Datetime <= today
                         where c.Datetime >= weekago
                         select c;
            var blogs = blogs1.ToList();
            string[] arrFoodName = new string[blogs.Count];
            int mk=0;

            if (blogs.Count() > 0)
            {
                foreach (var qw in blogs)
                {
                    Food food = (from c in DbCtx.Food
                                 where c.FoodID == qw.FoodID
                                 select c).FirstOrDefault();

                    arrFoodName[mk++] = food.foodName;
                    double divider = qw.Grams / 100;
                    VitC += food.vitaminCP * divider;
                    Iron += food.iron * divider;
                    Coles += food.colesterolP * divider;

                    //mine start

                    proteinP += food.proteinP * divider;
                    fiberP += food.fiberP * divider;
                    totalCarbP += food.totalCarb * divider;
                    sodiumP += food.sodiumP * divider;
                    colesterolP += food.colesterolP * divider;
                    totalSatFatP += food.totalSatFatP * divider;
                    totalFatP += food.totalFatP * divider;
                    vitaminAP += food.vitaminAP * divider;
                    calcium += food.calcium * divider;
                    thiamin += food.thiamin * divider;
                    riboflavin += food.riboflavin * divider;
                    niacin += food.niacin * divider;
                    vitaminB12 += food.vitaminB12 * divider;

                    folate += food.folate * divider;
                    phosphorus += food.phosphorus * divider;
                    magnesium += food.magnesium * divider;
                    zinc += food.zinc * divider;
                    vitaminEP += food.vitaminEP * divider;
                    vitaminB6P += food.vitaminB6P * divider;
                


                    //mine end
                }

                mk = 0;
                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;

                //Food.foodName, Meal.Datetime, Meal.Grams and Meal.Calories

                listView1.Columns.Add("FoodName", 100);
                listView1.Columns.Add("Date", 100);
                listView1.Columns.Add("Grams", 70);
                listView1.Columns.Add("Calories", 100);
                string[] arr = new string[5];
                ListViewItem itm;

                foreach (var q in blogs)
                {
                    arr[0] = arrFoodName[mk++];//(q.).ToString();
                    arr[1] = (q.Datetime).ToString();
                    arr[2] = (q.Grams).ToString();
                    arr[3] = (q.Calories).ToString();
                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
                this.Refresh();
                this.Invalidate();
                this.Update();




                labelColes.Text = (Coles / 7).ToString("0.##") + "%";
                labelIron.Text = (Iron / 7).ToString("0.##") + "%";
                labelVitC.Text = (VitC / 7).ToString("0.##") + "%";

                //mine start
                labelprotein.Text = (proteinP / 7).ToString("0.##") + "%";
                labelfibre.Text = (fiberP / 7).ToString("0.##") + "%";
                labeltotalcarb.Text = (totalCarbP / 7).ToString("0.##") + "%";
                labelsodium.Text = (sodiumP / 7).ToString("0.##") + "%";
                labeltotalsatfat.Text = (totalSatFatP / 7).ToString("0.##") + "%";
                labeltotalfat.Text = (totalFatP / 7).ToString("0.##") + "%";
                labelvitaminA.Text = (vitaminAP / 7).ToString("0.##") + "%";
                labelcalcium.Text = (calcium / 7).ToString("0.##") + "%";
                labelthiamin.Text = (thiamin / 7).ToString("0.##") + "%";
                Riboflavin.Text = (riboflavin / 7).ToString("0.##") + "%";
                Niacin.Text = (niacin / 7).ToString("0.##") + "%";
                VitaminB12.Text = (niacin / 7).ToString("0.##") + "%";
                Folate.Text = (folate / 7).ToString("0.##") + "%";
                Phospourus.Text = (phosphorus / 7).ToString("0.##") + "%";
                Magnesium.Text = (magnesium / 7).ToString("0.##") + "%";
                Zinc.Text = (zinc / 7).ToString("0.##") + "%";
                VitaminE.Text = (vitaminEP / 7).ToString("0.##") + "%";
                VitaminB6.Text = (vitaminB6P / 7).ToString("0.##") + "%";
                
                //mine end
            }
            //finish this part withselected data from food.cs actually all dat unxer comment is it o?
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void labelVitC_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }
    }
}
