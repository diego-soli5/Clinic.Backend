using Clinic.Core.Interfaces.EmailServices;
using Clinic.Core.Options;
using Microsoft.Extensions.Options;

namespace Clinic.Infrastructure.EmailServices
{
    public class BusisnessMailService : MasterMailServer, IBusisnessMailService
    {
        public BusisnessMailService(IOptions<EmailServiceOptions> options)
            : base(options)
        { }

    }
}
