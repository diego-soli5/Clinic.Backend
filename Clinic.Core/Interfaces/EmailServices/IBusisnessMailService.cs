
using System.Collections.Generic;

namespace Clinic.Core.Interfaces.EmailServices
{
    public interface IBusisnessMailService
    {
        void SendMail(string subject, string body, List<string> receiversMails);
    }
}
