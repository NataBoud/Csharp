using ExoLibrary.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Dao
{
    internal class BookDao : IBaseDao<Book>
    {
        private string request = "";

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            List<Book> books = [];

            request = @"SELECT Id, Title, Author, ISBN, PublicationYear, IsAvailable, CreatedAt, UpdatedAt
                        FROM Book";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetBoolean(5),
                        reader.GetDateTime(6),
                        reader.IsDBNull(7) ? null : reader.GetDateTime(7)
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetAll : " + ex.Message);
            }

            return books;
        }

        public Book? GetOneById(int id)
        {
            request = @"SELECT Id, Title, Author, ISBN, PublicationYear, IsAvailable, CreatedAt, UpdatedAt
                        FROM Book
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
                    return new Book(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetBoolean(5),
                        reader.GetDateTime(6),
                        reader.IsDBNull(7) ? null : reader.GetDateTime(7)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetOneById : " + ex.Message);
            }

            return null;
        }

        public Book Save(Book entity)
        {
            request = @"INSERT INTO Book (Title, Author, ISBN, PublicationYear, IsAvailable, CreatedAt)
                        OUTPUT INSERTED.Id
                        VALUES (@Title, @Author, @ISBN, @PublicationYear, @IsAvailable, SYSDATETIME())";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Author", entity.Author);
                command.Parameters.AddWithValue("@ISBN", entity.ISBN);
                command.Parameters.AddWithValue("@PublicationYear", entity.PublicationYear);
                command.Parameters.AddWithValue("@IsAvailable", entity.IsAvailable);

                connection.Open();
                entity.Id = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Save : " + ex.Message);
            }

            return entity;
        }

        public Book Update(Book entity)
        {
            request = @"UPDATE Book
                        SET Title = @Title,
                            Author = @Author,
                            ISBN = @ISBN,
                            PublicationYear = @PublicationYear,
                            IsAvailable = @IsAvailable,
                            UpdatedAt = SYSDATETIME()
                        WHERE Id = @Id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Author", entity.Author);
                command.Parameters.AddWithValue("@ISBN", entity.ISBN);
                command.Parameters.AddWithValue("@PublicationYear", entity.PublicationYear);
                command.Parameters.AddWithValue("@IsAvailable", entity.IsAvailable);
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
    }
}
