using System;
using Renci.SshNet;

namespace Main;

public class GloriousSftpClient
{
    public async Task<(byte[] Content, string ContentType)> DownloadFileBytesAsync(
        string host,
        string username,
        string password,
        string remotePath)
    {
        try
        {
            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();

                using (var memoryStream = new MemoryStream())
                {
                    await Task.Run(() => sftp.DownloadFile(remotePath, memoryStream));
                    sftp.Disconnect();
                    return (memoryStream.ToArray(), GetContentTypeFromPath(remotePath));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading file: {ex.Message}");
            return (null, null);
        }
    }

    public async Task<(string Base64Content, string ContentType)> DownloadFileBase64Async(
        string host,
        string username,
        string password,
        string remotePath)
    {
        var (content, contentType) = await DownloadFileBytesAsync(host, username, password, remotePath);
        if (content != null)
        {
            return (Convert.ToBase64String(content), contentType);
        }
        return (null, null);
    }

    private string GetContentTypeFromPath(string path)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();
        switch (extension)
        {
            case ".pdf": return "application/pdf";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            case ".png": return "image/png";
            case ".txt": return "text/plain";
            case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Add more content types as needed
            default: return "application/octet-stream"; // Default for unknown types
        }
    }
}
