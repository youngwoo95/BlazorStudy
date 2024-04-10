using BlazorAppWasmTest2.Shared;

namespace BlazorAppWasmTest2.Server.Interfaces
{
    public interface IGNPopulation
    {
        public void AddGNPopulation(Gnpopulation population);
        public void UpdateGNPopulation(Gnpopulation population);
        public void DeleteGNPopulation(int id);
        public Gnpopulation GetGNPopulation(int id);
        public List<Gnpopulation> GetAllGNPopulation();
    }
}
