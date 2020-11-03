using NetCode2.Client.Realtime.Service;
using NetCode2.Client.Realtime.Simulation;
using NetCode2.Client.UI.Core;
using NetCode2.Client.UI.Screens;
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

        public void ConnectToRoom()
        {

        }
    }
}