using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using WolframAlphaNET;
using WolframAlphaNET.Misc;
using WolframAlphaNET.Objects;
using System.Text.RegularExpressions;

namespace CalorieCounter
{
    public partial class Search : Form
    {
        int globeID;
        double divider;
        Food foodData = new Food();
        public Search(int id)
        {
            InitializeComponent();
            globeID = id;

        }
        public Search()
        {
            InitializeComponent();

        }
     


        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            labelSearchingWA.Visible = true;
            errorLabel.Visible = false; 
            Application.DoEvents();
            string str = textBox1.Text;
            string str2 = textBox2.Text;
            divider = double.Parse(str) / 100;
            string queryString = "nutritional information for " + str +"grams of " + str2;
            ListViewItem item;
            using (var dbCtx = new caloriedatabaseEntities1())
            {

                var blogs1 = from c in dbCtx.Food
                             where c.foodName == str2
                             select c;
                var blogs = blogs1.ToList();
                if (blogs.Count() > 0)
                {
                    foodData = blogs[0];
                    textBox4.Text = (foodData.Calories * divider).ToString();
                    foreach (var propertyInfo in foodData.GetType().GetProperties())
                    {
                        item = new ListViewItem(propertyInfo.Name +" "+ propertyInfo.GetValue(foodData, null));
                        listView1.Items.Add(item);
                        
                    }
                }
            else { 
  
                    WolframAlpha wolfram = new WolframAlpha("INSERT WolframAlpha API KEY HERE");
                    //wolfram.ScanTimeout = 0.5f;
            
                    QueryResult results = wolfram.Query(queryString);

                    if (results.Error != null)
                    {
                        errorLabel.Visible = true; 
                        errorLabel.Text = "Woops, where was an error: " + results.Error.Message;
                    }
                    if (results.Success == false){
                        errorLabel.Visible = true; 
                        errorLabel.Text = "Woops, we don't understand what you wrote :/";
                    }
                    Application.DoEvents();

                    Pod primaryPod = results.GetPrimaryPod();

                    if (primaryPod != null)
                    {
                        if (primaryPod.SubPods.HasElements())
                        {
                            int i = 0;
                            listView1.Columns.Add("WolframAlpha Raw Data", 500);
                            string strSubPod = "";
                            foreach (SubPod subPod in primaryPod.SubPods)
                            {
                                strSubPod += subPod.Plaintext;

                            }

                            foodData = fillFood(strSubPod, divider);
                            foodData.foodName = str2;
                            if (dbCtx.Food.Count() == 0)
                                foodData.FoodID = 1;
                            else
                                foodData.FoodID = dbCtx.Food.Max(f => f.FoodID) + 1;
                            dbCtx.Food.Add(foodData);
                            dbCtx.SaveChanges();


                            string[] numbers = Regex.Split(strSubPod, @"\D+");
  
                            string[] nutritionalDetails = strSubPod.Split('\n');
                            foreach (string detail in nutritionalDetails)
                            {
                                item = new ListViewItem(detail.Trim());
                                listView1.Items.Add(item);
                                i++;
                            }

                            this.listView1.View = View.Details;
                            textBox4.Text = (foodData.Calories * divider).ToString();
                        }

                    }
            }


            
            /*str += "g%20of%20" + str2;
            string strURI = "http://api.wolframalpha.com/v2/query?appid=WHVQ9A-GL538Y4ERV&input=calories%20in%20" + str + "&format=image,plaintext";
            WebRequest request = WebRequest.Create(strURI);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);
            string xpath = "queryresult/pod/subpod/plaintext";
            var nodes = xmlDoc.SelectNodes(xpath);
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            rr = childrenNode.InnerText.ToString();
                if (i == 1)
                {
                    String[] substrings = strr.Split(' ');
                    textBox4.Text = substrings[0].ToString();
                }
                item = new ListViewItem(strr);
                //Add items in the listview
                listView1.Items.Add(item);
                i++;
            }
            this.listView1.View = View.Details;*/
            }
            labelSearchingWA.Visible = false;
            Application.DoEvents();
        }



        private Food fillFood(string testText, double divider) {
            Food tempFood = new Food();
            Match tempMatch = Regex.Match(testText, @"total calories *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.Calories = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"total fat *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalFat = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"saturated fat *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalSatFat = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"trans fat *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalTransFat = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"cholesterol *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.colesterol = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"sodium *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.sodium = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"total carbohydrates *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalCarb = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"dietary fiber *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.fiber = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"sugar *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.sugar = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"protein *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.protein = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"Vitamin A *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.vitaminAP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"Vitamin C *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.vitaminCP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"Vitamin B12 *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.vitaminB12 = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"calcium *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.calcium = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"iron *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.iron = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"thiamin *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.thiamin = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"riboflavin *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.riboflavin = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"phosphorus *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.phosphorus = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"magnesium *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.magnesium = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"niacin *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.niacin = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"zinc *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.zinc = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"Vitamin B6 *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.vitaminB6P = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"Vitamin E *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.vitaminEP = int.Parse(tempMatch.Groups[1].Value) / divider; 
            tempMatch = Regex.Match(testText, @"folate *(\d*)");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.folate = int.Parse(tempMatch.Groups[1].Value) / divider;

            tempMatch = Regex.Match(testText, @"total fat.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalFatP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"saturated fat.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalSatFatP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"cholesterol.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.colesterolP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"sodium.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.sodiumP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"total carbohydrates.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.totalCarbP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"dietary fiber.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.fiberP = int.Parse(tempMatch.Groups[1].Value) / divider;
            tempMatch = Regex.Match(testText, @"protein.*[\t\u007c] (\d*)%");
            if (tempMatch.Groups[1].Success && tempMatch.Groups[1].Value != "")
                tempFood.proteinP = int.Parse(tempMatch.Groups[1].Value) / divider;
            
            return tempFood;

        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int cal = Convert.ToInt32(foodData.Calories*divider);
            using (var dbCtx = new caloriedatabaseEntities1())
            {

                Meal item = new Meal();
                if (dbCtx.Meal.Count() == 0)
                    item.MealID = 1;
                else
                item.MealID = dbCtx.Meal.Max(i => i.MealID) + 1;
                item.Calories = cal;
                item.Datetime = DateTime.Now;
                item.PersonID = globeID;
                item.Grams = Convert.ToInt32(divider * 100);
                item.FoodID = foodData.FoodID;
                //Add Student object into Students DBset
                dbCtx.Meal.Add(item);
                // call SaveChanges method to save student into database
                dbCtx.SaveChanges();
            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(textBox1.Text, out result) && textBox1.Text != "")
            {
                MessageBox.Show("Enter integer!");
                //er.SetError(this, "Only Integers are allowed");
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(textBox4.Text, out result) && textBox4.Text != "")
            {
                MessageBox.Show("Enter integer!");
                //er.SetError(this, "Only Integers are allowed");
                textBox4.Text = "";
            }
        }
    }
}
