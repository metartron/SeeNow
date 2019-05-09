using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeeNow
{
    public class GameHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        public async Task AddGroup(string groupName, string username)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            await Clients.Group(groupName).RecAddGroupMsg($"{username}加入：{groupName}。");
            
        }
        public Task SendQuizToGroup(string groupName, string username, string message, string msgJSON)
        {
            return Clients.Group(groupName).ReceiveGroupQuiz(groupName, username, message, msgJSON);
        }
        
        public async Task MsgToGroup(string groupName,string msgJSON)
        {
            dynamic msg = JsonConvert.DeserializeObject(msgJSON);
            await Clients.Group((string)msg.group).FromGroupMsg(msg.group+"::"+msg.name+":"+msg.message);
        }
    }
}