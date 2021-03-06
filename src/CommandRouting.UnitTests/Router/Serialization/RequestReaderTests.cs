﻿using CommandRouting.Helpers;
using CommandRouting.Router.Serialization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.Formatters;
using NSubstitute;
using Xunit;

namespace CommandRouting.UnitTests.Router.Serialization
{
    public class RequestReaderTests
    {
        class Foo
        {
            
        }

        [Fact]
        public void DeserializeRequestAsync_should_delegate_reading_to_the_appropriate_input_formatter()
        {
            // Given a dummy input format and selector
            IInputFormatter inputFormatter = Substitute.For<IInputFormatter>();
            var inputFormatterResult = InputFormatterResult.SuccessAsync(new Foo());
            inputFormatter
                .ReadAsync(Arg.Any<InputFormatterContext>())
                .Returns(inputFormatterResult);


            // When I deserialize a request
            HttpContext httpContext = new DefaultHttpContext();
            RequestReader requestReader = new RequestReader(inputFormatter.ArrayOfOne());
            Foo result = requestReader.DeserializeRequestAsync<Foo>(httpContext).Result;

            // Then the input formatter should be used to read the model
            inputFormatter.Received(1).ReadAsync(Arg.Any<InputFormatterContext>());
        }
    }
}
