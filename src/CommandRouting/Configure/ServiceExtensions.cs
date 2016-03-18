﻿using System;
using System.Linq;
using System.Reflection;
using CommandRouting.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CommandRouting.Services
{
    /// <summary>
    /// Provide extensions to register command handlers automatically with the
    /// dependency injection container
    /// </summary>
    public static class ServiceExtensions
    {
        public static void AddCommandHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            // If no assemblies were passed explicitly then we'll hunt for command handlers in all assemblies
            if (assemblies.Length == 0)
                assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Track down anything implementing ICommandHandler<TRequest>
            var handlerInterfaceType = typeof(ICommandHandler<>);
            var commandHandlers = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => handlerInterfaceType.IsAssignableFrom(x));

            // Register the concrete command handlers with the equivalent interface type
            foreach (Type handler in commandHandlers)
            {
                // TODO
            }
        }
    }
}
