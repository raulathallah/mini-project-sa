using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests;
using LibraryManagementSystem.Domain.Models.Requests.CheckOuts;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IStockService
    {
        Task<object> AddStocks(AddStockRequest request);
        Task<object> BookCheckOut(BookCheckOutRequest request);
        Task<object> BookCheckOutUserInformation(BookCheckOutUserRequest request);
        Task<object> BookCheckOutBookInformation(BookCheckOutBookRequest request);

        Task<object> UserBookRequest(UserBookRequest request);
        Task<object> ApprovalBookRequest(ApprovalBookRequest request);

        Task<object> GetDashboard();
        Task<List<object>> GetWorkflowDashboard();


        //REQUEST BOOK LIST & DETAIL
        Task<List<object>> GetRequestBookList();
        Task<object> GetRequestBookDetail(int id);
    }
    
}