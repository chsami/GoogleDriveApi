using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleDriveApi.SecurityHelper
{
    public interface IAuthenticationHelper
    {
        DriveService Authenticate();
    }
}
