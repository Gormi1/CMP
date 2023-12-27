using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMP.UnitTest
{
    public interface IAlertDisplay
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<AlertInfo> GetLastAlertInfo();  // Nuevo método para obtener información del último alert
    }

    public class AlertInfo
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string CancelButton { get; set; }
    }
    internal class MockNavigation : INavigation, IAlertDisplay
    {
        private int _pushAsyncCallCount = 0;
        private int _displayAlertCallCount = 0;

        public IReadOnlyList<Page> ModalStack => throw new NotImplementedException();

        public IReadOnlyList<Page> NavigationStack => throw new NotImplementedException();

        public int PushAsyncCalls => _pushAsyncCallCount;
        public int DisplayAlertCalls => _displayAlertCallCount;


        public string Title { get; private set; }
        public string Message { get; private set; }
        public string CancelButton { get; private set; }
        
        public Task PushAsync(Page page)
        {
            _pushAsyncCallCount++;
            return Task.CompletedTask;
        }


        public void InsertPageBefore(Page page, Page before)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            _pushAsyncCallCount = 0; // Reiniciar el contador al hacer pop hasta la raíz
            return Task.CompletedTask;
        }

        public Task PopToRootAsync(bool animated)
        {
            _pushAsyncCallCount = 0;
            return Task.CompletedTask;
        }

        public Task PushAsync(Page pag, bool animated)
        {
            _pushAsyncCallCount++;
            return Task.CompletedTask;
        }

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        public void RemovePage(Page page)
        {
            throw new NotImplementedException();
        }

        // Método adicional para verificar cuántas veces se llamó PushAsync
        public int GetPushAsyncCallCount()
        {
            return _pushAsyncCallCount;
        }
        private AlertInfo _lastAlertInfo;

        public Task<AlertInfo> GetLastAlertInfo()
        {
            return Task.FromResult(_lastAlertInfo);
        }

        public Task DisplayAlert(string title, string message, string cancel)
        {
            _displayAlertCallCount++;
            _lastAlertInfo = new AlertInfo { Title = title, Message = message, CancelButton = cancel };
            return Task.CompletedTask;
        }
    }
}
