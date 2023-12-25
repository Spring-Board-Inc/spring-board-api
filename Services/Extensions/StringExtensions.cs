namespace Services.Extensions
{
    public static class StringExtensions
    {
        public static Guid StringToGuid(this string value)
        {
            _= Guid.TryParse(value, out var result);
            return result;
        }
    }
}
