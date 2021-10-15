using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.Extensions
{
    internal static class DependencyInjectionExtension
    {
        internal static void AddTransientMock<INTERFACE, CLASS>(this IServiceCollection serviceCollection, CLASS clas) 
            where CLASS : class, INTERFACE
            where INTERFACE : class 
        {
            var descriptor = serviceCollection.FirstOrDefault(serviceDescriptor => serviceDescriptor.ServiceType == typeof(INTERFACE));

            if(descriptor != null)
                serviceCollection.Remove(descriptor);

            serviceCollection.AddTransient<INTERFACE>(serviceProvider => clas);
        }
    }
}