using Ninject.Modules;
using Store.BLL.Interfaces;
using Store.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            //Bind<IUserService>().To<UserService>();
            //Bind<IServiceCreator>().To<ServiceCreator>();
        }
    }
}