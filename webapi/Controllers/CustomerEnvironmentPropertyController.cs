using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapideneme.Classes;
using webapideneme.ViewModel;

namespace webapideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerEnvironmentPropertyController : Controller
    {
        public CustomerEnvironmentPropertyController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        /*[HttpGet]
        public List<CustomerEnvironmentProperty> GetCustomerEnvironmentPropertyListByCustomerId(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonData);

            var environmentProperties = CustomerEnvironmentProperties.CustomerEnvironmentProperty.Where(x => x.CustomerEnvironmentID == id).ToList();

            return environmentProperties;
        }*/

        [HttpGet]
        public CustomerEnvironmentPropertyViewModel GetCustomerEnvironmentPropertyList()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonData); //deserialize object as a list of users in accordance with your json file

            return CustomerEnvironmentProperties;
        }

        [HttpPost]
        public CustomerEnvironmentPropertyViewModel AddCustomerEnvironmentProperty(CustomerEnvironmentProperty customerEnvironmentProperty)

        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file            

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonData);

            var lastItem = CustomerEnvironmentProperties.CustomerEnvironmentProperty.LastOrDefault();

            int newId = 1;

            if (lastItem != null)
            {
                newId = lastItem.ID + 1;
            }

            customerEnvironmentProperty.ID = newId;

            CustomerEnvironmentProperties.CustomerEnvironmentProperty.Add(customerEnvironmentProperty);

            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironmentProperties));

            return CustomerEnvironmentProperties;
        }

        [HttpPut]
        public CustomerEnvironmentPropertyViewModel UpdateCustomerEnvironmentProperty(CustomerEnvironmentProperty customerEnvironmentProperty)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonData);

            var updateCustomerEnvironmentProperty = CustomerEnvironmentProperties.CustomerEnvironmentProperty.FirstOrDefault(x => x.ID == customerEnvironmentProperty.ID);

            if (updateCustomerEnvironmentProperty != null)
            {
                //İdsine göre getirip getirilen datanın propertylerin değiştirme                
                updateCustomerEnvironmentProperty.Value = customerEnvironmentProperty.Value;
                updateCustomerEnvironmentProperty.Name = customerEnvironmentProperty.Name;
                updateCustomerEnvironmentProperty.Status = customerEnvironmentProperty.Status;
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironmentProperties));

            return CustomerEnvironmentProperties;
        }


        [HttpDelete]
        public CustomerEnvironmentPropertyViewModel DeleteCustomerEnvironmentProperty(CustomerEnvironmentProperty customerEnvironmentProperty, int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonData);

            var deletedCustomerEnvironmentProperty = CustomerEnvironmentProperties.CustomerEnvironmentProperty.FirstOrDefault(x => x.ID == id);

            if (deletedCustomerEnvironmentProperty != null)
            {
                //İdsine göre getirip getirilen datayı silme
                CustomerEnvironmentProperties.CustomerEnvironmentProperty.Remove(deletedCustomerEnvironmentProperty);
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironmentProperties));

            return CustomerEnvironmentProperties;
        }


        [HttpGet]
        public CustomerEnvironmentPropertyViewModel GetCustomerEnvironmentPropertyListById(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonData);

            var oneCustomerEnvironmentProperty = CustomerEnvironmentProperties.CustomerEnvironmentProperty.FirstOrDefault(x => x.ID == id);

            if (oneCustomerEnvironmentProperty != null)
            {
                CustomerEnvironmentPropertyViewModel returnCustomerEnvironmentProperty = new CustomerEnvironmentPropertyViewModel()
                {
                    CustomerEnvironmentProperty = new List<CustomerEnvironmentProperty>()
                    {
                        oneCustomerEnvironmentProperty
                    }
                };
                return returnCustomerEnvironmentProperty;
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(CustomerEnvironmentProperties));

            return CustomerEnvironmentProperties;
        }
    }
}
