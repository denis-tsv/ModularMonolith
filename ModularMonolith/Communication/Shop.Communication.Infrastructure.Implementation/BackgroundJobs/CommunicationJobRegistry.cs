using FluentScheduler;

namespace Shop.Communication.BackgroundJobs.BackgroundJobs
{
    internal class CommunicationJobRegistry : Registry
    {
        public CommunicationJobRegistry()
        {
            //Schedule<SendEmailsJob>().NonReentrant().ToRunEvery(1).Minutes();
        }
    }
}
