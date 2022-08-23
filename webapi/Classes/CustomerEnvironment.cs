using System.ComponentModel.DataAnnotations.Schema;



namespace webapideneme.Classes
{
    public class CustomerEnvironment
    {
        public int CustomerID { get; set; }

        public int ID { get; set; }
        public string? Name { get; set; }                        
        public string? Description { get; set; }
        public int Status { get; set; }
        


        
    }
}
