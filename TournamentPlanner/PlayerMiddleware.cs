using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TournamentPlanner
{
    public class PlayerMiddleware
    {
        private readonly RequestDelegate _next;

        public PlayerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPlayerService playerService)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(playerService.Send());
        }
    }
}
