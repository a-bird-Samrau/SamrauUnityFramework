namespace Tools
{
    public class Mediator<T>
    {
        public Mediator(T target)
        {
            Target = target;
        }
        
        public T Target { get; }
    }
}