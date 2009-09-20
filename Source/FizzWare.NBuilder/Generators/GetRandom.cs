using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    public class GetRandom
    {
        private static readonly IRandomGenerator generator = new RandomGenerator();
        private static readonly string[] latinWords = { "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipisicing", "elit", "sed", "do", "eiusmod", "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua" };
        private static readonly string[] firstNames = { "Jacob", "Michael", "Matthew", "Joshua", "Christopher", "Nicholas", "Andrew", "Joseph", "Daniel", "Tyler", "William", "Brandon", "Ryan", "John", "Zachary", "David", "Anthony", "James", "Justin", "Alexander", "Jonathan", "Christian", "Austin", "Dylan", "Ethan", "Benjamin", "Noah", "Samuel", "Robert", "Nathan", "Cameron", "Kevin", "Thomas", "Jose", "Hunter", "Jordan", "Kyle", "Caleb", "Jason", "Logan", "Aaron", "Eric", "Brian", "Gabriel", "Adam", "Jack", "Isaiah", "Juan", "Luis", "Connor", "Charles", "Elijah", "Isaac", "Steven", "Evan", "Jared", "Sean", "Timothy", "Luke", "Cody", "Nathaniel", "Alex", "Seth", "Mason", "Richard", "Carlos", "Angel", "Patrick", "Devin", "Bryan", "Cole", "Jackson", "Ian", "Garrett", "Trevor", "Jesus", "Chase", "Adrian", "Mark", "Blake", "Sebastian", "Antonio", "Lucas", "Jeremy", "Gavin", "Miguel", "Julian", "Dakota", "Alejandro", "Jesse", "Dalton", "Bryce", "Tanner", "Kenneth", "Stephen", "Jake", "Victor", "Spencer", "Marcus", "Paul", "Brendan", "Xavier", "Jeremiah", "Jeffrey", "Tristan", "Jalen", "Jorge", "Edward", "Riley", "Colton", "Wyatt", "Joel", "Maxwell", "Aidan", "Travis", "Shane", "Colin", "Dominic", "Carson", "Vincent", "Derek", "Oscar", "Grant", "Eduardo", "Peter", "Henry", "Parker", "Hayden", "Collin", "George", "Bradley", "Mitchell", "Devon", "Ricardo", "Shawn", "Taylor", "Nicolas", "Gregory", "Francisco", "Liam", "Kaleb", "Preston", "Erik", "Alexis", "Owen", "Omar", "Diego", "Dustin", "Corey", "Fernando", "Clayton", "Carter", "Ivan", "Jaden", "Javier", "Alec", "Johnathan", "Scott", "Manuel", "Cristian", "Alan", "Raymond", "Brett", "Max", "Andres", "Gage", "Mario", "Dawson", "Dillon", "Cesar", "Wesley", "Levi", "Jakob", "Chandler", "Martin", "Malik", "Edgar", "Trenton", "Sergio", "Nolan", "Josiah", "Marco", "Peyton", "Harrison", "Hector", "Micah", "Roberto", "Drew", "Erick", "Brady", "Conner", "Jonah", "Casey", "Jayden", "Emmanuel", "Edwin", "Andre", "Phillip", "Brayden", "Landon", "Emily", "Hannah", "Madison", "Ashley", "Sarah", "Alexis", "Samantha", "Jessica", "Taylor", "Elizabeth", "Lauren", "Alyssa", "Kayla", "Abigail", "Brianna", "Olivia", "Emma", "Megan", "Grace", "Victoria", "Rachel", "Anna", "Sydney", "Destiny", "Morgan", "Jennifer", "Jasmine", "Haley", "Julia", "Kaitlyn", "Nicole", "Amanda", "Katherine", "Natalie", "Hailey", "Alexandra", "Savannah", "Chloe", "Rebecca", "Stephanie", "Maria", "Sophia", "Mackenzie", "Allison", "Isabella", "Amber", "Mary", "Danielle", "Gabrielle", "Jordan", "Brooke", "Michelle", "Sierra", "Katelyn", "Andrea", "Madeline", "Sara", "Kimberly", "Courtney", "Erin", "Brittany", "Vanessa", "Jacqueline", "Jenna", "Caroline", "Faith", "Makayla", "Bailey", "Paige", "Shelby", "Melissa", "Kaylee", "Christina", "Trinity", "Caitlin", "Mariah", "Autumn", "Marissa", "Angela", "Breanna", "Catherine", "Zoe", "Briana", "Jada", "Laura", "Claire", "Alexa", "Kelsey", "Kathryn", "Leslie", "Alexandria", "Sabrina", "Isabel", "Mia", "Molly", "Leah", "Katie", "Gabriella", "Cheyenne", "Cassandra", "Tiffany", "Erica", "Lindsey", "Kylie", "Diana", "Amy", "Cassidy", "Mikayla", "Ariana", "Margaret", "Kelly", "Miranda", "Maya", "Melanie", "Audrey", "Jade", "Gabriela", "Caitlyn", "Angel", "Jillian", "Alicia", "Jocelyn", "Erika", "Lily", "Madelyn", "Heather", "Adriana", "Arianna", "Lillian", "Kiara", "Riley", "Crystal", "Mckenzie", "Meghan", "Skylar", "Ana", "Britney", "Angelica", "Kennedy", "Chelsea", "Daisy", "Kristen", "Veronica", "Isabelle", "Summer", "Hope", "Brittney", "Hayley", "Lydia", "Evelyn", "Bethany", "Shannon", "Michaela", "Karen", "Jamie", "Daniela", "Angelina", "Kaitlin", "Karina", "Sophie", "Sofia", "Diamond", "Payton", "Cynthia", "Alexia", "Valerie", "Monica", "Peyton", "Carly", "Bianca", "Hanna", "Brenda", "Rebekah", "Alejandra", "Mya", "Avery", "Brooklyn", "Ashlyn", "Lindsay", "Ava", "Desiree", "Alondra", "Camryn", "Ariel", "Naomi", "Jordyn", "Kendra", "Mckenna", "Holly", "Julie", "Kendall", "Kara", "Jasmin", "Selena", "Esmeralda", "Amaya", "Kylee", "Maggie", "Makenzie", "Claudia" };
        private static readonly string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "García", "Rodríguez", "Wilson", "Martínez", "Anderson", "Taylor", "Thomas", "Hernández", "Moore", "Martin", "Jackson", "Thompson", "White", "López", "Lee", "González", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Pérez", "Hall", "Young", "Allen", "Sánchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson", "Hill", "Ramírez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gómez", "Kelly", "Howard", "Ward", "Cox", "Díaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutiérrez", "Perry", "Butler", "Barnes", "Fisher" };
        
        private static DateTime minSqlServerDate = new DateTime(1753, 1, 1);
        private static DateTime maxSqlServerDate = new DateTime(9999, 12, 31);

        public static string NumericString(int length)
        {
            var sb = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                sb.Append(generator.Next(0, 9).ToString());
            }
            return sb.ToString();
        }

        public static int Int()
        {
            return generator.Next(int.MinValue, int.MaxValue);
        }

        public static int Int(int minValue, int maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        public static int PositiveInt()
        {
            return Int(0, int.MaxValue);
        }

        public static int PositiveInt(int maxValue)
        {
            return Int(0, maxValue);
        }

        public static short Short()
        {
            return generator.Next(short.MinValue, short.MaxValue);
        }

        public static short Short(short minVaue, short maxValue)
        {
            return generator.Next(minVaue, maxValue);
        }

        public static short PositiveShort()
        {
            return Short(0, short.MaxValue);
        }

        public static short PositiveShort(short maxValue)
        {
            return Short(0, maxValue);
        }

        public static long Long()
        {
            return generator.Next(long.MinValue, long.MaxValue);
        }

        public static long Long(long minVaue, long maxValue)
        {
            return generator.Next(minVaue, maxValue);
        }

        public static long PositiveLong()
        {
            return Long(0, long.MaxValue);
        }

        public static long PositiveLong(long maxValue)
        {
            return Long(0, maxValue);
        }

        public static uint UInt()
        {
            return generator.Next(uint.MinValue, uint.MaxValue);
        }

        public static ulong ULong()
        {
            return generator.Next(ulong.MinValue, ulong.MaxValue);
        }

        public static ushort UShort()
        {
            return generator.Next(ushort.MinValue, ushort.MaxValue);
        }

        public static decimal Decimal()
        {
            return generator.Next(decimal.MinValue, decimal.MaxValue);
        }

        public static decimal Decimal(decimal minValue, decimal maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        public static decimal PositiveDecimal()
        {
            return Decimal(0, decimal.MaxValue);
        }

        public static decimal PositiveDecimal(decimal maxValue)
        {
            return Decimal(0, maxValue);
        }

        public static float Float()
        {
            return generator.Next(float.MinValue, float.MaxValue);
        }

        public static float Float(float minVaue, float maxValue)
        {
            return generator.Next(minVaue, maxValue);
        }

        public static float PositiveFloat()
        {
            return Float(0, float.MaxValue);
        }

        public static float PositiveFloat(float maxValue)
        {
            return Float(0, maxValue);
        }

        public static double Double()
        {
            return generator.Next(double.MinValue, double.MaxValue);
        }

        public static double Double(double minVaue, double maxValue)
        {
            return generator.Next(minVaue, maxValue);
        }

        public static double PositiveDouble()
        {
            return Double(0, double.MaxValue);
        }

        public static double PositiveDouble(double maxValue)
        {
            return Double(0, maxValue);
        }

        public static DateTime DateTime()
        {
            return generator.Next(minSqlServerDate, maxSqlServerDate);
        }

        public static DateTime DateTime(DateTime minValue, DateTime maxValue)
        {
            return generator.Next(minValue, maxValue);
        }

        public static DateTime DateTimeFrom(DateTime minValue)
        {
            return generator.Next(minValue, maxSqlServerDate);
        }

        public static DateTime DateTimeThrough(DateTime maxValue)
        {
            return generator.Next(minSqlServerDate, maxValue);
        }

        public static bool Boolean()
        {
            return generator.Next(0, 2) != 0;
        }

        public static byte Byte()
        {
            return generator.Next(byte.MinValue, byte.MaxValue);
        }

        public static sbyte SByte()
        {
            return generator.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        public static char Char()
        {
            return generator.Next(char.MinValue, char.MaxValue);
        }

        public static Guid Guid()
        {
            return generator.NextGuid();
        }

        public static string FirstName()
        {
            return firstNames[generator.Next(0, firstNames.Length - 1)];
        }

        public static string LastName()
        {
            return lastNames[generator.Next(0, lastNames.Length - 1)];
        }

        public static string Phrase(int length)
        {
            var count = latinWords.Length;
            var result = string.Empty;
            var done = false;
            while (!done)
            {
                var word = latinWords[generator.Next(0, count - 1)];
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

        public static string String(int length)
        {
            return String(length, null, null);
        }

        public static string UpperCaseString(int length)
        {
            return String(length, true, null);
        }

        public static string LowerCaseString(int length)
        {
            return String(length, false, null);
        }

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

        public static char Letter(bool? upperCase)
        {
            upperCase = upperCase ?? Boolean();
            var startingCharCode = (short)(upperCase.Value ? 'A' : 'a');
            return ((char)(generator.Next(startingCharCode, startingCharCode + 26)));
        }

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

            public static string County()
            {
                return _counties[generator.Next(0, _counties.Length - 1)];
            }
        }

        public static class Usa
        {
            private static readonly string[] states = { "TX", "CO", "GA", "LA", "NY", "CA" };

            public static string PhoneNumber()
            {
                return string.Format("{0:000}-{1:000}-{2:0000}",
                    Int(200, 999), Int(200, 999), Int(0, 9999));
            }

            public static string SocialSecurityNumber()
            {
                return string.Format("{0}-{1}-{2}", NumericString(3), NumericString(2), NumericString(4));
            }

            public static string State()
            {
                return states[generator.Next(0, states.Length - 1)];
            }
        }

        public static string Email()
        {
            return string.Format("{0}@{1}.com", String(8), String(7));
        }

        public static string WwwUrl()
        {
            return string.Format("www.{0}.com", String(10, false));
        }

        public static string Url()
        {
            return string.Format("{0}.{1}.com", String(5, false), String(10, false));
        }

        public static string IpAddress()
        {
            return string.Format("{0}.{1}.{2}.{3}",
                PositiveInt(255), PositiveInt(255), PositiveInt(255), PositiveInt(255));
        }

        public static T Enumeration<T>() where T : struct
        {
            var values = Enum.GetValues(typeof(T));
            var index = PositiveInt(values.Length - 1);
            return (T)values.GetValue(index);
        }

        public static Enum Enumeration(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("{0} is not an enum type.", type.FullName), "type");
            }
            var values = Enum.GetValues(type);
            var index = PositiveInt(values.Length - 1);
            return (Enum)values.GetValue(index);
        }   
    }
}