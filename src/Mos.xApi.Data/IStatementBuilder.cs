using Mos.xApi.Data.Actors;
using System;
using System.Collections.Generic;

namespace Mos.xApi.Data
{
    public interface IStatementBuilder
    {
        IStatementBuilder AddAttachment(Attachment attachment);

        IStatementBuilder AddAttachments(IEnumerable<Attachment> attachment);

        Statement Build();

        IStatementBuilder WithAuthority(Actor authority);

        IStatementBuilder WithContext(Context context);

        IStatementBuilder WithContext(IContextBuilder contextBuilder);

        IStatementBuilder WithResult(Result result);

        IStatementBuilder WithResult(IResultBuilder resultBuilder);

        IStatementBuilder WithTimeStamp(DateTime timeStamp);
    }
}