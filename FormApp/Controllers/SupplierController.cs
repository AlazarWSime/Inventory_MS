using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class SupplierController
    {
        // ================== GET ALL SUPPLIERS ==================
        public List<SupplierModel> GetAllSuppliers()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            string query = @"
                SELECT Supplier_ID, Supplier_Name, Contact_Email, 
                       Contact_Phone, Address 
                FROM Supplier 
                ORDER BY Supplier_Name";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    suppliers.Add(new SupplierModel
                    {
                        Supplier_ID = Guid.Parse(row["Supplier_ID"].ToString()),
                        Supplier_Name = row["Supplier_Name"].ToString(),
                        Contact_Email = row["Contact_Email"]?.ToString(),
                        Contact_Phone = row["Contact_Phone"]?.ToString(),
                        Address = row["Address"]?.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return suppliers;
        }

        // ================== CREATE SUPPLIER ==================
        public bool CreateSupplier(SupplierModel supplier)
        {
            try
            {
                string query = @"
                    INSERT INTO Supplier 
                        (Supplier_ID, Supplier_Name, Contact_Email, Contact_Phone, Address)
                    VALUES 
                        (NEWID(), @Supplier_Name, @Contact_Email, @Contact_Phone, @Address)";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Supplier_Name", supplier.Supplier_Name),
                    new SqlParameter("@Contact_Email", supplier.Contact_Email ?? (object)DBNull.Value),
                    new SqlParameter("@Contact_Phone", supplier.Contact_Phone ?? (object)DBNull.Value),
                    new SqlParameter("@Address", supplier.Address ?? (object)DBNull.Value)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating supplier: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ================== UPDATE SUPPLIER ==================
        public bool UpdateSupplier(SupplierModel supplier)
        {
            try
            {
                string query = @"
                    UPDATE Supplier 
                    SET Supplier_Name = @Supplier_Name,
                        Contact_Email = @Contact_Email,
                        Contact_Phone = @Contact_Phone,
                        Address = @Address
                    WHERE Supplier_ID = @Supplier_ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Supplier_ID", supplier.Supplier_ID),
                    new SqlParameter("@Supplier_Name", supplier.Supplier_Name),
                    new SqlParameter("@Contact_Email", supplier.Contact_Email ?? (object)DBNull.Value),
                    new SqlParameter("@Contact_Phone", supplier.Contact_Phone ?? (object)DBNull.Value),
                    new SqlParameter("@Address", supplier.Address ?? (object)DBNull.Value)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating supplier: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ================== DELETE SUPPLIER ==================
        public bool DeleteSupplier(Guid supplierId)
        {
            try
            {
                string query = "DELETE FROM Supplier WHERE Supplier_ID = @Supplier_ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Supplier_ID", supplierId)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting supplier: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}