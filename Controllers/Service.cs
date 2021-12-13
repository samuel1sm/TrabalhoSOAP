using System;
using System.Collections.Generic;
using System.Linq;
using SoapApi.Interfaces;
using SoapApi.Models;
using SoapApi.Util;

namespace SoapApi.Controllers
{
    public class Service : IUsuarioService, IMusicService
    {
        private static List<Usuario> _users;
        private static List<Music> _musics;

        public Service()
        {
            _users = new List<Usuario>
            {
                new(0, "samuel", 21),
                new(1, "Rafael", 13),
                new(2, "Igor", 42),
                new(3, "Cleber", 56)
            };

            _musics = new List<Music>
            {
                new(0, "hello", "adele"),
                new(1, "rain", "adele"),
                new(2, "noite", "estrelada"),
                new(3, "rabelo", "carlos"),
            };
        }

        public List<Usuario> GetUsuarios()
        {
            return _users;
        }

        public Message<Usuario> GetUsuario(string nome)
        {
            var exists = _users.Find(a => string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase));
            if (exists == null)
            {
                return new Message<Usuario>("Não existe");
            }

            return new Message<Usuario>("Encontrado", exists);
        }

        public Message<Usuario> CreateNewUser(string nome, int idade)
        {
            var exists = _users.Exists(a => string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase));
            if (exists)
            {
                return new Message<Usuario>("usuario já existe");
            }

            var user = new Usuario(_users.Max(a => a.Id) +1, nome, idade);
            _users.Add(user);

            return new Message<Usuario>("Criado", user);
        }

        public string DeleteUser(string nome)
        {
            var exists = _users.Find(a => string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase));
            if (exists == null)
            {
                return "Não existe";
            }

            _users.Remove(exists);
            return "Removido";
        }


        public List<Music> GetMusic()
        {
            return _musics;
        }

        public Message<Music> GetMusicByNameAndSinger(string nome, string singer)
        {
            var exists = _musics.Find(a =>
                string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.Singer, singer, StringComparison.CurrentCultureIgnoreCase));
            
            if (exists == null)
            {
                return new Message<Music>("Não existe");
            }

            return new Message<Music>("Encontrado", exists);
        }


        public Message<Music> CreateMusic(string nome, string singer)
        {
            var exists = _musics.Exists(a =>
                string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.Singer, singer, StringComparison.CurrentCultureIgnoreCase));
            
            if (exists)
            {
                return new Message<Music>("musica já existe");
            }

            var music = new Music(_musics.Count - 1, nome, singer);
            _musics.Add(music);

            return new Message<Music>("Criado", music);
        }

        public string DeleteMusic(string nome, string singer)
        {
            var exists = _musics.Exists(a =>
                string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.Singer, singer, StringComparison.CurrentCultureIgnoreCase));
            
            if (exists)
            {
                return "Não existe";
            }

            var music = new Music(_musics.Max(a => a.Id) +1, nome, singer);
            _musics.Add(music);

            return "Removido";
            
        }
    }
}