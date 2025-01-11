using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ASP_NET_MVC_Shop_Example.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace ASP_NET_MVC_Shop_Example.TagHelpers
{
    [HtmlTargetElement("sort-link")]
    public class SortLinkTagHelper : AnchorTagHelper
    {
        public string Property { get; set; } = null!;// значение текущего свойства, для которого создается тег
        public SortDirection SortDirection { get; set; }
        public string? CurrentSortField { get; set; }

        public SortLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "sort-link");

            if (CurrentSortField == Property)
            {
                var tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (SortDirection == SortDirection.Ascending)   // если сортировка по возрастанию
                    tag.AddCssClass("glyphicon-chevron-up");
                else if (SortDirection == SortDirection.Descending)   // если сортировка по убыванию
                    tag.AddCssClass("glyphicon-chevron-down");

                output.PreContent.AppendHtml(tag);
            }

            base.Process(context, output);
            output.TagName = "a";
        }
    }
}