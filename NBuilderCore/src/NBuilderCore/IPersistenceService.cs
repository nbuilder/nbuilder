using System;
using System.Collections.Generic;

namespace NBuilderCore
{
    public interface IPersistenceService
    {
        void Create<T>(T obj);
        void Create<T>(IList<T> obj);
        void Update<T>(T obj);
        void Update<T>(IList<T> obj);
        void SetPersistenceCreateMethod<T>(Action<T> saveMethod);
        void SetPersistenceUpdateMethod<T>(Action<T> saveMethod);
    }
}