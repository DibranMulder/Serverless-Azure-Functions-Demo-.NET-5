using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Messages
{
    public abstract class BaseMessage
    {
        public abstract string Type { get; }
    }
}
