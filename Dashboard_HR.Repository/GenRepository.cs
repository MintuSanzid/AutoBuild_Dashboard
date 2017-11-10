using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_HR.Repository
{
   public class GenRepository<T> : IGenRepository<T> where T: class
    {
       public IEnumerable<T> SelectAll()
       {
           throw new NotImplementedException();
       }

       public T SelectedById(object id)
       {
           throw new NotImplementedException();
       }

       public void InsertOrUpdate(T obj)
       {
           throw new NotImplementedException();
       }

       public void Delete(object id)
       {
           throw new NotImplementedException();
       }

       public void Save()
       {
           throw new NotImplementedException();
       }
        
       public DataTable GetAll(string one, string two, string three)
       {
           return null;
       }
    }
}
