using System;

namespace Microservices
{
    /// <summary>
    /// A base class for creating ambient services
    /// </summary>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    /// <typeparam name="TDefault">The type of the default.</typeparam>
    public abstract class AmbientService<TInterface, TDefault> where TDefault : TInterface, new()
    {
        private static Lazy<TInterface> _instance = GetDefault();

        public static TInterface Current => _instance.Value;

        public static void ProvidedBy(Func<TInterface> serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _instance = new Lazy<TInterface>(serviceProvider);
        }

        internal static void Reset()
        {
            _instance = GetDefault();
        }

        private static Lazy<TInterface> GetDefault()
        {
            return new Lazy<TInterface>(() => new TDefault());
        }

        public static Type DefaultType => typeof(TDefault);
    }
}