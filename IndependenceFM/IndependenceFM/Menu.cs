using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependenceFM
{
    public class Menu
    {
        public int CurPos = 0;
        public int CurPosWithinPlaylist = 0;
        public Player Player { get; set; }
        public void DrawMenu()
        {
            DrawHeader();
            DrawControls();
            DrawPlaylist();
            DrawTrack();
            DrawFooter();
        }

        public void DrawHeader()
        {
            Console.WriteLine(new String('=', 119));
            Console.WriteLine("{0,72}", "Welcome to IndependenceFM");
            Console.WriteLine(new String('=', 119));
        }

        public void DrawControls()
        {
            Console.WriteLine();
            Console.WriteLine("{0,75}","Press N to go the next track\n");
            Console.WriteLine("{0,77}","Press B to go the previous track\n");
            Console.WriteLine("{0,77}","Press V to go the next playlist\n");
            Console.WriteLine("{0,79}", "Press C to go the previous playlist\n");
        }

        public void DrawPlaylist()
        {
            CurPos = CurPos >= Player.Playlists.Count ? 0 : CurPos;
            CurPos = CurPos < 0 ? Player.Playlists.Count - 1 : CurPos;
            if (Player.Playlists[CurPos] != null)
            {
                Console.WriteLine(new String('\n', 5));
                string str = Player.Playlists[CurPos];
                string[] t = str.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine($"Playlist: {t[t.Length - 1].Replace(".txt", "")}");
            }
        }

        public void DrawTrack()
        {
            if(Player.Playlists[CurPos] != null)
            {
                if (Player.PlaylistsInDir[CurPos].Count <= CurPosWithinPlaylist) CurPosWithinPlaylist = 0;
                if (CurPosWithinPlaylist < 0) CurPosWithinPlaylist = Player.PlaylistsInDir[CurPos].Count - 1;
                if (Player.PlaylistsInDir[CurPos] != null)
                {
                    var CurPlaylist = Player.PlaylistsInDir[CurPos];
                    var CurTrack = CurPlaylist[CurPosWithinPlaylist];
                    var tfile = TagLib.File.Create(CurTrack);
                    Console.WriteLine($"Artist: {tfile.Tag.Performers[0]}\nTrack: {tfile.Tag.Title}");
                }    
            }
        }
        public void DrawFooter()
        {
            Console.WriteLine(new String('\n', 5));
            Console.WriteLine(new String('-', 119));
            Console.WriteLine("developed by TheSomberlain");
        }

        public string GetCurrentPlaylistPath()
        {
            CurPos = CurPos >= Player.Playlists.Count ? 0 : CurPos;
            CurPos = CurPos < 0 ? Player.Playlists.Count - 1 : CurPos;
            return Player.Playlists[CurPos];
        }

        public string GetCurrentTrackPath()
        {
            CurPos = CurPos >= Player.Playlists.Count ? 0 : CurPos;
            CurPos = CurPos < 0 ? Player.Playlists.Count - 1 : CurPos;
            if (Player.PlaylistsInDir[CurPos].Count <= CurPosWithinPlaylist) CurPosWithinPlaylist = 0;
            if (CurPosWithinPlaylist < 0) CurPosWithinPlaylist = Player.PlaylistsInDir[CurPos].Count - 1;
            return Player.PlaylistsInDir[CurPos][CurPosWithinPlaylist];
        }

        public void Play(string path)
        {
            Player.Play(path);
        }

        public void Stop()
        {
            Player.Stop();
        }
    }
}
