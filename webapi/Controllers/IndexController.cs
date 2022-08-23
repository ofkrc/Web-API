using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapideneme.Classes;
using webapideneme.ViewModel;

namespace webapideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IndexController : Controller
    {
        public IndexController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        [HttpGet]
        public ResponseViewModel GetList()
        {
            ResponseViewModel resultModel = new ResponseViewModel();
            resultModel.indexResponseViewModels = new List<IndexResponseViewModel>();
            //resultModel.Customer = new List<Customer>();
            //resultModel.CustomerEnvironment = new List<CustomerEnvironment>();
            //resultModel.CustomerEnvironmentProperty = new List<CustomerEnvironmentProperty>();

            //Index
            var rootPath = _hostingEnvironment.ContentRootPath; //base yol

            var fullPath = Path.Combine(rootPath, "JSON/Index.json"); //base yolu json dosyasıyla birleştir

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file    

            //var Indexs = JsonConvert.DeserializeObject<List<Classes.Index>>(jsonData);
            var Indexs = JsonConvert.DeserializeObject<Root>(jsonData);


            //Customer
            var fullPathCustomer = Path.Combine(rootPath, "JSON/Customer.json"); //base yolu json dosyasıyla birleştir

            var jsonDataCustomer = System.IO.File.ReadAllText(fullPathCustomer); //dosyadaki tüm içeriği oku  
            var Customers = JsonConvert.DeserializeObject<CustomersViewModel>(jsonDataCustomer);

            //Env
            var fullPathEnvironment = Path.Combine(rootPath, "JSON/CustomerEnvironment.json"); //base yolu json dosyasıyla birleştir

            var jsonDataEnvironment = System.IO.File.ReadAllText(fullPathEnvironment); //dosyadaki tüm içeriği oku      

            var CustomerEnvironments = JsonConvert.DeserializeObject<CustomerEnvironmentViewModel>(jsonDataEnvironment);

            //Property
            var fullPathProperty = Path.Combine(rootPath, "JSON/CustomerEnvironmentProperty.json"); //base yolu json dosyasıyla birleştir

            var jsonDataProperty = System.IO.File.ReadAllText(fullPathProperty); //dosyadaki tüm içeriği oku   

            var CustomerEnvironmentProperties = JsonConvert.DeserializeObject<CustomerEnvironmentPropertyViewModel>(jsonDataProperty);

            foreach (var item in Indexs.Index)
            {
                IndexResponseViewModel indexResponseViewModel = new IndexResponseViewModel();
                indexResponseViewModel.Index = item;
                var customers  = Customers.Customer.Where(x => x.ID == item.CustomerId).ToList();
                indexResponseViewModel.Customer = new List<CustomerResponseViewModel>();

                foreach (var item2 in customers)
                {
                    List<CustomerEnvironmentResponseViewModel> customerEnvironmentResponseViewModels = new List<CustomerEnvironmentResponseViewModel>();
                    var environments = CustomerEnvironments.CustomerEnvironment.Where(x => x.ID == item.EnvironmentId).ToList();

                    CustomerResponseViewModel customer = new CustomerResponseViewModel()
                    {
                        CustomerName = item2.CustomerName,
                        Description = item2.Description,            //Customer bilgilerinin çekildiği yer
                        ID = item2.ID,
                        Status = item2.Status,
                        CustomerEnvironment = customerEnvironmentResponseViewModels

                    };

                    foreach (var item3 in environments)
                    {
                        
                        List<CustomerEnvironmentPropResponseViewModel> customerEnvironmentPropResponseViewModels = new List<CustomerEnvironmentPropResponseViewModel>();           
                        var properties = CustomerEnvironmentProperties.CustomerEnvironmentProperty.Where(x => x.ID == item.PropertyId).ToList();
                        
                        CustomerEnvironmentResponseViewModel environment = new CustomerEnvironmentResponseViewModel()
                        {
                            ID = item.EnvironmentId,
                            Description = item3.Description,            //Customer Environment bilgilerinin çekildiği yer
                            Status = item3.Status,
                            Name = item3.Name,
                            CustomerEnvironmentProperty = customerEnvironmentPropResponseViewModels
                        };
                        customerEnvironmentResponseViewModels.Add(environment);

                        foreach(var item4 in properties)
                        {

                            List<CustomerEnvironmentPropResponseViewModel> customerEnvironmentPropertyResponseViewModels = new List<CustomerEnvironmentPropResponseViewModel>();
                            
                            CustomerEnvironmentPropResponseViewModel property = new CustomerEnvironmentPropResponseViewModel()
                            {
                                ID=item.PropertyId,
                                Name=item4.Name,
                                Value=item.PropertyValue,           //Customer Environment Property bilgilerinin çekildiği yer
                                Status =item4.Status
                            };
                            customerEnvironmentPropResponseViewModels.Add(property);
                        }
                    }
                    indexResponseViewModel.Customer.Add(customer);
                }
                resultModel.indexResponseViewModels.Add(indexResponseViewModel);
            }
            return resultModel;
        }

        [HttpPost]
        public List<Classes.Index> Add(Classes.Index indexModel)
        {
            var rootPath = _hostingEnvironment.ContentRootPath; // base yol

            var fullPath = Path.Combine(rootPath, "JSON/Index.json"); //base yolu json dosyasıyla birleştir

            var jsonData = System.IO.File.ReadAllText(fullPath); //dosyadaki tüm içeriği oku         

            var Indexs = JsonConvert.DeserializeObject<Root>(jsonData);

            var lastItem = Indexs.Index.LastOrDefault();

            int newId = 1;

            if (lastItem != null)
            {
                newId = lastItem.Id + 1;
            }

            indexModel.Id = newId;

            Indexs.Index.Add(indexModel);

            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));

            return Indexs.Index;
        }

        [HttpPut]
        public IndexViewModel Update(Classes.Index indexModel)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Index.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Indexs = JsonConvert.DeserializeObject<IndexViewModel>(jsonData);
            var updateIndex = Indexs.Index.FirstOrDefault(x => x.Id == indexModel.Id);

            if (updateIndex != null)
            {
                              
                updateIndex.CustomerId = indexModel.CustomerId;
                updateIndex.EnvironmentId = indexModel.EnvironmentId;
                updateIndex.PropertyId = indexModel.PropertyId;
                updateIndex.PropertyValue = indexModel.PropertyValue;
                updateIndex.Description = indexModel.Description;

            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));

            return Indexs;
        }

        [HttpDelete]
        public IndexViewModel Delete(Classes.Index indexModel, int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Index.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Indexs = JsonConvert.DeserializeObject<IndexViewModel>(jsonData);

            var deletedIndex = Indexs.Index.FirstOrDefault(x => x.Id == id);

            if (deletedIndex != null)
            {
                Indexs.Index.Remove(deletedIndex);
            }
            System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));

            return Indexs;
        }

        [HttpGet]
        public IndexViewModel GetIndexListById(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Index.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Indexs = JsonConvert.DeserializeObject<IndexViewModel>(jsonData);

            var oneIndex = Indexs.Index.FirstOrDefault(x => x.Id == id);

            if (oneIndex != null)
            {
                IndexViewModel returnIndex = new IndexViewModel()
                {
                    Index = new List<Classes.Index>()
                    {
                        oneIndex
                    }
                };
                System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));
                return returnIndex;
            }
            

            return Indexs;
        }


        
        [HttpGet]
        public IndexViewModel GetIndexListByCustomerId(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Index.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Indexs = JsonConvert.DeserializeObject<IndexViewModel>(jsonData); 

            var oneIndex = Indexs.Index.Where(x => x.CustomerId == id).ToList();            

            if (oneIndex != null)
            {
                IndexViewModel returnIndex = new IndexViewModel()
                {
                    Index = oneIndex
                };
                System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));
                return returnIndex;
            }
            return Indexs;
        }


        [HttpGet]
        public IndexViewModel GetIndexListByEnvironmentId(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Index.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Indexs = JsonConvert.DeserializeObject<IndexViewModel>(jsonData);

            var oneIndex = Indexs.Index.Where(x => x.EnvironmentId == id).ToList();

            if (oneIndex != null)
            {
                IndexViewModel returnIndex = new IndexViewModel()
                {
                    Index = oneIndex
                };
                System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));
                return returnIndex;
            }
            return Indexs;
        }


        [HttpGet]
        public IndexViewModel GetIndexListByPropertyId(int id)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;

            var fullPath = Path.Combine(rootPath, "JSON/Index.json");

            var jsonData = System.IO.File.ReadAllText(fullPath);

            var Indexs = JsonConvert.DeserializeObject<IndexViewModel>(jsonData);

            var oneIndex = Indexs.Index.Where(x => x.PropertyId == id).ToList();

            if (oneIndex != null)
            {
                IndexViewModel returnIndex = new IndexViewModel()
                {
                    Index = oneIndex
                };
                System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(Indexs));
                return returnIndex;
            }
            return Indexs;
        }






    }
}
