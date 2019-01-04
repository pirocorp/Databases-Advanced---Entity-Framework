namespace P01_OldestFamilyMember
{
    using System.Collections.Generic;
    using System.Linq;

    public class Family
    {
        private readonly List<Person> familyMembers;

        public Family()
        {
            this.familyMembers = new List<Person>();
        }

        public IReadOnlyCollection<Person> FamilyMembers => this.familyMembers.AsReadOnly();

        public void AddMember(Person member)
        {
            this.familyMembers.Add(member);
        }

        public Person GetOldestMember()
        {
            var oldestFamilyMember = this.familyMembers.OrderByDescending(x => x.Age).First();
            return oldestFamilyMember;
        }
    }
}