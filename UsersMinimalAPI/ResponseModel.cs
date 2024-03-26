namespace UsersMinimalAPI
{
    public class ResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
