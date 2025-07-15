
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppTemplate.Components
{
    public partial class YourBalanceViewModel : ObservableObject
    {
        [ObservableProperty]
        private string balanceText = "Loading";
    }
}
