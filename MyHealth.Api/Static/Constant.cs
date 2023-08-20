namespace MyHealth.Api.Static
{
    public class Constants
    {
        public static string FileStoragePath => Path.Combine(Directory.GetCurrentDirectory(), FileStorageName);
        public static string FileStorageName =>  "file_storage";
    }
}
