namespace Selftaught.Web.ViewModels.Language
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Selftaught.Web.Infrastructure.ModelMapping;
    using System.Web.Mvc;
    using AutoMapper;

    public class LanguageViewModel : IMapFrom<Selftaught.Data.Models.Language>, IRequiresCustomMapping
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            configuration.CreateMap<Selftaught.Data.Models.Language, LanguageViewModel>()
                .ForMember(vm => vm.Id,
                           o => o.MapFrom(l => l.Id))
                .ReverseMap();
        }
    }
}