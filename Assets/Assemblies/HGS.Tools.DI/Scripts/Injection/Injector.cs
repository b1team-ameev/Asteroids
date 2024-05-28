using System;
using System.Linq;
using System.Reflection;
using HGS.Tools.DI.Contexts;

namespace HGS.Tools.DI.Injection {

    public static class Injector {

        public static void Inject(object iObject) {

            if (iObject == null) {

                return;

            }
            
            MethodInfo resolveMethod = typeof(GlobalContext).GetMethod("ResolveOrRemember", BindingFlags.Static | BindingFlags.Public);

            if (resolveMethod != null) {

                Type type = iObject.GetType();

                MethodInfo[] methods = GetMethods(type);

                foreach(var method in methods) {
 
                    object attribute = method?.GetCustomAttribute(typeof(InjectAttribute));

                    if (attribute != null) {

                        ParameterInfo[] arParams = method.GetParameters();

                        if (arParams != null && arParams.Length > 0) {

                            object[] arguments = new object[arParams.Length];

                            int i = 0;
                            foreach(var param in arParams) {

                                MethodInfo resolveMethodVariant = resolveMethod.MakeGenericMethod(new Type[] { param.ParameterType });
                                arguments[i++] = resolveMethodVariant.Invoke(null, new object[]{ iObject });
                                
                            }

                            method.Invoke(iObject, arguments);

                        }

                    }        

                }

            }

        }

        private static MethodInfo[] GetMethods(Type type) {

            MethodInfo[] methods = type?.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            
            methods = methods?.Union(GetPrivateMethods(type))?.ToArray();            

            return methods;

        }

        private static MethodInfo[] GetPrivateMethods(Type type) {

            MethodInfo[] methods = type?.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            if (type?.BaseType != null) {

                methods = methods?.Union(GetPrivateMethods(type?.BaseType))?.ToArray();

            }

            return methods;

        }

    }

}