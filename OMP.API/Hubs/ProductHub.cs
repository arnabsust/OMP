using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace OMP.API.Hubs
{
    [HubName("product")]
    public class ProductHub : Hub
    {
        public void Send()
        {
            Clients.All.acceptGreet("Successfull Web Socket");
        }
    }
}