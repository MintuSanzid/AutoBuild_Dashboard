using System;
using System.Collections.Generic;
using System.Data;

namespace Dashboard_HR.Repository.Dashboards
{
   public class ManpowerRepository<T> : IGenRepository<T> where T: class
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
           throw new NotImplementedException();
       }
    }
}
