using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using System.Reflection;

namespace FizzWare.NBuilder.PropertyValueNaming
{
    public class SequentialPropertyNameNamingStrategy<T> : IPropertyValueNamingStategy<T>
    {
        public void SetValues(IList<T> objects)
        {
            var type = typeof(T);

            for (int i = 0; i < objects.Count; i++)
            {
                foreach (var propertyInfo in type.GetProperties())
                {
                    SetPropertyValue(propertyInfo, objects[i], i+1);
                }
            }
        }

        private static void SetPropertyValue(PropertyInfo propertyInfo, T obj, int sequenceNumber)
        {
            Type propertyType = propertyInfo.PropertyType;

            if (propertyType == typeof(short))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, sequenceNumber, null);
            }

            if (propertyType == typeof(int))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, sequenceNumber, null);
            }

            if (propertyType == typeof(long))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToInt64(sequenceNumber), null);
            }

            if (propertyType == typeof(decimal))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToDecimal(sequenceNumber), null);
            }

            if (propertyType == typeof(float))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToSingle(sequenceNumber), null);
            }

            if (propertyType == typeof(double))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToDouble(sequenceNumber), null);
            }

            if (propertyType == typeof(ushort))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToUInt16(sequenceNumber), null);
            }

            if (propertyType == typeof(uint))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToUInt32(sequenceNumber), null);
            }

            if (propertyType == typeof(ulong))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToUInt64(sequenceNumber), null);
            }

            if (propertyType == typeof(char))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToChar(sequenceNumber), null);
            }

            if (propertyType == typeof(byte))
            {
                // Reset it back to 0 if it exceeds 255
                int byteSequenceNumber = sequenceNumber == 256 ? 0 : sequenceNumber;

                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, Convert.ToByte(byteSequenceNumber), null);
            }

            if (propertyType == typeof(DateTime))
            {
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, DateTime.Now.AddDays(sequenceNumber), null);
            }

            if (propertyType == typeof(string))
            {
                var value = propertyInfo.Name + sequenceNumber;

                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, value, null);
            }
        }

        public void SetValue(T obj)
        {
            var type = typeof(T);

            foreach (var propertyInfo in type.GetProperties())
            {
                SetPropertyValue(propertyInfo, obj, 1);
            }
        }
    }
}