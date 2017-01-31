using System;

namespace Mos.xApi.Data.Objects
{
    public interface IActivityBuilder : IContainsExtension<IActivityBuilder>
    {
        IActivityBuilder AddDescription(string languageCode, string content);

        IActivityBuilder AddDescription(ILanguageMap languageMap);

        IActivityBuilder AddName(string languageCode, string content);

        IActivityBuilder AddName(ILanguageMap languageMap);
        Activity Build();

        IActivityBuilder WithActivityType(string typeUri);

        IActivityBuilder WithActivityType(Uri type);

        IActivityBuilder WithMoreInfo(Uri moreInfo);

        IActivityBuilder WithMoreInfo(string moreInfoUri);
    }
}