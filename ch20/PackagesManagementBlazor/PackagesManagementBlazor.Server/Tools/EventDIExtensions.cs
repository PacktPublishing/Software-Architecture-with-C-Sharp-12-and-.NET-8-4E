using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.DomainLayer;
using Microsoft.Extensions.DependencyInjection;
#nullable disable
namespace DDD.ApplicationLayer
{
    public static class EventDIExtensions
    {
        private static IDictionary<Type, List<Type>> eventDictionary =
            new Dictionary<Type, List<Type>>();
        public static IServiceCollection AddEventHandler<T, H>
            (this IServiceCollection service)
            where T : IEventNotification
            where H : class, IEventHandler<T>
        {
            service.AddScoped<H>();
            List<Type> list = null!;
            eventDictionary.TryGetValue(typeof(T), out list);
            if (list == null)
            {
                list = new List<Type>();
                eventDictionary.Add(typeof(T), list);
                service.AddScoped<EventTrigger<T>>(p =>
                {
                    var handlers = new List<IEventHandler<T>>();
                    foreach (var type in eventDictionary[typeof(T)])
                    {
                        handlers.Add(p.GetService(type) as IEventHandler<T>);
                    }
                    return new EventTrigger<T>(handlers);
                });
            }
            list.Add(typeof(H));

            return service;
        }
        public static IServiceCollection AddEventMediator(this IServiceCollection service)
        {
            service.AddTransient<IEventMediator, EventMediator>();
            return service;
        }
        public static IServiceCollection AddAllEventHandlers
            (this IServiceCollection service, Assembly assembly)
        {
            var method=typeof(EventDIExtensions).GetMethod("AddEventHandler",
                BindingFlags.Static | BindingFlags.Public);

            var handlers=assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass 
                && typeof(IEventHandler).IsAssignableFrom(x));
            foreach(var handler in handlers)
            {
                var handlerInterface = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && typeof(IEventHandler).IsAssignableFrom(i))
                    .SingleOrDefault();
                if(handlerInterface != null)
                {
                    var eventType = handlerInterface.GetGenericArguments()[0];
                    method.MakeGenericMethod(new Type[] { eventType, handler })
                        .Invoke(null, new object[] { service });
                }
            }
            service.AddEventMediator();
            return service;
        }
        public static IServiceCollection AddAllCommandHandlers
            (this IServiceCollection service, Assembly assembly)
        {
            var handlers = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(ICommandHandler).IsAssignableFrom(x));
            foreach (var handler in handlers)
            {
                var handlerInterface = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && typeof(ICommandHandler).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (handlerInterface != null)
                {
                    service.AddScoped(handlerInterface, handler);
                }
            }
            return service;
        }
        public static IServiceCollection AddAllQueries
            (this IServiceCollection service, Assembly assembly)
        {
            var queries = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(IQuery).IsAssignableFrom(x));
            foreach (var query in queries)
            {
                var queryInterface = query.GetInterfaces()
                    .Where(i => !i.IsGenericType && typeof(IQuery) != i &&
                    typeof(IQuery).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (queryInterface != null)
                {
                    service.AddTransient(queryInterface, query);
                }
            }
            return service;
        }
    }
}
