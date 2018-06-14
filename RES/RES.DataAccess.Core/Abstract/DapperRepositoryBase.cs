using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RES.DataAccess.Interfaces.Base;

namespace RES.DataAccess.Interfaces.Abstract
{
    public abstract class DapperRepositoryBase : IRepositoryBase
    {
        protected IDbConnection _connection = null;

        protected List<T> Query<T>(string storedProcName)
        {
            return Query<T>(storedProcName, null);
        }

        protected List<T> Query<T>(string storedProcName, object parameters)
        {
            return _connection.Query<T>(storedProcName, parameters, commandType: CommandType.StoredProcedure).ToList();
        }

        protected List<R> Query<R, P, C, RT>(string storedProcName, Func<R, P, C, R> map, object parameters, String split)
        {
            return _connection.Query<R, P, C, R>(storedProcName,map,parameters,null,false,split,null, commandType: CommandType.StoredProcedure).ToList();
        }

        protected List<C> Query<C, CT, Co>(string storedProcName, Func<C, CT, C> map, object parameters, String split)
        {
            return _connection.Query<C, CT, C>(storedProcName, map, parameters, null, false, split, null, commandType: CommandType.StoredProcedure).ToList();
        }

        protected void Execute(string storedProcName)
        {
            Execute(storedProcName, null);
        }

        protected void Execute(string storedProcName, object parameters)
        {
            _connection.Execute(storedProcName, parameters, commandType: CommandType.StoredProcedure);
        }

        protected void Execute(string storedProcName, object parameters, IDbTransaction transaction)
        {
            _connection.Execute(storedProcName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
        }

        protected object ExecuteScalar(string storedProcName)
        {
            return ExecuteScalar(storedProcName, null);
        }

        protected object ExecuteScalar(string storedProcName, object parameters)
        {
            return _connection.ExecuteScalar(storedProcName, parameters, commandType: CommandType.StoredProcedure);
        }

        protected List<T> QueryTableView<T>(string query)
        {
            return _connection.Query<T>(query).ToList();
        }

        #region IDisposable

        public bool CanDispose { get; set; }

        public void Dispose(bool force)
        {
            this.CanDispose = true;
            Dispose();
        }

        public void Dispose()
        {
            if (_connection != null && this.CanDispose)
                _connection.Dispose();
        }
        #endregion
    }

}