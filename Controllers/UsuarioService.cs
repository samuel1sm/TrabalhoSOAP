using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SoapApi.Interfaces;
using SoapApi.Models;
using SoapApi.Util;

namespace SoapApi.Controllers
{
    
    public class UsuarioService : IUsuarioService
    {
        private static List<Usuario> _usuarios;

        public UsuarioService()
        {
            _usuarios = new List<Usuario>
            {
                new (0, "samuel", 21),
                new (1, "Rafael", 13),
                new (2, "Igor", 42),
                new (3, "Cleber", 56)
            };
        }
        
        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public Message<Usuario> GetUsuario(string nome)
        {
            var exists = _usuarios.Find(a => string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase));
            if (exists == null)
            {
                return new Message<Usuario>("Não existe");
            }
            
            return new Message<Usuario>("Encontrado", exists);
        }

        public Message<Usuario> CreateNewUser(string nome, int idade)
        {
            var exists = _usuarios.Exists(a =>  string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase));
            if (exists)
            {
                return new Message<Usuario>("usuario já existe");
            }

            var user = new Usuario(_usuarios.Count - 1, nome, idade);
            _usuarios.Add(user);
            
            return new Message<Usuario>("Criado", user);
        }

        public string DeleteUser(string nome)
        {
            var exists = _usuarios.Find(a =>  string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase));
            if (exists == null)
            {
                return "Não existe";
            }

            _usuarios.Remove(exists);
            return "Removido";
        }

    }
}