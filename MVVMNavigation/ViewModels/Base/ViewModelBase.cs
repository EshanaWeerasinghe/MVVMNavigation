using MVVMNavigation.ViewModels.Base;

namespace ILearnIt.ViewModels.Base
{
    public class ViewModelBase : ExtendedBindableObject
    {
        public virtual Task Init(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task ReverseInit(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}