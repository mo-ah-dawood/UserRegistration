namespace CvSystem.Application.Exceptions
{
    public class AppException(string errorCode, string message) : Exception(message)
    {
        private readonly string _errorCode = errorCode;

        public string ErrorCode { get { return _errorCode; } }
    }
}