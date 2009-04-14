using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public interface IPersistenceService
    {
        void Persist<T>(T obj);
        void Persist<T>(IList<T> obj);
        void SetPersistenceMethod<T>(Action<IList<T>> saveMethod);
        void SetPersistenceMethod<T>(Action<T> saveMethod);
    }
}