
using Microsoft.AspNet.SignalR;

namespace WebCamFun.Hubs
{
    public class DashboardHub : Hub
    {
        readonly IHubContext _context = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();
        
        public void FullscreenWebCam(string camId, string message)
        {
            _context.Clients.All.fullscreenWebCam(camId, message);
        }

        public void ShowError(string error)
        {
            _context.Clients.All.showErrorMessage(error);
        }

        public void ShowMessage(string message)
        {
            _context.Clients.All.showMesage(message);
        }

        //public void TrackProgress(string jobId)
        //{
        //    Groups.Add(Context.ConnectionId, jobId);
        //}
    }
}