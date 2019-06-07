using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.DatasourceUsages
{
    // Sitecore.ExperienceEditor.Speak.Ribbon.Requests.DatasourceUsages.GetDatasourceUsagesDropdown
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.ExperienceEditor.Abstractions;
    using Sitecore.ExperienceEditor.Speak.Ribbon.Requests.DatasourceUsages;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.Links;

    public class GetDatasourceUsagesDropdown : GetDatasourceUsagesBase
    {
        public GetDatasourceUsagesDropdown()
        {
        }

        public GetDatasourceUsagesDropdown(IItemLinkCacheFactory itemLinkCacheFactory) : base(itemLinkCacheFactory)
        {
        }

        /// <summary>
        /// Generates the item usages data.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public override object GenerateItemUsagesData([NotNull] Item item, int count)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            var itemUsagesData = new
            {
                ItemId = item.ID,
                Title = FormatTitle(count, item),
                Tooltip = item.Paths.FullPath
            };

            return itemUsagesData;
        }

        /// <summary>
        /// Formats the title.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual string FormatTitle(int count, [NotNull] Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            return $"{count}. {item.DisplayName}   {GetFormattedWorkflowStateName(item)}   {item.Paths.FullPath}".Replace(" ", "&nbsp;");
        }

        /// <summary>
        /// Items the link identifier.
        /// </summary>
        /// <param name="linkedItem">The linked item.</param>
        /// <returns></returns>
        protected override ID ItemLinkId(ItemLink linkedItem)
        {
            Assert.ArgumentNotNull(linkedItem, nameof(linkedItem));

            return linkedItem.TargetItemID;
        }

        /// <summary>
        /// Gets the item links.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override ItemLink[] GetItemLinks(Item item)
        {
            IItemLinkCache itemLinksCache = ItemLinkCacheFactory.GetItemLinkCache("itemReferences");
            ItemLink[] links = itemLinksCache.GetItemLinks(item) ?? ItemUtility.GetItemReferences(item);
            itemLinksCache.AddItemLinks(item, links);

            return ItemUtility.GetItemReferences(item);
        }

        protected override Item[] GetItemVersions(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            Item[] itemVersions = { item.Versions.GetLatestVersion() };

            return itemVersions;
        }

    }
}