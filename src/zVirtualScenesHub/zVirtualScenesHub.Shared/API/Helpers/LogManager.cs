using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Helpers
{
    public class LogManager : ILogManager
    {

        public void ConfigureLogging()
        {
        }
        public ILog GetLogger<T>()
        {
            return new WP7Logger<T>();
        }

    }
}
