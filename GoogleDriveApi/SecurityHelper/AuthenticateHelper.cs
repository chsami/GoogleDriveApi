using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleDriveApi.SecurityHelper
{
    public static class AuthenticateHelper
    {
        static string ApplicationName = "YOURAPPLICATIONNAME";
        static string RefreshToken = "INSERTHERE";

        public static DriveService Authenticate()
        {
            var token = new TokenResponse
            {
                RefreshToken = RefreshToken
            };
            UserCredential credentials;
            using (var stream = File.OpenRead("credentials.json"))
            {
                credentials = new UserCredential(new GoogleAuthorizationCodeFlow(
                    new GoogleAuthorizationCodeFlow.Initializer()
                    {
                        ClientSecretsStream = stream,
                        Scopes = new List<string>()
                        {
                            DriveService.Scope.Drive
                        }
                    }), Environment.UserName, token);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = ApplicationName,
            });
            return service;
        }
    }
}
