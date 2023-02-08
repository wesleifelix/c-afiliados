using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbandonedCart_Models
{
    public class Produts_google_shopJSON
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public Link Link { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("item")]
        public List<Item> Item { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }

        [JsonProperty("image_link")]
        public Uri ImageLink { get; set; }

        [JsonProperty("additional_image_link")]
        public List<Uri> AdditionalImageLink { get; set; }

        [JsonProperty("condition")]
        public Condition Condition { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("google_product_category")]
        public long GoogleProductCategory { get; set; }

        [JsonProperty("quantity")]
        public long? Quantity { get; set; }

        [JsonProperty("availability")]
        public Availability Availability { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("mpn")]
        public long? Mpn { get; set; }

        [JsonProperty("identifier_exists")]
        public IdentifierExists IdentifierExists { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("age_group")]
        public AgeGroup AgeGroup { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }

        [JsonProperty("shipping_weight")]
        public string ShippingWeight { get; set; }
    }

    public partial class Shipping
    {
        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("service")]
        public Service Service { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }
    }

    public partial class Link
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public enum AgeGroup { Adult, Infant };

    public enum Availability { InStock };

    public enum Condition { New };

    public enum Gender { Female, Male, Unisex };

    public enum IdentifierExists { False };

    public enum Country { Br };

    public enum Price { The000Brl };

    public enum Service { Standard };

    

}
