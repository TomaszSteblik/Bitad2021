using System.Collections.Generic;
using System.Threading.Tasks;
using Bitad2021.Data.Interfaces;
using Bitad2021.Models;

namespace Bitad2021.Data.Implementations
{
    public class AgendaServiceRest : IAgendaService
    {
        public async Task<ICollection<Agenda>> GetAllAgendas()
        {
            throw new System.NotImplementedException();
        }
    }
}