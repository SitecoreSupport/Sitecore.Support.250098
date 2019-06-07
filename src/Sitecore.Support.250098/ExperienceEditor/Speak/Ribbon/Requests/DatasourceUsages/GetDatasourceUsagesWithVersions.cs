using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.DatasourceUsages
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.ExperienceEditor.Abstractions;
    /// <summary>
    ///    Represents the 'GetDatasourceUsagesDropdown' request.
    /// </summary>
    /// <seealso cref="Sitecore.ExperienceEditor.Speak.Ribbon.Requests.DatasourceUsages.GetDatasourceUsagesWithVersions" />
    public class GetDatasourceUsagesWithVersions : GetDatasourceUsagesDropdown
    {
        public GetDatasourceUsagesWithVersions()
        {
        }

        public GetDatasourceUsagesWithVersions(IItemLinkCacheFactory itemLinkCacheFactory) : base(itemLinkCacheFactory)
        {
        }

        /// <summary>
        /// Generates the item usages data.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public override object GenerateItemUsagesData(Item item, int count)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            var itemUsagesData = new
            {
                ItemId = item.ID,
                ItemVersion = item.Version.Number,
            };

            return itemUsagesData;
        }

        protected override Item[] GetItemVersions(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            return item.Versions.GetVersions();
        }
    }
}