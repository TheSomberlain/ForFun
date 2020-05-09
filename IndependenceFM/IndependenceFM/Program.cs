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
            menu.Play(menu.GetCurrentTrackPath());
             while (true)
            {
                menu.DrawMenu();
                char str = Console.ReadKey(false).KeyChar;            
                switch (str)
                {
                    case 'n':
                        menu.CurPosWithinPlaylist++;
                        menu.Stop();
                        menu.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'b':
                        menu.CurPosWithinPlaylist--;
                        menu.Stop();
                        menu.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'v':
                        menu.CurPos++;
                        menu.CurPosWithinPlaylist = 0;
                        menu.Stop();
                        menu.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'c':
                        menu.CurPos--;
                        menu.CurPosWithinPlaylist = 0;
                        menu.Stop();
                        menu.Play(menu.GetCurrentTrackPath());
                        break;
                    case 'e':
                        System.Environment.Exit(0);
                        break;
                }
                Console.Clear();
            };
            Console.ReadKey();
        }
    }
}
