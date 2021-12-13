using System.Runtime.Serialization;

namespace SoapApi.Models
{    
    [DataContract]
    public class Music
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Singer { get; set; }

        public Music(int id, string nome, string singer)
        {
            Id = id;
            Nome = nome;
            Singer = singer;
        }
    }
}