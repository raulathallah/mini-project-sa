using FoodOrderingSystemAPI.Dto.CustomerDto;

namespace FoodOrderingSystemAPI.Interfaces
{
    public interface ICustomerService
    {
        CustomerListDto GetAllCustomer();
        CustomerDetailDto AddCustomer(CustomerAddDto customer);
        CustomerDetailDto GetCustomerById(int id);
        CustomerDetailDto UpdateCustomer(int id, CustomerAddDto customer);
        CustomerDetailDto DeleteCustomer(int id);

    }
}
