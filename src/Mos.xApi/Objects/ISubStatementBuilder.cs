using System;
using System.Collections.Generic;

namespace Mos.xApi.Objects
{
    public interface ISubStatementBuilder
    {
        ISubStatementBuilder AddAttachment(Attachment attachment);

        ISubStatementBuilder AddAttachments(IEnumerable<Attachment> attachment);

        SubStatement Build();

        ISubStatementBuilder WithContext(Context context);

        ISubStatementBuilder WithResult(Result result);

        ISubStatementBuilder WithTimeStamp(DateTime timeStamp);
    }
}