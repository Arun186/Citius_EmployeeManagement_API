using System;
using System.Collections.Generic;
using Repository_Employee.Dtos;
using System.Diagnostics;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace Repository_Employee.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...

        private Dtos.EmployeeDBContext _context = null;
        private GenericRepository<Dtos.Employees> __employeesRepository;
        private GenericRepository<Roles> _roles;

        #endregion

        public UnitOfWork()
        {
            _context = new Dtos.EmployeeDBContext();
        }

        #region Public Repository Creation properties...

        public GenericRepository<Dtos.Employees> EmployeeRepository
        {
            get
            {
                if (__employeesRepository == null)
                {
                    __employeesRepository = new GenericRepository<Dtos.Employees>(_context);
                }
                return __employeesRepository;
            }
        }

        public GenericRepository<Roles> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new GenericRepository<Roles>(_context);
                }
                return _roles;
            }
        }


        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public async Task<int> Save()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                Debug.WriteLine("Errors : " + outputLines);
                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
