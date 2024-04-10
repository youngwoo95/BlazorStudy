using BlazorAppTestWasm.Server.Interfaces;
using BlazorAppTestWasm.Server.Models;
using BlazorAppTestWasm.Shared;

namespace BlazorAppTestWasm.Server.Services
{
    public class GanamguPopulationService : IGangnamguPopulation
    {
        private readonly InfrunContext context;

        public GanamguPopulationService(InfrunContext _context)
        {
            this.context = _context;
        }

        public void AddGangnamguPopulation(GanamguPopulation population)
        {
            try
            {
                context.GanamguPopulations.Add(population);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void DeleteGangnamguPopulation(int id)
        {
            try
            {
                GanamguPopulation? ganamguPopulation = context.GanamguPopulations.Find(id);

                if (ganamguPopulation != null)
                {
                    context.GanamguPopulations.Remove(ganamguPopulation);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<GanamguPopulation> GetAllGanamguPopulation()
        {
            try
            {
                return context.GanamguPopulations.ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public GanamguPopulation GetGanamguPopulation(int id)
        {
            try
            {
                GanamguPopulation? ganamguPopulation = context.GanamguPopulations.Find(id);
                
                if(ganamguPopulation != null)
                {
                    return ganamguPopulation;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void UpdateGangnamguPopulation(GanamguPopulation population)
        {
            try
            {
                context.GanamguPopulations.Update(population);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}