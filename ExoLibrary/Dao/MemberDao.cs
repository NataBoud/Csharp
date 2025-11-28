using ExoLibrary.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Dao
{
    internal class MemberDao : IBaseDao<Member>
    {
        private string request = "";

        public List<Member> GetAll()
        {
            List<Member> members = new List<Member>();

            request = @"SELECT Id, LastName, FirstName, Email, RegistrationDate, CreatedAt, UpdatedAt
                        FROM Member";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    members.Add(new Member(
                        reader.GetInt32(0),                  // Id
                        reader.GetString(1),                 // LastName
                        reader.GetString(2),                 // FirstName
                        reader.GetString(3),                 // Email
                        reader.GetDateTime(4),               // RegistrationDate
                        reader.GetDateTime(5),               // CreatedAt
                        reader.IsDBNull(6) ? null : reader.GetDateTime(6) // UpdatedAt
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetAll : " + ex.Message);
            }

            return members;
        }

        public Member? GetOneById(int id)
        {
            request = @"SELECT Id, LastName, FirstName, Email, RegistrationDate, CreatedAt, UpdatedAt
                        FROM Member
                        WHERE Id = @Id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Member(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDateTime(4),
                        reader.GetDateTime(5),
                        reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetOneById : " + ex.Message);
            }

            return null;
        }

        public Member Save(Member entity)
        {
            request = @"INSERT INTO Member (LastName, FirstName, Email, RegistrationDate, CreatedAt)
                        OUTPUT INSERTED.Id
                        VALUES (@LastName, @FirstName, @Email, @RegistrationDate, SYSDATETIME())";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);

                connection.Open();
                entity.Id = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Save : " + ex.Message);
            }

            return entity;
        }

        public Member Update(Member entity)
        {
            request = @"UPDATE Member
                        SET LastName = @LastName,
                            FirstName = @FirstName,
                            Email = @Email,
                            RegistrationDate = @RegistrationDate,
                            UpdatedAt = SYSDATETIME()
                        WHERE Id = @Id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);
                command.Parameters.AddWithValue("@Id", entity.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Update : " + ex.Message);
            }

            return entity;
        }

        public bool Delete(int id)
        {
            Member? member = GetOneById(id);
            if (member == null)
                return false;

            request = "DELETE FROM Member WHERE Id = @Id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                command.Parameters.AddWithValue("@Id", member.Id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Delete : " + ex.Message);
                return false;
            }
        }
    }
}
