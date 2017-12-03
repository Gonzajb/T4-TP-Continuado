using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TallerIV.Negocio.Servicios;
using TallerIV.Dominio;
using TallerIV.Datos;
using TallerIV.Dominio.Chat;

namespace TallerIV.SignalRHubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string encuentro_id, string userid)
        {
            TallerIVDbContext dbContext = new TallerIVDbContext();
            BaseService<Mensaje> mensajesService = new BaseService<Mensaje>(dbContext);
            mensajesService.AddEntity(new Mensaje(message, userid, int.Parse(encuentro_id)));
            Clients.Group(encuentro_id).send(name, message, userid);
        }
        public void JoinRoom(string encuentro_id)
        {
            Groups.Add(Context.ConnectionId, encuentro_id);
        }

        public void LeaveRoom(string encuentro_id)
        {
            Groups.Remove(Context.ConnectionId, encuentro_id);
        }
    }
}