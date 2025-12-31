using System.ComponentModel;

namespace Domain
{
    public enum OrderSideEnum
    {
        [Description("خرید")]
        Buy = 1,

        [Description("فروش")]
        Sell = 2,

        [Description("CrossOrder")]
        CrossOrder = 3
    }

}
