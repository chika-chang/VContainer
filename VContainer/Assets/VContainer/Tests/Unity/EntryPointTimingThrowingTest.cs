using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VContainer.Unity;

namespace VContainer.Tests.Unity
{
    [TestFixture(Lifetime.Singleton)]
    [TestFixture(Lifetime.Scoped)]
    [TestFixture(Lifetime.Transient)]
    public class EntryPointTimingThrowingTest
    {
        readonly Lifetime _lifetime;

        public EntryPointTimingThrowingTest(Lifetime lifetime)
        {
            _lifetime = lifetime;
        }

        [UnityTest]
        public IEnumerator InitializableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<InitializableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator PostInitializableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<PostInitializableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator StartableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<StartableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator PostStartableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<PostStartableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator FixedTickableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<FixedTickableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return new WaitForFixedUpdate();
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator PostFixedTickableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<PostFixedTickableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return new WaitForFixedUpdate();
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator TickableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<TickableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator PostTickableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<PostTickableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator LateTickableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<LateTickableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator PostLateTickableExceptionHandler()
        {
            var handled = 0;
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<PostLateTickableThrowable>(_lifetime).AsSelf();
                builder.RegisterEntryPointExceptionHandler(ex => { handled += 1; });
            });
            yield return null;
            if (_lifetime == Lifetime.Transient)
                Assert.That(handled, Is.EqualTo(0));
            else
                Assert.That(handled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }
    }
}
