using PlutoFramework.Components;
using PlutoFramework.Constants;
using PlutoFramework.Model;
using PlutoFramework.Model.Currency;
using PlutoFramework.Model.HydraDX;
using PlutoFramework.Types;
using System.Collections.ObjectModel;

namespace AppTemplate.Components;

public partial class BalancesCardListView : ContentView, ISubstrateClientLoadableAsyncView
{
	public BalancesCardListView()
	{
		InitializeComponent();

		BindingContext = new BalancesCardListViewModel();
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

        var viewModel = (BalancesCardListViewModel)BindingContext;

        var cards = new ObservableCollection<BalanceCard>();

        foreach(var a in AssetsModel.AssetsDict.Values)
        {
            if (a.Symbol.ToLower() != "dot" && a.Symbol.ToLower() != "usdc" && a.Symbol.ToLower() != "usdt")
            {
                continue;
            }

            if (a.Amount > 0 || a.Pallet == AssetPallet.Native)
            {
                cards.Add(new BalanceCard
                {
                    AmountSymbol = String.Format((string)Application.Current.Resources["CurrencyFormat"], a.Amount) + " " + a.Symbol,
                    UsdValue = a.UsdValue > 0 ? a.UsdValue.ToCurrencyString() : "~",
                    Icon = a.Symbol.ToLower() switch
                    {
                        "dot" => "polkadotwhite.png",
                        _ => a.ChainIcon
                    },
                    BackgroundColor = a.Symbol.ToLower() switch
                    {
                        "dot" => Color.FromArgb("#ff1493"),
                        "usdt" => Color.FromArgb("#5ad190"),
                        "usdc" => Color.FromArgb("#14adff"),
                        _ => Color.FromArgb("#555555"),
                    },
                    TokenName = a.Symbol.ToLower() switch
                    {
                        "dot" => "Polkadot",
                        "usdt" => "Digital dollar",
                        "usdc" => "Digital dollar",
                        _ => "Unknown"
                    },
                    TextColor = a.Symbol.ToLower() switch {
                        "usdt" => Colors.Black,
                        _ => Colors.White,
                    }
                });
            }
        }


        viewModel.Cards = cards;
    }
}