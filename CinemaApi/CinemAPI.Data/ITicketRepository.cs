using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.Ouput;
using CinemAPI.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        TicketReturnObject Insert(ITicketCreation ticket);
    }
}
