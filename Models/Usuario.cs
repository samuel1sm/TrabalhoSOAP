using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SoapApi.Models
{
    [DataContract]
    public class Usuario
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string Nome { get; set; }
        [DataMember] public int Idade { get; set; }

        [DataMember] public List<Playlist> PlayLists;

        public Usuario(int id, string nome, int idade)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            PlayLists = new List<Playlist>();
        }

        public Playlist getPlaylist(string playlistName)
        {
            return PlayLists.Find(a => string.Equals(a.Nome, playlistName, StringComparison.CurrentCultureIgnoreCase));
        }

        public void addMusicToPlaylist(string playlistName, Music music)
        {
            PlayLists.Find(a => string.Equals(a.Nome, playlistName, StringComparison.CurrentCultureIgnoreCase))
                ?.addMusic(music);
        }
    }
}