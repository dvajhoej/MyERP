using System;
using System.Globalization;
using Xunit;

namespace LNE_Unit_test
{
    public class TestCulture
    {
        [Fact]
        public void EnsureCultureIsDanish()
        {
            var culture = new CultureInfo("da-DK");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            // Act
            var currentCulture = CultureInfo.CurrentCulture;

            Assert.Equal("da-DK", currentCulture.Name);
        }
    }
}
