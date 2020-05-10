using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace IndependenceFM
{
    public class Menu
    {
        public int CurPos = 0;
        public int CurPosWithinPlaylist = 0;
        public Player Player { get; set; }
        static Menu()
        {
        }
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
            Console.WriteLine("{0, 68}", "Press R to rescan\n");
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
                string playListName = Path.ChangeExtension(t[t.Length - 1], null);
                Console.WriteLine($" Playlist: {playListName}");
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
                    Console.WriteLine($" Artist: {tfile.Tag.Performers[0]}\n Track: {tfile.Tag.Title}");
                }    
            }
            Console.WriteLine(new String('\n', 3));
        }
        public void DrawFooter()
        {
            Console.WriteLine(new String('-', 119));
            Console.WriteLine("developed by TheSomberlain");
        }
        
        public void ScanMenu()
        {
            Console.Clear();
            DrawHeader();
            Console.WriteLine("{0,65}","Scanning...");
            Thread.Sleep(2000);
            for(int i = 0; i < Player.Playlists.Count; i++)
            {
                string CurPlaylist = Player.Playlists[i];
                string[] t = CurPlaylist.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string playListName = Path.ChangeExtension(t[t.Length - 1], null);
                Console.WriteLine($" Playlist: {playListName}");
                foreach(var item in Player.PlaylistsInDir[i])
                {
                    var tfile = TagLib.File.Create(item);
                    Console.WriteLine($"   {tfile.Tag.Performers[0]} - {tfile.Tag.Title}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press B to exit");
            DrawFooter();
            char input = '\\';
            while(input != 'b')
            {
                input = Console.ReadKey(false).KeyChar;
            }
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
    }
}
