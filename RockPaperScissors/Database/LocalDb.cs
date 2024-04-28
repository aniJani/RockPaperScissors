using RockPaperScissors.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Database
{
    public class LocalDB
    {
        private const string DB_NAME = "RockPaper.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDB()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Statistics>().Wait();
        }

        public Task<Statistics> GetStatisticsAsync()
        {
            // Assuming there's only one statistics record.
            return _connection.Table<Statistics>().FirstOrDefaultAsync();
        }

        public Task UpdateStatisticsAsync(Statistics stats)
        {
            // If stats does not exist, insert a new one. Otherwise, update the existing one.
            if (stats.Id != 0)
                return _connection.UpdateAsync(stats);
            else
                return _connection.InsertAsync(stats);
        }
    }
}

namespace RockPaperScissors.Model
{
    [Table("Statistics")]
    public class Statistics
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("TotalGamesPlayed")]
        public int TotalGamesPlayed { get; set; }

        [Column("PlayerWins")]
        public int PlayerWins { get; set; }

        [Column("AIWins")]
        public int AIWins { get; set; }

        [Column("PlayerChoseRock")]
        public int PlayerChoseRock { get; set; }

        [Column("PlayerChosePaper")]
        public int PlayerChosePaper { get; set; }

        [Column("PlayerChoseScissors")]
        public int PlayerChoseScissors { get; set; }
    }
}