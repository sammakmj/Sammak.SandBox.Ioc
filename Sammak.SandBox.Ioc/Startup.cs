using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammak.SandBox.Ioc
{
    /// <summary>
    /// Structuremap IOC Container Setup - PersonSync APP
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Gets or builds the container
        /// </summary>
        /// <returns></returns>
        //public static IContainer EnsureIocSetUp()
        //{
        //    if (DependencyResolver.Container == null && !_initializing)
        //        return UseIoc();
        //    return DependencyResolver.Container;
        //}

        private static bool _initializing;

        /// <summary>
        /// Gets or sets the container (private).
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IContainer Container { get; set; }

        /// <summary>
        /// Builds the container (private).
        /// </summary>
        /// <returns></returns>
        protected internal static IContainer UseIoc()
        {
            //Logger.Trace("UseIoc: Initializing Container.");
            _initializing = true;

            try
            {
                Container = new Container(c =>
                {
                    //c.For<System.Configuration.Abstractions.IConfigurationManager>()
                    //    .Use(() => ConfigurationManager);

                    c.Scan(s =>
                    {
                        s.TheCallingAssembly();
                        s.AssembliesFromApplicationBaseDirectory();
                        s.IncludeNamespace("Sammak.SandBox.Ioc");
                        s.LookForRegistries();
                        s.WithDefaultConventions();
                    });

                    //c.For<ISessionFactory>().Singleton().Use(ConfigureOrm());
                    //c.For<ISession>().LifecycleIs(new ThreadLocalStorageLifecycle()).Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
                    //c.For<IBackgroundJobClient>().Use<BackgroundJobClient>();
                    //c.For<IValidationFactory>().Use<ValidationFactory>().Ctor<IContainer>("container").Is(DependencyResolver.Container);
                    //c.For<IQueryFactory>().Use<QueryFactory>().Ctor<IContainer>("container").Is(DependencyResolver.Container);
                    //c.For<Core.MassTransit.IBusFactory>().Use<Core.MassTransit.BusFactory>();
                    //c.For<IEnumService>().Singleton().Use<EnumService>();
                    //c.For<IValidator<AuditData>>().Singleton().Use<AuditData.AuditDataValidator>();
                    //c.For<IAccessTokenService>().Singleton().Use<AccessTokenService>();
                    //c.For<IRegistrationInterservice>().Singleton().Use<RegistrationInterservice>();
                    //c.For<IProfileInterservice>().Singleton().Use<ProfileInterservice>();
                    //c.For<IProgramInterservice>().Singleton().Use<ProgramInterservice>();
                    //c.For<IProductInterservice>().Singleton().Use<ProductInterservice>();
                    //c.For<ICacheManager<ExamFileData>>().Singleton().Use(d => CacheFactory.FromConfiguration<ExamFileData>("cache", UseServiceCacheManagement()));

                    //FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<ExamFileData.ExamFileDataValidator>()
                    //    .ForEach(r =>
                    //    {
                    //        c.For(r.InterfaceType)
                    //            .Singleton()
                    //            .Use(r.ValidatorType);
                    //    });
                });
                //DependencyResolver.Container = Container;
            }
            finally
            {
                _initializing = false;
            }

            //Logger.Trace("UseIoc: Container initialized.");
            return Container;
            //return DependencyResolver.Container;
        }
    }
}
