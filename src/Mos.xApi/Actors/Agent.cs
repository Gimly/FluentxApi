using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;

namespace Mos.xApi.Actors
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Agent : Actor
    {
        public Agent(IInverseFunctionalIdentifier identifier, string name = null) : base(identifier, name)
        {
        }

        public override string ObjectType => nameof(Agent);
    }
}