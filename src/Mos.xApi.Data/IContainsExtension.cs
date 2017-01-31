using System;

namespace Mos.xApi.Data
{
    public interface IContainsExtension<T>
    {
        T AddExtension(string extensionUri, string jsonContent);

        T AddExtension(Uri extension, string jsonContent);

        T AddExtension(Extension extension);
    }
}