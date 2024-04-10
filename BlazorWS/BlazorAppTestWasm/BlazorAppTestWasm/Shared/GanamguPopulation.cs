using System;
using System.Collections.Generic;

namespace BlazorAppTestWasm.Server
{
    public partial class GanamguPopulation
    {
        public int Id { get; set; }
        public string AdministrativeAgenct { get; set; } = null!;
        public int? TotalPopulation { get; set; }
        public int? MalePopulation { get; set; }
        public int? FemalePopulation { get; set; }
        public double? SexRatio { get; set; }
        public int? NumberOfHouseholdes { get; set; }
        public double? NumberOfPeoplePerHousehold { get; set; }
    }
}
