using System;
using Microsoft.Extensions.DependencyInjection;

namespace AppGM.Core
{
    class ServiceContainerBuilder
    {
        private ServiceCollection servicios = new ServiceCollection();

        public void AñadirServicio<Servicio, Implementacion>(Implementacion clase)
            where Servicio : class
            where Implementacion : class, Servicio
        {
            servicios.AddSingleton<Servicio, Implementacion>();
        }

        public IServiceProvider ConstruirServiceProvider() => servicios.BuildServiceProvider();
    }
    class ServiceContainer
    {
        private IServiceProvider mServiceProvider;

        ServiceContainer(IServiceProvider _serviceProvider)
            => mServiceProvider = _serviceProvider;
        
        public Servicio ObtenerServicio<Servicio>()
            where Servicio : class
        {
            return mServiceProvider.GetService<Servicio>();
        }
    }
}