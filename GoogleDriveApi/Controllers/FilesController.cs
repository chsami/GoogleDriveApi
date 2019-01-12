using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using GoogleDriveApi.SecurityHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/**
 *
 *String pageToken = null;
do {
  FileList result = driveService.files().list()
      .setQ("mimeType='image/jpeg'")
      .setSpaces("drive")
      .setFields("nextPageToken, files(id, name)")
      .setPageToken(pageToken)
      .execute();
  for (File file : result.getFiles()) {
    System.out.printf("Found file: %s (%s)\n",
        file.getName(), file.getId());
  }
  pageToken = result.getNextPageToken();
} while (pageToken != null);
 */



namespace GoogleDriveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        // GET api/files
        [HttpGet]
        public IEnumerable<File> Get()
        {
            var service = AuthenticateHelper.Authenticate();
            FilesResource.ListRequest listRequest = service.Files.List();
            //listRequest.PageSize = 10;
            listRequest.Q = "mimeType='audio/mpeg'";
            listRequest.Fields = "nextPageToken, files(id, name, mimeType)";

            // List files.
            IList<File> files = listRequest.Execute().Files;
            return files;
        }

        // GET api/files/dowload
        [HttpGet("download")]
        public IEnumerable<File> Download()
        {
            var service = AuthenticateHelper.Authenticate();
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name, size, description)";

            // List files.
            IList<File> files = listRequest.Execute().Files;
            return files;
        }
    }
}
