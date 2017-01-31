using System;

namespace Mos.xApi.Data
{
    internal class ResultBuilder : IResultBuilder
    {
        private bool? _completion;
        private TimeSpan? _duration;
        private Extension _extensions;
        private string _response;
        private Score _score;
        private bool? _success;

        public ResultBuilder()
        {
            _extensions = new Extension();
        }

        public IResultBuilder AddExtension(string key, string json) => AddExtension(new Uri(key), json);

        public IResultBuilder AddExtension(Uri key, string json)
        {
            _extensions.Add(key, json);
            return this;
        }

        public Result Build()
        {
            return new Result(_score, _success, _completion, _response, _duration, _extensions);
        }

        public IResultBuilder WithCompletion(bool completion)
        {
            _completion = completion;
            return this;
        }

        public IResultBuilder WithDuration(TimeSpan duration)
        {
            _duration = duration;
            return this;
        }

        public IResultBuilder WithResponse(string response)
        {
            _response = response;
            return this;
        }

        public IResultBuilder WithScore(Score score)
        {
            _score = score;
            return this;
        }

        public IResultBuilder WithSuccess(bool result)
        {
            _success = result;
            return this;
        }
    }
}