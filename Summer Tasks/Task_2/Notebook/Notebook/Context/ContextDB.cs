using Microsoft.EntityFrameworkCore;

namespace Notebook.Context
{
    class ContextDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Create connection to MySQL DataBase (they must coincide with real data)
            optionsBuilder.UseMySQL("server=127.0.0.1;user id=root;password=;persistsecurityinfo=True;port=3306;database=NotebookDB;SslMode=none");
        }

        // Set collection of all entities int the context
        public DbSet<Models.User> Users { get; set; }
    }
}
