using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;
using System.Linq.Expressions;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    // ReSharper disable InvokeAsExtensionMethod
    
    public class OperableExtensionTests
    {
        private IObjectBuilder<MyClass> objectBuilder;
        private Func<MyClass, float> func;
        private Expression<Func<MyClass, int>> propertyExpression;
        private IDeclaration<MyClass> operable;

        private class MyDeclaration : IDeclaration<MyClass>, IOperable<MyClass>
        {
            public void Construct()
            {
            }

            public void CallFunctions(IList<MyClass> masterList)
            {
                throw new NotImplementedException();
            }

            public void AddToMaster(MyClass[] masterList)
            {
                throw new NotImplementedException();
            }

            public int NumberOfAffectedItems { get; }
            public IList<int> MasterListAffectedIndexes { get; }
            public int Start { get; }
            public int End { get; }
            public IListBuilderImpl<MyClass> ListBuilderImpl { get; }
            public IObjectBuilder<MyClass> ObjectBuilder { get; }
            public BuilderSettings BuilderSettings { get; set; }
            public IList<MyClass> Build()
            {
                throw new NotImplementedException();
            }

            public IOperable<MyClass> All()
            {
                throw new NotImplementedException();
            }
        }

        public OperableExtensionTests()
        {
            objectBuilder = Substitute.For<IObjectBuilder<MyClass>>();

            operable = Substitute.For<IDeclaration<MyClass> , IOperable<MyClass>>(); // typeof(IOperable<MyClass>)
            func = x => x.Float = 1f;
            propertyExpression = x => x.IntGetterOnly;
        }

        [Fact]
        public void ShouldBeAbleToUseWith()
        {
            var builderSetup = new BuilderSettings();

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(func);
            }

            OperableExtensions.With((IOperable<MyClass>)operable, func);
        }

        [Fact]
        public void ShouldBeAbleToUseWith_WithAnIndex()
        {
            var builderSetup = new BuilderSettings();
            Action<MyClass, int> funcWithIndex = (x, idx) => x.StringOne = "String" + (idx + 5);

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(funcWithIndex);
            }

            OperableExtensions.With((IOperable<MyClass>)operable, funcWithIndex);
        }

        [Fact]
        public void ShouldBeAbleToUseHas()
        {
            var builderSetup = new BuilderSettings();

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(func);
            }

            OperableExtensions.With((IOperable<MyClass>)operable, func);
        }

        [Fact]
        public void ShouldBeAbleToUseAnd()
        {
            var builderSetup = new BuilderSettings();

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(func);
            }

            OperableExtensions.And((IOperable<MyClass>)operable, func);
        }

        [Fact]
        public void ShouldBeAbleToUseAndWithAnIndex()
        {
            var builderSetup = new BuilderSettings();
            Action<MyClass, int> funcWithIndex = (x, idx) => x.StringOne = "String" + (idx + 5);

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(funcWithIndex);
            }

            OperableExtensions.And((IOperable<MyClass>)operable, funcWithIndex);
        }

        [Fact]
        public void ShouldBeAbleToUseWithToSetPrivateProperties()
        {
            var builderSetup = new BuilderSettings();

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(propertyExpression, 100);
            }

            OperableExtensions.With((IOperable<MyClass>)operable, propertyExpression, 100);
        }

        [Fact]
        public void ShouldBeAbleToUseHasToSetPrivateProperties()
        {
            var builderSetup = new BuilderSettings();

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(propertyExpression, 100);
            }

            OperableExtensions.With((IOperable<MyClass>)operable, propertyExpression, 100);
        }

        [Fact]
        public void ShouldBeAbleToUseAndToSetPrivateProperties()
        {
            var builderSetup = new BuilderSettings();

            {
                operable.ObjectBuilder.Returns(new ObjectBuilder<MyClass>(null, builderSetup));
                objectBuilder.With(propertyExpression, 100);
            }

            OperableExtensions.And((IOperable<MyClass>)operable, propertyExpression, 100);
        }

        [Fact]
        public void ShouldBeAbleToUseDoForEach()
        {
            var simpleClasses = new List<SimpleClass>();
            Action<MyClass, SimpleClass> action = (x, y) => x.Add(y);


            {
                operable.ObjectBuilder.Returns(objectBuilder);
                objectBuilder.DoMultiple(action, simpleClasses).Returns(objectBuilder);
            }

            OperableExtensions.DoForEach((IOperable<MyClass>)operable, action, simpleClasses);
        }

        [Fact]
        public void ShouldBeAbleToUseHasDoneToItForAll()
        {
            var simpleClasses = new List<SimpleClass>();
            Action<MyClass, SimpleClass> action = (x, y) => x.Add(y);


            {
                operable.ObjectBuilder.Returns(objectBuilder);
                objectBuilder.DoMultiple(action, simpleClasses).Returns(objectBuilder);
            }

            OperableExtensions.DoForEach((IOperable<MyClass>)operable, action, simpleClasses);
        }

        [Fact]
        public void ShouldBeAbleToUseDo()
        {
            Action<MyClass> action = x => x.DoSomething();


            {
                operable.ObjectBuilder.Returns(objectBuilder);
                objectBuilder.Do(action).Returns(objectBuilder);
            }

            OperableExtensions.Do((IOperable<MyClass>)operable, action);
        }

        [Fact]
        public void ShouldBeAbleToUseHasDoneToIt()
        {
            Action<MyClass, int> action = (x, y) => x.DoSomething();


            {
                operable.ObjectBuilder.Returns(objectBuilder);
                objectBuilder.Do(action).Returns(objectBuilder);
            }

            OperableExtensions.With((IOperable<MyClass>)operable, action);
        }

        [Fact]
        public void ShouldBeAbleToUseAndToAddAnAction()
        {
            Action<MyClass> action = x => x.DoSomething();


            {
                operable.ObjectBuilder.Returns(objectBuilder);
                objectBuilder.Do(action).Returns(objectBuilder);
            }

            OperableExtensions.And((IOperable<MyClass>)operable, action);
        }

        [Fact]
        public void ShouldComplainIfOperableIsNotAlsoOfTypeIDeclaration()
        {
            var operableOnly = Substitute.For<IOperable<MyClass>>();

            Should.Throw<ArgumentException>(() =>
            {
                OperableExtensions.With(operableOnly, x => x.StringOne = "test");
            });
        }
    }
    // ReSharper restore InvokeAsExtensionMethod
}