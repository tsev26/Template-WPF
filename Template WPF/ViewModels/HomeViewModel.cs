using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template_WPF.Commands;
using Template_WPF.Services;
using Template_WPF.Stores;

namespace Template_WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
		private readonly MessageStore _messageStore;
        public ICommand NavigateSecondViewCommand { get; }
        public HomeViewModel(INavigationService navigateSecondViewService, MessageStore messageStore)
        {
            NavigateSecondViewCommand = new NavigateCommand(navigateSecondViewService);
			_messageStore = messageStore;
            _messageStore.MessageChanged += OnMessageChanged;
            Message = messageStore.Message;
        }
		private string _message;
		public string Message
		{
			get { return _message; }
			set
			{
                _message = value;
                _messageStore.Message = _message;
				OnPropertyChanged(nameof(Message));
			}
		}
        private void OnMessageChanged()
        {
            OnPropertyChanged(nameof(Message));
        }
        public override void Dispose()
        {
            _messageStore.MessageChanged -= OnMessageChanged;
            base.Dispose();
        }
    }
}
