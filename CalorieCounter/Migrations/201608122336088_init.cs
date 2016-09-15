namespace CalorieCounter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Food",
                c => new
                    {
                        FoodID = c.Int(nullable: false),
                        foodName = c.String(),
                        Calories = c.Double(nullable: false),
                        totalFat = c.Double(nullable: false),
                        totalFatP = c.Double(nullable: false),
                        totalSatFat = c.Double(nullable: false),
                        totalSatFatP = c.Double(nullable: false),
                        totalTransFat = c.Double(nullable: false),
                        totalTransFatP = c.Double(nullable: false),
                        colesterol = c.Double(nullable: false),
                        colesterolP = c.Double(nullable: false),
                        sodium = c.Double(nullable: false),
                        sodiumP = c.Double(nullable: false),
                        totalCarb = c.Double(nullable: false),
                        totalCarbP = c.Double(nullable: false),
                        fiber = c.Double(nullable: false),
                        fiberP = c.Double(nullable: false),
                        sugar = c.Double(nullable: false),
                        protein = c.Double(nullable: false),
                        proteinP = c.Double(nullable: false),
                        vitaminAP = c.Double(nullable: false),
                        vitaminCP = c.Double(nullable: false),
                        calcium = c.Double(nullable: false),
                        iron = c.Double(nullable: false),
                        thiamin = c.Double(nullable: false),
                        riboflavin = c.Double(nullable: false),
                        niacin = c.Double(nullable: false),
                        vitaminB12 = c.Double(nullable: false),
                        folate = c.Double(nullable: false),
                        phosphorus = c.Double(nullable: false),
                        magnesium = c.Double(nullable: false),
                        zinc = c.Double(nullable: false),
                        vitaminEP = c.Double(nullable: false),
                        vitaminB6P = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FoodID);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        MealID = c.Int(nullable: false),
                        Calories = c.Int(nullable: false),
                        Datetime = c.DateTime(nullable: false),
                        PersonID = c.Int(nullable: false),
                        FoodID = c.Int(nullable: false),
                        Grams = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MealID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        WeightStart = c.Double(nullable: false),
                        WeightCurrent = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meal", "PersonID", "dbo.Person");
            DropIndex("dbo.Meal", new[] { "PersonID" });
            DropTable("dbo.Person");
            DropTable("dbo.Meal");
            DropTable("dbo.Food");
        }
    }
}
