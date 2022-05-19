using Xunit;

namespace ReservationWebAPI.EndpointTests
{
    [CollectionDefinition("Client collection")]
    public class ClientCollection : ICollectionFixture<ClientFixture> { }
}
