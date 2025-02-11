namespace CreationHub.Controllers;

public class AzureBlobStrategy : IAzureBlobStorage
{
    private readonly Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
    private readonly long MaxFileSize = 5 * 1024 * 1024; // 5MB

    public string UploadFromStream(Stream stream)
    {
        if (stream.Length > MaxFileSize)
        {
            throw new IOException("File is too large");
        }
        
        stream.Position = 0;
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        var key = Guid.NewGuid().ToString();
        files.Add(key, bytes);
        return key;
    }

    public Stream RetrievePicture(string key)
    {
        var picture = files[key];
        
        var stream = new MemoryStream();
        stream.Write(picture, 0, picture.Length);
        return stream;
    }
}