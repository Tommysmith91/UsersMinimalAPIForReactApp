﻿
namespace UsersMinimalAPI
{
    public class ResponseDataModel<T> : ResponseModel , IResponseDataModel<T> where T : class
    {
        public T Data { get; set; }
    }
}
