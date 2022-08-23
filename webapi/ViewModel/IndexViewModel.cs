using webapideneme.Classes;

namespace webapideneme.ViewModel
{
    public class IndexViewModel
    {  
        public List<Classes.Index> Index { get; set; }
    }
    public class CustomerResponseViewModel
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public List<CustomerEnvironmentResponseViewModel> CustomerEnvironment { get; set; }
    }
    public class CustomerEnvironmentResponseViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public List<CustomerEnvironmentPropResponseViewModel> CustomerEnvironmentProperty { get; set; }
    }
    public class CustomerEnvironmentPropResponseViewModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public int Status { get; set; }
    }
    public class IndexResponseViewModel
    {
        public Classes.Index Index { get; set; }    
        public List<CustomerResponseViewModel> Customer { get; set; }
        public List<CustomerEnvironmentResponseViewModel> Environment { get; set; }

    }
    public class ResponseViewModel
    {
        public List<IndexResponseViewModel> indexResponseViewModels { get; set; }
    }

    
    

}
