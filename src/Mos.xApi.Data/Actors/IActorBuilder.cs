using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Actors
{
    public interface IActorBuilder<T> where T : Actor
    {
        T WithMailBox(string emailAddress);

        T WithHashedMailBox(string hashedEmailAddress);

        T WithHashedMailBoxFromEmail(string emailAddress);

        T WithOpenId(Uri openIdUri);

        T WithOpenId(string openIdUri);

        T WithAccount(string name, Uri homePage);

        T WithAccount(string name, string homePage);
    }
}
