using MovieApi.Shared.Events;

namespace MovieApi.Application.Common.Events;
public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}