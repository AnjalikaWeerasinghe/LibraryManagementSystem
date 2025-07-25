using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IPublisherService
    {
        PagedResult<PublisherViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<PublisherViewModel> GetPublisherByName(string name, int pageNumber, int pageSize);
        PublisherViewModel GetPublisherById(int PublisherId);
        Task<IEnumerable<PublisherViewModel>> GetAllAsync();
        void UpdatePublisher(PublisherViewModel publisher);
        string InsertPublisher(PublisherViewModel publisher);
        void DeletePublisher(int id);
    }
}
