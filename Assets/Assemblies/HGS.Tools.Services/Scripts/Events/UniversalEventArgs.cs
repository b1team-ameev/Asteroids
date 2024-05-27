using System;

namespace HGS.Tools.Services.ServiceEvents {

    public class UniversalEventArgs<T>: EventArgs {

        private readonly T val;

        public T Value {

            get { return val; }

        }

        public UniversalEventArgs(T val) {

            this.val = val;

        }

        public static explicit operator T(UniversalEventArgs<T> args) => args != null ? args.Value : default;

    }

}