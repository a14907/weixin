using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseBLL<TEntiry> : IBaseBLL<TEntiry> where TEntiry : class
    {
        protected IBaseDAL<TEntiry> baseDAL;
        public IQueryable<TEntiry> QueryWhere(System.Linq.Expressions.Expression<Func<TEntiry, bool>> where)
        {
            return baseDAL.QueryWhere(where);
        }

        public IQueryable<TEntiry> QueryJoinWhere(System.Linq.Expressions.Expression<Func<TEntiry, bool>> where, string[] tableNames)
        {
            return baseDAL.QueryJoinWhere(where, tableNames);
        }

        public IQueryable<TEntiry> QueryByPage<TKey>(int pageIndex, int pageSize, out int tcount, System.Linq.Expressions.Expression<Func<TEntiry, bool>> where, System.Linq.Expressions.Expression<Func<TEntiry, TKey>> order)
        {
            return baseDAL.QueryByPage(pageIndex, pageSize, out tcount, where, order);
        }

        public IQueryable<TEntiry> QueryByPage<TKey>(int pageIndex, int pageSize, out int tcount, System.Linq.Expressions.Expression<Func<TEntiry, bool>> where, System.Linq.Expressions.Expression<Func<TEntiry, TKey>> order, string[] tableNames)
        {
            return baseDAL.QueryByPage(pageIndex, pageSize, out tcount, where, order, tableNames);
        }

        public List<TElement> RunSql<TElement>(string sql, params object[] parms)
        {
            return baseDAL.RunSql<TElement>(sql, parms);
        }

        public void Add(TEntiry model)
        {
            baseDAL.Add(model);
        }

        public void Edit(TEntiry model, string[] propertyNames)
        {
            baseDAL.Edit(model, propertyNames);
        }

        public void Delete(TEntiry model, bool isAttached)
        {
            baseDAL.Delete(model, isAttached);
        }

        public int SaveChanges()
        {
            return baseDAL.SaveChanges();
        }
    }
}
