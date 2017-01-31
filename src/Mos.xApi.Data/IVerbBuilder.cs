namespace Mos.xApi.Data
{
    public interface IVerbBuilder
    {
        IVerbBuilder AddDisplay(string languageCode, string content);

        IVerbBuilder AddDisplay(ILanguageMap languageMap);

        Verb Build();
    }
}