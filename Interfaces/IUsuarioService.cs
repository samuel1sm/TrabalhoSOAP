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
        
    }
}