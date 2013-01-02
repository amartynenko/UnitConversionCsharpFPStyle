using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace UnitConversion
{
    [TestFixture]
    public abstract class InvariantCultureFixture
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
    }
}
