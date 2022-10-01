namespace Shared.DataTransferObjects
{
    public class UserSkillMinInfo
    {
        public Guid UserInformationId { get; set; }
        public Guid SkillId { get; set; }
        public string Skill { get; set; }
        public string Level { get; set; }
    }
}
