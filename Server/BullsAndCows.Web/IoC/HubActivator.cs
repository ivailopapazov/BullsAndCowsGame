namespace BullsAndCows.Web.IoC
{
    using Microsoft.AspNet.SignalR.Hubs;
    using Ninject;

    public class HubActivator : IHubActivator
    {
        private readonly IKernel container;

        public HubActivator(IKernel container)
        {
            this.container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)container.Get(descriptor.HubType);
        }
    }
}