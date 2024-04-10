using BlazorAppWasmTest2.Server.Interfaces;
using BlazorAppWasmTest2.Server.Models;
using BlazorAppWasmTest2.Shared;

namespace BlazorAppWasmTest2.Server.Services
{
    public class GNPopulationService : IGNPopulation
    {
        private readonly InfrunContext context;

        public GNPopulationService(InfrunContext _context)
        {
            this.context = _context;
        }

        public void AddGNPopulation(Gnpopulation population)
        {
            try
            {
                context.Gnpopulations.Add(population);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteGNPopulation(int id)
        {
            try
            {
                Gnpopulation? ganamguPopulation = context.Gnpopulations.Find(id);

                if (ganamguPopulation != null)
                {
                    context.Gnpopulations.Remove(ganamguPopulation);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Gnpopulation> GetAllGNPopulation()
        {
            try
            {
                return context.Gnpopulations.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Gnpopulation GetGNPopulation(int id)
        {
            try
            {
                Gnpopulation? ganamguPopulation = context.Gnpopulations.Find(id);

                if (ganamguPopulation != null)
                {
                    return ganamguPopulation;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateGNPopulation(Gnpopulation population)
        {
            try
            {
                context.Gnpopulations.Update(population);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
