using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FizzWare.NBuilder.Implementation
{
    public class DeclarationQueue<T> : IDeclarationQueue<T>
    {

        public DeclarationQueue(int listCapacity)
        {
            this.ListCapacity = listCapacity;
        }

        public int ListCapacity { get; private set; }

        private readonly List<IDeclaration<T>> queuedDeclarations = new List<IDeclaration<T>>();

        public void Prioritise()
        {
            // global declarations should be at the front of the list.
            var globals = queuedDeclarations
                .OfType<IGlobalDeclaration<T>>()
                .Cast<IDeclaration<T>>() // added for 3.5 compatibility
                .ToList();
            globals.ForEach(row => queuedDeclarations.Remove(row));
            queuedDeclarations.InsertRange(0, globals);
        }
        
        public void Enqueue(IDeclaration<T> item)
        {
            if (item.End > ListCapacity)
                throw new BuilderException("A declaration was added which had an end index greater than the capacity of the list being generated");

            if (item.Start < 0)
                throw new BuilderException("A declaration was added which had a start index less than zero");

            queuedDeclarations.Add(item);
        }

        public IDeclaration<T> Dequeue()
        {
            var item = queuedDeclarations.First();
            queuedDeclarations.Remove(item);
            return item;
        }

        public IDeclaration<T> GetLastItem()
        {
            return queuedDeclarations.LastOrDefault();
        }

        public int Count
        {
            get { return queuedDeclarations.Count; }
        }

        public int GetDistinctAffectedItemCount()
        {
            var distinctAffectedItemCalculator = new DistinctAffectedItemCalculator(ListCapacity);

            foreach (IDeclaration<T> declaration in this.queuedDeclarations)
            {
                distinctAffectedItemCalculator.AddRange(declaration.Start, declaration.End, declaration.NumberOfAffectedItems);
            }

            return distinctAffectedItemCalculator.GetTotal();
        }

        public void Construct()
        {
            Prioritise();

            foreach (IDeclaration<T> declarion in this.queuedDeclarations)
            {
                declarion.Construct();
            }
        }

        public bool ContainsGlobalDeclaration()
        {
            return queuedDeclarations.OfType<IGlobalDeclaration<T>>().Any();
        }

        public void AddToMaster(T[] mainList)
        {
            foreach (IDeclaration<T> declaration in this.queuedDeclarations)
            {
                declaration.AddToMaster(mainList);
            }
        }

        public void CallFunctions(IList<T> list)
        {
            foreach (IDeclaration<T> declaration in this.queuedDeclarations)
            {
                declaration.CallFunctions(list);
            }
        }
    }
}