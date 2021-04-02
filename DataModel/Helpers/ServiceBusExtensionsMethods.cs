using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Helpers
{
    public static class ServiceBusExtensionsMethods
    {
        public static Message ToServiceBusMessage<T>(this T obj) where T : class
        {
            var json = JsonConvert.SerializeObject(obj);
            var message = new Message(Encoding.UTF8.GetBytes(json));
            message.MessageId = Guid.NewGuid().ToString();
            message.UserProperties.Add("Type", typeof(T).Name);
            return message;
        }
    }
}
