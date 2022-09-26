using System.ComponentModel;

namespace Entities.Enums
{
    public enum EToken
    {
        [Description("ConfirmEmail")]
        ConfirmEmail,
        [Description("ResetPassword")]
        ResetPassword,
        [Description("Refresh")]
        Refresh
    }
}
