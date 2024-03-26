namespace UsersMinimalAPI
{
    public interface IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
