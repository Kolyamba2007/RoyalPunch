using strange.extensions.mediation.impl;

public class ViewMediator<T> : Mediator
{
    [Inject] public T View { get; set; }
}
