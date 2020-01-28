using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib.Common;

namespace BusinessLib.Data
{
    class ClientRepository
    {
        private static readonly string connString = @"Server=tcp:skeena.database.windows.net,1433; 
                                                      Initial Catalog=comp2614;
                                                      User ID=student;
                                                      Password=p8SmM5/mKZk=;
                                                      Encrypt=True;
                                                      TrustServerCertificate=False;
                                                      Connection Timeout=30;";

        private const string clientTableName = "Client025959";       

        public static ClientCollection GetClients() 
        {
            ClientCollection clients;

            using(SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"SELECT ClientCode, CompanyName, Address1, Address2, City, Province, PostalCode, YTDSales, CreditHold, Notes
                                FROM {clientTableName}";

                using (SqlCommand cmd = new SqlCommand()) 
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    conn.Open();

                    clients = new ClientCollection();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string clientCode;          // 0
                        string companyName;         // 1
                        string addressOne;          // 2
                        string addressTwo = null;   // 3
                        string city = null;         // 4
                        string province;            // 5
                        string postalCode = null;   // 6
                        decimal YTDSales;           // 7
                        bool creditHold;            // 8
                        string notes = null;        // 9

                        while (reader.Read())
                        {
                            clientCode = reader["ClientCode"] as string;
                            companyName = reader["CompanyName"] as string;
                            addressOne = reader["Address1"] as string;

                            if (!reader.IsDBNull(3))
                            {
                                addressTwo = reader["Address2"] as string;
                            }

                            if (!reader.IsDBNull(4))
                            {
                                city = reader["City"] as string;
                            }

                            province = reader["Province"] as string;

                            if (!reader.IsDBNull(6))
                            {
                                postalCode = reader["PostalCode"] as string;
                            }

                            YTDSales = (decimal)reader["YTDSales"];
                            creditHold = (bool)reader["CreditHold"];

                            if (!reader.IsDBNull(9))
                            {
                                notes = reader["Notes"] as string;
                            }

                            clients.Add(new Client
                            {
                                ClientCode = clientCode,
                                CompanyName = companyName,
                                AddressOne = addressOne,
                                AddressTwo = addressTwo,
                                City = city,
                                Province = province,
                                PostalCode = postalCode,
                                YTDSales = YTDSales,
                                CreditHold = creditHold,
                                Notes = notes
                            });

                            addressTwo = null;
                            city = null;
                            postalCode = null;
                            notes = null;
                        }
                    }

                    return clients;
                }
            }
        }

        public static int AddClient(Client client)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"INSERT INTO {clientTableName}
                                 (ClientCode, CompanyName, Address1, Address2, City, Province, PostalCode, YTDSales, CreditHold, Notes)
                                  VALUES (@clientCode, @companyName, @address1, @address2, @city, @province, @postalCode, @YTDSales, @creditHold, @notes)";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("clientCode", client.ClientCode);
                    cmd.Parameters.AddWithValue("companyName", client.CompanyName);
                    cmd.Parameters.AddWithValue("address1", client.AddressOne);
                    cmd.Parameters.AddWithValue("address2", (object)client.AddressTwo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("city", (object)client.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("province", client.Province);
                    cmd.Parameters.AddWithValue("postalCode", (object)client.PostalCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("YTDSales", client.YTDSales);
                    cmd.Parameters.AddWithValue("creditHold", client.CreditHold);
                    cmd.Parameters.AddWithValue("notes", (object)client.Notes ?? DBNull.Value);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public static int UpdateClient(Client client)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"UPDATE {clientTableName}
                                  SET ClientCode = @clientCode,
                                      CompanyName = @companyName,
                                      Address1 = @address1,
                                      Address2 = @address2,
                                      City = @city,
                                      Province = @province,
                                      PostalCode = @postalCode,
                                      YTDSales = @YTDSales,
                                      CreditHold = @creditHold,
                                      Notes = @notes
                                  WHERE ClientCode = @clientCode";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("clientCode", client.ClientCode);
                    cmd.Parameters.AddWithValue("companyName", client.CompanyName);
                    cmd.Parameters.AddWithValue("address1", client.AddressOne);
                    cmd.Parameters.AddWithValue("address2", (object)client.AddressTwo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("city", (object)client.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("province", client.Province);
                    cmd.Parameters.AddWithValue("postalCode", (object)client.PostalCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("YTDSales", client.YTDSales);
                    cmd.Parameters.AddWithValue("creditHold", client.CreditHold);
                    cmd.Parameters.AddWithValue("notes", (object)client.Notes ?? DBNull.Value);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public static int DeleteClient(Client client)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"DELETE {clientTableName}
                                  WHERE ClientCode = @clientCode";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("clientCode", client.ClientCode);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
    }
}
