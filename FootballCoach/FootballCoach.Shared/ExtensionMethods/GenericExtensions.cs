using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace FootballCoach.ExtensionMethods
{
    [DebuggerStepThrough]
    public static class GenericExtensions
    {
        public static T ThrowIfNull<T>(this T value, Exception e)
        {
            if (value == null)
            {
                throw e; // Use a better exception of course
            }
            return value;
        }

        public static T ThrowIfNull<T>(this T value, String message)
        {
            if (value == null)
            {
                throw new NullReferenceException("Type : " + typeof(T).ToString() + " is null. Message : " + message);
            }
            return value;
        }

        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (source == null) throw new ArgumentNullException("source");
            return list.Contains(source);
        }

        // <summary>
        // Get the name of a static or instance property from a property access lambda.
        // </summary>
        // <typeparam name="T">Type of the property</typeparam>
        // <param name="propertyLambda">lambda expression of the form: '() => Class.Property' or '() => object.Property'</param>
        // <returns>The name of the property</returns>
        
        public static string GetPropertyName<T>(this T value, Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

    }
}
