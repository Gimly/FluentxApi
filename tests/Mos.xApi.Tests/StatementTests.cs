using Newtonsoft.Json;
using System;
using Xunit;

namespace Mos.xApi.Tests
{
    public class StatementTests
    {
        [Fact]
        public void FromJsonWithNullValueShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("jsonString", () => Statement.FromJson(null));
        }

        [Fact]
        public void FromJsonWithEmptyValueShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("jsonString", () => Statement.FromJson(string.Empty));
        }

        [Fact]
        public void FromJsonWithNonJsonValueShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>("jsonString", () => Statement.FromJson("hello world"));

            Assert.NotNull(exception.InnerException);
            Assert.IsType<JsonReaderException>(exception.InnerException);
        }

        [Fact]
        public void FromJsonWithNonStatementJsonShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>("jsonString", () => Statement.FromJson("{\"hello\":\"world\"}"));

            Assert.NotNull(exception.InnerException);
            Assert.IsType<JsonSerializationException>(exception.InnerException);
        }

        [Fact]
        public void FromJsonWithStatementJsonMissingActorShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(
                "jsonString", 
                () => Statement.FromJson("{\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"}}"));

            Assert.NotNull(exception.InnerException);
            Assert.IsType<JsonSerializationException>(exception.InnerException);
        }

        [Fact]
        public void FromJsonWithStatementJsonMissingVerbShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(
                "jsonString",
                () => Statement.FromJson("{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"}}"));

            Assert.NotNull(exception.InnerException);
            Assert.IsType<JsonSerializationException>(exception.InnerException);
        }

        [Fact]
        public void FromJsonWithStatementJsonMissingObjectShouldThrowArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(
                "jsonString",
                () => Statement.FromJson("{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}}}"));

            Assert.NotNull(exception.InnerException);
            Assert.IsType<JsonSerializationException>(exception.InnerException);
        }
    }
}
