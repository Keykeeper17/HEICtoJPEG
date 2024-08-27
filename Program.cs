using System;
using System.IO;
using System.Diagnostics;
using ImageMagick;

static void FileCount(Stopwatch sw, string fPath)
{
    // Fetch all .heic files from the directory and subdirectories
    string[] allfiles = Directory.GetFiles(fPath, "*.heic*", SearchOption.AllDirectories);
    int counter = 0;
    Console.WriteLine($"{allfiles.Length} files found at this directory and all subdirectories.");


    // Loop through all files
    foreach (string file in allfiles)
    {
        // Build the output file
        string outputFilePath = Path.ChangeExtension(file, ".jpeg");

        // Convert file
        Convert(file, outputFilePath);
        counter++;

        // Log update every 10 files
        if(counter % 10 == 0)
        {
            var ts = sw.Elapsed;
            string elapsedTime = $"{ts.Hours:D2}H:{ts.Minutes:D2}M:{ts.Seconds:D2}S.{ts.Milliseconds / 10:D2}MS";
            Console.WriteLine($"counter: {counter} / {allfiles.Length} at {elapsedTime}");
        }
    }

    Console.WriteLine($"{counter.ToString()} processed.");
}


static void Convert(string fpath, string fpath2)
{
    // Read and convert image file
    using var image = new MagickImage(fpath);
    image.Format = MagickFormat.Jpeg;
    image.Write(fpath2);
}


Console.WriteLine("This program will convert all .HEIC files to .JPEG and search and convert all files in the folder and any subfolders. This takes roughly 6s per 10 files...");
Console.WriteLine("Please enter the file path to the directory:");
string filePath = Console.ReadLine();

Stopwatch sw = Stopwatch.StartNew();
FileCount(sw, filePath);
sw.Stop();