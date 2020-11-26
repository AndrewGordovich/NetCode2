using NetCode2.Client.Networking;
using NetCode2.Client.UI.Core;
using NetCode2.Client.UI.Screens;
using UnityEngine;

namespace NetCode2.Client.Core
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private RealtimeRunBehavior runBehavior;

        [SerializeField]
        private ScreensController screensController;

        private void Start()
        {
            var lobbyScreen = screensController.ShowScreen<LobbyScreen>();
            lobbyScreen.Setup(this);
        }

        public void StartMatch()
        {
            runBehavior.Connect();
        }
    }
}