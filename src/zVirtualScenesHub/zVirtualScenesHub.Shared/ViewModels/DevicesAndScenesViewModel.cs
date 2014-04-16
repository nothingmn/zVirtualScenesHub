using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using zVirtualClient;
using zVirtualClient.Configuration;
using zVirtualClient.Models;
using zVirtualScenesHub.API.Configuration;

namespace zVirtualScenesHub.ViewModels
{
    public class DevicesAndScenesViewModel : INotifyPropertyChanged
    {

        public DevicesAndScenesViewModel()
        {
            _credentialStore = new CredentialStore();
            this.SelectedCredential = _credentialStore.DefaultCredential;

        }

        private CredentialStore _credentialStore;
        private Client _client = null;
        private ObservableCollection<Device> _devices;
        private ObservableCollection<Scene> _scenes;
        private Device _selectedDevice;
        private Device _selectedScene;
        private Credential _selectedCredential;
        private string _devicesHeader = "Devices";
        private string _scenesHeader = "Scenes";

        public ObservableCollection<Device> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value; 
                OnPropertyChanged("Devices");
            }
        }

        public ObservableCollection<Scene> Scenes
        {
            get { return _scenes; }
            set
            {
                _scenes = value;
                OnPropertyChanged("Scenes");
            }
        }

        public Device SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                if (value.id != _selectedDevice.id)
                {
                    _selectedDevice = value;
                    OnPropertyChanged("SelectedDevice");
                }
            }
        }

        public Device SelectedScene
        {
            get { return _selectedScene; }
            set
            {
                if (value.id != _selectedScene.id)
                {
                    _selectedScene = value;
                    OnPropertyChanged("SelectedScene");
                }
            }
        }

        public Credential SelectedCredential
        {
            get { return _selectedCredential; }
            set
            {
                if (_selectedCredential != value)
                {
                    _selectedCredential = value;
                    OnPropertyChanged("SelectedCredential");
                }
            }
        }
        public string DevicesHeader
        {
            get { return _devicesHeader; }
            set
            {
                if (_devicesHeader != value)
                {
                    _devicesHeader = value;
                    OnPropertyChanged("DevicesHeader");
                }
            }
        }
        public string ScenesHeader
        {
            get { return _scenesHeader; }
            set
            {
                if (_scenesHeader != value)
                {
                    _scenesHeader = value;
                    OnPropertyChanged("ScenesHeader");
                }
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
