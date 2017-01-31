namespace Mos.xApi.Builders
{
    public interface IVerbBuilder
    {
        IVerbBuilder AddDisplay(string languageCode, string content);

        IVerbBuilder AddDisplay(ILanguageMap languageMap);

        Verb Build();
    }
}