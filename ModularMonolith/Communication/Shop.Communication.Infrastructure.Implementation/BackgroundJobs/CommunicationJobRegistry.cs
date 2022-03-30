using FluentScheduler;

namespace Shop.Communication.BackgroundJobs.BackgroundJobs
{
    public class CommunicationJobRegistry : Registry
    {
        public CommunicationJobRegistry()
        {
            //Schedule<SendEmailsJob>().NonReentrant().ToRunEvery(1).Minutes();
        }
    }
}
