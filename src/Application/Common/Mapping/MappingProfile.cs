using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFormAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFormAssembly(Assembly assembly)
        {
            var type = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapForm<>))).ToList();

            foreach (var t in type)
            {
                var instance = Activator.CreateInstance(t);

                var methodInfo = t.GetMethod("Mapping")
                    ?? t.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
