
namespace MultiTenantEMS.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public ApiResponseCode Code { get; }
        public string Error { get; }

        protected Result(bool isSuccess, ApiResponseCode code, string error)
        {
            IsSuccess = isSuccess;
            Code = code;
            Error = error;
        }

        public static Result Success() => new(true, ApiResponseCode.Success, string.Empty);

        public static Result Failure(string error, ApiResponseCode code = ApiResponseCode.BadRequest) => new(false, ApiResponseCode.BadRequest, error);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public ApiResponseCode Code { get; }
        public T? Data { get; }
        public string Error { get; }

        private Result(bool isSuccess, ApiResponseCode code, T? value, string error)
        {
            IsSuccess = isSuccess;
            Code = code;
            Data = value;
            Error = error;
        }

        public static Result<T> Success(T value, ApiResponseCode code = ApiResponseCode.Success)
            => new(true, code, value, string.Empty);

        public static Result<T> Failure(string error, ApiResponseCode code = ApiResponseCode.BadRequest)
            => new(false, code, default, error);
    }
}
