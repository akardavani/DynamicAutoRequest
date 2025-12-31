using System.ComponentModel;

namespace Domain
{
    public enum RequestTypeEnum
    {
        [Description("ایجاد")]
        Creation = 1,
        
        [Description("ویرایش")]
        Modification = 2,
        
        [Description("حذف")]
        Cancellation = 3
    }
}
