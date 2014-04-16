using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zVirtualClient.Storage
{
    interface IStoreage
    {        		
		Task<bool> Save(string file, System.IO.MemoryStream contents);
		Task<string> Load(string file);
        Task<bool> FileExists(string file);
    }
}
