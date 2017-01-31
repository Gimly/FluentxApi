namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public interface INumericPerformanceInteractionStepResponse
    {
        string Id { get; }

        INumericRange Range { get; }
    }
}