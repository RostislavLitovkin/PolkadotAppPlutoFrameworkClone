using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlutoFramework.Model;

namespace AppTemplate.Components
{
    public partial class ReceiveAndTransferButtonsViewModel : ObservableObject
    {
        [RelayCommand]
        public void Receive() => ReceiveAndTransferModel.Receive();

        [RelayCommand]
        public void Transfer() => ReceiveAndTransferModel.Transfer();
    }
}
