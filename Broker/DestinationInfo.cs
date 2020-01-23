using System;
using System.Collections.Generic;
using System.Text;

namespace Broker
{
    public class DestinationInfo
    {
        public string Destination { get; set; }
        public string HostName { get; set; }
        public Int32 Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static void Clone(DestinationInfo destinationInfo, ref DestinationInfo info)
        {
            info.Destination = destinationInfo.Destination;
            info.HostName = destinationInfo.HostName;
            info.Port = destinationInfo.Port;
            info.UserName = destinationInfo.UserName;
            info.Password = destinationInfo.Password;
        }

        public override string ToString()
        {
            return "Destination: " + Destination + "\nHostName: " + HostName +
                "\nPort: " + Port + "\nUserName: " + UserName + "\nPassword: " + Password;
        }
    }
}
