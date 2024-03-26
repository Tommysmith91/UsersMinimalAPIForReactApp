namespace UsersMinimalAPI
{
    public interface IResponseDataModel<T> where T : class
    {
        public T Results { get; set; }
    }
}
