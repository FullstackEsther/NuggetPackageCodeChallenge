using System;
using System.Globalization;
using Xunit;
using DateResolver;

namespace DateKeywordTest
{
    public class DateKeywordResolverTests
    {
        [Fact]
        public void Resolve_ShouldHandleBasicKeywords()
        {
            string input = "file-NOW.txt";
            string result = DateKeywordResolver.Resolve(input);
            Assert.Contains(DateTime.UtcNow.ToString("yyyyMMdd"), result);
        }

        [Fact]
        public void Resolve_ShouldHandleYesterday()
        {
            string input = "file-YESTERDAY.txt";
            string result = DateKeywordResolver.Resolve(input);
            Assert.Contains(DateTime.UtcNow.AddDays(-1).ToString("yyyyMMdd"), result);
        }

        [Fact]
        public void Resolve_ShouldApplyOffsets()
        {
            string input = "file-NOW+1d.txt";
            string result = DateKeywordResolver.Resolve(input);
            Assert.Contains(DateTime.UtcNow.AddDays(1).ToString("yyyyMMdd"), result);
        }
        [Fact]
        public void Resolve_ShouldThrowForInvalidInput()
        {
            Assert.Throws<ArgumentException>(() => DateKeywordResolver.Resolve(null));
            Assert.Throws<ArgumentException>(() => DateKeywordResolver.Resolve(""));
        }
        // [Fact]
        // public void Resolve_ShouldThrowForInvalidOffset()
        // {
        //     string input = "file-NOW+x.txt";
        //     var exception = Assert.Throws<FormatException>(() => DateKeywordResolver.Resolve(input));
        //     Assert.Equal("Invalid offset format: x", exception.Message);
        // }
        // [Fact]
        // public void Resolve_ShouldThrowForInvalidFormat()
        // {
        //     string input = "report-Format(NOW, \"invalid_format\").txt";
        //     var exception = Assert.Throws<FormatException>(() => DateKeywordResolver.Resolve(input));
        //     Assert.Contains("Invalid date format: \"invalid_format\"", exception.Message);
        // }


    }
}
