using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapideneme.Classes;
using webapideneme.ViewModel;

namespace webapideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerEnvironmentController : Controller
    {
        public CustomerEnvironmentController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        /*[HttpGet]
        public List<CustomerEnvironment> GetCustomerEnvironmentListByCustomerId(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironment.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);            

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonData);

            var customersEnv = CustomerEnvironments.CustomerEnvironment.Where(x => x.CustomerID == id).ToList();            

            return customersEnv;
        }*/
        
        [HttpGet]
        public CustomerEnvironmentViewModel GetCustomerEnvironmentList()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironment.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonData); //deserialize object as a list of users in accordance with your json file

            return CustomerEnvironments;
        }

        [HttpPost]
        public CustomerEnvironmentViewModel AddCustomerEnvironment(CustomerEnvironment customerEnvironment)

        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironment.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file            

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonData);

            var lastItem = CustomerEnvironments.CustomerEnvironment.LastOrDefault();

            int newId = 1;

            if (lastItem != null)
            {
                newId = lastItem.ID + 1;
            }

            customerEnvironment.ID = newId;

            CustomerEnvironments.CustomerEnvironment.Add(customerEnvironment);

            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironments));

            return CustomerEnvironments;
        }

        [HttpPut]
        public CustomerEnvironmentViewModel UpdateCustomerEnvironment(CustomerEnvironment customerEnvironment)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironment.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonData);
            var updateCustomerEnvironment = CustomerEnvironments.CustomerEnvironment.FirstOrDefault(x => x.ID == customerEnvironment.ID);

            if (updateCustomerEnvironment != null)
            {
                //İdsine göre getirip getirilen datanın propertylerin değiştirme                
                updateCustomerEnvironment.Description = customerEnvironment.Description;
                updateCustomerEnvironment.Name = customerEnvironment.Name;
                updateCustomerEnvironment.Status = customerEnvironment.Status;
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironments));

            return CustomerEnvironments;
        }


        [HttpDelete]
        public CustomerEnvironmentViewModel DeleteCustomerEnvironment(CustomerEnvironment customerEnvironment, int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironment.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonData);

            var deletedCustomerEnvironment = CustomerEnvironments.CustomerEnvironment.FirstOrDefault(x => x.ID == id);

            if (deletedCustomerEnvironment != null)
            {
                //İdsine göre getirip getirilen datayı silme
                CustomerEnvironments.CustomerEnvironment.Remove(deletedCustomerEnvironment);
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironments));

            return CustomerEnvironments;
        }


        [HttpGet]
        public CustomerEnvironmentViewModel GetCustomerEnvironmentListById(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironment.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonData);

            var oneCustomerEnvironment = CustomerEnvironments.CustomerEnvironment.FirstOrDefault(x => x.ID == id);

            if (oneCustomerEnvironment != null)
            {
                CustomerEnvironmentViewModel returnCustomerEnvironment = new CustomerEnvironmentViewModel()
                {
                    CustomerEnvironment = new List<CustomerEnvironment>()
                    {
                        oneCustomerEnvironment
                    }
                };
                return returnCustomerEnvironment;
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironments));

            return CustomerEnvironments;
        }
    }
}
