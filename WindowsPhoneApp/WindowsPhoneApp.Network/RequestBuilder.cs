using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp.Nework
{
    public class RequestBuilder
    {
        private string url;
        private HttpVerb method;

        ActionProvider actionProvider;
        Action<WebHeaderCollection, Stream> success;
        Action<WebException> fail;

        private BodyProvider bodyProvider;



        public RequestBuilder(string url, HttpVerb method)
        {
            this.url = url;
            this.method = method;
        }



        public void Go()
        {
            /*
             * If an actionprovider has not been set, we create one.
             */
            if (this.actionProvider == null)
            {
                this.actionProvider = new SettableActionProvider(success, fail);
            }

            HttpRequest req = new HttpRequest
            {
                Url = url,
                Method = method,
                Action = actionProvider,
              
                Body = bodyProvider
            };

            req.Go();

        }





        public HttpVerb Method { get { return method; } }
        public String Url { get { return url; } }



        public RequestBuilder OnSuccess(Action<WebHeaderCollection, String> action)
        {
            this.success = (headers, stream) =>
            {
                StreamReader reader = new StreamReader(stream);
                action(headers, reader.ReadToEnd());
            };


            return this;
        }

        public RequestBuilder OnSuccess(Action<String> action)
        {
            this.success = (headers, stream) =>
            {
                StreamReader reader = new StreamReader(stream);
                action(reader.ReadToEnd());
            };
            return this;
        }


        public RequestBuilder OnSuccess(Action<WebHeaderCollection, Stream> action)
        {
            this.success = action;
            return this;
        }

        public RequestBuilder OnFail(Action<WebException> action)
        {
            fail = action;
            return this;
        }


        public RequestBuilder Form(object body)
        {
            FormBodyProvider bodyProvider = new FormBodyProvider();
            bodyProvider.AddParameters(body);

            return this.Body(bodyProvider);
        }


        public RequestBuilder Form(IDictionary<String, String> body)
        {
            FormBodyProvider bodyProvider = new FormBodyProvider();
            bodyProvider.AddParameters(body);

            return this.Body(bodyProvider);
        }

        public RequestBuilder Body(BodyProvider provider)
        {
            if (this.method == HttpVerb.Get)
            {
                throw new InvalidOperationException("Cannot set the body of a GET or HEAD request");
            }

            this.bodyProvider = provider;
            return this;
        }
    }
}
