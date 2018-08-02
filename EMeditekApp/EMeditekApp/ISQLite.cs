using SQLite;

namespace EMeditekApp
{
    public interface ISQLite
    {
       SQLiteConnection GetConnection();
        string SavePdfFile(byte[] imageByte);
    }
}
