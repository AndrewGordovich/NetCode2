using System.Collections.Generic;
using NetCode2.Client.UI.Core.Contracts;
using UnityEngine;

namespace NetCode2.Client.UI.Core
{
    public class BaseScreen : MonoBehaviour
    {
        private readonly IList<IDisposablePresenter> disposablePresenters = new List<IDisposablePresenter>();

        public void Close()
        {
            Destroy(gameObject);
        }

        protected void AddDisposablePresenter(IDisposablePresenter disposablePresenter)
        {
            disposablePresenters.Add(disposablePresenter);
        }

        private void OnDestroy()
        {
            DisposePresenters();
        }

        private void DisposePresenters()
        {
            foreach (var presenter in disposablePresenters)
            {
                presenter.Dispose();
            }

            disposablePresenters.Clear();
        }
    }
}