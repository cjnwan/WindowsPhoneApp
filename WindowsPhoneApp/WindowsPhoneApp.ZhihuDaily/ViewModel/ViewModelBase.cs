using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.ZhihuDaily.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged,IDisposable
    {
        protected ViewModelBase() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected virtual void OnDispose()
        {

        }
        public void Dispose()
        {
            this.OnDispose();
        }
    }
}
