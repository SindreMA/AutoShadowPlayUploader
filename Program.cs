using System.Net;
Console.WriteLine("Watching for shadow stuff...");
void WaitForFile(string fullPath)
{
    while (true)
    {
        try
        {
            using (StreamReader stream = new StreamReader(fullPath))
            {
                break;
            }
        }
        catch
        {
            Thread.Sleep(1000);
        }
    }
}

FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = "D:\\Shadowplay\\World Of Warcraft";
     
        watcher.Filter = "*.*";
        string LastDetect = "";
        watcher.Changed += new FileSystemEventHandler((object sender, FileSystemEventArgs e) =>
        {
            if (LastDetect == e.FullPath)                
                {
                Console.WriteLine("["+ DateTime.Now.ToShortTimeString()+"]" + "New file: " + e.FullPath);
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("stream", "password here");
                    WaitForFile(e.FullPath);
                    client.UploadFile("ftp://192.168.10.106/latest.mp4", WebRequestMethods.Ftp.UploadFile, e.FullPath);
                    var clip = new TextCopy.Clipboard();
                    var link = "https://stream.sindrema.com/latest.mp4";
                    if (clip.GetText() != link)
                    {
                        clip.SetText(link);
                    }
                }
            }
            LastDetect = e.FullPath;
        });
        watcher.EnableRaisingEvents = true;


Console.ReadLine();