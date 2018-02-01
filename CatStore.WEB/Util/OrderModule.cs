using Ninject.Modules;
using CatStore.BLL.Services;
using CatStore.BLL.Interfaces;

namespace CatStore.WEB.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IService>().To<Service>();
        }
    }
}