using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLib.Common;
using BusinessLib.Data;

namespace BusinessLib.Business
{
    public class ClientValidation
    {
        private static List<string> errors = new List<string>();

        public static string ErrorMessage
        {
            get
            {
                string message = "";

                foreach (string error in errors)
                {
                    message += error + "\r\n";
                }

                return message;
            }
        }

        public static int AddClient(Client client)
        {
            if (validate(client))
            {
                return ClientRepository.AddClient(client);
            }
            else
            {
                return -1;
            }
        }

        public static int UpdateClient(Client client)
        {
            if (validate(client))
            {
                return ClientRepository.UpdateClient(client);
            }
            else
            {
                return -1;
            }
        }

        private static bool validate(Client client)
        {
            errors.Clear();
            bool valid = true;
            string regexClientCode = @"^[A-Z]{5}$";
            string regexPostalCode = @"^[A-Z]\d[A-Z] \d[A-Z]\d$";

            if (string.IsNullOrWhiteSpace(client.ClientCode))
            {
                errors.Add("Client Code can't be empty");
                valid = false;
            }
            
            if (!Regex.IsMatch(client.ClientCode, regexClientCode))
            {
                errors.Add("Client Code must have the pattern AAAAA");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(client.CompanyName))
            {
                errors.Add("Company Name can't be empty");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(client.AddressOne))
            {
                errors.Add("Address 1 can't be empty");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(client.Province))
            {
                errors.Add("Province can't be empty");
                valid = false;
            }

            if (!Regex.IsMatch(client.PostalCode ?? "", regexPostalCode))
            {
                errors.Add("Postal Code must have the pattern A9A 9A9");
                valid = false;
            }

            if (client.YTDSales < 0)
            {
                errors.Add("YTDSales can't be negative");
                valid = false;
            }

            return valid;
        }

        public static int DeleteClient(Client client) => ClientRepository.DeleteClient(client);
        public static ClientCollection GetClients() => ClientRepository.GetClients();
    }
}
