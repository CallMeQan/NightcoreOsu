using ManagedBass;
using ManagedBass.Fx;
using System;
using System.IO;

namespace NightcoreCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hey you, give the path to your file: ");
            string path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("Bruh, invalid file...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if(Bass.Init())
            {
                // Create a stream from a file
                MediaPlayerFX mediaPlayerFX = new MediaPlayerFX();
                bool created = mediaPlayerFX.LoadAsync(path).Result;
                if (created)
                {
                    // Osu! nightcore tempo is 150% and pitch is 1.5
                    mediaPlayerFX.Tempo = 1.5;
                    mediaPlayerFX.Frequency = mediaPlayerFX.Frequency * 1.5;
                    mediaPlayerFX.Play();
                }
                // Error creating the stream
                else Console.WriteLine("Error: {0}!", Bass.LastError);

                // Wait till user presses a key
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

                // Free the stream
                mediaPlayerFX.Stop();

                // Free current device.
                Bass.Free();
            }
            else Console.WriteLine("BASS could not be initialized!");
        }
    }
}
