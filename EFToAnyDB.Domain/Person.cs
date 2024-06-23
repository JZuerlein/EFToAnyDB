namespace EFToAnyDB.Domain
{
    public class Person
    {
        public int PersonId { get; protected set; }
        public string DisplayName { get; protected set; }

        public Person(string displayName) 
        {
            DisplayName = displayName;
        }
    }
}
