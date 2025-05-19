using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using SmartHouse.familyClass.childrenTasks;
using SmartHouse.familyClass.petInfo;
using SmartHouse.shopcClass;
using SmartHouse.familyClass.childrenInfo;


namespace SmartHouse
{
    public class SQLhelper
    {
        private readonly SQLiteAsyncConnection db;

        public SQLhelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            
          db.CreateTableAsync<ChildrenInfo>();

        }

       

        public Task<int> createChildrenTask(ChildrenInfo Ct)
        {
            return db.InsertAsync(Ct);

        }
    public Task<List<ChildrenInfo>> readChildrenTask()
        {
            return db.Table<ChildrenInfo>().ToListAsync();

        }

    public Task <int> UpdateChildrenTask(ChildrenInfo Ct)
        {
            return db.UpdateAsync(Ct);

        }
    public Task <int> DeletChildrenTask(ChildrenInfo Ct) { 
        
            return db.DeleteAsync(Ct);
        }
    
    }
}
