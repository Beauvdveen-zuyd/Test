using Microsoft.Data.Sqlite;

internal class WaarnemingRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public WaarnemingRepository()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        CreateOpenConnection();
    }

    private SqliteConnection CreateOpenConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    public List<Waarneming> HaalAlleWaarnemingenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Waarneming>();
        string selectQuery = @"
            SELECT * FROM WAARNEMING;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int wid = reader.GetInt32(0);
            string omschrijving = reader.GetString(1);
            int sid = reader.GetInt32(2);
            string datum = reader.GetString(3);
            string tijd = reader.GetString(4);
            int wnid = reader.GetInt32(5);
            int lid = reader.GetInt32(6);
            string toelichting = reader.GetString(7);
            int aantal = reader.GetInt32(8);
            string geslacht = reader.GetString(9);
            string gebruiker = reader.GetString(10);
            string zekerheid = reader.GetString(11);
            int webid = reader.GetInt32(12);
            string manierDelen = reader.GetString(13);
            soorten.Add(new Waarneming
            (
                wid,
                omschrijving,
                sid,
                datum,
                tijd,
                wnid,
                lid,
                toelichting,
                aantal,
                geslacht,
                gebruiker,
                zekerheid,
                webid,
                manierDelen
            ));
        }

        return soorten;
    }
    public void VoegWaarnemingToe(Waarneming waarneming)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO WAARNEMING (Wid, Omschrijving, Sid, Datum, Tijd, WNid, Lid, Toelichting, Aantal, Geslacht, Gebruiker, Zekerheid, Webid, ManierDelen)
            VALUES (@Wid, @Omschrijving, @Sid, @Datum, @Tijd, @WNid, @Lid, @Toelichting, @Aantal, @Geslacht, @Gebruiker, @Zekerheid, @Webid, @ManierDelen);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Wid", waarneming.Wid);
        command.Parameters.AddWithValue("@Omschrijving", waarneming.Omschrijving);
        command.Parameters.AddWithValue("@Sid", waarneming.Sid);
        command.Parameters.AddWithValue("@Datum", waarneming.Datum);
        command.Parameters.AddWithValue("@Tijd", waarneming.Tijd);
        command.Parameters.AddWithValue("@WNid", waarneming.WNid);
        command.Parameters.AddWithValue("@Lid", waarneming.Lid);
        command.Parameters.AddWithValue("@Toelichting", waarneming.Toelichting);
        command.Parameters.AddWithValue("@Aantal", waarneming.Aantal);
        command.Parameters.AddWithValue("@Geslacht", waarneming.Geslacht);
        command.Parameters.AddWithValue("@Gebruiker", waarneming.Gebruiker);
        command.Parameters.AddWithValue("@Zekerheid", waarneming.Zekerheid);
        command.Parameters.AddWithValue("@Webid", waarneming.Webid);
        command.Parameters.AddWithValue("@ManierDelen", waarneming.ManierDelen);

        command.ExecuteNonQuery();
    }

    public void VerwijderWaarneming(String omschrijving)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM WAARNEMING
        WHERE Omschrijving = @Omschrijving;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Omschrijving", omschrijving);

        command.ExecuteNonQuery();
    }
}