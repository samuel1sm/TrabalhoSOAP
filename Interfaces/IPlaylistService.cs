using System.Collections.Generic;
using System.ServiceModel;
using SoapApi.Models;
using SoapApi.Util;

namespace SoapApi.Interfaces
{
    [ServiceContract]
    public interface IPlaylistService
    {

        [OperationContract]
        Message<List<Playlist>> FindPlaylistsWithSong(string nome,string singer);
        
    }
}