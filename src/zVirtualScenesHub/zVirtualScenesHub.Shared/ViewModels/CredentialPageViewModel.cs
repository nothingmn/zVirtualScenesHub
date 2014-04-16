using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using zVirtualClient;

namespace zVirtualScenesHub.ViewModels
{
    public class CredentialPageViewModel : INotifyPropertyChanged
    {

        CredentialStore store = new CredentialStore();

        public CredentialPageViewModel()
        {
            saved = false;
            Credential = store.DefaultCredential;
            if (Credential == null)
            {
                Credential = new Credential();
                Credential.Default = true;
                Credential.Host = "http://www.myhouse.com";
                Credential.Port = 80;
                Credential.Password = "mypassword";
            }
        }

        public string Host
        {
            get { return string.Format("{0}:{1}", Credential.Host, Credential.Port); }
            set
            {
                System.Uri uri;
                if (Uri.TryCreate(value, UriKind.Absolute, out uri))
                {
                    Credential.Host = uri.Host;
                    Credential.Port = uri.Port;
                    OnPropertyChanged("Host");
                }
            }
        }
        public string Password
        {
            get { return Credential.Password; }
            set
            {
                Credential.Password = value;                
                OnPropertyChanged("Password");
            }
        }

        private bool saved = false;
        public async Task Save()
        {
            if (!saved)
            {
                await Task.Run(() =>
                {
                    store.Credentials = new Credentials();
                    store.Credentials.Add(this.Credential);
                    store.Save();
                    saved = true;
                });
            }
        }

        public Credential Credential { get; set; }

        public string AppHeaderName { get; set; }
        public string PageTitle { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
