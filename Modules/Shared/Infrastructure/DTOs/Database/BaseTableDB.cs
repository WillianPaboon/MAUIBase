using SQLite;

namespace Infrastructure.DTOs.Database
{
    internal class BaseTableDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
