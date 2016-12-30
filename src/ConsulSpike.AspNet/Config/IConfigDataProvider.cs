using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsulSpike.AspNet.Config
{
    public interface IConfigDataProvider
    {
        ConfigData GetConfigData();
    }
}
