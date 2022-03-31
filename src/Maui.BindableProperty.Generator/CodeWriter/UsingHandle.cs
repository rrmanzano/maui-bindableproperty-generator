using System;

// Based on https://github.com/SaladLab/CodeWriter
namespace Maui.BindableProperty.Generator
{
    public sealed class UsingHandle : IDisposable
    {
        private Action _disposed;

        public UsingHandle(Action disposed)
        {
            _disposed = disposed;
        }

        public void Dispose()
        {
            if (_disposed != null)
            {
                _disposed();
                _disposed = null;
            }
        }
    }
}
