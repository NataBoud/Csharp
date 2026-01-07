using Microsoft.EntityFrameworkCore;

namespace CaisseEnregistreuse.Services.Helpers
{
    public static class DbExceptionHelper
    {
        public static void HandleUniqueConstraint(DbUpdateException ex, string entityName, string nom)
        {
            if (ex.InnerException?.Message.Contains("Duplicate") == true)
            {
                throw new InvalidOperationException(
                    $"{entityName} avec le nom '{nom}' existe déjà."
                );
            }

            throw ex;
        }
    }

}
