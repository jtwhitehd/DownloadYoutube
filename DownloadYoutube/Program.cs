/*
 * TODO
 *  - provide/populate documentation
 *  - provide error handling
 *  - provide GUI (with WindowsForms)
 *  - optimize and refactor
 *  - allow for user specified quality
 *  - allow for user specified file name
 *  - allow for user specified download directory
 *  - allow for bulk downloads
 */

using VideoLibrary;
using System.IO;

// @version 0.2.2
// @since 0.1

public class Program
{

    public static string GetUrl()
    {
        Console.Write("paste url: ");
        string url = Console.ReadLine();
        return url;

        // TODO -> * Establish an HttpClient to the URL to verify that it is valid

    }

    public static void Download(string url, string dir, int res)
    {
        try
        {
            var youtube = YouTube.Default;
            var videos = youtube.GetAllVideos(url);
            foreach (YouTubeVideo video in videos)
            {
                if (video.Resolution == res)
                {
                    Console.WriteLine("\nVideo Title: " + video.Title);
                    Console.WriteLine("Resolution: " + video.Resolution + "p");
                    Console.WriteLine("Audio bitrate: " + video.AudioBitrate);

                    Console.WriteLine("\nDownload? Y / n");
                    char userDownload = Console.ReadLine().ToLower().ElementAt(0);

                    if (userDownload == 'y')
                    {   // download video to specified directory
                        Console.WriteLine("\nAttempting download...");
                        File.WriteAllBytes(dir + video.Title + "_" + video.Resolution + "p.mp4", video.GetBytes());
                        Console.WriteLine("Download Complete.");
                        Console.WriteLine("File location: " + dir);
                    }
                    else
                    {
                        Console.WriteLine("\nDownload aborted.");
                    }
                    return; // only download 1
                }
            }
        }
        catch (System.ArgumentException e)
        {
            Console.WriteLine("\nURL provided was not a valid YouTube URL. Unable to complete download.");
        }
        catch (Exception e)
        {
            Console.WriteLine("\nDownload failed! " +
                "\nAn unknown error was encountered. If this error persist, " +
                "try restarting the program and/or test a different link.");
        }
    }
    public static void Main(string[] args)
    {
        char userLoop = 'Y';
        do
        {
            for (int i = 0; i < 20; ++i) { Console.WriteLine("\n"); }   // clear screen

            string dir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 6);
            string url = GetUrl();

            int res = 720;   // default
            Console.WriteLine("\nForce low quality? Y / n ");
            char userRes = (char)Console.ReadLine().ToLower().ElementAt(0);
            if (userRes == 'y')
            {
                res = 360;
            }

            Download(url, dir, res);    // download 720

            Console.WriteLine("\n\nExit? Y / n");
            userLoop = (char)Console.ReadLine().ToLower().ElementAt(0);
        } while (userLoop != 'y');
    }
}