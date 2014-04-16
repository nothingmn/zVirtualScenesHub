
using System;
using System.Threading.Tasks;
using zVirtualClient.Helpers.Serialization;

namespace zVirtualClient
{
    public class CredentialStore
    {
        private Configuration.IConfigurationReader ConfigurationReader;
        public CredentialStore(Configuration.IConfigurationReader configurationReader = null)
        {
            ConfigurationReader = configurationReader;
            Init();
        }

        private void Init()
        {
            if (ConfigurationReader == null)
            {
                ConfigurationReader = new zVirtualScenesHub.API.Configuration.WindowsStorageConfigurationReader();
            }
            try
            {
                Credentials = new Credentials();
                var profilesIndex =  ConfigurationReader.ReadSetting<string>("Profiles").Result;
                if (!string.IsNullOrEmpty(profilesIndex))
                {
                    Credentials = NewtonSerializer<Credentials>.FromJSON<Credentials>(profilesIndex);                    
                    foreach (var c in Credentials)
                    {
                        if (c.Default)
                        {
                            DefaultCredential = c;
                            break;
                        }
                    }
                }

            }
            catch (Exception)
            {
            }
            
        }

        public Credential DefaultCredential { get; set; }
        public Credentials Credentials { get; set; }

        public Credential Find(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Credential found = null;
                foreach (var c in this.Credentials)
                {
                    if (c.Name == Name)
                    {
                        return c;
                        break;
                    }
                }
            }
            return null;
        }
        public async void Save()
        {
            try
            {
                var payload = zVirtualClient.Helpers.Serialization.NewtonSerializer<Credentials>.ToJSON(Credentials);
                if (!string.IsNullOrEmpty(payload))
                {
                    await ConfigurationReader.WriteSetting("Profiles", payload);
                }
            }
            catch (Exception e) 
            {
                    
                throw;
            }
        }
        public void UpdateCredential(string Name, Credential NewCredential)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Credential updateMe = null;
                foreach (var c in this.Credentials)
                {
                    if (c.Name == Name)
                    {
                        updateMe = c;
                        
                    }

                }
                if(updateMe!=null)
                {
                    updateMe.Default = NewCredential.Default;
                    updateMe.Domain = NewCredential.Domain;
                    updateMe.Host = NewCredential.Host;
                    updateMe.Password = NewCredential.Password;
                    updateMe.Port = NewCredential.Port;
                    updateMe.Username = NewCredential.Username;
                    updateMe.Name = NewCredential.Name;
                    if (updateMe.Default)
                    {
                        this.DefaultCredential = updateMe;
                        foreach (var c in this.Credentials)
                        {
                            if (c.Name != Name) c.Default = false;

                        }
                    }

                    this.Save();
                }
            }
        }

        public void SetDefault(string Name)
        {
            foreach (var c in this.Credentials)
            {
                if(c.Name == Name)
                {
                    this.DefaultCredential = c;
                    this.DefaultCredential.Default = true;
                } else
                {
                    c.Default = false;
                }
            }
            Save();
        }
        public void AddCredential(Credential Credential, bool MakeDefault = true)
        {
            if (Credential != null)
            {
                Credential.Default = false;
                if (MakeDefault)
                {
                    foreach (var c in this.Credentials)
                    {
                        c.Default = false;
                    }
                    Credential.Default = true;
                }
                this.Credentials.Add(Credential);
                this.Save();
            }
        }
        public void RemoveCredential(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Credential deleteMe = null;
                foreach (var c in this.Credentials)
                {
                    if (c.Name == Name)
                    {
                        deleteMe = c;
                        break;
                    }
                }
                if (deleteMe != null)
                {
                    this.Credentials.Remove(deleteMe);
                    if (deleteMe.Default)
                    {
                        DefaultCredential = null;
                    }
                    else
                    {
                        this.Save();
                    }
                }

            }
        }
    }
}