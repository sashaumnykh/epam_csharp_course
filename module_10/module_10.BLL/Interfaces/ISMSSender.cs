using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace module_10.BLL.Interfaces
{
    public interface ISMSSender
    {
        void SendSMS(string toEmail, string text);
    }
}
