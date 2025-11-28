using ExoCommande.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Dao
{
    internal class CommandeDao : BaseDao<Classes.Commande>
    {
        private readonly ClientDao clientDao = new ClientDao();

        // GET ALL
        public override List<Commande> GetAll()
        {
            List<Commande> commandes = new List<Commande>();
            request = @"SELECT id, client_id, date_commande, total, createdAt, updatedAt
                        FROM Commande";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                ClientDao clientDao = new ClientDao();

                while (reader.Read())
                {
                    Client client = clientDao.GetOneById(reader.GetInt32(1))!;
                    commandes.Add(new Commande(
                        reader.GetInt32(0),
                        client,
                        reader.GetDateTime(2),
                        reader.GetDecimal(3),
                        reader.GetDateTime(4),
                        reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetAll Commande : " + ex.Message);
            }

            return commandes;
        }

        // GET BY ID
        public override Commande? GetOneById(int id)
        {
            request = @"SELECT id, client_id, date_commande, total, created_at, updated_at
                        FROM Commande
                        WHERE id = @id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                ClientDao clientDao = new ClientDao();

                if (reader.Read())
                {
                    Client client = clientDao.GetOneById(reader.GetInt32(1))!;
                    return new Commande(
                        reader.GetInt32(0),
                        client,
                        reader.GetDateTime(2),
                        reader.GetDecimal(3),
                        reader.GetDateTime(4),
                        reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans getOneById Commande : " + ex.Message);
            }

            return null;
        }

        // INSERT
        public override Commande Save(Commande entity)
        {
            request = @"INSERT INTO Commande (client_id, date_commande, total, created_at)
                        OUTPUT INSERTED.id
                        VALUES (@client_id, @date_commande, @total, SYSDATETIME())";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@client_id", entity.Client.Id);
                command.Parameters.AddWithValue("@date_commande", entity.DateCommande);
                command.Parameters.AddWithValue("@total", entity.Total);

                connection.Open();
                entity.Id = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Save Commande : " + ex.Message);
            }

            return entity;
        }

        // UPDATE
        public override Commande Update(Commande entity)
        {
            request = @"UPDATE Commande
                        SET client_id=@client_id,
                            date_commande=@date_commande,
                            total=@total,
                            updated_at=SYSDATETIME()
                        WHERE id=@id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@client_id", entity.Client.Id);
                command.Parameters.AddWithValue("@date_commande", entity.DateCommande);
                command.Parameters.AddWithValue("@total", entity.Total);
                command.Parameters.AddWithValue("@id", entity.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Update Commande : " + ex.Message);
            }

            return entity;
        }

        // DELETE
        public override bool Delete(int id)
        {
            request = @"DELETE FROM Commande WHERE id=@id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Delete Commande : " + ex.Message);
                return false;
            }
        }

        public void DeleteAllCommandsOfAClient(Client client)
        {
            request = "DELETE FROM Commande WHERE client_id=@client_id";

            using SqlConnection connection = DataConnection.GetConnection;
            using SqlCommand command = new SqlCommand(request, connection);

            command.Parameters.AddWithValue("@client_id", client.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public Commande AddCommandeToClient(Client client, decimal total)
        {
            Commande commande = new Commande(client, total);
            return Save(commande);
        }

        public List<Commande> GetCommandesByClientId(int clientId)
        {
            List<Commande> commandes = new List<Commande>();
            request = @"SELECT id, client_id, date_commande, total, created_at, updated_at
                FROM Commande
                WHERE client_id = @clientId";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                command.Parameters.AddWithValue("@clientId", clientId);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client? client = clientDao.GetOneById(reader.GetInt32(1));
                    if (client == null) continue; // sécurité

                    commandes.Add(new Commande(
                        reader.GetInt32(0),
                        client,
                        reader.GetDateTime(2),
                        reader.GetDecimal(3),
                        reader.GetDateTime(4),
                        reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetCommandesByClientId : " + ex.Message);
            }

            return commandes;
        }

    }
}
