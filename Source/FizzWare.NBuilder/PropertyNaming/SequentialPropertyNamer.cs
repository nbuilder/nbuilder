using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using System.Reflection;
using System.Diagnostics;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.PropertyNaming
{
    public class SequentialPropertyNamer : PropertyNamer
    {
        public SequentialPropertyNamer(IReflectionUtil reflectionUtil) 
            : base(reflectionUtil)
        {
        }

        private int sequenceNumber;
        

        public override void SetValuesOfAllIn<T>(IList<T> objects)
        {
            sequenceNumber = 1;

            var type = typeof(T);

            for (int i = 0; i < objects.Count; i++)
            {
                foreach (var propertyInfo in type.GetProperties(FLAGS))
                {
                    SetMemberValue(propertyInfo, objects[i]);
                }

                foreach (var fieldInfo in type.GetFields())
                {
                    SetMemberValue(fieldInfo, objects[i]);
                }

                sequenceNumber++;
            }
        }

        public override void SetValuesOf<T>(T obj)
        {
            sequenceNumber = 1;
            base.SetValuesOf(obj);
        }

        /// <summary>
        /// Gets the new sequence number taking into account a maximum value.
        /// 
        /// If the current sequence number is above the maximum value it will 
        /// reset it to zero, and continue the sequence from there until the maximum 
        /// value is reached again.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        private static int GetNewSequenceNumber(int sequenceNumber, int maxValue)
        {
            int newSequenceNumber;
            if (sequenceNumber > maxValue)
            {
                int divisor = sequenceNumber / maxValue;
                newSequenceNumber = sequenceNumber - (divisor * maxValue);
            }
            else
                newSequenceNumber = sequenceNumber;

            return newSequenceNumber;
        }

        protected override short GetInt16(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(sequenceNumber, short.MaxValue);
            return Convert.ToInt16(newSequenceNumber);
        }

        protected override int GetInt32(MemberInfo memberInfo)
        {
            return sequenceNumber;
        }

        protected override long GetInt64(MemberInfo memberInfo)
        {
            return Convert.ToInt64(sequenceNumber);
        }

        protected override decimal GetDecimal(MemberInfo memberInfo)
        {
            return Convert.ToDecimal(sequenceNumber);
        }

        protected override float GetSingle(MemberInfo memberInfo)
        {
            return Convert.ToSingle(sequenceNumber);
        }

        protected override double GetDouble(MemberInfo memberInfo)
        {
            return Convert.ToDouble(sequenceNumber);
        }

        protected override ushort GetUInt16(MemberInfo memberInfo)
        {
            return Convert.ToUInt16(sequenceNumber);
        }

        protected override uint GetUInt32(MemberInfo memberInfo)
        {
            return Convert.ToUInt32(sequenceNumber);
        }

        protected override ulong GetUInt64(MemberInfo memberInfo)
        {
            return Convert.ToUInt64(sequenceNumber);
        }

        protected override char GetChar(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(sequenceNumber, 26);
            newSequenceNumber += 64;

            return Convert.ToChar(newSequenceNumber);
        }

        protected override byte GetByte(MemberInfo memberInfo)
        {
            int newSequenceNumber = GetNewSequenceNumber(sequenceNumber, byte.MaxValue);

            return Convert.ToByte(newSequenceNumber);
        }

        protected override DateTime GetDateTime(MemberInfo memberInfo)
        {
            return DateTime.Now.Date.AddDays(sequenceNumber - 1);
        }

        protected override string GetString(MemberInfo memberInfo)
        {
            return memberInfo.Name + sequenceNumber;
        }

        protected override bool GetBoolean(MemberInfo memberInfo)
        {
            return (sequenceNumber % 2) == 0 ? true : false;
        }

        // TODO: Implement this
        //public static void AddHandlerFor<T>(Func<MemberInfo, int, T> func)
        //{
        //    throw new NotImplementedException();
        //}
    }
}