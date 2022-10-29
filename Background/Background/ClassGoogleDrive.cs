using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace GoogleDrive
{
    class ClassGoogleDrive
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveFile };
        static string ApplicationName = "Drive API .NET Quickstart";
        static string OrdnernameInDrive = "Background";
        static string RootId = "x";
        static DriveService service;
        static bool logged = false;

        public static bool IsLogged()
        {
            return logged;
        }

        public static void InitClassGoogleDrive()
        {
            UserCredential credential;
            string pfad = "C:\\BackGround\\client_secret.json";

            if (!System.IO.File.Exists(pfad))
            {
                MessageBox.Show("Die json-Datei für den GoogleDrive-Zugang konnte nicht gefunden werden!");
                return;
            }

            using (var stream =
                new FileStream(pfad, FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine("C:\\BackGround\\", ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            if (!FolderExists(service))
                CreateFolder(OrdnernameInDrive, service);

            logged = true;
        }

        private static bool FolderExists(DriveService service)
        {
            FilesResource.ListRequest request = service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder' and trashed=false";
            FileList files = request.Execute();

            if (files != null)
            {
                foreach (var file in files.Files)
                {
                    if (file.Name == OrdnernameInDrive)
                    {
                        RootId = file.Id;
                        return true;
                    }
                }
            }

            return false;
        }

        public static Dictionary<string, string> GetFiles(bool gelöschteanzeigen)
        {
            Dictionary<string, string> listdateien = new Dictionary<string, string>();



            string pageToken = null;
            do
            {
                var request = service.Files.List();


                if (gelöschteanzeigen)
                {
                    request.Q = "trashed = true";
                    //and '" + RootId + "'" + " in parents
                }
                else
                {
                    request.Q = "trashed = false";
                    //and '" + RootId + "'" + " in parents
                }



                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name)";
                request.PageToken = pageToken;
                var result = request.Execute();
                foreach (var file in result.Files)
                {
                    listdateien.Add(file.Id, file.Name);
                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);



            return listdateien;
        }

        private static void CreateFolder(string folderName, DriveService service)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            RootId = file.Id;
        }

        public static void UploadFile(string filename, string filepath, string contenttype)
        {
            var filemetadata = new Google.Apis.Drive.v3.Data.File();
            filemetadata.Name = filename;
            filemetadata.MimeType = contenttype;
            filemetadata.Parents = new List<string> { RootId };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                request = service.Files.Create(filemetadata, stream, contenttype);
                request.Upload();
            }
        }

        public static void DownloadFile(string fileId,string pfad)
        {
            var request = service.Files.Get(fileId);
            
            using (var memorystream = new MemoryStream())
            {
                request.Download(memorystream);

                using (var filestream = new FileStream(pfad, FileMode.Create, FileAccess.Write))
                {
                    filestream.Write(memorystream.GetBuffer(), 0, memorystream.GetBuffer().Length);
                }
            }            
        }
    }
}
