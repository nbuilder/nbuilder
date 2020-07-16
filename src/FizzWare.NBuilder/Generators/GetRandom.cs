using FizzWare.NBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    /// <summary>
    /// Static class used to generate specific random data.
    /// </summary>
    public static class GetRandom
    {
        private static readonly IRandomGenerator generator = new RandomGenerator();        
        
        private static DateTime minSqlServerDate = new DateTime(1753, 1, 1);
        private static DateTime maxSqlServerDate = new DateTime(9999, 12, 31);

        private static readonly string[] firstNames = { "Jacob", "Michael", "Matthew", "Joshua", "Christopher", "Nicholas", "Andrew", "Joseph", "Daniel", "Tyler", "William", "Brandon", "Ryan", "John", "Zachary", "David", "Anthony", "James", "Justin", "Alexander", "Jonathan", "Christian", "Austin", "Dylan", "Ethan", "Benjamin", "Noah", "Samuel", "Robert", "Nathan", "Cameron", "Kevin", "Thomas", "Jose", "Hunter", "Jordan", "Kyle", "Caleb", "Jason", "Logan", "Aaron", "Eric", "Brian", "Gabriel", "Adam", "Jack", "Isaiah", "Juan", "Luis", "Connor", "Charles", "Elijah", "Isaac", "Steven", "Evan", "Jared", "Sean", "Timothy", "Luke", "Cody", "Nathaniel", "Alex", "Seth", "Mason", "Richard", "Carlos", "Angel", "Patrick", "Devin", "Bryan", "Cole", "Jackson", "Ian", "Garrett", "Trevor", "Jesus", "Chase", "Adrian", "Mark", "Blake", "Sebastian", "Antonio", "Lucas", "Jeremy", "Gavin", "Miguel", "Julian", "Dakota", "Alejandro", "Jesse", "Dalton", "Bryce", "Tanner", "Kenneth", "Stephen", "Jake", "Victor", "Spencer", "Marcus", "Paul", "Brendan", "Xavier", "Jeremiah", "Jeffrey", "Tristan", "Jalen", "Jorge", "Edward", "Riley", "Colton", "Wyatt", "Joel", "Maxwell", "Aidan", "Travis", "Shane", "Colin", "Dominic", "Carson", "Vincent", "Derek", "Oscar", "Grant", "Eduardo", "Peter", "Henry", "Parker", "Hayden", "Collin", "George", "Bradley", "Mitchell", "Devon", "Ricardo", "Shawn", "Taylor", "Nicolas", "Gregory", "Francisco", "Liam", "Kaleb", "Preston", "Erik", "Alexis", "Owen", "Omar", "Diego", "Dustin", "Corey", "Fernando", "Clayton", "Carter", "Ivan", "Jaden", "Javier", "Alec", "Johnathan", "Scott", "Manuel", "Cristian", "Alan", "Raymond", "Brett", "Max", "Andres", "Gage", "Mario", "Dawson", "Dillon", "Cesar", "Wesley", "Levi", "Jakob", "Chandler", "Martin", "Malik", "Edgar", "Trenton", "Sergio", "Nolan", "Josiah", "Marco", "Peyton", "Harrison", "Hector", "Micah", "Roberto", "Drew", "Erick", "Brady", "Conner", "Jonah", "Casey", "Jayden", "Emmanuel", "Edwin", "Andre", "Phillip", "Brayden", "Landon", "Emily", "Hannah", "Madison", "Ashley", "Sarah", "Alexis", "Samantha", "Jessica", "Taylor", "Elizabeth", "Lauren", "Alyssa", "Kayla", "Abigail", "Brianna", "Olivia", "Emma", "Megan", "Grace", "Victoria", "Rachel", "Anna", "Sydney", "Destiny", "Morgan", "Jennifer", "Jasmine", "Haley", "Julia", "Kaitlyn", "Nicole", "Amanda", "Katherine", "Natalie", "Hailey", "Alexandra", "Savannah", "Chloe", "Rebecca", "Stephanie", "Maria", "Sophia", "Mackenzie", "Allison", "Isabella", "Amber", "Mary", "Danielle", "Gabrielle", "Jordan", "Brooke", "Michelle", "Sierra", "Katelyn", "Andrea", "Madeline", "Sara", "Kimberly", "Courtney", "Erin", "Brittany", "Vanessa", "Jacqueline", "Jenna", "Caroline", "Faith", "Makayla", "Bailey", "Paige", "Shelby", "Melissa", "Kaylee", "Christina", "Trinity", "Caitlin", "Mariah", "Autumn", "Marissa", "Angela", "Breanna", "Catherine", "Zoe", "Briana", "Jada", "Laura", "Claire", "Alexa", "Kelsey", "Kathryn", "Leslie", "Alexandria", "Sabrina", "Isabel", "Mia", "Molly", "Leah", "Katie", "Gabriella", "Cheyenne", "Cassandra", "Tiffany", "Erica", "Lindsey", "Kylie", "Diana", "Amy", "Cassidy", "Mikayla", "Ariana", "Margaret", "Kelly", "Miranda", "Maya", "Melanie", "Audrey", "Jade", "Gabriela", "Caitlyn", "Angel", "Jillian", "Alicia", "Jocelyn", "Erika", "Lily", "Madelyn", "Heather", "Adriana", "Arianna", "Lillian", "Kiara", "Riley", "Crystal", "Mckenzie", "Meghan", "Skylar", "Ana", "Britney", "Angelica", "Kennedy", "Chelsea", "Daisy", "Kristen", "Veronica", "Isabelle", "Summer", "Hope", "Brittney", "Hayley", "Lydia", "Evelyn", "Bethany", "Shannon", "Michaela", "Karen", "Jamie", "Daniela", "Angelina", "Kaitlin", "Karina", "Sophie", "Sofia", "Diamond", "Payton", "Cynthia", "Alexia", "Valerie", "Monica", "Peyton", "Carly", "Bianca", "Hanna", "Brenda", "Rebekah", "Alejandra", "Mya", "Avery", "Brooklyn", "Ashlyn", "Lindsay", "Ava", "Desiree", "Alondra", "Camryn", "Ariel", "Naomi", "Jordyn", "Kendra", "Mckenna", "Holly", "Julie", "Kendall", "Kara", "Jasmin", "Selena", "Esmeralda", "Amaya", "Kylee", "Maggie", "Makenzie", "Claudia" };
        private static readonly string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "García", "Rodríguez", "Wilson", "Martínez", "Anderson", "Taylor", "Thomas", "Hernández", "Moore", "Martin", "Jackson", "Thompson", "White", "López", "Lee", "González", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Pérez", "Hall", "Young", "Allen", "Sánchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson", "Hill", "Ramírez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gómez", "Kelly", "Howard", "Ward", "Cox", "Díaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutiérrez", "Perry", "Butler", "Barnes", "Fisher" };

        /// <summary>
        /// Generate a random string of specified length that is composed only of the numeric characters 0-9.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string NumericString(int length)
        {
            var sb = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                sb.Append(generator.Next(0, 10).ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generate a random integer.
        /// </summary>
        public static int Int()
        {
            return generator.Int();
        }

        /// <summary>
        /// Generate a random integer within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static int Int(int minValue, int maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generate a random positive integer.
        /// </summary>
        public static int PositiveInt()
        {
            return Int(0, int.MaxValue);
        }

        /// <summary>
        /// Generate a random positive integer with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static int PositiveInt(int maxValue)
        {
            return Int(0, maxValue);
        }

        /// <summary>
        /// Generate a random short.
        /// </summary>
        public static short Short()
        {
            return generator.Next(short.MinValue, short.MaxValue);
        }

        /// <summary>
        /// Generate a random short within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static short Short(short minValue, short maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generate a random positive short.
        /// </summary>
        public static short PositiveShort()
        {
            return Short(0, short.MaxValue);
        }

        /// <summary>
        /// Generate a random positive short with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static short PositiveShort(short maxValue)
        {
            return Short(0, maxValue);
        }

        /// <summary>
        /// Generate a random long.
        /// </summary>
        public static long Long()
        {
            return generator.Long();
        }

        /// <summary>
        /// Generate a random short within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static long Long(long minValue, long maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generate a random positive long.
        /// </summary>
        public static long PositiveLong()
        {
            return Long(0, long.MaxValue);
        }

        /// <summary>
        /// Generate a random positive long with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static long PositiveLong(long maxValue)
        {
            return Long(0, maxValue);
        }

        /// <summary>
        /// Generate a random uint.
        /// </summary>
        public static uint UInt()
        {
            return generator.UInt();
        }

        /// <summary>
        /// Generate a random ulong.
        /// </summary>
        public static ulong ULong()
        {
            return generator.ULong();
        }

        /// <summary>
        /// Generate a random ushort.
        /// </summary>
        public static ushort UShort()
        {
            return generator.UShort();
        }

        /// <summary>
        /// Generate a random decimal.
        /// </summary>
        public static decimal Decimal()
        {
            return generator.Decimal();
        }

        /// <summary>
        /// Generate a random decimal within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static decimal Decimal(decimal minValue, decimal maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generate a random positive decimal.
        /// </summary>
        public static decimal PositiveDecimal()
        {
            return Decimal(0, decimal.MaxValue);
        }

        /// <summary>
        /// Generate a random positive decimal with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static decimal PositiveDecimal(decimal maxValue)
        {
            return Decimal(0, maxValue);
        }

        /// <summary>
        /// Generate a random float.
        /// </summary>
        public static float Float()
        {
            return generator.Float();
        }

        /// <summary>
        /// Generate a random float within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static float Float(float minValue, float maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generate a random positive float.
        /// </summary>
        public static float PositiveFloat()
        {
            return Float(0, float.MaxValue);
        }

        /// <summary>
        /// Generate a random positive float with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static float PositiveFloat(float maxValue)
        {
            return Float(0, maxValue);
        }

        /// <summary>
        /// Generate a random double.
        /// </summary>
        public static double Double()
        {
            return generator.Double();
        }

        /// <summary>
        /// Generate a random double within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static double Double(double minValue, double maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generate a random positive double.
        /// </summary>
        public static double PositiveDouble()
        {
            return Double(0, double.MaxValue);
        }

        /// <summary>
        /// Generate a random positive double with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static double PositiveDouble(double maxValue)
        {
            return Double(0, maxValue);
        }

        /// <summary>
        /// Generate a random DateTime.
        /// </summary>
        public static DateTime DateTime()
        {
            return generator.DateTime();
        }

        /// <summary>
        /// Generate a random DateTime within a specified range.
        /// </summary>
        /// <param name="minValue">The earliest possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The latest possible value to generate. This value is exclusive.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        public static DateTime DateTime(DateTime minValue, DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return generator.Next(minValue, maxValue, kind);
        }

        /// <summary>
        /// Generate a random DateTime with a specified earliest DateTime value. The latest date will be the maximum value possible for a SQL Server datetime of December 31, 9999.
        /// </summary>
        /// <param name="minValue">The earliest possible value to generate.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        public static DateTime DateTimeFrom(DateTime minValue, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return generator.Next(minValue, maxSqlServerDate, kind);
        }

        /// <summary>
        /// Generate a random DateTime with a specified latest DateTime value. The earliest date will be the minimum value possible for a SQL Server datetime of January 1, 1753.
        /// </summary>
        /// <param name="maxValue">The latest possible value to generate.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        public static DateTime DateTimeThrough(DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return generator.Next(minSqlServerDate, maxValue, kind);
        }

        /// <summary>
        /// Generate a random bool value.
        /// </summary>
        public static bool Boolean()
        {
            return generator.Next(0, 2) != 0;
        }

        /// <summary>
        /// Generate a random byte.
        /// </summary>
        public static byte Byte()
        {
            return generator.Next(byte.MinValue, byte.MaxValue);
        }

        /// <summary>
        /// Generate a random sbyte.
        /// </summary>
        public static sbyte SByte()
        {
            return generator.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        /// <summary>
        /// Generate a random char.
        /// </summary>
        public static char Char()
        {
            return generator.Next(char.MinValue, char.MaxValue);
        }

        /// <summary>
        /// Generate a random Guid.
        /// </summary>
        public static Guid Guid()
        {
            return generator.Guid();
        }


        /// <summary>
        /// Generate a random first name from a pre-defined list of names.
        /// </summary>
        [Obsolete("Use new ValueSet<string>(name1, name2...) instead.")]
        public static string FirstName()
        {
            return firstNames[generator.Next(0, firstNames.Length - 1)];
        }

        /// <summary>
        /// Generate a random last name from a pre-defined list of names.
        /// </summary>
        [Obsolete("Use new ValueSet<string>(name1, name2...) instead.")]
        public static string LastName()
        {
            return lastNames[generator.Next(0, lastNames.Length - 1)];
        }

        /// <summary>
        /// Generate a random phrase using words from Lorem Ipsum as a string that is at most the specified length.
        /// </summary>
        /// <param name="length">The maximum length the phrase should be.</param>
        [Obsolete("Use new ValueSet<string>(phrase1, phrase2...) instead.")]
        public static string Phrase(int length)
        {
            return generator.Phrase(length);
        }

        /// <summary>
        /// Generate a random string of specified length. Each letter will randomly be uppercase or lowercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string String(int length)
        {
            return String(length, null, null);
        }

        /// <summary>
        /// Generate a random string of specified length. Every letter will be uppercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string UpperCaseString(int length)
        {
            return String(length, true, null);
        }

        /// <summary>
        /// Generate a random string of specified length. Every letter will be lowercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string LowerCaseString(int length)
        {
            return String(length, false, null);
        }

        /// <summary>
        /// Generate a random string of specified length. Can choose if the string should be entirely uppercase or lowercase. Can also choose any characters that should not be included.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="upperCase">Whether or not the string should be uppercase. Leave null to have each character randomly determined as uppercase or lowercase.</param>
        /// <param name="characterToExclude">Any characters that should not be included in the randomly generated string.</param>
        public static string String(int length, bool? upperCase, params char[] characterToExclude)
        {
            var sb = new StringBuilder();
            var exclude = new List<char>(characterToExclude ?? new char[] { });
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

        /// <summary>
        /// Generate a random character.
        /// </summary>
        /// <param name="upperCase">Whether or not the character should be uppercase. If specified as null, case will be randomly determined.</param>
        public static char Letter(bool? upperCase)
        {
            upperCase = upperCase ?? Boolean();
            var startingCharCode = (short)(upperCase.Value ? 'A' : 'a');
            return ((char)(generator.Next(startingCharCode, startingCharCode + 26)));
        }

        /// <summary>
        /// This class can randomly generate data specifically for the United Kingdom.
        /// </summary>
        public static class UK
        {
            // TODO: Add wales and scotland
            private static string[] _counties = new[]
                                                    {
                                                        "Bedfordshire",
                                                        "Berkshire",
                                                        "Buckinghamshire",
                                                        "Cambridgeshire",
                                                        "Cheshire",
                                                        "Cornwall",
                                                        "Cumberland",
                                                        "Derbyshire",
                                                        "Devon",
                                                        "Dorset",
                                                        "Durham",
                                                        "Essex",
                                                        "Gloucestershire",
                                                        "Hampshire",
                                                        "Herefordshire",
                                                        "Hertfordshire",
                                                        "Huntingdonshire",
                                                        "Kent",
                                                        "Lancashire",
                                                        "Leicestershire",
                                                        "Lincolnshire",
                                                        "Middlesex",
                                                        "Norfolk",
                                                        "Northamptonshire",
                                                        "Northumberland",
                                                        "Nottinghamshire",
                                                        "Oxfordshire",
                                                        "Rutland",
                                                        "Shropshire",
                                                        "Somerset",
                                                        "Staffordshire",
                                                        "Suffolk",
                                                        "Surrey",
                                                        "Sussex",
                                                        "Warwickshire",
                                                        "Westmorland",
                                                        "Wiltshire",
                                                        "Worcestershire",
                                                        "Yorkshire"
                                                    };

            /// <summary>
            /// Generate a random phone number in the format #### ### ###
            /// </summary>
            public static string PhoneNumber()
            {
                // TODO: This could be improved upon
                //
                // e.g. London:  020 xxxx xxxx
                //      Cardiff: 029 xxxx xxxx
                //      Mobile:  07xxx xxx xxx
                //      Other landlines: 01xxx xxx xxx
                //      Freephone: 0800 xxx xxx

                return string.Format("0{0:0000} {1:000} {2:000}",
                    Int(0, 9999), Int(0, 999), Int(0, 999));
            }

            // TODO: Add postcode, national insurance number

            //public static string PostCode()
            //{
                
            //}

            /// <summary>
            /// Generate a random county in the United Kingdom.
            /// </summary>
            public static string County()
            {
                return _counties[generator.Next(0, _counties.Length - 1)];
            }
        }

        /// <summary>
        /// This class can randomly generate data specifically for the United States.
        /// </summary>
        [Obsolete("Use Generators.Usa instead. For states, use ValueSet<string>()")]
        public static class Usa
        {
            private static readonly string[] states = { "TX", "CO", "GA", "LA", "NY", "CA" };

            /// <summary>
            /// Generate a random phone number in the format ###-###-####.
            /// </summary>
            public static string PhoneNumber()
            {
                return string.Format("{0:000}-{1:000}-{2:0000}",
                    Int(200, 999), Int(200, 999), Int(0, 9999));
            }

            /// <summary>
            /// Generate a random SSN in the format of ###-##-####.
            /// </summary>
            public static string SocialSecurityNumber()
            {
                return string.Format("{0}-{1}-{2}", NumericString(3), NumericString(2), NumericString(4));
            }

            public static string State()
            {
                return states[generator.Next(0, states.Length - 1)];
            }
        }

        /// <summary>
        /// Generate a random e-mail address on a .com domain.
        /// </summary>
        [Obsolete("Use Generators.Email.Random() instead.")]
        public static string Email()
        {
            return string.Format("{0}@{1}.com", String(8), String(7));
        }

        /// <summary>
        /// Generate a random website URL preceded with www.
        /// </summary>
        [Obsolete("use Generators.Url.RandomWithWWW() instead.")]
        public static string WwwUrl()
        {
            return string.Format("www.{0}.com", String(10, false));
        }

        /// <summary>
        /// Generate a random website URL with a subdomain, but not beginning with www.
        /// </summary>
        [Obsolete("Use Generators.Url.Random() instead.")]
        public static string Url()
        {
            return string.Format("{0}.{1}.com", String(5, false), String(10, false));
        }

        /// <summary>
        /// Generate a random IPv4 Address.
        /// </summary>
        [Obsolete("Use Generators.IpAddress.Random() instead.")]
        public static string IpAddress()
        {
            return $"{PositiveInt(255)}.{PositiveInt(255)}.{PositiveInt(255)}.{PositiveInt(255)}";
        }

        /// <summary>
        /// Generate a random IPv6 Address.
        /// </summary>
        [Obsolete("Use Generators.IpAddress.RandomV6() instead.")]
        public static string IpAddressV6()
        {
            return $"{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}:{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}{PositiveShort(16):X}";
        }

        /// <summary>
        /// Generate a random MAC address.
        /// </summary>
        /// <param name="separator">Optional to override the default separator from - used in IEEE 802</param>
        [Obsolete("Use Generators.MacAddress.Random() instead.")]
        public static string MacAddress(string separator = "-")
        {
            return $"{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}{separator}{PositiveShort(16):X}{PositiveShort(16):X}";
        }

        /// <summary>
        /// Get a random option from a specified enum type.
        /// </summary>
        /// <typeparam name="T">An enum type</typeparam>
        public static T Enumeration<T>() where T : struct
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

        /// <summary>
        /// Get a random option from a specified enum type.
        /// </summary>
        /// <param name="type">The enum type you wish to get a random value of.</param>
        public static Enum Enumeration(Type type)
        {
            if (!type.IsEnum())
            {
                throw new ArgumentException(string.Format("{0} is not an enum type.", type.FullName), "type");
            }
            var values = EnumHelper.GetValues(type);
            var index = PositiveInt(values.Length);
            return (Enum)values.GetValue(index);
        }   
    }
}