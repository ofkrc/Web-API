using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapideneme.Classes;


namespace webapideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        [HttpGet]
        public CustomersViewModel GetCustomerList()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "JSON/Customer.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file
            
            var Customers = JsonConvert.DeserializeObject<CustomersViewModel>(jsonData); //deserialize object as a list of users in accordance with your json file
            
            return Customers;
        }


        [HttpPost]
        public CustomersViewModel AddCustomer(Customer customer)
           
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "JSON/Customer.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file            
            
            var Customers = JsonConvert.DeserializeObject<CustomersViewModel>(jsonData);

            var lastItem = Customers.Customer.LastOrDefault();

            int newId = 1;

            if(lastItem != null)
            {
                newId = lastItem.ID + 1;
            }

            customer.ID = newId;           
            
            Customers.Customer.Add(customer);  

            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Customers));           

            return Customers;
        }

        [HttpPut]
        public CustomersViewModel UpdateCustomer(Customer customer)
        {
            var rootPath = _hostingEnvironment.ContentRootPath; 

            var fullPath = Path.Combine(rootPath, "JSON/Customer.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Customers = JsonConvert.DeserializeObject<CustomersViewModel>(jsonData);
            var updateCustomer = Customers.Customer.FirstOrDefault(x => x.ID == customer.ID);

            if (updateCustomer != null)
            {
                //İdsine göre getirip getirilen datanın propertylerin değiştirme                
                updateCustomer.Description = customer.Description;
                updateCustomer.CustomerName = customer.CustomerName;
                updateCustomer.Status = customer.Status;
            }          
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Customers));

            return Customers;
        }


        [HttpDelete]
        public CustomersViewModel DeleteCustomer(Customer customer,int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Customer.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Customers = JsonConvert.DeserializeObject<CustomersViewModel>(jsonData);

            var deletedCustomer = Customers.Customer.FirstOrDefault(x => x.ID == id);

            if (deletedCustomer != null)
            {
                //İdsine göre getirip getirilen datayı silme
                Customers.Customer.Remove(deletedCustomer);
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Customers));

            return Customers;
        }
   

        [HttpGet]
        public CustomersViewModel GetOneCustomerList(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Customer.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Customers = JsonConvert.DeserializeObject<CustomersViewModel>(jsonData);

            var oneCustomer = Customers.Customer.FirstOrDefault(x => x.ID == id);

            if (oneCustomer != null)
            {
                CustomersViewModel returnCustomer = new CustomersViewModel()
                {
                    Customer = new List<Customer>()
                    {
                        oneCustomer
                    }
                };
                return returnCustomer;                
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Customers));

            return Customers;
        }




    }

    
}


