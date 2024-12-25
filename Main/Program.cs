using Main;
using Renci.SshNet;

var host = "ABC.ABC.ABC.ABC";
var port = 22;
var username = "username";
var password = "password"; // TODO: appsettings.json
var remotePath = "/Server/EdamaTech/ApplicationFiles/img.jpg";

var gloriousSftpClient = new GloriousSftpClient();

var (content, contentType) = await gloriousSftpClient.DownloadFileBytesAsync(host, username, password, remotePath);

if (content != null)
{
    // Use the byte array (content) and content type
    File.WriteAllBytes("img.jpg", content); // merge to pdf
    Console.WriteLine($"Uploaded\nContent Type: {contentType}\n");
}
else
{
    Console.WriteLine("File download failed.");
}

/*-- Upload logic below --*/
// var client = new SftpClient(host, port, username, password);
// client.Connect();


// var localPath = @"C:\Users\amjad\Downloads\img.jpg";
// var remotePath = "/Server/ApplicationFiles/img.jpg";

// using (var fileStream = new FileStream(localPath, FileMode.Open))
// {
//     try {
//         client.UploadFile(fileStream, remotePath);
//     }
//     catch(Exception exception)
//     {
//         Console.WriteLine(exception.ToString());
//     } 
// }

// client.Disconnect();
