#pragma checksum "E:\XY.RabbitMQ\CoreMQ\Views\Home\MQTest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccb56e4a591f6a94d663c76bb91f09a664e8e0b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_MQTest), @"mvc.1.0.view", @"/Views/Home/MQTest.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/MQTest.cshtml", typeof(AspNetCore.Views_Home_MQTest))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\XY.RabbitMQ\CoreMQ\Views\_ViewImports.cshtml"
using CoreMQ;

#line default
#line hidden
#line 2 "E:\XY.RabbitMQ\CoreMQ\Views\_ViewImports.cshtml"
using CoreMQ.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccb56e4a591f6a94d663c76bb91f09a664e8e0b5", @"/Views/Home/MQTest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"280ca15e86a25bc3b3ac204268299c2d61783a77", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_MQTest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "E:\XY.RabbitMQ\CoreMQ\Views\Home\MQTest.cshtml"
  
    ViewData["Title"] = "MQTest";

#line default
#line hidden
            BeginContext(44, 340, true);
            WriteLiteral(@"
<h2>MQTest</h2>
<input type=""button"" value=""发送到消息队列""  id=""send"" onclick=""sendToMq()""/>
<script type=""text/javascript"">

    function sendToMq() {
        var url = ""/home/RabbitMQTest"";
        $.post(url, { content:'4567y8ui9ovgbhnj'}, function(d) {
            alert(d);
            
        });
    }

</script>
$.post()
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591