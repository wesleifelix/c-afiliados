using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AbandonedCart_Models
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Products_GoogleShop
    {
        // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
        /// <remarks/>
        public Rss ProductsRss { get; set; }

        public int Id_productsgoogleshop { get; set; }

        public virtual Contracts GetContracts { get; set; }

        [XmlRoot(ElementName = "link")]
        public class Link
        {
            [XmlAttribute(AttributeName = "href")]
            public string Href { get; set; }
            [XmlAttribute(AttributeName = "rel")]
            public string Rel { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "image")]
        public class Image
        {
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }
            [XmlElement(ElementName = "link")]
            public string Link { get; set; }
        }

        [XmlRoot(ElementName = "author")]
        public class Author
        {
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "shipping", Namespace = "http://base.google.com/ns/1.0")]
        public class Shipping
        {
            [XmlElement(ElementName = "country", Namespace = "http://base.google.com/ns/1.0")]
            public string Country { get; set; }
            [XmlElement(ElementName = "service", Namespace = "http://base.google.com/ns/1.0")]
            public string Service { get; set; }
            [XmlElement(ElementName = "price", Namespace = "http://base.google.com/ns/1.0")]
            public string Price { get; set; }
        }

        [XmlRoot(ElementName = "item")]
        public class Item
        {
            [XmlElement(ElementName = "id", Namespace = "http://base.google.com/ns/1.0")]
            public string Id { get; set; }
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "link")]
            public string Link { get; set; }
            [XmlElement(ElementName = "image_link", Namespace = "http://base.google.com/ns/1.0")]
            public string Image_link { get; set; }
            [XmlElement(ElementName = "additional_image_link", Namespace = "http://base.google.com/ns/1.0")]
            public List<string> Additional_image_link { get; set; }
            [XmlElement(ElementName = "condition", Namespace = "http://base.google.com/ns/1.0")]
            public string Condition { get; set; }
            [XmlElement(ElementName = "product_type", Namespace = "http://base.google.com/ns/1.0")]
            public string Product_type { get; set; }
            [XmlElement(ElementName = "google_product_category", Namespace = "http://base.google.com/ns/1.0")]
            public string Google_product_category { get; set; }
            [XmlElement(ElementName = "quantity", Namespace = "http://base.google.com/ns/1.0")]
            public string Quantity { get; set; }
            [XmlElement(ElementName = "availability", Namespace = "http://base.google.com/ns/1.0")]
            public string Availability { get; set; }
            [XmlElement(ElementName = "price", Namespace = "http://base.google.com/ns/1.0")]
            public string Price { get; set; }
            [XmlElement(ElementName = "mpn", Namespace = "http://base.google.com/ns/1.0")]
            public string Mpn { get; set; }
            [XmlElement(ElementName = "identifier_exists", Namespace = "http://base.google.com/ns/1.0")]
            public string Identifier_exists { get; set; }
            [XmlElement(ElementName = "gender", Namespace = "http://base.google.com/ns/1.0")]
            public string Gender { get; set; }
            [XmlElement(ElementName = "age_group", Namespace = "http://base.google.com/ns/1.0")]
            public string Age_group { get; set; }
            [XmlElement(ElementName = "shipping", Namespace = "http://base.google.com/ns/1.0")]
            public Shipping Shipping { get; set; }
            [XmlElement(ElementName = "shipping_weight", Namespace = "http://base.google.com/ns/1.0")]
            public string Shipping_weight { get; set; }
        }

        [XmlRoot(ElementName = "channel")]
        public class Channel
        {
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "link")]
            public Link Link { get; set; }
            [XmlElement(ElementName = "image")]
            public Image Image { get; set; }
            [XmlElement(ElementName = "modified")]
            public string Modified { get; set; }
            [XmlElement(ElementName = "author")]
            public Author Author { get; set; }
            [XmlElement(ElementName = "item")]
            public List<Item> Item { get; set; }
        }

        [XmlRoot(ElementName = "rss")]
        public class Rss
        {
            [XmlElement(ElementName = "channel")]
            public Channel Channel { get; set; }
            [XmlAttribute(AttributeName = "version")]
            public string Version { get; set; }
            [XmlAttribute(AttributeName = "g", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string G { get; set; }
        }

    }
}
