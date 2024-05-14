using System.ComponentModel.DataAnnotations;

namespace Models;

public class csWine : ISeed<csWine>{

    [Key]
    public Guid WineId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public override string ToString() => $"{WineId}, {Name} is a {Type} wine from {Year}. Price is {Price:N2} kr";


    //Navigation property
    public csWineCellar WineCellar { get; set; } = null;

    #region Seeder
    public bool Seeded { get; set; } = false;

    public csWine Seed(csSeedGenerator seedGenerator)    {
        
       return  new csWine() {
         Seeded = true,

            WineId = Guid.NewGuid(),

            Name = seedGenerator.FromString("Chataux the paraply, Bollibompa, Alles verloren, Televinken, Humpty Dumpty"),
            Type = seedGenerator.FromString("Red, White, Rose, Champage"),
            Year = seedGenerator.Next (1999, 2023),
            Price = seedGenerator.NextDecimal (75, 600)
        };
    }
    #endregion
}