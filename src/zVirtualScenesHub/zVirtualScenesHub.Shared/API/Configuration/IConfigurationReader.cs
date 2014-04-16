using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zVirtualClient.Configuration
{
    public interface IConfigurationReader
    {
        Task<T> ReadSetting<T>(string Key);
        Task WriteSetting<T>(string Key, T Value);
    }
}
