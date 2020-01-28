using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib.Common
{
    public class Client
    {          
        public string ClientCode { get; set; }
        public string CompanyName { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public decimal YTDSales { get; set; }
        public bool CreditHold { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return $"ClientCode: {ClientCode}, CompanyName: {CompanyName}, AddressOne: {AddressOne}, AddressTwo: {AddressTwo}, City: {City}, Province: {Province}, PostalCode: {PostalCode}, YTDSales: {YTDSales}, CreditHold: {CreditHold}, Notes: {Notes}";
        }
    }
}
