using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace aspnetcore3_demo {
   // [Authorize]
    public class CountHub : Hub {
        private readonly CountService countService;

        public CountHub (CountService countService) {
            this.countService = countService;
        }

        public async Task GetLatestCount (string random) {
            //var user = Context.User.Identity.Name;
            int count;
            do {
                count = countService.GetLstestCount ();
                Thread.Sleep (1000);
                await Clients.All.SendAsync ("ReceiveUpdate", count);
            }
            while (count < 10);
            await Clients.All.SendAsync ("Finished");
        }
        /// <summary>
        /// 连接调用客户端方法
        /// </summary>
        /// <returns></returns>
        //public override async Task OnConnectedAsync () {
            // //获取连接到客户端的HUB唯一连接标识
            // var connectionId = Context.ConnectionId;
            // //通过ID获得一个客户端
            // var client = Clients.Client (connectionId);
            // //调用客户端方法someFunc
            // await client.SendAsync ("someFunc", new { });

            // //排除connectionId客户端标识,其他客户端在Clients上面调用客户端方法otherFunc
            // await Clients.AllExcept (connectionId).SendAsync ("otherFunc");

            // //向MyGroup分组添加connectionId标识的客户端
            // await Groups.AddToGroupAsync (connectionId, "MyGroup");

            // //移除MyGroup分组
            // // await Groups.RemoveFromGroupAsync(connectionId,"MyGroup");

            // //调用MyGroup分组上的客户端的someFunc方法
            // await Clients.Groups ("MyGroup").SendAsync ("someFunc");
       // }
    }
}
