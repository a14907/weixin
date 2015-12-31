using IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDAL<TEntity> : IBaseDAL<TEntity> where TEntity : class
    {
        public BaseDBContext db
        {
            get
            {
                object obj = CallContext.GetData("BaseDBContext");
                if (obj == null)
                {
                    obj = new BaseDBContext();
                    CallContext.SetData("BaseDBContext", obj);
                }
                return obj as BaseDBContext;
            }
        }

        DbSet<TEntity> _db = null;
        public BaseDAL()
        {
            _db = db.Set<TEntity>();
        }
        public IQueryable<TEntity> QueryWhere(System.Linq.Expressions.Expression<Func<TEntity, bool>> where)
        {
            return _db.Where(where);
        }

        public IQueryable<TEntity> QueryJoinWhere(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            if (tableNames == null || tableNames.Any())
            {
                throw new Exception("连表方法的tableNames至少要有一个值");
            }
            DbQuery<TEntity> obj = _db;
            foreach (var item in tableNames)
            {
                obj = obj.Include(item);
            }
            return obj.Where(where);
        }

        public IQueryable<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int tcount, System.Linq.Expressions.Expression<Func<TEntity, bool>> where, System.Linq.Expressions.Expression<Func<TEntity, TKey>> order)
        {
            int skipNum = (pageIndex - 1) * pageSize;
            tcount = _db.Count(where);
            return _db.Where(where).OrderBy(order).Skip(skipNum).Take(pageSize);
        }

        public IQueryable<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int tcount, System.Linq.Expressions.Expression<Func<TEntity, bool>> where, System.Linq.Expressions.Expression<Func<TEntity, TKey>> order, string[] tableNames)
        {
            if (tableNames == null || tableNames.Any())
            {
                throw new Exception("连表方法的tableNames至少要有一个值");
            }
            int skipNum = (pageIndex - 1) * pageSize;
            tcount = _db.Count(where);
            DbQuery<TEntity> obj = _db;
            foreach (var item in tableNames)
            {
                obj = obj.Include(item);
            }
            return obj.Where(where).OrderBy(order).Skip(skipNum).Take(pageSize);
        }

        public List<TElement> RunSql<TElement>(string sql, params object[] parms)
        {
            return db.Database.SqlQuery<TElement>(sql, parms).ToList();
        }

        public void Add(TEntity model)
        {
            if (model == null)
            {
                throw new Exception("model实体不能为null");
            }

            _db.Add(model);
        }

        public void Edit(TEntity model, string[] propertyNames)
        {
            //1.0 检查model的合法性
            if (model == null)
            {
                throw new Exception("model实体不能为null");
            }

            //2.0 检查属性propertyNames至少有一个
            if (propertyNames == null || propertyNames.Any() == false)
            {
                throw new Exception("propertyNames数组至少要有一个值");
            }
            var entry = db.Entry(model);
            entry.State = System.Data.EntityState.Unchanged;
            foreach (var item in propertyNames)
            {
                entry.Property(item).IsModified = true;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
        }

        public void Delete(TEntity model, bool isAttached)
        {
            if (model == null)
            {
                throw new Exception("model实体不能为null");
            }
            if (!isAttached)
            {
                _db.Attach(model);
            }
            _db.Remove(model);
        }

        public int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
        }
    }
}
