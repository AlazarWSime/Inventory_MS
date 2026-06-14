using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class LocationController
    {
        // ================== GET ALL ZONES ==================
        public List<LocationModel> GetAllZones()
        {
            List<LocationModel> zones = new List<LocationModel>();

            string query = "SELECT Zone_ID, Zone_Name, Description FROM Location ORDER BY Zone_Name";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    zones.Add(new LocationModel
                    {
                        Zone_ID = Guid.Parse(row["Zone_ID"].ToString()),
                        Zone_Name = row["Zone_Name"].ToString(),
                        Description = row["Description"]?.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading zones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return zones;
        }

        // ================== CREATE ZONE ==================
        public bool CreateZone(LocationModel zone)
        {
            try
            {
                string query = @"
                    INSERT INTO Location (Zone_ID, Zone_Name, Description)
                    VALUES (NEWID(), @Zone_Name, @Description)";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Zone_Name", zone.Zone_Name),
                    new SqlParameter("@Description", zone.Description ?? (object)DBNull.Value)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating zone: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ================== UPDATE ZONE ==================
        public bool UpdateZone(LocationModel zone)
        {
            try
            {
                string query = @"
                    UPDATE Location 
                    SET Zone_Name = @Zone_Name, Description = @Description
                    WHERE Zone_ID = @Zone_ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Zone_ID", zone.Zone_ID),
                    new SqlParameter("@Zone_Name", zone.Zone_Name),
                    new SqlParameter("@Description", zone.Description ?? (object)DBNull.Value)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating zone: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ================== DELETE ZONE ==================
        public bool DeleteZone(Guid zoneId)
        {
            try
            {
                string query = "DELETE FROM Location WHERE Zone_ID = @Zone_ID";

                SqlParameter[] parameters = { new SqlParameter("@Zone_ID", zoneId) };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting zone: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}