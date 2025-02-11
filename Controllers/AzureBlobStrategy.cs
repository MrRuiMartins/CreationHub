namespace CreationHub.Controllers;

public class AzureBlobStrategy : IAzureBlobStorage
{
    private readonly Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
    
    public string UploadFromStream(Stream stream)
    {
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