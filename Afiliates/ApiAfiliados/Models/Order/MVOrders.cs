using ApiAfiliados.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAfiliados.Models.Order
{
    public class MVOrders
    {
        public string seller { get; set; }
        public string publisher { get; set; }
        public string m8a_idproduct { get; set; }
        public string m8a_page { get; set; }
        public string type { get; set; }
        public string utm_source { get; set; }
        public string utm_medium { get; set; }
        public string utm_campaign { get; set; }
        public string m8a_sid { get; set; }
        public string m8a_visity { get; set; }

        public virtual MVProductsViews ProdView { get; set; }

        public virtual OrderViews orders { get; set; }

        public class OrderViews
        {
            public string Reference { get; set; }
            public string Status { get; set; }
            public string Paymode { get; set; }
            public string Linkorder { get; set; }

            private string _shipping { get; set; }
            public string Shipping { get { return _shipping.Replace(".", ","); } set { this._shipping = value.Replace(".", ","); } }

            private string _totalpay { get; set; }
            public string Totalpay { get { return _totalpay.Replace(".", ","); } set { this._totalpay = value.Replace(".", ","); } }
            public string Customer { get; set; }
            public virtual ICollection<Cartviews> Cart { get; set; }

            public class Cartviews
            {
                public string Id { get; set; }
                public string Sku { get; set; }
                public string Name { get; set; }
                private string __price { get; set; }
                public decimal price { get { return decimal.Parse(__price.Replace(".", ",")); } set { this.__price = value.ToString().Replace(".", ","); } }

                private string _quantity { get; set; }
                public decimal Quantity { get { return decimal.Parse(_quantity.Replace(".", ",")); } set { this._quantity = value.ToString().Replace(".", ","); } }

            }
        }
    }


}
