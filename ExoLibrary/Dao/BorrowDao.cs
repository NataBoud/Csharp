using System;
using System.Collections.Generic;
using System.Text;
using ExoLibrary.Classes;
using Microsoft.Data.SqlClient;

namespace ExoLibrary.Dao
{
    internal class BorrowDao : IBaseDao<Borrow>
    {
        private string request = "";

        public List<Borrow> GetAll()
        {
            List<Borrow> borrows = new List<Borrow>();

            request = @"SELECT Id, BookId, MemberId, BorrowDate, ReturnDate, CreatedAt, UpdatedAt
                        FROM Borrow";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    borrows.Add(new Borrow(
                        reader.GetInt32(0),                     // Id
                        reader.GetInt32(1),                     // BookId
                        reader.GetInt32(2),                     // MemberId
                        reader.GetDateTime(3),                  // BorrowDate
                        reader.IsDBNull(4) ? null : reader.GetDateTime(4),  // ReturnDate nullable
                        reader.GetDateTime(5),                  // CreatedAt
                        reader.IsDBNull(6) ? null : reader.GetDateTime(6)   // UpdatedAt nullable
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetAll : " + ex.Message);
            }

            return borrows;
        }

        public Borrow? GetOneById(int id)
        {
            request = @"SELECT Id, BookId, MemberId, BorrowDate, ReturnDate, CreatedAt, UpdatedAt
                        FROM Borrow
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
                    return new Borrow(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetDateTime(3),
                        reader.IsDBNull(4) ? null : reader.GetDateTime(4),
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

        public Borrow Save(Borrow entity)
        {
            request = @"INSERT INTO Borrow (BookId, MemberId, BorrowDate, ReturnDate, CreatedAt)
                        OUTPUT INSERTED.Id
                        VALUES (@BookId, @MemberId, @BorrowDate, @ReturnDate, SYSDATETIME())";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@BookId", entity.BookId);
                command.Parameters.AddWithValue("@MemberId", entity.MemberId);
                command.Parameters.AddWithValue("@BorrowDate", entity.BorrowDate);
                command.Parameters.AddWithValue("@ReturnDate", (object?)entity.ReturnDate ?? DBNull.Value);

                connection.Open();
                entity.Id = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Save : " + ex.Message);
            }

            return entity;
        }

        public Borrow Update(Borrow entity)
        {
            request = @"UPDATE Borrow
                        SET BookId = @BookId,
                            MemberId = @MemberId,
                            BorrowDate = @BorrowDate,
                            ReturnDate = @ReturnDate,
                            UpdatedAt = SYSDATETIME()
                        WHERE Id = @Id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@BookId", entity.BookId);
                command.Parameters.AddWithValue("@MemberId", entity.MemberId);
                command.Parameters.AddWithValue("@BorrowDate", entity.BorrowDate);
                command.Parameters.AddWithValue("@ReturnDate", (object?)entity.ReturnDate ?? DBNull.Value);
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
            Borrow? borrow = GetOneById(id);
            if (borrow == null)
                return false;

            request = "DELETE FROM Borrow WHERE Id = @Id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                command.Parameters.AddWithValue("@Id", borrow.Id);

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
