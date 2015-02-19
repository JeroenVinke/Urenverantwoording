namespace Urenverantwoording.Interfaces
{
    public interface IWrapEntity<T>
    {
        void Load(T entity);
    }
}