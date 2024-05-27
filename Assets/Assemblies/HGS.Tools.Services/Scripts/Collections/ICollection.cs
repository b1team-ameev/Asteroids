using System.Collections;
using UnityEngine;

namespace HGS.Tools.Services.Collections {

    public interface ICollection<T> where T : Object {

        public T GetObject(string name);

    }

}