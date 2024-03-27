namespace UsersMinimalAPI
{
    public interface IResponseDataModel<T> : IResponseModel
    {
        public T Data { get; set; }
    }
}
