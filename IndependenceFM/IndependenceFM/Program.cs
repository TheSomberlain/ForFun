using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace IndependenceFM
{
    class Program
    {
        public const string path = @"C:\Users\Reaper\Documents\DoDiezForFun\IndependenceFM\IndependenceFM\Music\";
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Player player = new Player();
            player.Scan(path);
            menu.Player = player;
            Random r = new Random(DateTime.Now.Millisecond);
            int rInt = r.Next(0, Player.Playlists.Count - 1);
            menu.CurPos = rInt;
            player.Play(Player.PlaylistsInDir[rInt][0]);
            while (true)
            {
                char choice = menu.DrawMenu();       
                switch (choice)
                {
                    case 'n':
                        menu.CurPosWithinPlaylist++;
                        player.Stop();
                        player.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'b':
                        menu.CurPosWithinPlaylist--;
                        player.Stop();
                        player.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'v':
                        menu.CurPos++;
                        menu.CurPosWithinPlaylist = 0;
                        player.Stop();
                        player.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'c':
                        menu.CurPos--;
                        menu.CurPosWithinPlaylist = 0;
                        player.Stop();
                        player.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'e':
                        System.Environment.Exit(0);
                        break;
                    case 'r':
                        menu.CurPos = r.Next(0, Player.Playlists.Count - 1);
                        menu.CurPosWithinPlaylist = 0;
                        player.Stop();
                        player.Clear();
                        player.Scan(path);
                        menu.ScanMenu();
                        player.Play(menu.GetCurrentTrackPath());
                        break;
                }
                Console.Clear();
            };
        }
    }
}
