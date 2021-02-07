using System;
using System.Reflection;
using Autofac;

namespace CodeIsBug.Admin.Api.Extensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
     
        protected override void Load(ContainerBuilder builder)
        {
          
            var assemblysServicesNoInterfaces = Assembly.Load("CodeIsBug.Admin.Services");
            builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);


        }
    }
}
