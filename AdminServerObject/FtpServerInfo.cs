using System;
using System.Collections.Generic;

namespace AdminServerObject
{
    public class FtpServerInfo
    {
        public int status { get; set; } = 0;
        public int controlPort { get; set; } = 21;
        public String serverId { get; set; } = "";
        public String passiveModePortRange { get; set; } = "";
        public String description { get; set; } = "New Server";
        public bool passiveModeEnabled { get; set; } = false;
        public List<string> bindingAddresses { get; set; } = new List<string>();
        public SortedDictionary<String, FtpUserInfo> ftpUserInfoList { get; set; } = new SortedDictionary<String, FtpUserInfo>();
    }
}
