using System.ComponentModel;

namespace Domain
{
    public enum StrategyTypeEnum
    {
        [Description("صف خرید کمتر از")]
        BuyQueueLessThan = 1,

        [Description("آخرین قیمت کمتر از")]
        LastPriceLessThan = 2,

        [Description("درصد آخرین قیمت کمتر از")]
        LastPricePercentageLessThan = 3
    }
}
