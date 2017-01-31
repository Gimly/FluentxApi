using System;

namespace Mos.xApi.Builders
{
    public interface IResultBuilder
    {
        IResultBuilder AddExtension(string key, string json);

        IResultBuilder AddExtension(Uri key, string json);

        Result Build();

        IResultBuilder WithCompletion(bool completion);

        IResultBuilder WithDuration(TimeSpan duration);

        IResultBuilder WithResponse(string response);

        IResultBuilder WithScore(Score score);

        IResultBuilder WithSuccess(bool result);
    }
}