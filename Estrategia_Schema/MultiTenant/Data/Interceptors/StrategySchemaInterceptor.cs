using Microsoft.EntityFrameworkCore.Diagnostics;
using MultiTenant.Provider;
using System.Data.Common;

namespace MultiTenant.Data.Interceptors
{
    public class StrategySchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantData _tenantData;

        public StrategySchemaInterceptor(TenantData tenantData)
        {
            _tenantData = tenantData;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            ReplaceSchema(command);

            return base.ReaderExecuting(command, eventData, result);
        }
        
        private void ReplaceSchema(DbCommand command)
        {
            //Aplicando o REGEX fica mais rápido
            command.CommandText = command.CommandText.Replace("FROM", $" FROM [{_tenantData.TenantId}].").Replace("JOIN", $" JOIN [{_tenantData.TenantId}].");
        }
    }
}
