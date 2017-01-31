namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public interface IInteractionActivity<T>
    {
        T CorrectResponse { get; }
    }
}