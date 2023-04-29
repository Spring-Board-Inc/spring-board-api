namespace Shared.DataTransferObjects
{
    public abstract record AboutUsForManipulationDto
    {
        public string About { get; init; }
        public string Mission { get; init; }
        public string Vision { get; init; }
    }
}
