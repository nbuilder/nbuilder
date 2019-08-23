using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using FizzWare.NBuilder.Tests.TestClasses;
using Shouldly;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class ParallelListBuilderTests
    {
        [Fact]
        public void CreateListOfSize_Parallel_PropertiesHaveSequentialNaming()
        {
            const int listSize = 1000;
            const int numThreads = 50;
            ConcurrentBag<Exception> exceptions = new ConcurrentBag<Exception>();

            void ThreadFunc()
            {
                try
                {
                    IList<SimpleClass> instances = Builder<SimpleClass>.CreateListOfSize(listSize).Build();
                    for (int i = 0; i < listSize; i++)
                    {
                        instances[i].PropA.ShouldBe(i + 1);
                        instances[i].PropB.ShouldBe(i + 1);
                        instances[i].PropC.ShouldBe(i + 1);
                        instances[i].String1.ShouldBe("String1" + (i + 1));
                        instances[i].String2.ShouldBe("String2" + (i + 1));
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            Thread[] threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                threads[i] = new Thread(ThreadFunc);
                threads[i].Start();
            }

            for (int i = 0; i < numThreads; i++)
            {
                threads[i].Join();
            }

            exceptions.ShouldBeEmpty();
        }
    }
}
