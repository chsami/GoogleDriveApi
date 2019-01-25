using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleDriveApi.Utils
{
    public class FileHandler
    {
        private readonly DriveService _driveService;
        public FileHandler(DriveService driveService)
        {
            _driveService = driveService;
        }
        public IEnumerable<File> FetchFiles(string mimeType, int pageSize = 20)
        {
            FilesResource.ListRequest listRequest = _driveService.Files.List();

            listRequest.PageToken = Pagination.NextPageToken;
            listRequest.Q = mimeType;
            listRequest.PageSize = pageSize;
            listRequest.Fields = "nextPageToken, files(id, name, mimeType)";

            FileList execute = listRequest.Execute();
            Pagination.NextPageToken = execute.NextPageToken;
            IList<File> files = execute.Files;

            return files;
        }
    }
}
