using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OrbitStore.Web.Query
{
    using LinqKit;

    public class BaseQuery<TEntity> 
    {
        #region Fields
        private Expression<Func<TEntity, bool>> _query;
        #endregion

        #region Expression Methods
        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return this._query;
        }

        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        {
            return this._query = this._query == null ? query : this._query.And(query.Expand());
        }

        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        {
            return this._query = this._query == null ? query : this._query.Or(query.Expand());
        }

        #endregion
    }
}