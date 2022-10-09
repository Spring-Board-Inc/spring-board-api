using System.ComponentModel;

namespace Entities.Enums
{
    public enum ELevel
    {
        [Description("Basic")]
        Basic,
        [Description("Post Basic")]
        PostBasic,
        [Description("Diploma")]
        Diploma,
        [Description("Bachelor Degree")]
        Bachelor,
        [Description("Master Degree")]
        Master,
        [Description("Ph.D")]
        PhD
    }
}
