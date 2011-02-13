using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FizzWare.NBuilder.Implementation;
using System.Text;

namespace FizzWare.NBuilder.PropertyNaming
{
    public class SequentialPropertyNamer : PropertyNamer
    {
        public SequentialPropertyNamer(IReflectionUtil reflectionUtil) 
            : base(reflectionUtil)
        {
        }

        private int _sequenceNumber;

        public override void SetValuesOfAllIn<T>(IList<T> objects)
        {
            _sequenceNumber = 1;

            var type = typeof(T);

            for (int i = 0; i < objects.Count; i++)
            {
                foreach (var propertyInfo in type.GetProperties(FLAGS))
                {
                    SetMemberValue(propertyInfo, objects[i]);
                }

                foreach (var fieldInfo in type.GetFields().Where(f => !f.IsLiteral))
                {
                    SetMemberValue(fieldInfo, objects[i]);
                }

                _sequenceNumber++;
            }
        }

        public override void SetValuesOf<T>(T obj)
        {
            _sequenceNumber = 1;
            base.SetValuesOf(obj);
        }

        /// <summary>
        /// Gets the new sequence number taking into account a maximum value.
        /// 
        /// If the current sequence number is above the maximum value it will 
        /// reset it to one, and continue the sequence from there until the maximum 
        /// value is reached again.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        private static int GetNewSequenceNumber(int sequenceNumber, int maxValue)
        {
            int newSequenceNumber = sequenceNumber % maxValue;
			if (newSequenceNumber == 0)
			{
				newSequenceNumber = maxValue;
			}

			return newSequenceNumber;
        }

        protected override short GetInt16(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(_sequenceNumber, short.MaxValue);
            return Convert.ToInt16(newSequenceNumber);
        }

        protected override int GetInt32(MemberInfo memberInfo)
        {
            return _sequenceNumber;
        }

        protected override long GetInt64(MemberInfo memberInfo)
        {
            return Convert.ToInt64(_sequenceNumber);
        }

        protected override decimal GetDecimal(MemberInfo memberInfo)
        {
            return Convert.ToDecimal(_sequenceNumber);
        }

        protected override float GetSingle(MemberInfo memberInfo)
        {
            return Convert.ToSingle(_sequenceNumber);
        }

        protected override double GetDouble(MemberInfo memberInfo)
        {
            return Convert.ToDouble(_sequenceNumber);
        }

        protected override ushort GetUInt16(MemberInfo memberInfo)
        {
            return Convert.ToUInt16(_sequenceNumber);
        }

        protected override uint GetUInt32(MemberInfo memberInfo)
        {
            return Convert.ToUInt32(_sequenceNumber);
        }

        protected override ulong GetUInt64(MemberInfo memberInfo)
        {
            return Convert.ToUInt64(_sequenceNumber);
        }

        protected override char GetChar(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(_sequenceNumber, 26);
            newSequenceNumber += 64;

            return Convert.ToChar(newSequenceNumber);
        }

        protected override byte GetByte(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(_sequenceNumber, byte.MaxValue);

            return Convert.ToByte(newSequenceNumber);
        }

        protected override sbyte GetSByte(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(_sequenceNumber, sbyte.MaxValue);

            return Convert.ToSByte(newSequenceNumber);
        }

        protected override DateTime GetDateTime(MemberInfo memberInfo)
        {
            return DateTime.Now.Date.AddDays(_sequenceNumber - 1);
        }

        protected override string GetString(MemberInfo memberInfo)
        {
            return memberInfo.Name + _sequenceNumber;
        }

        protected override bool GetBoolean(MemberInfo memberInfo)
        {
            return (_sequenceNumber % 2) == 0 ? true : false;
        }

		protected override Enum GetEnum(MemberInfo memberInfo)
		{
			Type enumType = GetMemberType(memberInfo);
			var enumValues = GetEnumValues(enumType);
			int newSequenceNumber = GetNewSequenceNumber(_sequenceNumber, enumValues.Length);
			return Enum.Parse(enumType, enumValues.GetValue(newSequenceNumber - 1).ToString(), true) as Enum;
		}

        protected override Guid GetGuid(MemberInfo memberInfo)
        {
            var bytes = new byte[16];
            var convertedBytes = BitConverter.GetBytes(_sequenceNumber);

            bytes[12] = convertedBytes[3];
            bytes[13] = convertedBytes[2];
            bytes[14] = convertedBytes[1];
            bytes[15] = convertedBytes[0];

            return new Guid(bytes);
        }

        // TODO: Implement this
        //public static void AddHandlerFor<T>(Func<MemberInfo, int, T> func)
        //{
        //    throw new NotImplementedException();
        //}
    }
}