using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace InfoSniffer
{
    public interface IPlugin
    {
        void Receive(DataSet data, string file);
    }
}
