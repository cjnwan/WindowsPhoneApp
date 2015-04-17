﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WindowsPhoneApp.Nework
{
    public class SettableActionProvider:ActionProvider
    {
        private Action<WebHeaderCollection, Stream> success;
        private Action<WebException> fail;

        private NonActionProvider nonaction = new NonActionProvider();

        public SettableActionProvider(Action<WebHeaderCollection, Stream> success, Action<WebException> fail)
        {
            if (success == null)
            {
                this.success = nonaction.Success;
            }

            if (fail == null)
            {
                this.fail = nonaction.Fail;
            }

            this.success = success;
            this.fail = fail;
        }

        public Action<WebHeaderCollection, Stream> Success
        {
            get { return success; }
        }

        public Action<WebException> Fail
        {
            get { return fail; }
        }
    }
}
