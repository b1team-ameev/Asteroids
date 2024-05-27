using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HGS.Tools.Services.Collections {

	public class ResourceCollection<T>: ICollection<T> where T : Object {

		private T[] objects;
		private string[] names;
		
		private static Dictionary<string, T[]> allObjects = new Dictionary<string, T[]>();

		public ResourceCollection(string spritesheet) {

			if (allObjects.ContainsKey(spritesheet)) {

				objects = allObjects[spritesheet];

			}
			else {
				
				objects = Resources.LoadAll<T>(spritesheet);
				allObjects.Add(spritesheet, objects);

			}

			names = new string[objects.Length];
			
			for(var i = 0; i < names.Length; i++) {

				names[i] = objects[i].name;

			}
			
		}
		
		public T GetObject(string name) {

			if (!names.Contains(name)) {

				return null;
				
			}

			return objects[System.Array.IndexOf(names, name)];

		}

	}

}