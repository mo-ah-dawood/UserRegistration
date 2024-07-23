using System.Text.RegularExpressions;
using Riok.Mapperly.Abstractions;
using UserRegistration.Api.Requests;
using UserRegistration.Api.Response;
using UserRegistration.Core.Entities;

namespace UserRegistration.Api
{
    [Mapper(UseDeepCloning = true)]
    public static partial class MappingExtensions
    {
        public static partial User ToUser(this RegisterNewUser input);
        private static partial UserDto UserToDto(this User input);
        private static partial LoginDto UserToLoginDto(this User input);
        public static LoginDto ToLoginDto(this User input)
        {
            var obj = input.UserToLoginDto();
            obj.EmailAddress = Regex.Replace(obj.EmailAddress, @"(?<=.{2}).(?=[^@]*?@)|(?:(?<=@)|\G(?=[^@]*$)).(?=.*\.)", m => new string('*', m.Length));
            obj.MobileNumber = Regex.Replace(obj.MobileNumber, @".+(?=.{4})", m => new string('*', m.Length));
            return obj;
        }
        public static UserDto ToDto(this User input)
        {
            var obj = input.UserToDto();
            obj.IsPinCodeSet = !string.IsNullOrEmpty(input.PinCode);
            return obj;
        }

    }
}