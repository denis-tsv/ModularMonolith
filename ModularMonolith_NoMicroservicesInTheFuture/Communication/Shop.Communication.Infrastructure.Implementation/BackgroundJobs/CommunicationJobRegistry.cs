using FluentScheduler;

namespace Shop.Communication.Infrastructure.Implementation.BackgroundJobs
{
    public class CommunicationJobRegistry : Registry
    {
        public CommunicationJobRegistry()
        {
            //Schedule<SendEmailsJob>().NonReentrant().ToRunEvery(1).Minutes();
        }
    }
}
