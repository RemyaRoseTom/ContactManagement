using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ContactManager.Models
{
    public class ContactManagerRepository : IContactManagerRepository
    {
        string connection = ConfigurationManager.ConnectionStrings["ContactManagerDB"].ConnectionString;
       
       
        public Contact CreateContact(Contact contactToCreate)
        {
            String query = "INSERT INTO dbo.Contacts (FirstName,LastName,Phone,Email) VALUES (@firstName,@lastName,@phone, @email)";
            query += "SELECT SCOPE_IDENTITY()";
           
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@firstName", contactToCreate.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", contactToCreate.LastName);
                    cmd.Parameters.AddWithValue("@phone", contactToCreate.Phone);
                    cmd.Parameters.AddWithValue("@email", contactToCreate.Email);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                        return null;
                    }
                    else

                        return contactToCreate;
                }
            }
        }

        public void DeleteContact(Contact contactToDelete)
        {
            string query = "DELETE FROM dbo.Contacts where Id=" + contactToDelete.Id;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error deleting data from Database!");
                    }
                }
            }
        }

        public Contact EditContact(Contact contactToUpdate)
        {
          
            string query = "UPDATE dbo.Contacts  SET FirstName=@firstName, LastName=@lastName, Phone=@phone, Email=@email where Id="+contactToUpdate.Id;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    try 
                    {
                        cmd.Parameters.AddWithValue("@firstName", contactToUpdate.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", contactToUpdate.LastName);
                        cmd.Parameters.AddWithValue("@phone", contactToUpdate.Phone);
                        cmd.Parameters.AddWithValue("@email", contactToUpdate.Email);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("Invalid Operation Exception occured while updating details of Contact Id : {0} : Exception Details :{1}", contactToUpdate.Id,ex.Message);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Operation Exception occured while updating details of Contact Id : {0} : Exception Details :{1}", contactToUpdate.Id, ex.Message);
                    }

                }
            }
            return contactToUpdate;
      
        }

        public Contact GetContact(int id)
        {
          
            string query = "SELECT * FROM dbo.Contacts where Id="+id;

            DataTable dtlContact = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);
                    sqlDataAdapter.Fill(dtlContact);
                }
            }
            catch (InvalidOperationException ex) 
            { 
                Console.WriteLine("Invalid Operation Exception occured while retrieving details of Contact Id : {0} : Exception Details :{1}", id, ex.Message);
            }
            
            Contact contactToUpdate = new Contact();
            if (dtlContact.Rows.Count > 0)
            {
              foreach (DataRow row in dtlContact.Rows)
                {
                   
                    contactToUpdate.FirstName = row["FirstName"].ToString();
                    contactToUpdate.LastName = row["LastName"].ToString();
                    contactToUpdate.Phone = row["Phone"].ToString();
                    contactToUpdate.Email = row["Email"].ToString();
                }
            }
            return contactToUpdate;
           
        }

        public DataTable ListContacts()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Contacts", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception occured while retrieving contacts from Dbo.Contacts: Exception Details :{0}",ex.Message);
            }

            return dt;

        }

    }
}