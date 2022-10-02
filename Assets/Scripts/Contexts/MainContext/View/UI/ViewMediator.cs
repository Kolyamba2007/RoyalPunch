using strange.extensions.mediation.impl;

namespace Contexts.MainContext
{
    public class ViewMediator<T> : Mediator
    {
        [Inject] public T View { get; set; }
    }
}
