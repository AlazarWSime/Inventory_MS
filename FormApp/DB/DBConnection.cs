using FormApp.Properties;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;

namespace FormApp.DB
{
    static class DBConnection
    {
       
        static string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;
              Initial Catalog=invetory_MS;
              Integrated Security=True;
              Encrypt=False;
              TrustServerCertificate=True";

        
        public static SqlConnection _Connection = new SqlConnection(_connectionString);

        public static void Initiate()
        {
           
            try 
            {
                if (_Connection.State == ConnectionState.Open)
                    _Connection.Close();

                _Connection.Open();
                //MessageBox.Show("Connected successuflly! ");
            }
            
            catch(Exception ex) 
            {
             MessageBox.Show
                    (
                       $"Unable to connect to database\n {ex.Message}",
                       "DB ERROR",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error
                    ); 
            }
          
        }
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {

            SqlCommand sqlCommand = null;
            
            try
            {
                if (_Connection.State != ConnectionState.Open)
                    Initiate();

                sqlCommand = new SqlCommand(query, _Connection);
              
                if (parameters != null)                          // Step 3 - null check
                {
                    sqlCommand.Parameters.AddRange(parameters);  // Adds all the parameters from your array into the command at once.
                }

                int rowAffected = sqlCommand.ExecuteNonQuery();

                //_Connection.Close();
                //MessageBox.Show("Saved to database! ");
                return rowAffected;
                
            }
            catch (Exception ex) 
            {
                
                MessageBox.Show($"Query failed: {ex.Message}", "DB ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return 0;
            }

        }


        public static SqlDataReader ExecuteReader(string query, SqlParameter[] parameters=null) 
        {
            try
            {
                if (_Connection.State != ConnectionState.Open)
                    Initiate();

                SqlCommand sqlCommand = new SqlCommand(query, _Connection);

                if (parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters);
                }

                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                _Connection.Close();
                MessageBox.Show($"Query failed: {ex.Message}", "DB ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (_Connection.State != ConnectionState.Open)
                    Initiate();

                using (SqlCommand sqlCommand = new SqlCommand(query, _Connection))
                {
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(dataTable);//Select
                    }
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Query failed: {ex.Message}", "DB ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return dataTable; // returns empty table
            }
        }

    }
}
