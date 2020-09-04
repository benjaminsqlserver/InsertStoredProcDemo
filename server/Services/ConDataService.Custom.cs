using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InsertStoredProcDemo
{
    public partial class ConDataService
    {

        public async Task<int> MuyikAddNewMarket(string MarketLocation, string MarketName, decimal? MarketSizeInHectares)
        {
           

            SqlParameter[] @params =
            {
              new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
              new SqlParameter("@MarketLocation", SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = MarketLocation},
              new SqlParameter("@MarketName", SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = MarketName},
              new SqlParameter("@MarketSizeInHectares", SqlDbType.Decimal) {Direction = ParameterDirection.Input, Value = MarketSizeInHectares},
          };
            context.Database.ExecuteSqlRaw("EXEC @returnVal=[dbo].[MuyikInsertNewMarket] @MarketLocation, @MarketName, @MarketSizeInHectares", @params);

            int result = Convert.ToInt32(@params[0].Value);

          

            return await Task.FromResult(result);
        }

        public async Task MuyikAddMarketADONet(string connectionString,string MarketLocation, string MarketName, decimal? MarketSizeInHectares)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                var command = new SqlCommand();
                command.CommandText = "dbo.MuyikInsertNewMarket";
                command.CommandTimeout = 1000000;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.AddWithValue("@MarketLocation", MarketLocation);
                command.Parameters.AddWithValue("@MarketName", MarketName);
                command.Parameters.AddWithValue("@MarketSizeInHectares", MarketSizeInHectares);

                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();

            }
            catch(Exception)
            {

            }
            finally
            {
               await connection.CloseAsync();
            }

        }
    }
}
