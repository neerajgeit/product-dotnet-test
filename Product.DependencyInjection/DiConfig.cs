using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Product.Data.Context;
using Product.Data.Repositories;
using Product.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace Product.DependencyInjection
{
    public static class DiConfig
    {
        public static void Register()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            container.Register<ProductDataContext, ProductDataContext>(Lifestyle.Scoped);

            container.Register<IProductRepository, ProductRepository>();
            container.Register<IMaterialRepository, MaterialRepository>();
            container.Register<IProductService, ProductService>();
            container.Register<IMaterialService, MaterialService>();
            
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
