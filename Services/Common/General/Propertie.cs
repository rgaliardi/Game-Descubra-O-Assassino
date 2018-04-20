namespace System.Web
{
    //Class instance = new Class();
    //instance.Propertie = 32;
    //int someInt = instance.Propertie;
    public class Propertie<T>
    {
        private T _value;

        public T Value
        {
            get
            {
                // insert desired logic here
                return _value;
            }
            set
            {
                // insert desired logic here
                _value = value;
            }
        }

        public static implicit operator T(Propertie<T> value)
        {
            return value.Value;
        }

        public static implicit operator Propertie<T>(T value)
        {
            return new Propertie<T> { Value = value };
        }
    }
}