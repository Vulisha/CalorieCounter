namespace CalorieCounter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        public Person()
        {
            Meal = new HashSet<Meal>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double WeightStart { get; set; }

        public double WeightCurrent { get; set; }

        public double Height { get; set; }

        public virtual ICollection<Meal> Meal { get; set; }
    }
}
