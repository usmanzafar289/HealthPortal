#pragma checksum "C:\Projects\Health Portal\Source\CarePortal.Web\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17b14ccedc975d550cd9678cb77a007a9356fb75"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Index), @"mvc.1.0.view", @"/Views/Dashboard/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Dashboard/Index.cshtml", typeof(AspNetCore.Views_Dashboard_Index))]
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
#line 1 "C:\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "C:\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Web;

#line default
#line hidden
#line 3 "C:\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Data.Models;

#line default
#line hidden
#line 4 "C:\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Data.Models.AccountViewModels;

#line default
#line hidden
#line 5 "C:\Projects\Health Portal\Source\CarePortal.Web\Views\_ViewImports.cshtml"
using CarePortal.Data.Models.ManageViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17b14ccedc975d550cd9678cb77a007a9356fb75", @"/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73e7a73d591e31c021ae7fdd1f492536c53eaa3b", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/dashboard.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Appointment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-circle-arrow b-grey"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Projects\Health Portal\Source\CarePortal.Web\Views\Dashboard\Index.cshtml"
  
    ViewData["Title"] = "Dashboard";

#line default
#line hidden
            DefineSection("Styles", async() => {
                BeginContext(61, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            DefineSection("Scripts", async() => {
                BeginContext(83, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(89, 71, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6d4f5f99230a44cc8186bcf15deee715", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(160, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(165, 5578, true);
            WriteLiteral(@"<div class=""page-content-wrapper"">
    <div class=""content sm-gutter"">
        <div class=""container-fluid padding-25 sm-padding-10"">
            <div class=""row"">
                <div class=""col-lg-4 col-xl-2 col-xlg-2 "">
                    <div class=""row"">
                        <div class=""col-md-12 m-b-10"">
                            <div class=""widget-8 card no-border bg-success no-margin widget-loader-bar"">
                                <div class=""container-xs-height full-height"">
                                    <div class=""row-xs-height"">
                                        <div class=""col-xs-height col-top"">
                                            <div class=""card-header  top-left top-right"">
                                                <div class=""card-title text-black hint-text"">
                                                    <span class=""font-montserrat fs-11 all-caps"">
                                                        Weekly Revenue <i class=""fa fa-ch");
            WriteLiteral(@"evron-right""></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""row-xs-height "">
                                        <div class=""col-xs-height col-top relative"">
                                            <div class=""row"">
                                                <div class=""col-sm-6"">
                                                    <div class=""p-l-20"">
                                                        <h3 class=""no-margin p-b-5 text-white"">$140</h3>
                                                        <p class=""small hint-text m-t-5"">
                                                            <span class=""label  font-montserrat m-r-5"">60%</span>Higher
                                                        </p>
   ");
            WriteLiteral(@"                                                 </div>
                                                </div>
                                                <div class=""col-sm-6"">
                                                </div>
                                            </div>
                                            <div class='widget-8-chart line-chart' data-line-color=""black"" data-points=""true"" data-point-color=""success"" data-stroke-width=""2"">
                                                <svg></svg>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-12 m-b-10"">
                            <div class=""widget-9 card no-border bg-primary no-margin widget-loader-bar"">
            ");
            WriteLiteral(@"                    <div class=""full-height d-flex flex-column"">
                                    <div class=""card-header "">
                                        <div class=""card-title text-black"">
                                            <span class=""font-montserrat fs-11 all-caps"">
                                                Monthly Revenue <i class=""fa fa-chevron-right""></i>
                                            </span>
                                        </div>
                                    </div>
                                    <div class=""p-l-20"">
                                        <h3 class=""no-margin p-b-5 text-white"">$2300</h3>
                                        <span class=""small hint-text text-white"">65% lower than last month</span>
                                    </div>
                                    <div class=""mt-auto"">
                                        <div class=""progress progress-small m-b-20"">
                           ");
            WriteLiteral(@"                 <div class=""progress-bar progress-bar-white"" style=""width:45%""></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-md-12"">
                            <div class=""widget-10 card no-border bg-white no-margin widget-loader-bar"">
                                <div class=""card-header  top-left top-right "">
                                    <div class=""card-title text-black hint-text"">
                                        <span class=""font-montserrat fs-11 all-caps"">
                                            Total Appointments <i class=""fa fa-chevron-right""></i>
                                        </span>
                                    </div>
                                </div>
                                ");
            WriteLiteral(@"<div class=""card-body p-t-40"">
                                    <div class=""row"">
                                        <div class=""col-sm-12"">
                                            <h4 class=""no-margin p-b-5 text-danger semi-bold"">2032</h4>
                                        </div>
                                    </div>
                                    <div class=""p-t-10 full-width"">
                                        ");
            EndContext();
            BeginContext(5743, 144, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcfbe0626394fa294cc76def5707611", async() => {
                BeginContext(5838, 45, true);
                WriteLiteral("<i class=\"pg-arrow_minimize text-danger\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5887, 7532, true);
            WriteLiteral(@"
                                        <span class=""hint-text small"">Show more</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-6 col-xl-5 m-b-10 hidden-xlg"">
                    <div class=""widget-11-2 card no-border card-condensed no-margin widget-loader-circle full-height d-flex flex-column"">
                        <div class=""padding-25"">
                            <div class=""pull-left"">
                                <h2 class=""text-success no-margin"">Appointments</h2>
                                <p class=""no-margin"">Upcoming</p>
                            </div>
                            <div class=""clearfix""></div>
                        </div>
                        <div class=""auto-overflow widget-11-2-table"">
                            <table class=""table table-condensed table-h");
            WriteLiteral(@"over"">
                                <tbody>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #11</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #12</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                       ");
            WriteLiteral(@"                 </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #13</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointme");
            WriteLiteral(@"nt #14</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #15</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
             ");
            WriteLiteral(@"                           </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #16</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #17</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</sp");
            WriteLiteral(@"an>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #18</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat al");
            WriteLiteral(@"l-caps fs-12 w-50"">Appointment #19</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #20</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-1");
            WriteLiteral(@"8"">$10</span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class=""padding-25 mt-auto"">
                            <p class=""small no-margin"">
                                ");
            EndContext();
            BeginContext(13419, 134, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e7f8f506ad544289e322ee7c511d64e", async() => {
                BeginContext(13482, 67, true);
                WriteLiteral("<i class=\"fa fs-16 fa-arrow-circle-o-down text-success m-r-10\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(13553, 7432, true);
            WriteLiteral(@"
                                <span class=""hint-text "">Show more details</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-6 col-xl-5 m-b-10 hidden-xlg"">
                    <div class=""widget-11-2 card no-border card-condensed no-margin widget-loader-circle full-height d-flex flex-column"">
                        <div class=""padding-25"">
                            <div class=""pull-left"">
                                <h2 class=""text-success no-margin"">Appointments</h2>
                                <p class=""no-margin"">Previous</p>
                            </div>
                            <div class=""clearfix""></div>
                        </div>
                        <div class=""auto-overflow widget-11-2-table"">
                            <table class=""table table-condensed table-hover"">
                                <tbody>
                                    <tr>
");
            WriteLiteral(@"                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #1</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #2</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
          ");
            WriteLiteral(@"                                  <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #3</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #4</td>
                                        <td class=""text-right b-r b-dashed b-grey ");
            WriteLiteral(@"w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #5</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                   ");
            WriteLiteral(@"                 <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #6</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #7</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td cl");
            WriteLiteral(@"ass=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #8</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #9</td>
                                        <td class=""text-rig");
            WriteLiteral(@"ht b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class=""font-montserrat all-caps fs-12 w-50"">Appointment #10</td>
                                        <td class=""text-right b-r b-dashed b-grey w-25"">
                                            <span class=""hint-text small"">Min: 30</span>
                                        </td>
                                        <td class=""w-25"">
                                            <span class=""font-montserrat fs-18"">$10</span>
                                        </td>
                                    </");
            WriteLiteral(@"tr>
                                </tbody>
                            </table>
                        </div>
                        <div class=""padding-25 mt-auto"">
                            <p class=""small no-margin"">
                                ");
            EndContext();
            BeginContext(20985, 134, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e30811df717d4549bd7429782432fa8f", async() => {
                BeginContext(21048, 67, true);
                WriteLiteral("<i class=\"fa fs-16 fa-arrow-circle-o-down text-success m-r-10\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(21119, 257, true);
            WriteLiteral(@"
                                <span class=""hint-text "">Show more details</span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>");
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
