using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;


namespace Jibrary.Data
{
    public class Randomizer : IDisposable
    {
        public const String DefaultCharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxz!@#$%^&*()_+=-\\][{}|;',./<>?~`";
        public Boolean Disposed
        {
            get;
            private set;
        }
        RandomNumberGenerator rng;
        public Randomizer()
        {
            rng = new RNGCryptoServiceProvider();
        }
        public Randomizer(RandomNumberGenerator NumberGenerator)
        {
            Disposed = false;
            rng = NumberGenerator;
        }

        public void FillProperties(Object obj, BindingFlags flags = BindingFlags.Default)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                if (typeof(Int32) == (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType))
                    property.SetValue(obj, GetInt32());
                if (typeof(String) == (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType))
                    property.SetValue(obj, GetString());
                if (typeof(DateTime) == (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType))
                    property.SetValue(obj, GetDateTime());
            }
        }

        public void FillFields(Object obj, BindingFlags flags = BindingFlags.Default)
        {
            foreach (var field in obj.GetType().GetFields())
            {
                if (typeof(Int32) == (Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType))
                    field.SetValue(obj, GetInt32());
                if (typeof(String) == (Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType))
                    field.SetValue(obj, GetString());
                if (typeof(DateTime) == (Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType))
                    field.SetValue(obj, GetDateTime());
            }
        }

        public void Fill(Object obj, BindingFlags flags = BindingFlags.Default)
        {
            FillProperties(obj, flags);
            FillFields(obj, flags);
        }

        public Int32 GetInt32()
        {
            Byte[] buffer = GetBytes(sizeof(int));
            return BitConverter.ToInt32(buffer, 0);
        }

        public String GetString()
        {
            return GetString(8);
        }

        public String GetString(int size)
        {
            return GetString(size, DefaultCharacterSet);
        }
        
        public String GetString(int size, String CharacterSet)
        {
            if (String.IsNullOrEmpty(CharacterSet))
                throw new ArgumentException("CharacterSet cannot be Null or Empty");
            if(size <= 0)
                throw new ArgumentException("Size must be a positive integer");

            String RandomString = String.Empty;
            for (int i = 0; i < size; i++)
                RandomString += CharacterSet[Math.Abs(GetInt32() % CharacterSet.Length)];

            return RandomString;
        }

        public DateTime GetDateTime()
        {
            return GetDateTime(DateTime.MinValue);
        }
        public DateTime GetDateTime(DateTime minValue)
        {
            return GetDateTime(minValue, DateTime.MaxValue);
        }
        public DateTime GetDateTime(DateTime minValue, DateTime maxValue)
        {
            int range = (maxValue - minValue).Days;
            range = Math.Abs(GetInt32()) % range;
            return minValue.AddDays(range);
        }   

        public Byte[] GetBytes(int size)
        {
            Byte[] buffer = new Byte[size];
            rng.GetBytes(buffer);
            return buffer;
        }
        
        protected virtual void Dispose(Boolean Disposing)
        {
            if(!Disposed)
                if (Disposing)
                    rng.Dispose();
            Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
