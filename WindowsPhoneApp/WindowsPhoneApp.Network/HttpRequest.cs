using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Nework
{
    public class HttpRequest
    {
        protected string url;
        protected HttpVerb method = HttpVerb.Get;
        protected BodyProvider body;
        protected ActionProvider action;
        protected static CookieContainer cookies = new CookieContainer();


        public String Url
        {
            set
            {
                this.url = value;
            }
            get
            {
                return this.url;
            }
        }

        public HttpVerb Method
        {
            set
            {
                this.method = value;
            }

            get
            {
                return this.method;
            }
        }

        public ActionProvider Action
        {
            set
            {
                this.action = value;
            }
            get
            {
                return this.action;
            }
        }

        public BodyProvider Body
        {
            set
            {
                this.body = value;
            }
            get
            {
                return this.body;
            }
        }



        public void Go()
        {
            MakeRequest();
        }


        protected virtual HttpWebRequest GetWebRequest(string url)
        {
            return (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
        }

        protected void MakeRequest()
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url is empty");
            }

            try
            {
                /*
                 * Create new Request
                 */
                HttpWebRequest request = this.GetWebRequest(url);
                request.CookieContainer = cookies;
                request.Method = method.ToString().ToUpper();

                if (method == HttpVerb.Get)
                {
                    ExecuteRequestWithoutBody(request);
                }
                else
                {
                    request.ContentType = body.GetContentType();
                    ExecuteRequestWithBody(request);
                }
            }
            catch (WebException webEx)
            {
                action.Fail(webEx);
            }
        }

        protected virtual void ExecuteRequestWithoutBody(HttpWebRequest request)
        {
            request.BeginGetResponse(ProcessCallback(action.Success, action.Fail), request);
        }

        protected virtual void ExecuteRequestWithBody(HttpWebRequest request)
        {
            request.BeginGetRequestStream(new AsyncCallback((IAsyncResult callbackResult) =>
            {
                HttpWebRequest tmprequest = (HttpWebRequest)callbackResult.AsyncState;

                Go(body.GetBody(), tmprequest.EndGetRequestStream(callbackResult));

                tmprequest.BeginGetResponse(ProcessCallback(action.Success, action.Fail), tmprequest);


            }), request);
        }



        protected AsyncCallback ProcessCallback(Action<WebHeaderCollection, Stream> success, Action<WebException> fail)
        {
            return new AsyncCallback((callbackResult) =>
            {
                HttpWebRequest webRequest = (HttpWebRequest)callbackResult.AsyncState;

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)webRequest.EndGetResponse(callbackResult))
                    {
                        // if (response.ContentLength > 0) { response.Headers.Add("Content-Length", response.ContentLength.ToString()); }
                        success(response.Headers, response.GetResponseStream());
                    }

                }
                catch (WebException webEx)
                {
                    fail(webEx);
                }
            });
        }

        public void Go(Stream from, Stream to)
        {
            int count = 0;
            byte[] buffer = new byte[4096];

            while ((count = from.Read(buffer, 0, 4096)) != 0)
            {

                to.Write(buffer, 0, count);
            }
        }
    }
}
