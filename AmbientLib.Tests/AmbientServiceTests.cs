using System;
using FluentAssertions;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microservices.UnitTests
{
    [TestClass]
    public class AmbientServiceTests
    {
        [TestInitialize]
        public void BeforeEach()
        {
            Clock.Reset();
        }

        [TestMethod]
        public void GIVEN_null_service_provider_WHEN_setting_THEN_should_throw()
        {
            Action action = () => Clock.ProvidedBy(null);

            action
                .Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'serviceProvider')");
        }

        [TestMethod]
        public void GIVEN_defaults_WHEN_getting_current_THEN_should_be_default()
        {
            Clock.Current
                .Should().BeOfType(Clock.DefaultType);
        }

        [TestMethod]
        public void GIVEN_provider_set_WHEN_getting_current_THEN_should_be_type_of_client()
        {
            var customAppNameProvider = new SystemClockProvider();
            Clock.ProvidedBy(() => customAppNameProvider);
            Clock.Current
                .Should().BeOfType(customAppNameProvider.GetType());
        }

        [TestMethod]
        public void GIVEN_provider_set_to_substitute_WHEN_getting_current_THEN_should_get_name_from_substitute()
        {
            var expected = DateTimeOffset.Now;
            var substitute = Substitute.For<IClockProvider>();
            substitute.GetDateTimeOffsetNow().Returns(expected);

            Clock.ProvidedBy(() => substitute);
            Clock.Current
                .GetDateTimeOffsetNow()
                .Should().Be(expected);
        }
    }
}