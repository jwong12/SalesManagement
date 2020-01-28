using BusinessLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib.Data
{
    public class ProvinceRepository
    {
        private static readonly string connString = @"Server=tcp:skeena.database.windows.net,1433; 
                                                      Initial Catalog=comp2614;
                                                      User ID=student;
                                                      Password=p8SmM5/mKZk=;
                                                      Encrypt=True;
                                                      TrustServerCertificate=False;
                                                      Connection Timeout=30;";

        private const string provinceTableName = "Province";

        public static ProvinceCollection GetProvinces()
        {
            ProvinceCollection provinces;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"SELECT ProvinceId, Sort, Abbreviation, Name                                
                                FROM {provinceTableName}
                                ORDER BY Abbreviation";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    conn.Open();

                    provinces = new ProvinceCollection();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int provinceId;         
                        int sort;               
                        string abbreviation;    
                        string name = null;            

                        while (reader.Read())
                        {
                            provinceId = (int) reader["ProvinceId"];
                            sort = (int) reader["Sort"];
                            abbreviation = reader["Abbreviation"] as string;

                            if (!reader.IsDBNull(3))
                            {
                                name = reader["Name"] as string;
                            }

                            provinces.Add(new Province
                            {
                                ProvinceId = provinceId,
                                Sort = sort,
                                Abbreviation = abbreviation,
                                Name = name                               
                            });

                            name = null;
                        }
                    }

                    return provinces;
                }
            }
        }
    }
}
