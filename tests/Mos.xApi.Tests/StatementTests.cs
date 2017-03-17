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
    }
}
