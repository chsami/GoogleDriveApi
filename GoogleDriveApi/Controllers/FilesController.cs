using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using GoogleDriveApi.SecurityHelper;
using GoogleDriveApi.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace GoogleDriveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        public FilesController(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }
        // GET api/files
        [HttpGet]
        public IEnumerable<File> Get()
        {
            var service = _authenticationHelper.Authenticate();
            var fileHandler = new FileHandler(service);
            var files = fileHandler.FetchFiles("mimeType='audio/mpeg'");
            return files;
        }

        // GET api/files/dowload
        [HttpGet("download")]
        public IEnumerable<File> Download()
        {
            var service = _authenticationHelper.Authenticate();
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name, size, description)";

            // List files.
            IList<File> files = listRequest.Execute().Files;
            return files;
        }
    }
}
