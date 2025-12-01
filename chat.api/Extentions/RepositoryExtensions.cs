using Foundation.Business;
using Foundation.Business.Repositories.Abtractions;
using Foundation.Business.Repositories.Implements;
using Microsoft.EntityFrameworkCore;
using Foundation.Business.Repositories;
namespace chat.api.Extentions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string mongoConnectionString = configuration.GetConnectionString("MongoConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(
                        connectionString,
                        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "auth")
                    );
            });
            services.AddDbContext<MongoDbContext>(options =>
            {
                options.UseMongoDB(mongoConnectionString, "chat_app");
            });
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IUserConversationRepository, UserConversationRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }
    }
}
