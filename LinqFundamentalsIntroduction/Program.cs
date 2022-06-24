﻿string path = @"C:\Windows";

ShowLargeFilesWithoutLinq(path);
Console.WriteLine("******");
ShowLargeFilesWithLinq(path);

void ShowLargeFilesWithLinq(string path)
{
    var query = from file in new DirectoryInfo(path).GetFiles()
                orderby file.Length descending
                select file;
    
    foreach (var file in query.Take(5))
    {
        Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
    }
}

void ShowLargeFilesWithoutLinq(string path)
{
    DirectoryInfo directory = new DirectoryInfo(path);
    FileInfo[] files = directory.GetFiles();
    Array.Sort(files, new FileInfoComparer());

    for (int i = 0; i < 5; i++)
    {
        FileInfo file = files[i];
        Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
    }
}

public class FileInfoComparer : IComparer<FileInfo>
{
    public int Compare(FileInfo? x, FileInfo? y)
    {
        return y?.Length.CompareTo(x?.Length) ?? 0;
    }
}