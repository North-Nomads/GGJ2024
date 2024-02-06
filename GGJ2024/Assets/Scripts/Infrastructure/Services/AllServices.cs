using Microsoft.Unity.VisualStudio.Editor;

namespace GGJ.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        /// <summary>
        /// Will register an implementation of <see cref="TService"/> type
        /// </summary>
        /// <param name="implementation">Instance of <see cref="TService"/> type</param>
        /// <typeparam name="TService">Common service interface</typeparam>
        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;

        /// <summary>
        /// Will return <see cref="TService"/> that registered in <see cref="AllServices"/>
        /// </summary>
        /// <typeparam name="TService">Common service interface</typeparam>
        /// <returns>Instance of object as <see cref="TService"/> type</returns>
        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        /// <summary>
        /// Static class for each service that given in program. When <see cref="AllServices.RegisterSingle{TService}"/>
        /// called, it'll created new implementation for each instance of service
        /// </summary>
        /// <typeparam name="TService">Common service interface</typeparam>
        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance { get; set; }
        } 
    }
}