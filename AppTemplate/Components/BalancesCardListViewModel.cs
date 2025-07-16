using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppTemplate.Components
{
    public record BalanceCard
    {
        public required string UsdValue { get; set; }
        public required Color BackgroundColor { get; set; }
        public required string AmountSymbol { get; set; }
        public required string TokenName { get; set; }
        public required string Icon { get; set; }
        public required Color TextColor { get; set; }
    }
    public partial class BalancesCardListViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<BalanceCard> cards = new ObservableCollection<BalanceCard>();


    }
}
