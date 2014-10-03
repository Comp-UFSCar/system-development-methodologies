using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Gamedalf.Core.Models;
using System.Data.Entity;
using Gamedalf.Core.Data;
using Gamedalf.Controllers;
using Microsoft.Owin.Security;
using Gamedalf.Services;
using Microsoft.Owin;

namespace Gamedalf.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
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

            container.RegisterType<DbContext, ApplicationDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new PerRequestLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<PlayersController>(
                new InjectionConstructor(
                    typeof(ApplicationUserManager),
                    typeof(PlayerService)
                ));
            container.RegisterType<DevelopersController>(
                new InjectionConstructor(
                    typeof(ApplicationUserManager),
                    typeof(DeveloperService),
                    typeof(PlayerService)
                ));
        }
    }
}
