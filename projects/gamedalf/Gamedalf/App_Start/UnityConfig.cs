using Gamedalf.Controllers;
using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace Gamedalf.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<PlayersController>(
                new InjectionConstructor(
                    typeof(ApplicationUserManager),
                    typeof(PlayerService)
                ));
        }

        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}