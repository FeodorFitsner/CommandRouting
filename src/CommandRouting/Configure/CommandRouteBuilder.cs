﻿using System;
using System.Collections.Generic;
using CommandRouting.Handlers;
using CommandRouting.Router;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRouting.Configure
{
    /// <summary>
    /// Provide extensions to register command handlers automatically with the
    /// dependency injection container
    /// </summary>
    public class CommandRouteBuilder
    {
        private readonly IRouteBuilder _routeBuilder;

        public CommandRouteBuilder(IRouteBuilder routeBuilder)
        {
            _routeBuilder = routeBuilder;
        }

        internal void AddRoute<TRequest>(HttpVerb verb, string routeTemplate, Type[] commandHandlerTypes)
        {
            var serviceProvider = _routeBuilder.ServiceProvider;

            // Instanciate concrete instances for each handler in the command pipeline
            CommandPipeline<TRequest> pipeline = new CommandPipeline<TRequest>(verb);
            foreach (Type handlerType in commandHandlerTypes)
            {
                var handler = (ICommandHandler<TRequest>)ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
                pipeline.AddHandler(handler);
            }

            // Register a route for the command pipeline
            var constraintsResolver = serviceProvider.GetService<IInlineConstraintResolver>();
            _routeBuilder.Routes.Add(new TemplateRoute(
                new CommandRoute<TRequest>(pipeline),
                routeTemplate,
                constraintsResolver
                ));

        }
    }
}
