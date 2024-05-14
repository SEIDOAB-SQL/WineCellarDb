using System.ComponentModel.DataAnnotations;

namespace Models;

public class csWineCellar : ISeed<csWineCellar>{

    [Key]
    public Guid WineCellarId { get; set; }
    public string Name { get; set; }
    public override string ToString() => $"{WineCellarId}, {Name} contains {Wines?.Count?? 0} wines";

    //Navigation property
    public List<csWine> Wines { get; set; } = null;


    #region Seeder
    public bool Seeded { get; set; } = false;

    public csWineCellar Seed(csSeedGenerator seedGenerator)    {
        
       return  new csWineCellar() {
            Seeded = true,

            WineCellarId = Guid.NewGuid(),
            Name = $"{seedGenerator.FullName}'s winecellar"
        };
    }
    #endregion
}