using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder.Implementation
{
    public abstract class Declaration<T> : IDeclaration<T>, IOperable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Declaration&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="listBuilderImpl">The list builder.</param>
        /// <param name="objectBuilder">The object builder.</param>
        public BuilderSettings BuilderSettings { get; set; }
        protected Declaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder)
        {
            this.listBuilderImpl = listBuilderImpl;
            this.objectBuilder = objectBuilder;
            MasterListAffectedIndexes = new List<int>();
            BuilderSettings = listBuilderImpl.BuilderSettings;
        }

        /// <summary>
        /// A reference to the list builder that contains this declaration
        /// </summary>
        protected readonly IListBuilderImpl<T> listBuilderImpl;

        public IListBuilderImpl<T> ListBuilderImpl => listBuilderImpl;

        public IObjectBuilder<T> ObjectBuilder => objectBuilder;

        /// <summary>
        /// The object builder that this declaration uses to create its objects
        /// </summary>
        protected readonly IObjectBuilder<T> objectBuilder;

        /// <summary>
        /// The list of objects that this declaration is responsible for
        /// </summary>
        protected readonly List<T> myList = new List<T>();

        /// <summary>
        /// Gets or sets the master list affected keys.
        /// 
        /// Implementing classes should add items to this list when they add their items to the master list
        /// </summary>
        public IList<int> MasterListAffectedIndexes { get; set; }

        /// <summary>
        /// Gets the start index (relating to the master list) that this declaration will affect.
        /// </summary>
        public abstract int Start { get; }

        /// <summary>
        /// Gets the end index (relating to the master list) that this declaration will affect.
        /// </summary>
        public abstract int End { get; }

        public IOperable<T> All()
        {
            return listBuilderImpl.All();
        }


        public IList<T> Build()
        {
            return listBuilderImpl.Build();
        }

        public abstract void Construct();

        public virtual void CallFunctions(IList<T> masterList)
        {
            // At first glance this might seem a strange, roundabout way of implementing this...
            // i.e. why not just call the functions on myList since they hold references to
            // the same objects in the master list, however NBuilder should also support structs
            // so in fact they need to be called on the actual objects in the master list

            for (var i = 0; i < MasterListAffectedIndexes.Count; i++)
            {
                var index = MasterListAffectedIndexes[i];
                objectBuilder.CallFunctions(masterList[index], i);
            }
        }

        public abstract void AddToMaster(T[] masterList);
        public abstract int NumberOfAffectedItems { get; }

        protected virtual void AddItemToMaster(T item, T[] masterList, int index)
        {
            masterList[index] = item;
            MasterListAffectedIndexes.Add(index);
        }
    }
}