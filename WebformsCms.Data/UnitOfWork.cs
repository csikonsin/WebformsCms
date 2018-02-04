using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Data
{

    public class DataSession : IDisposable
    {
        public DataSession()
        {
            var conStr= "Data Source=DESKTOP-V7SQE52;Initial Catalog=WebformsCms;Integrated Security=True";

            _connection = new SqlConnection(conStr);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }

        public DataSession(bool useTestDb)
        {
            var conStr = "Data Source=DESKTOP-V7SQE52;Initial Catalog=WebformsCms;Integrated Security=True";
            var testDbConstr = "Data Source=DESKTOP-V7SQE52;Initial Catalog=WebformsCms.Tests;Integrated Security=True";

            if (useTestDb) conStr = testDbConstr;

            _connection = new SqlConnection(conStr);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }


        IDbConnection _connection = null;
        IUnitOfWork _unitOfWork = null;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
        }
    }

    


    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Begin(IsolationLevel il);
        void Commit();
        void Rollback();
    }

    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private Guid _id = Guid.Empty;
        private IDbTransaction _transaction = null;

        internal UnitOfWork(IDbConnection cn)
        {
            _id = Guid.NewGuid();
            _connection = cn;
        }

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }

        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }

        Guid IUnitOfWork.Id
        {
            get { return _id;  }
        }

        public void Begin()
        {
             _transaction = _connection.BeginTransaction();
        }

        public void Begin(IsolationLevel il) 
        {            
            _transaction = _connection.BeginTransaction(il);
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if(_transaction!=null)
            {
                _transaction.Dispose();
            }
            _transaction = null;
        }


    }
}
