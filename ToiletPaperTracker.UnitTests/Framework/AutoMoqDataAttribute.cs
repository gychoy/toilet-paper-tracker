using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToiletPaperTrackerUnitTests.Framework
{
    /// <summary>
    /// AutoMoqDataAttribute
    /// </summary>
    /// <seealso cref="https://blog.ploeh.dk/2010/10/08/AutoDataTheorieswithAutoFixture/"/>
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}
