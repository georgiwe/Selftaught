namespace Selftaught.Web.Infrastructure.ModelMapping
{
    using AutoMapper;

    public interface IRequiresCustomMapping
    {
        void CreateMapping(IConfiguration configuration);
    }
}
