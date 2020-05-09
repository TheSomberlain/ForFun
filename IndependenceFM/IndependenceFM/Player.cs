using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WMPLib;

namespace IndependenceFM
{
    public class Player
    {
        public List<string> Playlists { get; set; }
        public Dictionary<int, List<string>> PlaylistsInDir { get; set; }
        private WindowsMediaPlayer _wplayer;

        public Player()
        {
            Playlists = new List<string>();
            PlaylistsInDir = new Dictionary<int, List<string>>();
            _wplayer = new WindowsMediaPlayer();
        }
        public void Scan(string path)
        {
            var t = Directory.GetFiles(path);
            foreach(var item in t)
            {
                if (item.EndsWith(".txt")) Playlists.Add(item);  
            };

            int i = 0;
            foreach (var item in Playlists)
            {
                PlaylistsInDir.Add(i, ParseDirectoriesFromPlaylist(item).ToList());
                i++;
            }
        }

        public IEnumerable<string> ParseDirectoriesFromPlaylist(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines;
        }

        public void Play(string path)
        {
            _wplayer.URL = path;
            _wplayer.controls.play();
        }

        public void Stop()
        {
            _wplayer.controls.stop();
        }
    }
}
