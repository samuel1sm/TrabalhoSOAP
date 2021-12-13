using System.Collections.Generic;
using System.ServiceModel;
using SoapApi.Models;
using SoapApi.Util;

namespace SoapApi.Interfaces
{
    [ServiceContract]
    public interface IMusicService
    {
        [OperationContract]
        List<Music> GetMusic();

        [OperationContract]
        Message<Music> GetMusicByNameAndSinger(string nome,string singer);

        [OperationContract]
        public Message<Music> CreateMusic(string nome, string artista);
        
        [OperationContract]
        public string DeleteMusic(string nome, string singer);
        
    }
}