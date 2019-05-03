﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Shouldly;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class ParallelListBuilderTests
    {
        private class MyClass
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

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
                    IList<MyClass> instances = Builder<MyClass>.CreateListOfSize(listSize).Build();
                    for (int i = 0; i < listSize; i++)
                    {
                        instances[i].IntProperty.ShouldBe(i + 1);
                        instances[i].StringProperty.ShouldBe("StringProperty" + (i + 1));
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
