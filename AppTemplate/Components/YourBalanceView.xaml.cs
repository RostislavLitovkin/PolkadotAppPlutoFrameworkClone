using PlutoFramework.Components;
using PlutoFramework.Constants;
using PlutoFramework.Model;
using PlutoFramework.Model.Currency;
using PlutoFramework.Model.HydraDX;

namespace AppTemplate.Components;

public partial class YourBalanceView : ContentView, ISubstrateClientLoadableAsyncView
{
	public YourBalanceView()
	{
		InitializeComponent();

		BindingContext = new YourBalanceViewModel();
    }

    public async Task LoadAsync(PlutoFrameworkSubstrateClient client, CancellationToken token)
    {
        if (!KeysModel.HasSubstrateKey())
        {
            return;
        }

        if (client is not null && client.Endpoint.Key == EndpointEnum.Hydration && client.SubstrateClient.IsConnected)
        {
            try
            {
                await Sdk.GetAssetsAsync((Hydration.NetApi.Generated.SubstrateClientExt)client.SubstrateClient, null, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            AssetsModel.UpdateUsdBalance();
        }

        await AssetsModel.GetBalanceAsync(client, KeysModel.GetSubstrateKey(), token, false);

        var viewModel = (YourBalanceViewModel)BindingContext;
        viewModel.BalanceText = AssetsModel.UsdSum.ToCurrencyString();
    }
}