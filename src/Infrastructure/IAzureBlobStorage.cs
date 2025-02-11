namespace CreationHub.Controllers;

public interface IAzureBlobStorage
{
    string UploadFromStream(Stream stream);

    Stream RetrievePicture(String url);
}