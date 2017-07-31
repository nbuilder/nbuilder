using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder.Tests.TestClasses;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class ListBuilderBuilderTests_WithAClassThatHasANullCharConstant
    {
        [Fact]
        public void ShouldBeAbleToCreateAListOfAClassThatHasANullCharConstant()
        {
            var builderSetup = new BuilderSettings();
            var list = new Builder(builderSetup).CreateListOfSize< MyClassWithCharConst>(2).Build();

            foreach (var item in list)
            {
                item.GetNullCharConst().ShouldBe(MyClassWithCharConst.NullCharConst);
            }           
        }

        [Fact]
        public void ShouldBeAbleToCreateAListOfAClassThatHasACharConstant()
        {
            var builderSetup = new BuilderSettings();
            var list = new Builder(builderSetup).CreateListOfSize< MyClassWithCharConst>(2).Build();

            foreach (var item in list)
            {
                item.GetNonNullCharConst().ShouldBe(MyClassWithCharConst.NonNullCharConst);
            }            
        }
    }
}
