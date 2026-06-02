using Microsoft.Extensions.Configuration;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using Npgsql;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    internal class TenantDatabaseManager : ITenantDatabaseManager
    {
        private readonly string _masterConnection;

        public TenantDatabaseManager()
        {
            _masterConnection = Helper.DatabaseConnectionString;
        }

        public async Task<bool> DatabaseExistsAsync(string tenantId)
        {
            await using var conn = new NpgsqlConnection(_masterConnection);
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT 1 FROM pg_database WHERE datname = @db";
            cmd.Parameters.AddWithValue("db", tenantId);

            var result = await cmd.ExecuteScalarAsync();
            return result != null;
        }

        public async Task<string> CreateDatabaseAsync(string tenantId)
        {
            await using var conn = new NpgsqlConnection(_masterConnection);
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = $"CREATE DATABASE \"{tenantId}\"";

            await cmd.ExecuteNonQueryAsync();

            return GetConnectionStringAsync(tenantId);
        }
        private string GetConnectionStringAsync(string tenantId)
        {
            var builder = new NpgsqlConnectionStringBuilder(_masterConnection)
            {
                Database = tenantId
            };

            return builder.ConnectionString;
        }   

        public async Task DropDatabaseAsync(string tenantId)
        {
            await using var conn = new NpgsqlConnection(_masterConnection);
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT pg_terminate_backend(pid)
                FROM pg_stat_activity
                WHERE datname = @db AND pid <> pg_backend_pid();
            ";
            cmd.Parameters.AddWithValue("db", tenantId);
            await cmd.ExecuteNonQueryAsync();

            cmd.Parameters.Clear();
            cmd.CommandText = $"DROP DATABASE IF EXISTS \"{tenantId}\"";

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
