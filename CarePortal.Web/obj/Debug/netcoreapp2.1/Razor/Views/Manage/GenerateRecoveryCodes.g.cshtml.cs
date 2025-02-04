#pragma checksum "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e7045f8a82bfce523014d2d51f564a86a0726a37"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manage_GenerateRecoveryCodes), @"mvc.1.0.view", @"/Views/Manage/GenerateRecoveryCodes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manage/GenerateRecoveryCodes.cshtml", typeof(AspNetCore.Views_Manage_GenerateRecoveryCodes))]
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
#line 1 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Web;

#line default
#line hidden
#line 3 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Data.Models;

#line default
#line hidden
#line 4 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Data.Models.AccountViewModels;

#line default
#line hidden
#line 5 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Data.Models.ManageViewModels;

#line default
#line hidden
#line 1 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\_ViewImports.cshtml"
using CarePortal.Web.Views.Manage;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e7045f8a82bfce523014d2d51f564a86a0726a37", @"/Views/Manage/GenerateRecoveryCodes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73e7a73d591e31c021ae7fdd1f492536c53eaa3b", @"/Views/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1cf3dd13fab378e13bfa9a4f4df16f26a8c862f", @"/Views/Manage/_ViewImports.cshtml")]
    public class Views_Manage_GenerateRecoveryCodes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarePortal.Data.Models.ManageViewModels.GenerateRecoveryCodesViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml"
  
    ViewData["Title"] = "Recovery codes";
    ViewData.AddActivePage(ManageNavPages.TwoFactorAuthentication);

#line default
#line hidden
            BeginContext(198, 6, true);
            WriteLiteral("\r\n<h4>");
            EndContext();
            BeginContext(205, 17, false);
#line 7 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(222, 377, true);
            WriteLiteral(@"</h4>
<div class=""alert alert-warning"" role=""alert"">
    <p>
        <span class=""glyphicon glyphicon-warning-sign""></span>
        <strong>Put these codes in a safe place.</strong>
    </p>
    <p>
        If you lose your device and don't have the recovery codes you will lose access to your account.
    </p>
</div>
<div class=""row"">
    <div class=""col-md-12"">
");
            EndContext();
#line 19 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml"
         for (var row = 0; row < Model.RecoveryCodes.Count(); row += 2)
        {

#line default
#line hidden
            BeginContext(683, 18, true);
            WriteLiteral("            <code>");
            EndContext();
            BeginContext(702, 24, false);
#line 21 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml"
             Write(Model.RecoveryCodes[row]);

#line default
#line hidden
            EndContext();
            BeginContext(726, 7, true);
            WriteLiteral("</code>");
            EndContext();
            BeginContext(739, 6, true);
            WriteLiteral("&nbsp;");
            EndContext();
            BeginContext(752, 6, true);
            WriteLiteral("<code>");
            EndContext();
            BeginContext(759, 28, false);
#line 21 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml"
                                                                      Write(Model.RecoveryCodes[row + 1]);

#line default
#line hidden
            EndContext();
            BeginContext(787, 15, true);
            WriteLiteral("</code><br />\r\n");
            EndContext();
#line 22 "D:\Pacsquare\Projects\Health Portal\Source\CarePortal.Web\Views\Manage\GenerateRecoveryCodes.cshtml"
        }

#line default
#line hidden
            BeginContext(813, 18, true);
            WriteLiteral("    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarePortal.Data.Models.ManageViewModels.GenerateRecoveryCodesViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
