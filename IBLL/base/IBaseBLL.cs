using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBLL<TEntity> where TEntity : class
    {
        #region 1.0 查询

        //1.0 带条件查询
        IQueryable<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where);

        //2.0 带条件的连表查询
        IQueryable<TEntity> QueryJoinWhere(Expression<Func<TEntity, bool>> where, string[] tableNames);

        //3.0 带条件的分页查询
        IQueryable<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int tcount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order);

        //4.0 带条件的分页连表查询
        IQueryable<TEntity> QueryByPage<TKey>(int pageIndex, int pageSize, out int tcount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, string[] tableNames);

        //5.0 执行一条sql语句或者一个存储过程
        List<TElement> RunSql<TElement>(string sql, params object[] parms);

        #endregion

        #region 2.0 新增

        void Add(TEntity model);

        #endregion

        #region 3.0 编辑

        //自定义实体指定属性进行编译
        void Edit(TEntity model, string[] propertyNames);

        #endregion

        #region 4.0 删除

        void Delete(TEntity model, bool isAttached);

        #endregion

        #region 5.0 统一保存

        int SaveChanges();

        #endregion
    }
}
