using System;
using System.Collections.Generic;
using System.Linq;
using SoapApi.Interfaces;
using SoapApi.Models;
using SoapApi.Util;

namespace SoapApi.Controllers
{
    public class Service : IUsuarioService, IMusicService, IPlaylistService
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

            var playlist1 = new Playlist(0, "a1");
            var playlist2 = new Playlist(1, "a2");
            var playlist3 = new Playlist(2, "b1");
            var playlist4 = new Playlist(3, "c1");

            playlist1.addMusic(_musics[0]);
            playlist1.addMusic(_musics[1]);

            playlist2.addMusic(_musics[2]);
            playlist2.addMusic(_musics[3]);

            playlist3.addMusic(_musics[0]);

            playlist4.addMusic(_musics[0]);
            playlist4.addMusic(_musics[3]);

            _users[0].PlayLists.Add(playlist1);
            _users[0].PlayLists.Add(playlist2);
            _users[1].PlayLists.Add(playlist3);
            _users[2].PlayLists.Add(playlist4);
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

            var user = new Usuario(_users.Max(a => a.Id) + 1, nome, idade);
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

        public string AddMusicToPlaylist(string userName, string playlistName, string musicName, string singerName)
        {
            var music = _musics.Find(a =>
                string.Equals(a.Nome, musicName, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.Singer, singerName, StringComparison.CurrentCultureIgnoreCase));

            if (music == null)
            {
                return "Musica não existe";
            }

            var user = _users.Find(a => string.Equals(a.Nome, userName, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
            {
                return "Usuario não existe";
            }

            var playList = user.PlayLists.Exists(a =>
                string.Equals(a.Nome, playlistName, StringComparison.CurrentCultureIgnoreCase));
            if (!playList)
            {
                return "Playlist não existe";
            }

            user.addMusicToPlaylist(playlistName, music);

            return "adicionado";
        }

        public string createPlaylist(string userName, string playlistName)
        {
            var user = _users.Find(a => string.Equals(a.Nome, userName, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
            {
                return "Usuario não existe";
            }

            var playlist = user.getPlaylist(playlistName);
            if (playlist != null)
            {
                return "Playlist existe";
            }

            var id = user.PlayLists.Max(a => a.Id) + 1;

            user.PlayLists.Add(new Playlist(id, playlistName));
            return "Criado";
        }

        public Message<List<Playlist>> getPlaylists(string userName)
        {
            var exists = _users.Find(a => string.Equals(a.Nome, userName, StringComparison.CurrentCultureIgnoreCase));
            if (exists == null)
            {
                return new Message<List<Playlist>>("Usuario não existe");
            }

            return new Message<List<Playlist>>("Encontrado", exists.PlayLists);
        }

        public Message<Playlist> getPlaylist(string userName, string playlistName)
        {
            var user = _users.Find(a => string.Equals(a.Nome, userName, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
            {
                return new Message<Playlist>("Usuario não existe");
            }

            var playlist = user.getPlaylist(playlistName);
            if (playlist == null)
            {
                return new Message<Playlist>("Playlist não existe");
            }

            return new Message<Playlist>("Encontrado", playlist);
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

            if (!exists)
            {
                return "Não existe";
            }

            var music = new Music(_musics.Max(a => a.Id) + 1, nome, singer);
            _musics.Add(music);

            return "Removido";
        }

        public Message<List<Playlist>> FindPlaylistsWithSong(string nome, string singer)
        {
            var music = _musics.Find(a =>
                string.Equals(a.Nome, nome, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(a.Singer, singer, StringComparison.CurrentCultureIgnoreCase));

            if (music == null)
            {
                return new Message<List<Playlist>>("Musica errada");
            }

            var result = _users.SelectMany(a => a.PlayLists.Where(b => b.Musics.Contains(music))).ToList();
            return new Message<List<Playlist>>("Encontrados", result);
        }
    }
}