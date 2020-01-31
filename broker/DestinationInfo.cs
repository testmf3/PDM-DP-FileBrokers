using System;

namespace broker
{
    public class DestinationInfo
    {
        
        public string hostName { get; set; }
        public Int32 port { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public static void Clone(DestinationInfo destinationInfo, ref DestinationInfo info)
        {
          
            info.hostName = destinationInfo.hostName;
            info.port = destinationInfo.port;
            info.userName = destinationInfo.userName;
            info.password = destinationInfo.password;
        }

        public override string ToString()
        {
            return "\nHostName: " + hostName +
                "\nPort: " + port + "\nUserName: " + userName + "\nPassword: " + password;
        }
    }
}
