using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MSSQLDockerSample
{
    internal class Storage
    {
        private readonly string connectionString;

        public Storage(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        internal async Task SaveAsync(string text)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new SqlCommand("INSERT INTO tabRecord VALUES(@Text)", connection))
                {
                    cmd.Parameters.AddWithValue("Text", text);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        internal async Task<IEnumerable<Record>> ListAsync()
        {
            var records = new List<Record>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new SqlCommand("SELECT Id, Text FROM tabRecord", connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var id = reader.GetInt32(0);
                        var text = reader.GetString(1);
                        records.Add(new Record(id, text));
                    }
                }
            }

            return records;
        }
    }
}
