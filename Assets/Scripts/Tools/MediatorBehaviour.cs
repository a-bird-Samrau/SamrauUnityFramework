using Core;

namespace Tools
{
    public class MediatorBehaviour<T> : Behaviour
    {
        public void Construct(T target)
        {
            Mediator = new Mediator<T>(target);
        }
    
        public Mediator<T> Mediator { get; private set; }
    }
}