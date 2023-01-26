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
    public class EntryPointTimingTest
    {
        readonly Lifetime _lifetime;

        public EntryPointTimingTest(Lifetime lifetime)
        {
            _lifetime = lifetime;
        }

        [Test]
        public void InstanceOf()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            Assert.That(entryPoint, Is.InstanceOf<SampleEntryPoint>());
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator InitializeCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            Assert.That(entryPoint.InitializeCalled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposeInitializeCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.InitializeCalled = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.InitializeCalled, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator PostInitializeCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            Assert.That(entryPoint.PostInitializeCalled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposePostInitializeCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.PostInitializeCalled = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.PostInitializeCalled, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator StartCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            Assert.That(entryPoint.StartCalled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposeStartCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.StartCalled = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.StartCalled, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator PostStartCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            Assert.That(entryPoint.PostStartCalled, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposePostStartCalled()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.PostStartCalled = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.PostStartCalled, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator FixedTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return new WaitForFixedUpdate();
            entryPoint.FixedTickCalls = 0;
            yield return new WaitForFixedUpdate();
            Assert.That(entryPoint.FixedTickCalls, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposeFixedTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return new WaitForFixedUpdate();
            entryPoint.FixedTickCalls = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.FixedTickCalls, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator PostFixedTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return new WaitForFixedUpdate();
            entryPoint.PostFixedTickCalls = 0;
            yield return new WaitForFixedUpdate();
            Assert.That(entryPoint.PostFixedTickCalls, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposePostFixedTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return new WaitForFixedUpdate();
            entryPoint.PostFixedTickCalls = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.PostFixedTickCalls, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator TickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.TickCalls = 0;
            yield return null;
            Assert.That(entryPoint.TickCalls, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposeTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.TickCalls = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.TickCalls, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator PostTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.PostTickCalls = 0;
            yield return null;
            Assert.That(entryPoint.PostTickCalls, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposePostTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.PostTickCalls = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.PostTickCalls, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator LateTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.LateTickCalls = 0;
            yield return null;
            Assert.That(entryPoint.LateTickCalls, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposeLateTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.LateTickCalls = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.LateTickCalls, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator PostLateTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.PostLateTickCalls = 0;
            yield return null;
            Assert.That(entryPoint.PostLateTickCalls, Is.EqualTo(1));
            lifetimeScope.Dispose();
        }

        [UnityTest]
        public IEnumerator DisposePostLateTickCalls()
        {
            var lifetimeScope = LifetimeScope.Create(builder =>
            {
                builder.RegisterEntryPoint<SampleEntryPoint>(_lifetime).AsSelf();
            });
            var entryPoint = lifetimeScope.Container.Resolve<SampleEntryPoint>();
            yield return null;
            entryPoint.PostLateTickCalls = 0;
            lifetimeScope.Dispose();
            yield return null;
            Assert.That(entryPoint.PostLateTickCalls, Is.EqualTo(0));
        }
    }
}
