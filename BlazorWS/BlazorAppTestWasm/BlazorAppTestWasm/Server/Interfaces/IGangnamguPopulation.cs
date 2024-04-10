using BlazorAppTestWasm.Shared;

namespace BlazorAppTestWasm.Server.Interfaces
{
    public interface IGangnamguPopulation
    {
        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="population"></param>
        public void AddGangnamguPopulation(GanamguPopulation population);

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="population"></param>
        public void UpdateGangnamguPopulation(GanamguPopulation population);

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public void DeleteGangnamguPopulation(int id);

        /// <summary>
        /// SELECT - ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GanamguPopulation GetGanamguPopulation(int id);

        /// <summary>
        /// SELECR - ALL
        /// </summary>
        /// <returns></returns>
        public List<GanamguPopulation> GetAllGanamguPopulation();

    }
}
