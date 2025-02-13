using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.Base
{
    public  abstract class BaseViewModel : ObservableObject
    {
        // virtual: lehet felülírni
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
