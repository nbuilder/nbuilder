using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FizzWare.NBuilder.Implementation
{
    public class DeclarationQueue<T> : IDeclarationQueue<T>
    {
        private readonly int listCapacity;

        public DeclarationQueue(int listCapacity)
        {
            this.listCapacity = listCapacity;
        }

        private readonly List<IDeclaration<T>> queuedDeclarations = new List<IDeclaration<T>>();

        public void Prioritise()
        {
            queuedDeclarations.Sort(new DeclarationComparer<T>());
        }
        
        public void Enqueue(IDeclaration<T> item)
        {
            if (item.End > listCapacity)
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
            var distinctAffectedItemCalculator = new DistinctAffectedItemCalculator(listCapacity);

            for (int i = 0; i < queuedDeclarations.Count; i++)
            {
                distinctAffectedItemCalculator.AddRange(queuedDeclarations[i].Start, queuedDeclarations[i].End, queuedDeclarations[i].NumberOfAffectedItems);
            }

            return distinctAffectedItemCalculator.GetTotal();
        }

        public void Construct()
        {
            Prioritise();
            for (int i = 0; i < queuedDeclarations.Count; i++)
                queuedDeclarations[i].Construct();
        }

        public bool ContainsGlobalDeclaration()
        {
            return queuedDeclarations.OfType<IGlobalDeclaration<T>>().Count() > 0;
        }

        public void AddToMaster(T[] mainList)
        {
            for (int i = 0; i < queuedDeclarations.Count; i++)
                queuedDeclarations[i].AddToMaster(mainList);
        }

        public void CallFunctions(IList<T> list)
        {
            for (int i = 0; i < queuedDeclarations.Count; i++)
                queuedDeclarations[i].CallFunctions(list);
        }
    }
}