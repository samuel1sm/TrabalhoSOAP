using System.Runtime.Serialization;

namespace SoapApi.Util
{
    [DataContract]
    public class Message <T>
    {
        [DataMember]
        private string _message;
        [DataMember]
        private T _data;
        
        public Message(string message)
        {
            _message = message;
        }

        public Message(string message, T data)
        {
            _message = message;
            _data = data;
        }
    }
}