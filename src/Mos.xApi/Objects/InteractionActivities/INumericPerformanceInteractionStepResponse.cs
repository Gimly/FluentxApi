namespace Mos.xApi.Objects.InteractionActivities
{
    public interface INumericPerformanceInteractionStepResponse
    {
        string Id { get; }

        INumericRange Range { get; }
    }
}