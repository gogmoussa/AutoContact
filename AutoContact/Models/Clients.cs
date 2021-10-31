using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoContact.Models
{
    public class Clients
    {
        public List<Client> ClientList { get; set; }

        public Clients(List<Client> emps)
        {
            this.ClientList = emps;
        }
    }
}
