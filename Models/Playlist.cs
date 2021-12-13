using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SoapApi.Models
{    
    [DataContract]
    public class Playlist
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public List<Music> Musics { get; set; }

        public Playlist(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Musics = new List<Music>();
        }

        public void addMusic(Music music)
        {
            Musics.Add(music);
        }
    }
}