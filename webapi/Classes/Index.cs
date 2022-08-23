namespace webapideneme.Classes
{
    public class Index
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EnvironmentId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
        public string Description { get; set; }

    }
    public class Root
    {
        public List<Index> Index { get; set; }
    }
}
