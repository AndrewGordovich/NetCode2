using NetCode2.Client.Network.Enet;
using NetCode2.Client.Realtime.Connection;
using NetCode2.Client.Realtime.Service;
using NetCode2.Client.Realtime.Simulation;
using NetCode2.Client.UI.Core;
using NetCode2.Client.UI.Screens;
using NetCode2.Common.Realtime.Data.Commands;
using UnityEngine;

namespace NetCode2.Client.Core
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private ScreensController screensController;

        private INetworkService networkService;
        
        public IClientSimulation ClientSimulation { get; private set; }

        private void Start()
        {
            var lobbyScreen = screensController.ShowScreen<LobbyScreen>();
            lobbyScreen.Setup(this);
        }

        private void Update()
        {
            networkService?.Send();
        }

        public void ConnectToRoom()
        {
            IGamePlayConnection connection = new EnetClient(new ENetClientSettings
            {
                ServerHostName = "18.185.139.165",
                ServerPort = 40002
            });

            networkService = new NetworkService(connection);
            ClientSimulation = new ClientSimulation(networkService);

            connection.Connect();
        }

        public void AddCommand(IGameCommand gameCommand) => ClientSimulation.AddCommand(gameCommand);
    }
}