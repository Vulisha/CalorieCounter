namespace CalorieCounter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Meal")]
    public partial class Meal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MealID { get; set; }
        public int Calories { get; set; }
        public DateTime Datetime{ get; set; }
        public int PersonID { get; set; }
        public int FoodID { get; set; }
        public int Grams { get; set; }

        public virtual Person Person { get; set; }
    }
}
