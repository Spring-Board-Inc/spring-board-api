using System.ComponentModel;

namespace Entities.Enums
{
    public enum ERoles
    {
        [Description("Applicant")]
        Applicant,
        [Description("Employer")]
        Employer,
        [Description("Administrator")]
        Administrator,
        [Description("SuperAdministrator")]
        SuperAdministrator
    }
}