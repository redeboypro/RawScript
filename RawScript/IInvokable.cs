using System;

namespace RawScript
{
    public interface IInvokable : IDisposable
    {
        void Invoke();
    }
}