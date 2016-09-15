namespace CalorieCounter
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


    [Table("Food")]
    public partial class  Food
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodID { get; set; }
        public string foodName { get; set; }
        public double Calories { get; set; }
        public double totalFat { get; set; }
        public double totalSatFat { get; set; }
        public double totalTransFat { get; set; }
        public double totalTransFatP { get; set; }
        public double colesterol { get; set; }
        public double sodium { get; set; }
        public double totalCarb { get; set; }
        public double fiber { get; set; }
        public double sugar { get; set; }
        public double protein { get; set; }



        //Percenages
        public double proteinP { get; set; }
        public double fiberP { get; set; }
        public double totalCarbP { get; set; }
        public double sodiumP { get; set; }
        public double colesterolP { get; set; }
        public double totalSatFatP { get; set; }
        public double totalFatP { get; set; }
        public double vitaminAP { get; set; }
        public double vitaminCP { get; set; }
        public double calcium { get; set; }
        public double iron { get; set; }
        public double thiamin { get; set; }
        public double riboflavin { get; set; }
        public double niacin { get; set; }
        public double vitaminB12 { get; set; }
        public double folate { get; set; }
        public double phosphorus { get; set; }
        public double magnesium { get; set; }
        public double zinc { get; set; }
        public double vitaminEP { get; set; }
        public double vitaminB6P { get; set; }
        

      
    }
}
