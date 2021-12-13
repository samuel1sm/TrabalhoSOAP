using System.Collections.Generic;
using System.ServiceModel;
using SoapApi.Models;
using SoapApi.Util;

namespace SoapApi.Interfaces
{
    [ServiceContract]
    public interface IUsuarioService
    {
        [OperationContract]
        List<Usuario> GetUsuarios();

        [OperationContract]
        Message<Usuario> GetUsuario(string nome);

        [OperationContract]
        public Message<Usuario> CreateNewUser(string nome, int idade);
        
        [OperationContract]
        public string DeleteUser(string nome);
        
        [OperationContract]
        public string AddMusicToPlaylist(string userName ,string playlistName, string musicName, string singerName);

        [OperationContract]
        public string createPlaylist(string userName, string playlistName);
        
        [OperationContract]
        public Message<List<Playlist>> getPlaylists(string userName);
        
        [OperationContract]
        public Message<Playlist> getPlaylist(string userName, string playlistName);
        
    }
}