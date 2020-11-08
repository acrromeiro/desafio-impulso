using System;

namespace desafio_impulso_dotnet.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string msg):base(msg){}
        
    }
}