using System;
using System.Collections.Generic;
using System.Reflection;
using FizzWare.NBuilder.FunctionalTests.Model;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.FunctionalTests.Extensibility
{
    public class CustomProductPropertyNamer : SequentialPropertyNamer    
    {
        private readonly SequentialGenerator<char> aisleGenerator;
        private readonly SequentialGenerator<int> shelfGenerator;
        private readonly SequentialGenerator<int> locGenerator;

        public CustomProductPropertyNamer(IReflectionUtil reflectionUtil, BuilderSettings builderSettings) 
            : base(reflectionUtil,builderSettings)
        {
            aisleGenerator = new SequentialGenerator<char>();
            shelfGenerator = new SequentialGenerator<int> { Increment = 2 };
            locGenerator = new SequentialGenerator<int> {Increment = 1000};
        }

        public override void SetValuesOfAllIn<T>(IList<T> objects)
        {
            aisleGenerator.StartingWith('A');
            shelfGenerator.StartingWith(2);
            locGenerator.StartingWith(1000);
            base.SetValuesOfAllIn(objects);
        }

        protected override void HandleUnknownType<T>(Type memberType, MemberInfo memberInfo, T obj)
        {
            if (memberType == typeof(WarehouseLocation))
            {
                var location = new WarehouseLocation(aisleGenerator.Generate(),
                                                     shelfGenerator.Generate(),
                                                     locGenerator.Generate());

                SetValue(memberInfo, obj, location);
            } 
        }
    }
}