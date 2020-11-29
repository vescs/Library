using Autofac;
using Library.Infrastructure.AutoMapper;
using Library.Infrastructure.IoC.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfiguration.Initialize())
                .SingleInstance();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<CommandModule>();
        }
    }
}
