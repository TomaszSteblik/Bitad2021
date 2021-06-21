using System.Collections.Generic;
using System.Threading.Tasks;
using Bitad2021.Models;

namespace Bitad2021.Data.Interfaces
{
    public interface IAgendaService
    {
        public Task<ICollection<Agenda>> GetAllAgendas();
    }
}