using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Nework
{
    public interface BodyProvider
    {
        string GetContentType();

        Stream GetBody();
    }
}
