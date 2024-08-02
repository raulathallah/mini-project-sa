using FoodOrderingSystemAPI.Dto.CustomerDto;
using FoodOrderingSystemAPI.Dto.MenuDto;
using FoodOrderingSystemAPI.Interfaces;
using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private static List<Customer> listCustomer = new List<Customer>();
        public CustomerDetailDto AddCustomer(CustomerAddDto customerData)
        {

            if(listCustomer.Find(lc=>lc.Name == customerData.Name || lc.Email == customerData.Email) != null)
            {
                return ResponseDetail(false, null, "Customer exist");
            }

            var customer = new Customer()
            {
                CustomerId = listCustomer.Count + 1,
                Name = customerData.Name,
                Address = customerData.Address,
                Email = customerData.Email,
                PhoneNumber = customerData.PhoneNumber,
                RegistrationDate = DateTime.Now,
            };

            
            listCustomer.Add(customer);
            return ResponseDetail(true, customer, "Add customer success!");
        }

        public CustomerDetailDto DeleteCustomer(int id)
        {
            var customer = SearchCustomer(id);
            if (customer == null)
            {
                return ResponseDetail(false, null, "No customer available!");
            }
            listCustomer.Remove(customer);

            return ResponseDetail(true, customer, "Delete customer success!");
        }

        public CustomerListDto GetAllCustomer()
        {
            if(listCustomer.Count == 0)
            {
                return ResponseList(false, null, "Customer list is empty");
            }

            return ResponseList(true, listCustomer, "Get all customer success!");
        }

        public CustomerDetailDto GetCustomerById(int id)
        {
            var customer = SearchCustomer(id);
            if(customer == null)
            {
                return ResponseDetail(false, null, "No customer available!");
            }
            return ResponseDetail(true, customer, "Get customer by ID success!");
        }

        public CustomerDetailDto UpdateCustomer(int id, CustomerAddDto customerData)
        {
            var customer = SearchCustomer(id);
            if (customer == null)
            {
                return ResponseDetail(false, null, "No customer available!");
            }
            customer.Name = customerData.Name;
            customer.Address = customerData.Address;
            customer.Email = customerData.Email;
            customer.PhoneNumber = customerData.PhoneNumber;
            return ResponseDetail(true, customer, "Get customer by ID success!");
        }


        Customer SearchCustomer(int id)
        {
            var customer = listCustomer.FirstOrDefault(lc => lc.CustomerId == id);
            return customer;
        }

        CustomerDetailDto ResponseDetail(bool status, Customer customer, string message)
        {
            return new CustomerDetailDto()
            {
                Message = message,
                Status = status,
                Data = customer
            };
        }

        CustomerListDto ResponseList(bool status, List<Customer> customer, string message)
        {
            return new CustomerListDto()
            {
                Message = message,
                Status = status,
                Data = customer,
            };
        }
    }
}
