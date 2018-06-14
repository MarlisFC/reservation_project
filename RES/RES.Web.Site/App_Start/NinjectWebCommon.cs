using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RES.DataAccess.Interfaces.Interfaces;
using RES.BusinessLogic.Core.Data;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UI.App_Start.NinjectWebCommon), "Stop")]

namespace UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var useEf = Convert.ToBoolean(ConfigurationManager.AppSettings["UseEF"]);
            var targetDataBase = ConfigurationManager.AppSettings["TargetDataBase"];

            var conString = targetDataBase.Equals("mssql") ? ConfigurationManager.ConnectionStrings["mssql"].ConnectionString : ConfigurationManager.ConnectionStrings["oracle"].ConnectionString;

            if (useEf)
            {
                kernel.Bind<ResEntities>().To<ResEntities>().InRequestScope().WithConstructorArgument("conString", conString);
                kernel.Bind<IReservationRepository>().To<RES.DataAccess.Core.Repository.EF.ReservationEfRepository>().InRequestScope();
                kernel.Bind<IContactRepository>().To<RES.DataAccess.Core.Repository.EF.ContactEfRepository>().InRequestScope();               
            }
            else
            {
                if (targetDataBase.ToLower().Equals("mssql"))
                {
                    kernel.Bind<IDbConnection>().To<SqlConnection>().InRequestScope();
                    kernel.Bind<IReservationRepository
                        >().To<RES.DataAccess.Core.Repository.Drapper.Mssql.ReservationSqlRepository>().InRequestScope().WithConstructorArgument("conString", conString);
                    kernel.Bind<IContactRepository
                        >().To<RES.DataAccess.Core.Repository.Drapper.Mssql.ContactSqlRepository>().InRequestScope().WithConstructorArgument("conString", conString);

                }
                else if (targetDataBase.ToLower().Equals("oracle"))
                {
                    //kernel.Bind<IDbConnection>().To<OracleConnection>().InRequestScope();
                    //kernel.Bind<IReservationRepository>().To<DataAccess.Repository.Dapper.ORA.CompanyOraRepository>().InRequestScope().WithConstructorArgument("conString", conString);
                }
            }
        }
    }
}