using System;
using NetCode2.Client.Core;
using NetCode2.Client.UI.Core.Contracts;
using NetCode2.Client.UI.Views;
using NetCode2.Common.Realtime.Data.Commands;
using UniRx;

namespace NetCode2.Client.UI.Presenters
{
    public class LobbyScreenPresenter : IDisposablePresenter
    {
        private readonly CompositeDisposable subscriptions = new CompositeDisposable();
        private readonly Main main;
        private readonly LobbyView lobbyView;

        public LobbyScreenPresenter(Main main, LobbyView lobbyView)
        {
            this.main = main;
            this.lobbyView = lobbyView;

            Observable.FromEvent(h => lobbyView.ConnectButtonClickedEvent += h,
                    h => lobbyView.ConnectButtonClickedEvent -= h)
                .Subscribe(onNext => ConnectButtonEventHandler())
                .AddTo(subscriptions);
        }

        public void Dispose()
        {
            subscriptions.Dispose();
        }

        private void ConnectButtonEventHandler()
        {
            try
            {
                main.StartMatch();
            }
            catch (Exception e)
            {
                lobbyView.DebugText = $"{e}, \n {e.Message}, \n{e.StackTrace}";
            }
        }
    }
}