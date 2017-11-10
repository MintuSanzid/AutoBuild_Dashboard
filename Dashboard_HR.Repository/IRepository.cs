using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_HR.Repository
{
    public interface IGenRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectedById(object id);
        void InsertOrUpdate(T obj); 
        void Delete(object id);
        void Save();
        DataTable GetAll(string one , string two, string three);  
    }
}
