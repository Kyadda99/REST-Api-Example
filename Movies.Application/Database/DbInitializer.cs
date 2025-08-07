using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbInitializer(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            await connection.ExecuteAsync(
                """
                create table if not exist in movies(
                id UUID primary key,
                slug TEXT not null,
                title TEXT not null,
                yearOfRelese intiger not null);
                """);

            await connection.ExecuteAsync(
                """
                create unique index concurrently if not exists movies_slug_idx
                on movies
                using btree(slug)
                """);
        }
    }
}
