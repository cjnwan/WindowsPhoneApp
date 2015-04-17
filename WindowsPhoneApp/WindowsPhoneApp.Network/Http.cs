using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Nework
{
    public static class Http
    {
        public static RequestBuilder Get(string url)
        {
            return new RequestBuilder(url, HttpVerb.Get);
        }

        public static RequestBuilder Post(string url)
        {
            return new RequestBuilder(url, HttpVerb.Post);
        }
    }
}
