namespace System.Web.Mvc
{
    public class Invoker : Singleton<Invoker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton"/> class.
        /// </summary>
        private Invoker() { }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        private object InvokerMember(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            Type _type = Type.GetTypeFromProgID(TypeFromProgID);
            object _target = Activator.CreateInstance(_type);
            object _value = _type.InvokeMember(MemberInvoker, System.Reflection.BindingFlags.InvokeMethod, null, _target, Parameters);

            return _value;
        }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        public void None(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            object _object = InvokerMember(TypeFromProgID, MemberInvoker, Parameters);
        }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        public object Object(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            object _object = InvokerMember(TypeFromProgID, MemberInvoker, Parameters);

            return _object;
        }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        public bool Boolean(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            object _object = InvokerMember(TypeFromProgID, MemberInvoker, Parameters);
            bool _value = (bool)_object;

            return _value;
        }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        public int Integer(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            object _object = InvokerMember(TypeFromProgID, MemberInvoker, Parameters);
            int _value = (int)_object;

            return _value;
        }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        public long Long(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            object _object = InvokerMember(TypeFromProgID, MemberInvoker, Parameters);
            long _value = (long)_object;

            return _value;
        }

        /// <summary>
        /// Chamada e execução de um componente COM+
        /// </summary>
        public string String(string TypeFromProgID, string MemberInvoker, object[] Parameters = null)
        {
            object _object = InvokerMember(TypeFromProgID, MemberInvoker, Parameters);
            string _value = (string)_object;

            return _value;
        }
    }
}
