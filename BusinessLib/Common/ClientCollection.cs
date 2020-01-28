using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib.Common
{
    public class ClientCollection : BindingList<Client>
    {
        public decimal TotalYTDSales => this.Sum(x => x.YTDSales);

        public int CreditHoldCount => this.Count(x => x.CreditHold);

        public bool ContainsClientCode(Client client)
        {
            foreach (Client record in this)
            { 
                if(record.ClientCode == client.ClientCode)
                {
                    return true;
                }
            }            

            return false;
        }
    }
}
