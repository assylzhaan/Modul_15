using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modul15PR
{
    public class MyClass
    {
        public int PublicProperty { get; set; }
        private string PrivateProperty { get; set; }
        public readonly int ReadOnlyField = 15;
        private int privateField;

        public MyClass() { }

        private MyClass(string privateValue)
        {
            PrivateProperty = privateValue;
        }

        public void PublicMethod()
        {
            Console.WriteLine("Public method called");
        }

        private void PrivateMethod()
        {
            Console.WriteLine("Private method called");
        }

        public void ChangeState()
        {
            privateField++;
            Console.WriteLine($"State changed: privateField = {privateField}");
        }
    }

    class Program
    {
        static void Main()
        {
            InvestigateType();
            CreateInstance();
            ManipulateObject();
            InvokePrivateMethod();
        }

        static void InvestigateType()
        {
            Type myClassType = typeof(MyClass);

            Console.WriteLine($"Class Name: {myClassType.Name}\n");

            Console.WriteLine("Constructors:");
            foreach (var constructor in myClassType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{constructor}");
            }

            Console.WriteLine("\nFields and Properties:");
            foreach (var field in myClassType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{field.FieldType} {field.Name}");
            }
            foreach (var property in myClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{property.PropertyType} {property.Name}");
            }

            Console.WriteLine("\nMethods:");
            foreach (var method in myClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{method.ReturnType} {method.Name}");
            }

            Console.WriteLine("\n-----------------------------------\n");
        }

        static void CreateInstance()
        {
            Type myClassType = typeof(MyClass);
            object myClassInstance = Activator.CreateInstance(myClassType, nonPublic: true);
            Console.WriteLine("MyClass instance created using Activator.CreateInstance.");

            Console.WriteLine("\n-----------------------------------\n");
        }

        static void ManipulateObject()
        {
            Type myClassType = typeof(MyClass);
            object myClassInstance = Activator.CreateInstance(myClassType, nonPublic: true);

            PropertyInfo publicProperty = myClassType.GetProperty("PublicProperty");
            publicProperty.SetValue(myClassInstance, 100);

            MethodInfo publicMethod = myClassType.GetMethod("ChangeState");
            publicMethod.Invoke(myClassInstance, null);

            Console.WriteLine("\n-----------------------------------\n");
        }

        static void InvokePrivateMethod()
        {
            Type myClassType = typeof(MyClass);
            object myClassInstance = Activator.CreateInstance(myClassType, nonPublic: true);

            MethodInfo privateMethod = myClassType.GetMethod("PrivateMethod", BindingFlags.Instance | BindingFlags.NonPublic);
            privateMethod.Invoke(myClassInstance, null);

            Console.WriteLine("\n-----------------------------------\n");
        }
    }
}

