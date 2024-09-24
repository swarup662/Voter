﻿using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Voter_API.Utility
{
    public class DbAccess
    {
        private readonly string _connectionString; //= "Server=192.168.2.112\\SQLEXPRESS,1430;Initial Catalog=DB_PAYROLL_TEST;User ID=sa;Password=infoage@123;Trusted_Connection=False;TrustServerCertificate=True;";
                                                   //  private readonly string _connectionString = "Server=49.249.100.138;Initial Catalog=DB_PAYROLL_TC;User ID=T€ch;Password=#termInet0r-2324;Trusted_Connection=False;TrustServerCertificate=True;";
        public static byte[] pImage;

        public DbAccess(IConfiguration configuration)
        {
            // Read the connection string from the configuration file
            _connectionString = configuration.GetConnectionString("db1");

        }

        public DataTable GetDataTable(SqlCommand query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                query.Connection = connection;
                query.CommandType = CommandType.StoredProcedure;
                using (query)
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query))
                    {
                        adapter.Fill(dataTable);
                    }
                    connection.Close();
                }
            }

            return dataTable;
        }
        public int int_Process(string query, string[] parametername, string[] parametervalue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    for (int i = 0; i < parametername.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(parametername[i], parametervalue[i]);
                    }

                    con.Open();
                    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    cmd.ExecuteNonQuery();
                    int id = (int)returnParameter.Value;
                    return id;
                }
            }
        }
        //public int ExecuteNonQuery(string query, string[] parametername, string[] parametervalue)
        //{
        //    //int rowsAffected = 0;
        //    SqlConnection con = new SqlConnection(_connectionString);
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.CommandTimeout = 0;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    for (int i = 0; i < parametername.Length; i++)
        //    {
        //        if (parametername[i] == "@photo")
        //        {
        //            cmd.Parameters.AddWithValue(parametername[i], pImage);
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue(parametername[i], parametervalue[i]);
        //        }
        //    }
        //    con.Open();
        //    SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
        //    returnParameter.Direction = ParameterDirection.ReturnValue;

        //    cmd.ExecuteNonQuery();
        //    int id = (int)returnParameter.Value;
        //    con.Dispose();
        //    return id;


        //    //using (SqlConnection connection = new SqlConnection(_connectionString))
        //    //{

        //    //    query.Connection = connection;
        //    //    query.CommandType = CommandType.StoredProcedure;
        //    //    using (query)
        //    //    {
        //    //        connection.Open();
        //    //        rowsAffected = query.ExecuteNonQuery();
        //    //        connection.Close();
        //    //    }
        //    //}

        //    //return rowsAffected;
        //}
        public DataSet Ds_Process(String Storp, string[] parametername, string[] parametervalue)
        {

            try
            {

                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand(Storp, con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parametername.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parametername[i], parametervalue[i]);
                }
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                con.Dispose();
                return ds;
            }
            catch (Exception ex)
            {

                DataSet ds = null;
                return ds;
            }

        }
        public void SendEmail(string recipientEmail, string subject, string body)
        {
            string senderEmail = "technoconsalary@gmail.com"; // Replace with the sender email address
            string senderPassword = "technoconsalary123"; // Replace with the sender email password

            using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465)) // Replace with the SMTP server details
                {
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
