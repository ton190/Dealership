using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using EntityLibrary;
using EntityLibrary.CarRecords;
using ModelLibrary.CarRecords;

namespace ModelLibrary.Mappingds
{
    public interface IMap<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : IDto
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap<TEntity, TDto>()
                .EqualityComparison((e, d) => e.Id == d.Id);
            profile.CreateMap<TDto, TEntity>()
                .EqualityComparison((d, e) => d.Id == e.Id);
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(
                Assembly.GetExecutingAssembly());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<SearchAddress, AddressDto>().ReverseMap();
        }


        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMap<,>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMap`2")?.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
