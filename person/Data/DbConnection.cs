
#region Avant OMR

using Npgsql;
using System.Data;

namespace CoursSupDeVinci;

public class DbConnection
{
    private readonly NpgsqlConnection _connection;

    public DbConnection(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task init(Classe maClasse)
    {
        // ✅ Ouvre la connexion si elle n’est pas déjà ouverte
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();

        // Commence une transaction pour tout insérer ensemble
        await using var transaction = await _connection.BeginTransactionAsync();

        // --- Insert Classe ---
        var insertClasseCmd = new NpgsqlCommand(
            "INSERT INTO classe(name, level, school) VALUES (@name, @level, @school) RETURNING id",
            _connection, transaction);

        insertClasseCmd.Parameters.AddWithValue("name", maClasse.name);
        insertClasseCmd.Parameters.AddWithValue("level", maClasse.level);
        insertClasseCmd.Parameters.AddWithValue("school", maClasse.school);

// ✅ On récupère l'id généré par la base
        Guid idclasse = (Guid)await insertClasseCmd.ExecuteScalarAsync();

        // --- Insert Persons ---
        foreach (var person in maClasse.Eleves)
        {
            // Insert Person (sans fournir l'ID)
            var insertPersonCmd = new NpgsqlCommand(
                @"INSERT INTO person(firstname, lastname, birthdate, size, idclasse) 
                  VALUES (@firstname, @lastname, @birthdate, @size, @idclasse)
                  RETURNING id",
                _connection, transaction);
            insertPersonCmd.Parameters.AddWithValue("firstname", person.Firstname);
            insertPersonCmd.Parameters.AddWithValue("lastname", person.Lastname);
            insertPersonCmd.Parameters.AddWithValue("birthdate", person.Birthdate);
            insertPersonCmd.Parameters.AddWithValue("size", person.Taille);
            insertPersonCmd.Parameters.AddWithValue("idclasse", idclasse);

            // Récupère l'ID généré pour la personne
            Guid idperson = (Guid)await insertPersonCmd.ExecuteScalarAsync();
            // --- Insert Details ---
            var insertDetailCmd = new NpgsqlCommand(
                "INSERT INTO details(street, city, zipCode) VALUES (@street, @city, @zipCode) RETURNING id",
                _connection, transaction);
            insertDetailCmd.Parameters.AddWithValue("street", person.AdressDetails.Street);
            insertDetailCmd.Parameters.AddWithValue("city", person.AdressDetails.City);
            insertDetailCmd.Parameters.AddWithValue("zipCode", person.AdressDetails.ZipCode);

            Guid iddetails = (Guid)await insertDetailCmd.ExecuteScalarAsync();

            var insertPersonDetailCmd = new NpgsqlCommand(
                "INSERT INTO person_detail(idperson, iddetails) VALUES (@idperson, @iddetails)",
                _connection, transaction);
            insertPersonDetailCmd.Parameters.AddWithValue("idperson", idperson);
            insertPersonDetailCmd.Parameters.AddWithValue("iddetails", iddetails);
            await insertPersonDetailCmd.ExecuteNonQueryAsync();
        }

        // ✅ Valide la transaction
        await transaction.CommitAsync();

        Console.WriteLine("Insertion hiérarchique réussie ✅");

        // (Optionnel) Ferme proprement la connexion
        await _connection.CloseAsync();
    }
}

#endregion