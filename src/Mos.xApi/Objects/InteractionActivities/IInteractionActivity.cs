namespace Mos.xApi.Objects.InteractionActivities
{
    public interface IInteractionActivity<T>
    {
        T CorrectResponse { get; }
    }
}