using System.ComponentModel.DataAnnotations;

namespace Domain.Enum
{
    public enum OmsProvider
    {
        [Display(Name ="صحرا")]
        Sahra = 1,
        [Display(Name = "تدبیر")]
        Tadbir = 2,
        [Display(Name = "رابین")]
        Rabin = 3,
        [Display(Name = "آرمان اکس")]        
        Armanx = 4,
        [Display(Name = "ایزی تریدر")]
        EasyTrader = 5
    }
}
