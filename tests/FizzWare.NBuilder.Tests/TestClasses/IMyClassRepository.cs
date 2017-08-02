using System.Collections.Generic;

namespace FizzWare.NBuilder.Tests.TestClasses
{
    public interface IMyClassRepository
    {
        void Save(MyClass obj);
        void SaveAll(IList<MyClass> list);
    }
}