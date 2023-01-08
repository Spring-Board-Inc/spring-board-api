﻿namespace Entities.ErrorModel
{
    public class ResponseDetails
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }

    public class ResponseMessage
    {
        public string Message { get; set; }
    }
}
