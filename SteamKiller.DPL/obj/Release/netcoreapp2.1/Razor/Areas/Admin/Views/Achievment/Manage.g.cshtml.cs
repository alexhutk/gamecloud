#pragma checksum "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "60e715e3892b8cc0412f4d19d118b1a3825d2427"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Achievment_Manage), @"mvc.1.0.view", @"/Areas/Admin/Views/Achievment/Manage.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Achievment/Manage.cshtml", typeof(AspNetCore.Areas_Admin_Views_Achievment_Manage))]
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
#line 1 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\_ViewImports.cshtml"
using SteamKiller.DPL;

#line default
#line hidden
#line 2 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\_ViewImports.cshtml"
using SteamKiller.DPL.Models;

#line default
#line hidden
#line 1 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
using SteamKiller.WEB.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60e715e3892b8cc0412f4d19d118b1a3825d2427", @"/Areas/Admin/Views/Achievment/Manage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ff08abf22e869445f6dcd266bd4a4550b298389", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Achievment_Manage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SteamKiller.WEB.Models.AchievmentCollectionViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/default_img.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Achievment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteAchievment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Manage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
  
    ViewData["Title"] = "Applications";
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(139, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(202, 62, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <h2>");
            EndContext();
            BeginContext(265, 10, false);
#line 11 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
       Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(275, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(278, 11, false);
#line 11 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                    Write(Model.AppId);

#line default
#line hidden
            EndContext();
            BeginContext(289, 268, true);
            WriteLiteral(@")</h2>
        <p>Manage your achievments on that page.</p>
        <button type=""button"" class=""btn btn-primary"" data-toggle=""modal"" data-target=""#exampleModalCenter"">
            Create new
        </button>
    </div>
</div>
<div class=""row gameContainer"">
");
            EndContext();
#line 19 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
     foreach (var child in Model.Entries)
    {
        var childName = ((AchievmentEntryViewModel)child).Name;
        var childDescription = ((AchievmentEntryViewModel)child).Description;

#line default
#line hidden
            BeginContext(751, 86, true);
            WriteLiteral("        <div class=\"card manage-card ach-card\">\r\n            <div class=\"card-body\">\r\n");
            EndContext();
#line 25 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                 if (((AchievmentEntryViewModel)child).ImageData != null)
                {

#line default
#line hidden
            BeginContext(931, 24, true);
            WriteLiteral("                    <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 955, "\"", 1054, 2);
            WriteAttributeValue("", 961, "data:image/jpeg;base64,", 961, 23, true);
#line 27 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
WriteAttributeValue("", 984, Convert.ToBase64String(((AchievmentEntryViewModel)child).ImageData), 984, 70, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1055, 3, true);
            WriteLiteral(">\r\n");
            EndContext();
#line 28 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(1118, 20, true);
            WriteLiteral("                    ");
            EndContext();
            BeginContext(1138, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6acf0a2676e740f48d05a93765c2ea09", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1176, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 32 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                }

#line default
#line hidden
            BeginContext(1197, 39, true);
            WriteLiteral("                <h5 class=\"card-title\">");
            EndContext();
            BeginContext(1237, 9, false);
#line 33 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                                  Write(childName);

#line default
#line hidden
            EndContext();
            BeginContext(1246, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(1249, 8, false);
#line 33 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                                              Write(child.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1257, 45, true);
            WriteLiteral(")</h5>\r\n                <p class=\"card-text\">");
            EndContext();
            BeginContext(1303, 16, false);
#line 34 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                                Write(childDescription);

#line default
#line hidden
            EndContext();
            BeginContext(1319, 22, true);
            WriteLiteral("</p>\r\n                ");
            EndContext();
            BeginContext(1341, 298, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "10334076640c46d19b5af4e72504c7e9", async() => {
                BeginContext(1524, 108, true);
                WriteLiteral("\r\n                    <button type=\"submit\" class=\"btn btn-outline-danger\">Delete</button>\r\n                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 35 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                                                                                                                 WriteLiteral(child.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 35 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                                                                                                                                             WriteLiteral(Model.AppId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["appId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-appId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["appId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 35 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
                                                                                                                                                                              WriteLiteral(Model.Name);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["appName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-appName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["appName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1639, 38, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 40 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
    }

#line default
#line hidden
            BeginContext(1684, 619, true);
            WriteLiteral(@"</div>
<!-- Modal -->
<div class=""modal fade"" id=""exampleModalCenter"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLongTitle"">Create new achievment</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            ");
            EndContext();
            BeginContext(2303, 1537, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5dcd718e729642c78d94a4f5402c8305", async() => {
                BeginContext(2418, 62, true);
                WriteLiteral("\r\n                <input type=\"hidden\" id=\"AppId\" name=\"AppId\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2480, "\"", 2500, 1);
#line 53 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
WriteAttributeValue("", 2488, Model.AppId, 2488, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2501, 67, true);
                WriteLiteral(">\r\n                <input type=\"hidden\" id=\"AppName\" name=\"AppName\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2568, "\"", 2587, 1);
#line 54 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
WriteAttributeValue("", 2576, Model.Name, 2576, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2588, 307, true);
                WriteLiteral(@">
                <div class=""modal-body"">
                    <div class=""form-group"">
                        <label for=""exampleInputEmail1"">Name</label>
                        <input type=""text"" class=""form-control"" id=""Name"" name=""Name"" placeholder=""Enter ach name here"">
                        ");
                EndContext();
                BeginContext(2895, 34, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4910e17b68eb4003be3f2dd7ef8f2f01", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 59 "C:\Users\User\source\repos\steamkiller-master\SteamKiller.DPL\Areas\Admin\Views\Achievment\Manage.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Name);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2929, 904, true);
                WriteLiteral(@"
                    </div>
                    <div class=""form-group"">
                        <label for=""exampleInputEmail1"">Description</label>
                        <textarea class=""form-control"" id=""Description"" name=""Description"" aria-label=""With textarea"" placeholder=""Enter ach descr here""></textarea>
                    </div>
                    <div class=""form-group"">
                        <label for=""exampleFormControlFile1"">Achievment image</label>
                        <input type=""file"" class=""form-control-file"" id=""Image"" name=""Image"">
                    </div>
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Close</button>
                    <button type=""submit"" class=""form-btn btn btn-primary"">Create</button>
                </div>
            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3840, 36, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SteamKiller.WEB.Models.AchievmentCollectionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
