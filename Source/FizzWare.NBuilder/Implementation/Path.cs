using System.Collections.Generic;
using System.Linq;

namespace FizzWare.NBuilder.Implementation
{
    public class Path
    {
        private readonly List<int> levels;
        private string formatString;

        public Path()
        {
            this.levels = new List<int>();
            levels.Add(1);
            GenerateFormatString();
        }
 
        public void IncreaseDepth()
        {
            levels.Add(1);
            GenerateFormatString();
        }

        private void GenerateFormatString()
        {
            List<string> formatStringList = new List<string>();
            for (int i = 0; i < levels.Count; i++)
            {
                formatStringList.Add("{" + i + "}");
            }

            formatString = string.Join(".", formatStringList.ToArray());
        }

        public void SetCurrent(int sequenceNumber)
        {
            levels[levels.Count - 1] = sequenceNumber;
        }
        
        public void DecreaseDepth()
        {
            levels.RemoveAt(levels.Count - 1);
            GenerateFormatString();
        }

        public override string ToString()
        {
            return string.Format(formatString, levels.Cast<object>().ToArray());
        }
    }
}