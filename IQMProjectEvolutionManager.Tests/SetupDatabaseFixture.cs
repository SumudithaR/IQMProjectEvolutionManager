using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using NUnit.Framework;

[SetUpFixture]
public class SetupDatabaseFixture
{
    [SetUp]
    public void SetupAssembly()
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SetupConnection"].ConnectionString))
        {
            connection.Open();
            using (FileStream fs = File.OpenRead("Resources/Setup.sql"))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.Peek() >= 0)
                    {
                        try
                        {
                            using (SqlCommand cmd = connection.CreateCommand())
                            {
                                // fix the database name
                                string sql = sr.ReadToEnd().Replace("$DATABASE_NAME$", "ProjectName");
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (SqlException exception)
                        {
                            // ok something went wrong so lets dump the exeption and carry on
                            Console.WriteLine(exception.Message);
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }
}

