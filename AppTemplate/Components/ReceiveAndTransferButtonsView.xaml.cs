namespace AppTemplate.Components;

public partial class ReceiveAndTransferButtonsView : ContentView
{
	public ReceiveAndTransferButtonsView()
	{
		InitializeComponent();

		BindingContext = new ReceiveAndTransferButtonsViewModel();
	}
}