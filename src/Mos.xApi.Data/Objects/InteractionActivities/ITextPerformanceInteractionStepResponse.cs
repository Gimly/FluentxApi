namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public interface ITextPerformanceInteractionStepResponse
    {
        string Id { get; }

        string LanguageCode { get; }

        string Response { get; }
    }
}