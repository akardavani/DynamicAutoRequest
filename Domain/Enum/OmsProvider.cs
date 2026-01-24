using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public enum OmsProvider
    {
        [Display(Name = "ایزی تریدر")]
        EasyTrader = 1,

        [Display(Name = "اسمارت")]
        Smart = 2,        

        [Display(Name = "تدبیر")]
        Tadbir = 3,

        [Display(Name = "رابین")]
        Rabin = 4,

        [Display(Name = "صحرا")]
        Phoenix = 5,

        [Display(Name = "آرمان اکس")]        
        Armanx = 6,

        [Display(Name = "آسا تریدر")]
        AsaTrader = 7,
    }
}
