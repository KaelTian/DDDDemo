namespace User.Domain
{
    public class PhoneNumber : ValueObject
    {
        public int RegionNumber { get; set; }
        public int Number { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return RegionNumber;
            yield return Number;
        }
    }
}
