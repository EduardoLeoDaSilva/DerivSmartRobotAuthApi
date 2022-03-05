using System.Net;
using AuthControl.Models;
using Newtonsoft.Json;

namespace AuthControl.Services
{
    using System.Security.Authentication;
    using System.Text.Json;
    using WebSocketSharp;
    using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

    namespace DerivSmartRobot.Services
    {



        public class DerivClient
        {


            private WebSocket _ws = null!;
            private readonly ApplicationContext _context;
            public AuthorizationResponse authorization;

            public DerivClient(ApplicationContext context)
            {
                _context = context;
            }

            public void SubscribeErroEvents()
            {
                _ws.OnError += ((sender, args) =>
                {
                    Console.WriteLine(args.Message);
                    Console.WriteLine(args.Exception.Message);

                });
            }

            public void SubscribeSucessEvents()
            {
                _ws.OnMessage += (sender, args) =>
                {
                    var response = JsonConvert.DeserializeObject<AuthorizationResponse>(args.Data);

                    if (response?.Authorize != null)
                    {
                        authorization = response;
                    }
                };

            }

            public void SetConfigurations(string appId)
            {
                _ws = new WebSocket($"wss://ws.binaryws.com/websockets/v3?app_id={appId}");
                _ws.SslConfiguration.EnabledSslProtocols = SslProtocols.None;
            }

            public void Connect()
            {
                _ws.Connect();
                SubscribeSucessEvents();
                SubscribeErroEvents();
            }

            public void Authorize(string token)
            {
                var command = new {authorize = token};
                _ws.Send(JsonConvert.SerializeObject(command));
            }
        }
    }





}
