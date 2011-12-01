using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FizzWare.NBuilder.Implementation;
using System.Linq;

namespace FizzWare.NBuilder.PropertyNaming
{
    public class RandomValuePropertyNamer : PropertyNamer
    {
        private readonly bool generatePositiveValuesOnly;
        private readonly DateTime minDate;
        private readonly DateTime maxDate;
        private static readonly List<char> allowedChars;
        private static readonly string[] words;

        private readonly IRandomGenerator generator;
        private readonly bool useLoremIpsumForStrings;

        public RandomValuePropertyNamer()
            : this (new RandomGenerator(), new ReflectionUtil(), false)
        {
        }

        public RandomValuePropertyNamer(IRandomGenerator generator, IReflectionUtil reflectionUtil, bool generatePositiveValuesOnly)
            : this(generator, reflectionUtil, generatePositiveValuesOnly, DateTime.MinValue, DateTime.MaxValue, false)
        {
            this.generator = generator;
        }

        public RandomValuePropertyNamer(IRandomGenerator generator, IReflectionUtil reflectionUtil, bool generatePositiveValuesOnly, DateTime minDate, DateTime maxDate, bool useLoremIpsumForStrings)
            : base(reflectionUtil)
        {
            this.generator = generator;
            this.generatePositiveValuesOnly = generatePositiveValuesOnly;
            this.useLoremIpsumForStrings = useLoremIpsumForStrings;
            this.minDate = minDate;
            this.maxDate = maxDate;
        }

        static RandomValuePropertyNamer()
        {
            allowedChars = new List<char>();
            for (char c = 'a'; c < 'z'; c++)
                allowedChars.Add(c);

            for (char c = 'A'; c < 'Z'; c++)
                allowedChars.Add(c);

            for (char c = '0'; c < '9'; c++)
                allowedChars.Add(c);

            //

            words =
                @"lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborum".Split(' ');
        }

        public override void SetValuesOfAllIn<T>(IList<T> objects)
        {
            var type = typeof(T);

            for (int i = 0; i < objects.Count; i++)
            {
                foreach (var propertyInfo in type.GetProperties(FLAGS).Where(p => p.CanWrite))
                {
                    SetMemberValue(propertyInfo, objects[i]);
                }

                foreach (var fieldInfo in type.GetFields().Where(f => !f.IsLiteral))
                {
                    SetMemberValue(fieldInfo, objects[i]);
                }
            }
        }

        protected override short GetInt16(MemberInfo memberInfo)
        {
            var minValue = generatePositiveValuesOnly ? (short)0 : short.MinValue;
            return generator.Next(minValue, short.MaxValue);
        }

        protected override int GetInt32(MemberInfo memberInfo)
        {
            var minValue = generatePositiveValuesOnly ? 0 : int.MinValue;
            return generator.Next(minValue, int.MaxValue);
        }

        protected override long GetInt64(MemberInfo memberInfo)
        {
            var minValue = generatePositiveValuesOnly ? 0 : long.MinValue;
            return generator.Next(minValue, long.MaxValue);
        }

        protected override decimal GetDecimal(MemberInfo memberInfo)
        {
            var minValue = generatePositiveValuesOnly ? 0 : decimal.MinValue;
            return generator.Next(minValue, decimal.MaxValue);
        }

        protected override float GetSingle(MemberInfo memberInfo)
        {
            var minValue = generatePositiveValuesOnly ? 0 : float.MinValue;
            return generator.Next(minValue, float.MaxValue);
        }

        protected override double GetDouble(MemberInfo memberInfo)
        {
            var minValue = generatePositiveValuesOnly ? 0 : double.MinValue;
            return generator.Next(minValue, double.MaxValue);
        }

        protected override ushort GetUInt16(MemberInfo memberInfo)
        {
            return generator.Next(ushort.MinValue, ushort.MaxValue);
        }

        protected override uint GetUInt32(MemberInfo memberInfo)
        {
            return generator.Next(uint.MinValue, uint.MaxValue);
        }

        protected override ulong GetUInt64(MemberInfo memberInfo)
        {
            return generator.Next(ulong.MinValue, ulong.MaxValue);
        }

        protected override byte GetByte(MemberInfo memberInfo)
        {
            return generator.Next(byte.MinValue, byte.MaxValue);
        }

        protected override sbyte GetSByte(MemberInfo memberInfo)
        {
            return generator.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        protected override DateTime GetDateTime(MemberInfo memberInfo)
        {
            return generator.Next(minDate, maxDate);
        }

        protected override string GetString(MemberInfo memberInfo)
        {
            if (useLoremIpsumForStrings)
            {
                int length = generator.Next(1, 10);

                string[] sentence = new string[length];

                for (int i = 0; i < length; i++)
                {
                    int index = generator.Next(0, allowedChars.Count - 1);
                    sentence[i] = words[index];
                }

                return string.Join(" ", sentence);
            } 
            else
            {
                int length = generator.Next(0, 255);

                char[] chars = new char[length];

                for (int i = 0; i < length; i++)
                {
                    int index = generator.Next(0, allowedChars.Count - 1);
                    chars[i] = allowedChars[index];
                }

                byte[] bytes = Encoding.UTF8.GetBytes(chars);
                return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            }
        }

        protected override bool GetBoolean(MemberInfo memberInfo)
        {
            return generator.Next();
        }

        protected override char GetChar(MemberInfo memberInfo)
        {
            return generator.Next(char.MinValue, char.MaxValue);
        }

		protected override Enum GetEnum(MemberInfo memberInfo)
		{
			Type enumType = GetMemberType(memberInfo);
            var enumValues = GetEnumValues(enumType);
			return Enum.Parse(enumType,enumValues.GetValue(generator.Next(0, enumValues.Length)).ToString(), true) as Enum;
		}

        protected override Guid GetGuid(MemberInfo memberInfo)
        {
            return generator.Guid();
        }
    }
}