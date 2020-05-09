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
                if (item.EndsWith(".m3u")) Playlists.Add(item);
                if (item.EndsWith(".aimppl4")) Playlists.Add(item);
            };

            int i = 0;
            foreach (var item in Playlists)
            {
                if (item.EndsWith(".txt"))
                    PlaylistsInDir.Add(i, ParseDirectoriesFromPlaylist(item).ToList());
                if (item.EndsWith(".m3u"))
                    PlaylistsInDir.Add(i, ParseDirectoriesFromM3UPlaylist(item).ToList());
                if (item.EndsWith(".aimppl4"))
                    PlaylistsInDir.Add(i, ParseDirectoriesFromAIMPPlaylist(item).ToList());
                i++;
            }
        }

        public IEnumerable<string> ParseDirectoriesFromPlaylist(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines;
        }

        public IEnumerable<string> ParseDirectoriesFromM3UPlaylist(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<string> parsed = lines.ToList();
            parsed.RemoveAt(0);
            for(int i = 1; i < parsed.Count; i += 2)
            {
                i--;
                parsed.RemoveAt(i);
            }
            return parsed;
        }

        public IEnumerable<string> ParseDirectoriesFromAIMPPlaylist(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<string> parsed = lines.ToList();
            for(int i = 0; i < parsed.Count; i++)
            {
                if (parsed[i] != "#-----CONTENT-----#") parsed.RemoveAt(i);
                if (parsed[i] == "#-----CONTENT-----#") break;
                i--;
            }
            parsed.RemoveAt(0);
            parsed.RemoveAt(0);
            for(int i = 0; i < parsed.Count; i++)
            {
                int index = parsed[i].IndexOf('|');
                parsed[i] = parsed[i].Substring(0, index);
            }
            return parsed;
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
