using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_WPF.Stores
{
    public class MessageStore
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnMessageChanged();
            }
        }
        public event Action MessageChanged;
        private void OnMessageChanged()
        {
            MessageChanged?.Invoke();
        }
    }
}
