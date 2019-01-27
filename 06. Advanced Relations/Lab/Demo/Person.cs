namespace Demo
{
    public class Person
    {
        [Xor(nameof(Ssn))]
        public int? Age { get; set; }

        public int? Ssn { get; set; }
    }
}