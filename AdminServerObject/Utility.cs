using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminServerObject
{
    public static class Utility
    {
        public static bool isValidPassiveModePortRange(string input)
        {
            bool result = true;
            if (String.IsNullOrEmpty(input))
                result = false;
            else
            {
                string[] hypenPort;
                string[] ports = input.Split(',');
                foreach (string port in ports)
                {
                    if (Utility.isValidTCPPortNo(port)==-1)
                    {
                        if (port.Contains("-"))
                        {
                            hypenPort=port.Split('-');
                            if (hypenPort.Length!=2)
                            {
                                result = false;
                                break;
                            }
                            else
                            {
                                if ((Utility.isValidTCPPortNo(hypenPort[0])==-1) || (Utility.isValidTCPPortNo(hypenPort[1])==-1))
                                {
                                    result = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        public static int isValidTCPPortNo(string input)
        {
            int result;
            if (int.TryParse(input, out result))
            {
                if ((result < 0) || (result > 65535))
                    result = -1;
            }
            else
                result = -1;
            return result;
        }

    }
}
