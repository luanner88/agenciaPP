#pragma checksum "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07bfeaf9cf1bbaeeeb97b29deb230622751f9be6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Agenciapp.Pages.Orders.Pages_Orders_Details), @"mvc.1.0.razor-page", @"/Pages/Orders/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Orders/Details.cshtml", typeof(Agenciapp.Pages.Orders.Pages_Orders_Details), null)]
namespace Agenciapp.Pages.Orders
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\_ViewImports.cshtml"
using Agenciapp;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07bfeaf9cf1bbaeeeb97b29deb230622751f9be6", @"/Pages/Orders/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"478d1f5f7dd9c215c5984108d37c4763126da28b", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Orders_Details : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(51, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Pages/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(143, 119, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n<div>\r\n    <h4>Order</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(263, 46, false);
#line 16 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Type));

#line default
#line hidden
            EndContext();
            BeginContext(309, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(353, 42, false);
#line 19 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Type));

#line default
#line hidden
            EndContext();
            BeginContext(395, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(439, 48, false);
#line 22 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Number));

#line default
#line hidden
            EndContext();
            BeginContext(487, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(531, 44, false);
#line 25 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Number));

#line default
#line hidden
            EndContext();
            BeginContext(575, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(619, 46, false);
#line 28 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Date));

#line default
#line hidden
            EndContext();
            BeginContext(665, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(709, 42, false);
#line 31 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Date));

#line default
#line hidden
            EndContext();
            BeginContext(751, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(795, 48, false);
#line 34 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Amount));

#line default
#line hidden
            EndContext();
            BeginContext(843, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(887, 44, false);
#line 37 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Amount));

#line default
#line hidden
            EndContext();
            BeginContext(931, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(975, 48, false);
#line 40 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Status));

#line default
#line hidden
            EndContext();
            BeginContext(1023, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1067, 44, false);
#line 43 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Status));

#line default
#line hidden
            EndContext();
            BeginContext(1111, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1155, 49, false);
#line 46 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.PriceLb));

#line default
#line hidden
            EndContext();
            BeginContext(1204, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1248, 45, false);
#line 49 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.PriceLb));

#line default
#line hidden
            EndContext();
            BeginContext(1293, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1337, 48, false);
#line 52 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.CantLb));

#line default
#line hidden
            EndContext();
            BeginContext(1385, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1429, 44, false);
#line 55 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.CantLb));

#line default
#line hidden
            EndContext();
            BeginContext(1473, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1517, 53, false);
#line 58 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.ValorPagado));

#line default
#line hidden
            EndContext();
            BeginContext(1570, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1614, 49, false);
#line 61 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.ValorPagado));

#line default
#line hidden
            EndContext();
            BeginContext(1663, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1707, 49, false);
#line 64 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Balance));

#line default
#line hidden
            EndContext();
            BeginContext(1756, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1800, 45, false);
#line 67 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Balance));

#line default
#line hidden
            EndContext();
            BeginContext(1845, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1889, 53, false);
#line 70 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.OtrosCostos));

#line default
#line hidden
            EndContext();
            BeginContext(1942, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1986, 49, false);
#line 73 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.OtrosCostos));

#line default
#line hidden
            EndContext();
            BeginContext(2035, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2079, 54, false);
#line 76 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.ValorAduanal));

#line default
#line hidden
            EndContext();
            BeginContext(2133, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2177, 50, false);
#line 79 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.ValorAduanal));

#line default
#line hidden
            EndContext();
            BeginContext(2227, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2271, 48, false);
#line 82 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Agency));

#line default
#line hidden
            EndContext();
            BeginContext(2319, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2363, 54, false);
#line 85 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Agency.LegalName));

#line default
#line hidden
            EndContext();
            BeginContext(2417, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2461, 48, false);
#line 88 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Client));

#line default
#line hidden
            EndContext();
            BeginContext(2509, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2553, 50, false);
#line 91 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Client.Email));

#line default
#line hidden
            EndContext();
            BeginContext(2603, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2647, 49, false);
#line 94 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Contact));

#line default
#line hidden
            EndContext();
            BeginContext(2696, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2740, 54, false);
#line 97 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Contact.LastName));

#line default
#line hidden
            EndContext();
            BeginContext(2794, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2838, 48, false);
#line 100 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.Office));

#line default
#line hidden
            EndContext();
            BeginContext(2886, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2930, 49, false);
#line 103 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Office.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2979, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(3023, 50, false);
#line 106 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.TipoPago));

#line default
#line hidden
            EndContext();
            BeginContext(3073, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(3117, 51, false);
#line 109 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.TipoPago.Type));

#line default
#line hidden
            EndContext();
            BeginContext(3168, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(3212, 46, false);
#line 112 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Order.User));

#line default
#line hidden
            EndContext();
            BeginContext(3258, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(3302, 48, false);
#line 115 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.User.Email));

#line default
#line hidden
            EndContext();
            BeginContext(3350, 193, true);
            WriteLiteral("\r\n        </dd>\r\n        <dd>\r\n            <table class=\"table\" id=\"tblCustomers\">\r\n                <thead>\r\n                    <tr>\r\n                        <th>\r\n                            ");
            EndContext();
            BeginContext(3544, 58, false);
#line 122 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.listPackageItem[0].Qty));

#line default
#line hidden
            EndContext();
            BeginContext(3602, 91, true);
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
            EndContext();
            BeginContext(3694, 74, false);
#line 125 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.listPackageItem[0].Product.Description));

#line default
#line hidden
            EndContext();
            BeginContext(3768, 90, true);
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                           ");
            EndContext();
            BeginContext(3859, 66, false);
#line 128 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                      Write(Html.DisplayNameFor(model => model.listPackageItem[0].Description));

#line default
#line hidden
            EndContext();
            BeginContext(3925, 146, true);
            WriteLiteral("\r\n                        </th>\r\n                        <th></th>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
            EndContext();
#line 134 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                     foreach (var item in Model.listPackageItem)
                    {

#line default
#line hidden
            BeginContext(4160, 84, true);
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(4245, 38, false);
#line 138 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Qty));

#line default
#line hidden
            EndContext();
            BeginContext(4283, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(4375, 54, false);
#line 141 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Product.Description));

#line default
#line hidden
            EndContext();
            BeginContext(4429, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(4521, 46, false);
#line 144 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
            EndContext();
            BeginContext(4567, 60, true);
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
            EndContext();
#line 147 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                    }

#line default
#line hidden
            BeginContext(4650, 93, true);
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(4743, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3814d3e27e9d47d9a966948bb7a3724d", async() => {
                BeginContext(4800, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 154 "C:\Users\Saul\source\repos\Agenciapp\Agenciapp\Pages\Orders\Details.cshtml"
                           WriteLiteral(Model.Order.OrderId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4808, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(4816, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b9cdd18359b48ed9de012e901d25953", async() => {
                BeginContext(4838, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4854, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Agenciapp.Pages.Orders.DetailsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Agenciapp.Pages.Orders.DetailsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Agenciapp.Pages.Orders.DetailsModel>)PageContext?.ViewData;
        public Agenciapp.Pages.Orders.DetailsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
