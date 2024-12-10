using System.Text.RegularExpressions;
using Riok.Mapperly.Abstractions;
using CvSystem.Api.Requests;
using CvSystem.Api.Response;
using CvSystem.Core.Entities;
using System.Runtime.Serialization;

namespace CvSystem.Api
{
    [Mapper(UseDeepCloning = true)]

    public static partial class MappingExtensions
    {
        /// <summary>
        /// Cv mappers
        /// </summary>
        [MapperIgnoreTarget(nameof(CV.Id))]
        [MapperIgnoreTarget(nameof(CV.PersonalInformationId))]
        [MapperIgnoreTarget(nameof(CV.PersonalInformation))]
        [MapperIgnoreTarget(nameof(CV.Experiences))]
        public static partial CV ToEntity(this CreateCVRequest input);

        [MapperIgnoreSource(nameof(CV.PersonalInformationId))]
        public static partial CVDto ToDto(this CV entity);
        public static partial IEnumerable<CVDto> ProjectToDto(this IEnumerable<CV> q);

        /// <summary>
        /// PersonalInformation mappers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [MapperIgnoreTarget(nameof(PersonalInformation.Id))]
        public static partial PersonalInformation ToEntity(this PersonalInformationRequest input);
        [MapperIgnoreTarget(nameof(PersonalInformation.Id))]
        public static partial void MapTo(this PersonalInformationRequest input, PersonalInformation entity);

        public static partial PersonalInformationDto ToDto(this PersonalInformation entity);


        /// <summary>
        /// ExperienceInformation mappers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [MapperIgnoreTarget(nameof(ExperienceInformation.CVId))]
        [MapperIgnoreTarget(nameof(ExperienceInformation.CV))]
        public static partial ExperienceInformation ToEntity(this ExperienceInformationRequest input);


        [MapperIgnoreSource(nameof(ExperienceInformation.CVId))]
        [MapperIgnoreSource(nameof(ExperienceInformation.CV))]
        public static partial ExperienceInformationDto ToDto(this ExperienceInformation entity);

        public static partial IEnumerable<ExperienceInformation> ToEntity(this IEnumerable<ExperienceInformationRequest> input);
        public static partial IEnumerable<ExperienceInformationDto> ToDto(this IEnumerable<ExperienceInformation> input);


    }
}