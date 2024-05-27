using System;

namespace HGS.Tools.Services.ServiceEvents {

    public class UniversalDoubleEventArgs<T1, T2>: EventArgs {

        private readonly T1 val1;
        private readonly T2 val2;

        public T1 Value1 {

            get { return val1; }

        }

        public T2 Value2 {

            get { return val2; }

        }

        public UniversalDoubleEventArgs(T1 val1, T2 val2) {

            this.val1 = val1;
            this.val2 = val2;

        }

    }

}