﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using VContainer.Internal;

[assembly: InternalsVisibleTo("VContainer.Tests")]
[assembly: InternalsVisibleTo("VContainer.StandaloneTests")]

namespace VContainer
{
    public interface IObjectResolver : IDisposable
    {
        object Resolve(Type type);
        object Resolve(IRegistration registration);
        IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null);
    }

    public static class ObjectResolverExtensions
    {
        public static T Resolve<T>(this IObjectResolver resolver) => (T)resolver.Resolve(typeof(T));
    }

    public interface IScopedObjectResolver : IObjectResolver
    {
        IObjectResolver Root { get; }
        IScopedObjectResolver Parent { get; }
        bool TryGetRegistration(Type type, out IRegistration registration);
    }

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
    }

    public enum Lifetime
    {
        Transient,
        Singleton,
        Scoped
    }

    public enum ScopeFilter
    {
        Local,
        All
    }

    public sealed class ScopedContainer : IScopedObjectResolver
    {
        public IObjectResolver Root { get; }
        public IScopedObjectResolver Parent { get; }

        readonly IRegistry registry;
        readonly ConcurrentDictionary<IRegistration, Lazy<object>> sharedInstances = new ConcurrentDictionary<IRegistration, Lazy<object>>();
        readonly CompositeDisposable disposables = new CompositeDisposable();
        readonly Func<IRegistration, Lazy<object>> createInstance;

        internal ScopedContainer(
            IRegistry registry,
            IObjectResolver root,
            IScopedObjectResolver parent = null)
        {
            this.registry = registry;
            Root = root;
            Parent = parent;
            createInstance = CreateInstance;
        }

        public object Resolve(Type type)
        {
            var registration = FindRegistration(type);
            return Resolve(registration);
        }

        public object Resolve(IRegistration registration)
        {
            switch (registration.Lifetime)
            {
                case Lifetime.Transient:
                    return registration.SpawnInstance(this);

                case Lifetime.Singleton:
                    return Root.Resolve(registration);

                case Lifetime.Scoped:
                    var lazy = sharedInstances.GetOrAdd(registration, createInstance);
                    if (!lazy.IsValueCreated && lazy.Value is IDisposable disposable)
                    {
                        disposables.Add(disposable);
                    }
                    return lazy.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null)
        {
            var containerBuilder = new ScopedContainerBuilder(Root, this);
            installation?.Invoke(containerBuilder);
            return containerBuilder.BuildScope();
        }

        public bool TryGetRegistration(Type type, out IRegistration registration)
            => registry.TryGet(type, out registration);

        public void Dispose()
        {
            disposables.Dispose();
            sharedInstances.Clear();
        }

        IRegistration FindRegistration(Type type)
        {
            IScopedObjectResolver scope = this;
            while (scope != null)
            {
                if (scope.TryGetRegistration(type, out var registration))
                {
                    return registration;
                }
                scope = scope.Parent;
            }
            throw new VContainerException($"No such registration of type: {type.FullName}");
        }

        Lazy<object> CreateInstance(IRegistration registration)
        {
            return new Lazy<object>(() => registration.SpawnInstance(this));
        }
    }

    public sealed class Container : IObjectResolver
    {
        readonly IRegistry registry;
        readonly IScopedObjectResolver rootScope;
        readonly ConcurrentDictionary<IRegistration, Lazy<object>> sharedInstances = new ConcurrentDictionary<IRegistration, Lazy<object>>();
        readonly Func<IRegistration, Lazy<object>> createInstance;

        internal Container(IRegistry registry)
        {
            this.registry = registry;
            rootScope = new ScopedContainer(registry, this);

            createInstance = CreateInstance;
        }

        public object Resolve(Type type)
        {
            if (registry.TryGet(type, out var registration))
            {
                return Resolve(registration);
            }
            throw new VContainerException($"No such registration of type: {type.FullName}");
        }

        public object Resolve(IRegistration registration)
        {
            switch (registration.Lifetime)
            {
                case Lifetime.Transient:
                    return registration.SpawnInstance(this);
                case Lifetime.Singleton:
                    return sharedInstances.GetOrAdd(registration, createInstance).Value;
                case Lifetime.Scoped:
                    return rootScope.Resolve(registration);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null)
            => rootScope.CreateScope(installation);

        public void Dispose() => rootScope.Dispose();

        Lazy<object> CreateInstance(IRegistration registration)
        {
            return new Lazy<object>(() => registration.SpawnInstance(this));
        }
    }
}