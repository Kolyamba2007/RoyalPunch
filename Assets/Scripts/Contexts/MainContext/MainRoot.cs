using strange.extensions.context.impl;

namespace Contexts.MainContext
{
    public class MainRoot : ContextView
    {
        void Awake()
        {
            context = new MainContext(this);
        }
    }
}