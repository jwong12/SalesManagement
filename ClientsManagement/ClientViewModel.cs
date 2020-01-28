using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib.Common;
using BusinessLib.Business;

namespace ClientsManagement
{
    public class ClientViewModel : ViewModelBase
    {
        private Client client;

        public ClientViewModel() 
        {
            this.Clients = ClientValidation.GetClients();
            this.Client = new Client();
        }
        
        public Client Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged();
            }
        }

        public ClientCollection Clients { get; set; }

        public void SetDisplayClient(Client client)
        {
            this.Client = new Client
            {
                ClientCode = client.ClientCode,
                CompanyName = client.CompanyName,
                AddressOne = client.AddressOne,
                AddressTwo = client.AddressTwo,
                City = client.City,
                Province = client.Province,
                PostalCode = client.PostalCode,
                YTDSales = client.YTDSales,
                CreditHold = client.CreditHold,
                Notes = client.Notes
            };
        }

        public Client GetDisplayClient()
        {
            OnPropertyChanged("Client");
            return this.Client;
        }
    }
}
