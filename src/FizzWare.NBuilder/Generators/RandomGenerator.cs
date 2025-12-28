using FizzWare.NBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace FizzWare.NBuilder
{
    public class RandomGenerator : IRandomGenerator
    {
        private static readonly Lazy<RandomGenerator> _default = new(() => new RandomGenerator());
        
        /// <summary>
        /// Gets the default shared instance of RandomGenerator.
        /// </summary>
        public static RandomGenerator Default => _default.Value;
        
        private readonly Random _random;

        private static readonly DateTime _minSqlServerDate = new(1753, 1, 1);
        private static readonly DateTime _maxSqlServerDate = new(9999, 12, 31);

        private static readonly string[] firstNames = ["Jacob", "Michael", "Matthew", "Joshua", "Christopher", "Nicholas", "Andrew", "Joseph", "Daniel", "Tyler", "William", "Brandon", "Ryan", "John", "Zachary", "David", "Anthony", "James", "Justin", "Alexander", "Jonathan", "Christian", "Austin", "Dylan", "Ethan", "Benjamin", "Noah", "Samuel", "Robert", "Nathan", "Cameron", "Kevin", "Thomas", "Jose", "Hunter", "Jordan", "Kyle", "Caleb", "Jason", "Logan", "Aaron", "Eric", "Brian", "Gabriel", "Adam", "Jack", "Isaiah", "Juan", "Luis", "Connor", "Charles", "Elijah", "Isaac", "Steven", "Evan", "Jared", "Sean", "Timothy", "Luke", "Cody", "Nathaniel", "Alex", "Seth", "Mason", "Richard", "Carlos", "Angel", "Patrick", "Devin", "Bryan", "Cole", "Jackson", "Ian", "Garrett", "Trevor", "Jesus", "Chase", "Adrian", "Mark", "Blake", "Sebastian", "Antonio", "Lucas", "Jeremy", "Gavin", "Miguel", "Julian", "Dakota", "Alejandro", "Jesse", "Dalton", "Bryce", "Tanner", "Kenneth", "Stephen", "Jake", "Victor", "Spencer", "Marcus", "Paul", "Brendan", "Xavier", "Jeremiah", "Jeffrey", "Tristan", "Jalen", "Jorge", "Edward", "Riley", "Colton", "Wyatt", "Joel", "Maxwell", "Aidan", "Travis", "Shane", "Colin", "Dominic", "Carson", "Vincent", "Derek", "Oscar", "Grant", "Eduardo", "Peter", "Henry", "Parker", "Hayden", "Collin", "George", "Bradley", "Mitchell", "Devon", "Ricardo", "Shawn", "Taylor", "Nicolas", "Gregory", "Francisco", "Liam", "Kaleb", "Preston", "Erik", "Alexis", "Owen", "Omar", "Diego", "Dustin", "Corey", "Fernando", "Clayton", "Carter", "Ivan", "Jaden", "Javier", "Alec", "Johnathan", "Scott", "Manuel", "Cristian", "Alan", "Raymond", "Brett", "Max", "Andres", "Gage", "Mario", "Dawson", "Dillon", "Cesar", "Wesley", "Levi", "Jakob", "Chandler", "Martin", "Malik", "Edgar", "Trenton", "Sergio", "Nolan", "Josiah", "Marco", "Peyton", "Harrison", "Hector", "Micah", "Roberto", "Drew", "Erick", "Brady", "Conner", "Jonah", "Casey", "Jayden", "Emmanuel", "Edwin", "Andre", "Phillip", "Brayden", "Landon", "Emily", "Hannah", "Madison", "Ashley", "Sarah", "Alexis", "Samantha", "Jessica", "Taylor", "Elizabeth", "Lauren", "Alyssa", "Kayla", "Abigail", "Brianna", "Olivia", "Emma", "Megan", "Grace", "Victoria", "Rachel", "Anna", "Sydney", "Destiny", "Morgan", "Jennifer", "Jasmine", "Haley", "Julia", "Kaitlyn", "Nicole", "Amanda", "Katherine", "Natalie", "Hailey", "Alexandra", "Savannah", "Chloe", "Rebecca", "Stephanie", "Maria", "Sophia", "Mackenzie", "Allison", "Isabella", "Amber", "Mary", "Danielle", "Gabrielle", "Jordan", "Brooke", "Michelle", "Sierra", "Katelyn", "Andrea", "Madeline", "Sara", "Kimberly", "Courtney", "Erin", "Brittany", "Vanessa", "Jacqueline", "Jenna", "Caroline", "Faith", "Makayla", "Bailey", "Paige", "Shelby", "Melissa", "Kaylee", "Christina", "Trinity", "Caitlin", "Mariah", "Autumn", "Marissa", "Angela", "Breanna", "Catherine", "Zoe", "Briana", "Jada", "Laura", "Claire", "Alexa", "Kelsey", "Kathryn", "Leslie", "Alexandria", "Sabrina", "Isabel", "Mia", "Molly", "Leah", "Katie", "Gabriella", "Cheyenne", "Cassandra", "Tiffany", "Erica", "Lindsey", "Kylie", "Diana", "Amy", "Cassidy", "Mikayla", "Ariana", "Margaret", "Kelly", "Miranda", "Maya", "Melanie", "Audrey", "Jade", "Gabriela", "Caitlyn", "Angel", "Jillian", "Alicia", "Jocelyn", "Erika", "Lily", "Madelyn", "Heather", "Adriana", "Arianna", "Lillian", "Kiara", "Riley", "Crystal", "Mckenzie", "Meghan", "Skylar", "Ana", "Britney", "Angelica", "Kennedy", "Chelsea", "Daisy", "Kristen", "Veronica", "Isabelle", "Summer", "Hope", "Brittney", "Hayley", "Lydia", "Evelyn", "Bethany", "Shannon", "Michaela", "Karen", "Jamie", "Daniela", "Angelina", "Kaitlin", "Karina", "Sophie", "Sofia", "Diamond", "Payton", "Cynthia", "Alexia", "Valerie", "Monica", "Peyton", "Carly", "Bianca", "Hanna", "Brenda", "Rebekah", "Alejandra", "Mya", "Avery", "Brooklyn", "Ashlyn", "Lindsay", "Ava", "Desiree", "Alondra", "Camryn", "Ariel", "Naomi", "Jordyn", "Kendra", "Mckenna", "Holly", "Julie", "Kendall", "Kara", "Jasmin", "Selena", "Esmeralda", "Amaya", "Kylee", "Maggie", "Makenzie", "Claudia"];
        private static readonly string[] lastNames = ["Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "García", "Rodríguez", "Wilson", "Martínez", "Anderson", "Taylor", "Thomas", "Hernández", "Moore", "Martin", "Jackson", "Thompson", "White", "López", "Lee", "González", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Pérez", "Hall", "Young", "Allen", "Sánchez", "Wright", "King", "Scott", "Green", "Baker", "AdAMS", "Nelson", "Hill", "Ramírez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "EvANS", "Turner", "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan", "PeTerson", "Cooper", "Reed", "Bailey", "Bell", "Gómez", "Kelly", "Howard", "Ward", "Cox", "Díaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutiérrez", "Perry", "Butler", "Barnes", "Fisher"];
        private static readonly string[] latinWords = [ "lorem", "ipsum", "dolor", "sit", "amet", "consectetur","adipisicing", "elit", "sed", "do", "eiusmod", "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua" ];

        public RandomGenerator() : this(System.Guid.NewGuid().GetHashCode()) { }

        public RandomGenerator(int seed) : this(new Random(seed)) { }

        public RandomGenerator(Random random) => _random = random;

        public virtual short Next(short min, short max)
        {
            return (short)Next((int)min, max);
        }

        public virtual int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public virtual long Next(long min, long max)
        {
            double rn = (max * 1.0 - min * 1.0) * _random.NextDouble() + min * 1.0;
            return Convert.ToInt64(rn);
        }

        public virtual float Next(float min, float max)
        {
            return (float)Next((double)min, (double)max);
        }

        public virtual double Next(double min, double max)
        {
            return min + _random.NextDouble() * (max - min);
        }

        public virtual decimal Next(decimal min, decimal max)
        {
            if (min < int.MinValue)
                min = (decimal)int.MinValue;

            if (max > int.MaxValue)
                max = (decimal)int.MaxValue;

            int iMin = (int)min;
            int iMax = (int)max;

            int integer = _random.Next(iMin, iMax);
            int fraction = _random.Next(0, 4000);
         
            var separator = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

            return (decimal)Convert.ToDecimal(string.Format("{0}{1}{2}", integer, separator, fraction));
        }

        public virtual char Next(char min, char max)
        {
            return (char)Next((int)min, (int)max);
        }

        public virtual byte Next(byte min, byte max)
        {
            return (byte)Next((int)min, (int)max);
        }

        public virtual sbyte Next(sbyte min, sbyte max)
        {
            return (sbyte)Next((int)min, (int)max);
        }

        public DateTime Next(DateTime min, DateTime max, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            long minTicks = min.Ticks;
            long maxTicks = max.Ticks;
            double rn = (Convert.ToDouble(maxTicks)
               - Convert.ToDouble(minTicks)) * _random.NextDouble()
               + Convert.ToDouble(minTicks);
            return new DateTime(Convert.ToInt64(rn), kind);

        }

        public virtual bool Next()
        {
            return _random.Next(2) == 1;
        }

        /// <inheritdoc/>
        public Guid Guid() => System.Guid.NewGuid();

        /// <inheritdoc/>
        public virtual bool Boolean() => Next(0, 2) != 0;

        /// <inheritdoc/>
        public virtual int Int() => Next(int.MinValue, int.MaxValue);

        /// <inheritdoc/>
        public virtual int Int(int minValue, int maxValue) => Next(minValue, maxValue);

        /// <inheritdoc/>
        public virtual short Short() => Next(short.MinValue, short.MaxValue);

        /// <inheritdoc/>
        public virtual long Long() => Next(long.MinValue, long.MaxValue);

        /// <inheritdoc/>
        public long Long(long minValue, long maxValue) => Next(minValue, maxValue);

        /// <inheritdoc/>
        public virtual uint UInt() => Next(uint.MinValue, uint.MaxValue);

        /// <inheritdoc/>
        public virtual ulong ULong() => Next(ulong.MinValue, ulong.MaxValue);

        /// <inheritdoc/>
        public virtual ushort UShort() => Next(ushort.MinValue, ushort.MaxValue);

        /// <inheritdoc/>
        public virtual decimal Decimal() => Next(decimal.MinValue, decimal.MaxValue);

        /// <inheritdoc/>
        public virtual float Float() => Next(float.MinValue, float.MaxValue);

        /// <inheritdoc/>
        public virtual double Double() => Next(double.MinValue, double.MaxValue);

        /// <inheritdoc/>
        public virtual byte Byte() => Next(byte.MinValue, byte.MaxValue);

        /// <inheritdoc/>
        public virtual sbyte SByte() => Next(sbyte.MinValue, sbyte.MaxValue);

        /// <inheritdoc/>
        public virtual DateTime DateTime() => Next(_minSqlServerDate, _maxSqlServerDate);

        /// <inheritdoc/>
        public virtual string Phrase(int length)
        {
            var count = latinWords.Length;
            var result = string.Empty;
            var done = false;
            while (!done)
            {
                var word = latinWords[Next(0, count)];
                if (result.Length + word.Length + 1 > length)
                {
                    done = true;
                }
                else
                {
                    result += word + " ";
                }
            }
            return result.Trim();
        }

        /// <inheritdoc/>
        public virtual string String(int length) => String(length, null, null);

        /// <inheritdoc/>
        public virtual string String(int length, bool? upperCase, params char[] characterToExclude)
        {
            var sb = new StringBuilder();
            var exclude = new List<char>(characterToExclude ?? []);
            var i = 0;
            while (i < length)
            {
                var c = Letter(upperCase);
                if (!exclude.Contains(c))
                {
                    sb.Append(c);
                    i++;
                }
            }
            return sb.ToString();
        }

        /// <inheritdoc/>
        public virtual char Letter(bool? upperCase)
        {
            upperCase ??= Boolean();
            var startingCharCode = (short)(upperCase.Value ? 'A' : 'a');
            return ((char)(Int(startingCharCode, startingCharCode + 26)));
        }

        /// <inheritdoc/>
        public virtual char Char() => Next(char.MinValue, char.MaxValue);

        /// <inheritdoc/>
        public virtual ushort Next(ushort min, ushort max)
        {
            return (ushort)Next((int)min, (int)max);
        }

        /// <inheritdoc/>
        public virtual uint Next(uint min, uint max)
        {
            byte[] buffer = new byte[sizeof(uint)];
            _random.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <inheritdoc/>
        public virtual ulong Next(ulong min, ulong max)
        {
            byte[] buffer = new byte[sizeof(ulong)];
            _random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <inheritdoc/>
        public T Enumeration<T>() where T : struct
        {
            var values = EnumHelper.GetValues(typeof(T));

            /*
             * This method is called to generate random enum values. Because
             * Random.Next(min, max) is not upper-inclusive, we pass values.Length
             * rather than values.Length - 1 as the upper bound to make sure all
             * enum values are potentially returned.
             */

            var index = PositiveInt(values.Length);
            return (T)values.GetValue(index);
        }

        /// <inheritdoc/>
        public Enum Enumeration(Type type)
        {
            if (!type.IsEnum())
            {
                throw new ArgumentException(string.Format("{0} is not an enum type.", type.FullName), nameof(type));
            }
            var values = EnumHelper.GetValues(type);
            var index = Next(0, values.Length);
            return (Enum)values.GetValue(index);
        }

        /// <inheritdoc/>
        public virtual string NextString(int minLength, int maxLength)
        {
            var lexicon = latinWords.OrderBy(row => Next(0, latinWords.Length)).ToArray();

            var wordIndex = 0;
            string nextWord()
            {
                var word = lexicon[wordIndex++];
                if (wordIndex == lexicon.Length)
                {
                    wordIndex = 0;
                }

                return word;
            }

            var builder = new StringBuilder();

            var targetLength = Next(minLength, maxLength);


            while (builder.Length < targetLength)
            {
                var word = nextWord();

                var potential = builder + " " + word;
                if (potential.Length < targetLength)
                {
                    builder.Append(" " + word);
                    continue;
                }

                var remaining = targetLength - builder.Length;
                if (remaining == 1)
                {
                    builder.Append('!');
                    break;
                }

                var suitableWord = lexicon.FirstOrDefault(row => row.Length < remaining);
                if (!suitableWord.IsNullOrWhiteSpace())
                {
                    builder.Append(" " + suitableWord);
                }
                else
                {
                    var filler = new string('!', remaining);
                    builder.Append(filler);
                }

            }
            return builder.ToString();
        }

        /// <inheritdoc/>
        public virtual int PositiveInt() => Int(0, int.MaxValue);

        /// <inheritdoc/>
        public virtual int PositiveInt(int maxValue) => Int(0, maxValue);

        /// <inheritdoc/>
        public virtual short Short(short minValue, short maxValue) => Next(minValue, maxValue);

        /// <inheritdoc/>
        public virtual string NumericString(int length)
        {
            var sb = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                sb.Append(Next(0, 10));
            }
            return sb.ToString();
        }

        /// <inheritdoc/>
        public virtual short PositiveShort() => Next((short)0, short.MaxValue);

        /// <inheritdoc/>
        public virtual short PositiveShort(short maxValue) => Next((short)0, maxValue);

        /// <inheritdoc/>
        public virtual long PositiveLong() => Next(0L, long.MaxValue);

        /// <inheritdoc/>
        public virtual long PositiveLong(long maxValue) => Next(0L, maxValue);
        
        /// <inheritdoc/>
        public virtual decimal PositiveDecimal() => Next(0M, decimal.MaxValue);

        /// <inheritdoc/>
        public virtual decimal PositiveDecimal(decimal maxValue) => Next(0M, maxValue);

        /// <inheritdoc/>
        public virtual float PositiveFloat() => Next(0F, float.MaxValue);

        /// <inheritdoc/>
        public virtual float PositiveFloat(float maxValue) => Next(0F, maxValue);

        /// <inheritdoc/>
        public virtual double PositiveDouble() => Next(0D, double.MaxValue);

        /// <inheritdoc/>
        public virtual double PositiveDouble(double maxValue) => Next(0D, maxValue);

        /// <inheritdoc/>
        public virtual decimal Decimal(decimal minValue, decimal maxValue) => Next(minValue, maxValue);

        /// <inheritdoc/>
        public virtual float Float(float minValue, float maxValue) => Next(minValue, maxValue);

        /// <inheritdoc/>
        public virtual double Double(double minValue, double maxValue) => Next(minValue, maxValue);

        /// <inheritdoc/>
        public virtual DateTime DateTime(DateTime minValue, DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified) => Next(minValue, maxValue, kind);

        /// <inheritdoc/>
        public DateTime DateTimeFrom(DateTime minValue, DateTimeKind kind = DateTimeKind.Unspecified) => DateTime(minValue, _maxSqlServerDate, kind);

        /// <inheritdoc/>
        public DateTime DateTimeThrough(DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified) => DateTime(_minSqlServerDate, maxValue, kind);

        /// <inheritdoc/>
        public virtual string FirstName() => firstNames[Int(0, firstNames.Length)];

        /// <inheritdoc/>
        public virtual string LastName() => lastNames[Int(0, lastNames.Length)];

        /// <inheritdoc/>
        public virtual string UpperCaseString(int length)
        {
            return String(length, true, null);
        }

        /// <inheritdoc/>
        public virtual string LowerCaseString(int length)
        {
            return String(length, false, null);
        }

        /// <inheritdoc/>
        public virtual string Email() => string.Format("{0}@{1}.com", String(8), String(7));

        /// <inheritdoc/>
        public virtual string WwwUrl() => string.Format("www.{0}.com", String(10, false));

        /// <inheritdoc/>
        public virtual string Url() => string.Format("{0}.{1}.com", String(5, false), String(10, false));

        /// <inheritdoc/>
        public virtual string IpAddress() => $"{PositiveInt(255)}.{PositiveInt(255)}.{PositiveInt(255)}.{PositiveInt(255)}";

        /// <inheritdoc/>
        public virtual string IpAddressV6() => $"{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}";

        /// <inheritdoc/>
        public virtual string MacAddress(string separator = "-") => $"{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}";

        /// <summary>
        /// This class can randomly generate data specifically for the United Kingdom.
        /// </summary>
        public class UK
        {
            private static readonly Lazy<UK> _default = new(() => new UK());

            /// <summary>
            /// Gets the default shared instance of UK.
            /// </summary>
            public static UK Default => _default.Value;

            private readonly RandomGenerator _randomGenerator;

            // TODO: Add wales and scotland
            private static readonly string[] _counties = ["Bedfordshire", "Berkshire", "Buckinghamshire", "Cambridgeshire", "Cheshire", "Cornwall", "Cumberland", "Derbyshire", "Devon", "Dorset", "Durham", "Essex", "Gloucestershire", "Hampshire", "Herefordshire", "Hertfordshire", "Huntingdonshire", "Kent", "Lancashire", "Leicestershire", "Lincolnshire", "Middlesex", "Norfolk", "Northamptonshire", "Northumberland", "Nottinghamshire", "Oxfordshire", "Rutland", "Shropshire", "Somerset", "Staffordshire", "Suffolk", "Surrey", "Sussex", "Warwickshire", "Westmorland", "Wiltshire", "Worcestershire", "Yorkshire"];

            public UK() : this(System.Guid.NewGuid().GetHashCode()) { }

            public UK(int seed) : this(new RandomGenerator(seed)) { }

            public UK(RandomGenerator randomGenerator) => _randomGenerator = randomGenerator;

            /// <summary>
            /// Generate a random phone number in the format #### ### ###
            /// </summary>
            public string PhoneNumber()
            {
                // TODO: This could be improved upon
                //
                // e.g. London:  020 xxxx xxxx
                //      Cardiff: 029 xxxx xxxx
                //      Mobile:  07xxx xxx xxx
                //      Other landlines: 01xxx xxx xxx
                //      Freephone: 0800 xxx xxx

                return string.Format("0{0:0000} {1:000} {2:000}", 
                    _randomGenerator.Int(0, 9999),
                    _randomGenerator.Int(0, 999),
                    _randomGenerator.Int(0, 999));
            }

            // TODO: Add postcode, national insurance number

            //public string PostCode()
            //{

            //}

            /// <summary>
            /// Generate a random county in the United Kingdom.
            /// </summary>
            public string County() => _counties[_randomGenerator.PositiveInt(_counties.Length)];
        }

        /// <summary>
        /// This class can randomly generate data specifically for the United States.
        /// </summary>
        public class USA
        {
            private static readonly Lazy<USA> _default = new(() => new USA());

            private static readonly string[] states = ["TX", "CO", "GA", "LA", "NY", "CA"];

            /// <summary>
            /// Gets the default shared instance of USA.
            /// </summary>
            public static USA Default => _default.Value;

            private readonly RandomGenerator _randomGenerator;

            public USA() : this(System.Guid.NewGuid().GetHashCode()) { }

            public USA(int seed) : this(new RandomGenerator(seed)) { }

            public USA(RandomGenerator randomGenerator) => _randomGenerator = randomGenerator;

            /// <summary>
            /// Generate a random phone number in the format ###-###-####.
            /// </summary>
            public string PhoneNumber() => 
                string.Format("{0:000}-{1:000}-{2:0000}",
                    _randomGenerator.Int(200, 999), 
                    _randomGenerator.Int(200, 999), 
                    _randomGenerator.Int(0, 9999));

            /// <summary>
            /// Generate a random SSN in the format of ###-##-####.
            /// </summary>
            public string SocialSecurityNumber() => 
                string.Format("{0}-{1}-{2}", 
                    _randomGenerator.NumericString(3),
                    _randomGenerator.NumericString(2),
                    _randomGenerator.NumericString(4));

            public string State() => states[_randomGenerator.PositiveInt(states.Length - 1)];
        }
    }
}
