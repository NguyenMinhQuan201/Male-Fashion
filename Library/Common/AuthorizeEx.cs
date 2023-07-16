using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public static class AuthorizeEx
    {
        public static IServiceCollection AddAuthorizeEx(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var assembly = Assembly.Load("API");
                var controllerTypes = assembly.GetExportedTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.Name.EndsWith("Controller"))
                    .ToList();
                var constFields = new List<FieldInfo>();

                foreach (var type in controllerTypes)
                {
                    var controllerType = type;
                    var fields = controllerType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                        .Where(f => f.IsLiteral && !f.IsInitOnly)
                        .ToList();
                    constFields.AddRange(fields);
                }
                foreach (var field in constFields)
                {
                    var fieldValue = field.GetValue(null);
                    if (fieldValue != null)
                    {
                        if (!String.IsNullOrEmpty(fieldValue.ToString()))
                        {
                            options.AddPolicy(fieldValue.ToString(), policy =>
                            {
                                policy.RequireClaim(fieldValue.ToString());
                            });
                        }
                    }
                }
            });
            return services;
        }
    }
}
