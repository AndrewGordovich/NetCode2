using NetCode2.Client.Core;
using NetCode2.Client.UI.Core;
using NetCode2.Client.UI.Presenters;
using NetCode2.Client.UI.Views;
using UnityEngine;

namespace NetCode2.Client.UI.Screens
{
    public class LobbyScreen : BaseScreen
    {
        [SerializeField]
        private LobbyView lobbyView = null;

        private LobbyScreenPresenter lobbyScreenPresenter;

        public void Setup(Main main)
        {
            lobbyScreenPresenter = new LobbyScreenPresenter(main, lobbyView);
            AddDisposablePresenter(lobbyScreenPresenter);
        }
    }
}