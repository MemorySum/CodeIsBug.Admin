using System.Reflection;
using Autofac;
using Module = Autofac.Module;
namespace CodeIsBug.Admin.Api.Extensions
{
    public class AutofacModuleRegister : Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            var assemblesServicesNoInterfaces = Assembly.Load("CodeIsBug.Admin.Services");
            builder.RegisterAssemblyTypes(assemblesServicesNoInterfaces);


        }
    }
}
