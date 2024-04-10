namespace BlazorAppWasmTest2.Shared
{
    public partial class Gnpopulation
    {
        public int Id { get; set; }
        public string AdministrativeAgency { get; set; } = null!;
        public int? TotalPopulation { get; set; }
        public int? MalePopulation { get; set; }
        public int? FemalePopulation { get; set; }
        public double? SexRatio { get; set; }
        public int? NumberOfHouseholdes { get; set; }
        public double? NumberOfPeoplePerHousehold { get; set; }
    }
}
