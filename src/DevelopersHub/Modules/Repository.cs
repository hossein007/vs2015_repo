using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
//using System.Data.Entity;
//using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.SqlClient;




    public interface IRepository<C, T>
        where T : class
        where C : DbContext
    {
        IEnumerable<T> GetAll(Func<T, bool> predicate = null);
        T Get(Func<T, bool> predicate);
    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> Add(T entity);
        void Attach(T entity);
        void Delete(T entity);


    }


    public interface IUnitOfWork<C> where C : DbContext
    {
        IRepository<C, T> Repository<T>() where T : class;
        void SaveChanges();

    Microsoft.EntityFrameworkCore.Storage.RelationalDataReader GetResult<O>(QueryParameters QueryParameters);

    }


    public class GenericRepository<C, T> : IRepository<C, T>
        where T : class
        where C : DbContext
    {

        private C _DbContext = null;
        DbSet<T> _dbSet;
        public GenericRepository()
        {

        }
        public GenericRepository(C DbContext)
        {
            _DbContext = DbContext;
            _dbSet = _DbContext.Set<T>();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            if (predicate != null)
            {
                return _dbSet.Where(predicate);
            }

            return _dbSet.AsEnumerable();
        }

        public T Get(Func<T, bool> predicate)
        {
            return _dbSet.First(predicate);
        }

        public Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


    }


    public abstract class GenericUnitOfWork<C> : IDisposable, IUnitOfWork<C> where C : DbContext, new()
    {
        private C _DbContext = null;

        public GenericUnitOfWork()
        {
            _DbContext = new C();
        }
    public void CloseReader()
    {
        _DbContext.Database.CloseConnection();

    }
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public IRepository<C, T> Repository<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)) == true)
            {
                return _repositories[typeof(T)] as IRepository<C, T>;
            }

            IRepository<C, T> __repo = new GenericRepository<C, T>(_DbContext);
            _repositories.Add(typeof(T), __repo);
            return __repo;
        }

        public void SaveChanges()
        {
            _DbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Microsoft.EntityFrameworkCore.Storage.RelationalDataReader GetResult<O>(QueryParameters QueryParameters)
        {
            int __pagerowfrom = (QueryParameters.PageIndex - 1) * QueryParameters.PageSize;
            string str_query =
                string.Format(
     @"
set @totalresultcount = (SELECT   count(*) FROM  {3} {1}) 
SELECT {2} *
FROM    
(SELECT    ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNum, * FROM {3} {1}) AS RowConstrainedResult
WHERE   RowNum > @pagerowfrom 
ORDER BY RowNum",
     QueryParameters.OrderBy,
     (QueryParameters.Where.Length > 0 ? " WHERE " : "") + QueryParameters.Where,

     (QueryParameters.PageSize > 0 ? "top(@pagesize)" : ""), QueryParameters.SqlSourceObjectName);

            System.Data.SqlClient.SqlParameter param_totalresultcount = new System.Data.SqlClient.SqlParameter();
            param_totalresultcount.Direction = System.Data.ParameterDirection.Output;
            param_totalresultcount.SqlDbType = System.Data.SqlDbType.Int;
            param_totalresultcount.ParameterName = "@totalresultcount";
            System.Data.SqlClient.SqlParameter param_pagesize = new System.Data.SqlClient.SqlParameter("@pagesize", QueryParameters.PageSize);
            System.Data.SqlClient.SqlParameter param_pagerowfrom = new System.Data.SqlClient.SqlParameter("@pagerowfrom", __pagerowfrom);



            QueryParameters.PageCount = -1;
            int __sqlparamcount = 0;
            if (QueryParameters.SqlParams != null)
            {
                __sqlparamcount = QueryParameters.SqlParams.Count;
            }
            object[] __params = new object[__sqlparamcount + 3];
            __params[0] = param_pagesize;
            __params[1] = param_pagerowfrom;
            __params[2] = param_totalresultcount;

            if (__sqlparamcount > 0)
            {
                int __i = 3;
                foreach (SqlParameter sqlParameter in QueryParameters.SqlParams)
                {
                    __params[__i] = sqlParameter;
                    __i++;
                }
            }


        _DbContext.Database.ExecuteSqlCommand(str_query, __params);


        if (QueryParameters.PageSize > 0)
            QueryParameters.PageCount =
                Convert.ToInt32(param_totalresultcount.Value) / QueryParameters.PageSize +
                (Convert.ToInt32(param_totalresultcount.Value) % QueryParameters.PageSize == 0 ? 0 : 1);


        var __result =
            _DbContext.Database.ExecuteSqlQuery(str_query, __params);

            

            

            return __result;
        }


    }




namespace Microsoft.EntityFrameworkCore
{
    //using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;
    //using SirenTek.Areas.TechDemo.Areas.SirenTransferTests.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public static class RDFacadeExtensions
    {
        public static RelationalDataReader ExecuteSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade
                    .GetService<IRawSqlCommandBuilder>()
                    .Build(sql, parameters);

                return rawSqlCommand
                    .RelationalCommand
                    .ExecuteReader(
                        databaseFacade.GetService<IRelationalConnection>(),
                        parameterValues: rawSqlCommand.ParameterValues);
            }
        }

        public static async Task<RelationalDataReader> ExecuteSqlCommandAsync(this DatabaseFacade databaseFacade,
                                                             string sql,
                                                             CancellationToken cancellationToken = default(CancellationToken),
                                                             params object[] parameters)
        {

            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade
                    .GetService<IRawSqlCommandBuilder>()
                    .Build(sql, parameters);

                return await rawSqlCommand
                    .RelationalCommand
                    .ExecuteReaderAsync(
                        databaseFacade.GetService<IRelationalConnection>(),
                        parameterValues: rawSqlCommand.ParameterValues,
                        cancellationToken: cancellationToken);
            }
        }
    }
}