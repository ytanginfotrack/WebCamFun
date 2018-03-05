using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using WebCamFun.Models;

namespace WebCamFun.Hubs
{
    public class DashboardHub : Hub, IDashboardHub
    {
        static IAmazonS3 _client;
        readonly IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();

        protected bool IsInitialized;

        protected Command LastCommand { get; set; }

        private void SwitchCam(string camId, string message)
        {
            _context.Clients.All.switchCam(camId, message);
        }

        private void ShowError(string error)
        {
            _context.Clients.All.showErrorMessage(error);
        }

        private void ShowMessage(string message)
        {
            _context.Clients.All.showMesage(message);
        }

        public void Run()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                LastCommand = GetCommand();
            }

            Task.Run(() => ListenForCommand());
        }

        public void ListenForCommand()
        {
            while (true)
            {
                var newCommand = GetCommand();

                if (newCommand.Status != LastCommand.Status)
                {
                    var message = "Switch to " + (newCommand.Status == 0 ? "Dashboard" : $"Cam{newCommand.Status}");
                    SwitchCam(newCommand.Status.ToString(), message);
                    LastCommand.Status = newCommand.Status;
                }

                Thread.Sleep(2000);
            }
        }

        private Command GetCommand()
        {
            var json = DownloadCommand();

            var command = JsonConvert.DeserializeObject<Command>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return command;
        }

        private string DownloadCommand()
        {
            const string key = "camera.json";

            using (_client = new AmazonS3Client("AKIAJMVYCNYI6XQQ5FVA", "t+pVuS8vud/aPCRiUOBR5tlhFzi4rWo2mR9+WTEQ", Amazon.RegionEndpoint.USEast1))
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = "camera-for-echo",
                    Key = key,
                };

                using (var response = _client.GetObject(request))
                {
                    var reader = new StreamReader(response.ResponseStream);
                    var text = reader.ReadToEnd();
                    return text;
                }
            }
        }
    }
}