﻿using Xunit;
using Moq;
using WebApp.Models.HtmlNotifications;

namespace Tests
{
    public class ResultNotificationHtmlProviderTests
    {
        private HtmlNotificationProvider provider;

        public ResultNotificationHtmlProviderTests()
        {
            provider = new HtmlNotificationProvider();
        }
        [Fact]
        public void ReturnValidHtmlStringUsingGivenClassAndContent()
        {
            var @out = provider.GetNotificationBody("cl-test","content");

            Assert.Equal("<div class =\"notification\"><p class=\"cl-test\">content</p></div>", @out);
        }

    }
}
