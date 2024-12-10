using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CvSystem.Api.Response
{
    public class GenericResult
    {
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }

        public int Status { get; set; } = 200;
        public Dictionary<string, IEnumerable<string>?> Errors { get; set; } = [];

        public int Total { get; set; } = 0;

        public static GenericResult<T> Success<T>(T result)
        {
            return new GenericResult<T>()
            {
                Data = result,
                Status = 200
            };
        }
        public static GenericResult<T> Success<T>(T result, int total)
        {
            return new GenericResult<T>()
            {
                Data = result,
                Status = 200,
                Total = total
            };
        }
        public static GenericResult Error(string errorMessage, string errorCode, int status = 400)
        {
            return new GenericResult()
            {
                Status = status,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode,
            };
        }

        public static GenericResult ValidationErrors(ModelStateDictionary modelState, int status = 400)
        {
            var errors = modelState.Select((x) => new
            {
                Field = x.Key,
                Errors = x.Value?.Errors.Select(e => e.ErrorMessage),
            });
            return new GenericResult()
            {
                Status = status,
                Errors = errors.ToDictionary((x) => x.Field, (x) => x.Errors),
                ErrorCode = "Validation",
                ErrorMessage = errors.FirstOrDefault()?.Errors?.FirstOrDefault(),
            };
        }


    }
    public class GenericResult<T> : GenericResult
    {
        public T? Data { get; set; }

    }
}