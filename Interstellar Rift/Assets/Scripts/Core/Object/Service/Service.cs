using System;

namespace Core.Object.Service
{
    public abstract class Service : IDisposable
    {
        public abstract void Dispose();
    }
}
