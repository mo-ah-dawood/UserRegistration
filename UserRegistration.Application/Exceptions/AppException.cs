namespace UserRegistration.Application.Exceptions
{
    public class AppException : Exception
    {
        private readonly string _errorCode;
        public AppException(string errorCode, string message) : base(message)
        {
            _errorCode = errorCode;
        }

        public string ErrorCode { get { return _errorCode; } }
    }
}