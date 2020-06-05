using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection connection;

        public EventStoreService(IConfiguration configuration)
        {
            connection = EventStoreConnection.Create(configuration.GetConnectionString("EventStoreConnection"));

            connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return connection;
        }
    }
}
