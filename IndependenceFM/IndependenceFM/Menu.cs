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
        private List<Control> _controls;
        private int _index = 0;
        public Player Player { get; set; }
        public Menu()
        {
            _controls = new List<Control>()
            {
                new Control()
                {
                    Value = "Press N to go the next track\n",
                    Space = "{0,75}",
                    Letter = 'n'
                },
                new Control()
                {
                    Value = "Press B to go the previous track\n",
                    Space = "{0,77}",
                    Letter = 'b'
                },
                new Control()
                {
                    Value = "Press V to go the next playlist\n",
                    Space = "{0,77}",
                    Letter = 'v'
                },
                new Control()
                {
                    Value = "Press C to go the previous playlist\n",
                    Space = "{0,79}",
                    Letter = 'c'
                },
                new Control()
                {
                    Value = "Press R to rescan\n",
                    Space = "{0,68}",
                    Letter = 'r'
                },
            };
        }
        public char DrawMenu()
        {
            DrawHeader();
            DrawControls();
            DrawPlaylist();
            DrawTrack();
            DrawFooter();
            char choice = SwitchKey();
            return choice;
        }

        public void DrawHeader()
        {
            Console.WriteLine(new String('=', 119));
            Console.WriteLine("{0,72}", "Welcome to IndependenceFM");
            Console.WriteLine(new String('=', 119));
        }

        public void DrawControls()
        {
            for (int i = 0; i < _controls.Count; i++)
            {
                if (_index == i)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(_controls[i].Space, _controls[i].Value);
                }
                else
                {
                    Console.WriteLine(_controls[i].Space, _controls[i].Value);
                }
                Console.ResetColor();
            }
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
            Console.WriteLine(new String('\n', 4));
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

        public char SwitchKey()
        {
            ConsoleKeyInfo ckey = Console.ReadKey(false);

            switch (ckey.Key)
            {
                case ConsoleKey.N:
                    return 'n';

                case ConsoleKey.B:
                    return 'b';

                case ConsoleKey.V:
                    return 'v';

                case ConsoleKey.C:
                    return 'c';

                case ConsoleKey.E:
                    return 'e';

                case ConsoleKey.R:
                    return 'r';

                case ConsoleKey.DownArrow:
                    _index = _index == _controls.Count - 1 ? _index = 0 : ++_index;
                    break;

                case ConsoleKey.UpArrow:
                    _index = _index <= 0 ? _index = _controls.Count - 1 : --_index;
                    break;
                case ConsoleKey.Enter:
                    return _controls[_index].Letter;
            }
            return '\0';
        }
    }
}
