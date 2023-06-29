using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Extensions.ExtensionServices
{
    public static class AutoMapperExtensions
    {
        public static void CreateMapByNamingConvention(this IMapperConfigurationExpression configuration)
        {
            var assembly = Assembly.Load("Domain");
            var assembly2 = Assembly.Load("Infrastructure");
            var types = assembly2.GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .ToList();

            foreach (var type in types)
            {
                var dtoType = assembly.GetExportedTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).FirstOrDefault(t => t.Name == type.Name + "Dto");
                if (dtoType != null)
                {
                    configuration.CreateMap(dtoType, type);
                    configuration.CreateMap(type, dtoType);
                }
            }
        }
    }
}
