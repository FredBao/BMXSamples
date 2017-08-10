namespace DistributeExtensionPatternDemo
{
    public interface IDistribute<T>
    {
        void DistributeOrder(T t);
    }
}