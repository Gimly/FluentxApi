using System;

namespace Mos.xApi
{
    public interface IContainsExtension<T>
    {
        T AddExtension(string extensionUri, string jsonContent);

        T AddExtension(Uri extension, string jsonContent);

        T AddExtension(Extension extension);
    }
}