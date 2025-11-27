using System;
using System.Collections.Generic;
using System.Text;

using MMABooksTools;
using DBDataReader = MySql.Data.MySqlClient.MySqlDataReader;

using System.Text.Json;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;

namespace MMABooksProps
{

    public class ProductProps : IBaseProps
    {

        #region Auto-Implemented Properties

        // Properties for the products
        public int ProductID { get; set; } = 0;

        public string ProductCode { get; set; } = "";

        public string Description { get; set; } = "";

        public decimal UnitPrice { get; set; } = 0;

        public int OnHandQuantity { get; set; } = 0;

        /// <summary>
        /// ConcurrencyID. Don't manipulate directly.
        /// </summary>
        public int ConcurrencyID { get; set; } = 0;
        #endregion
    
        public object Clone()
        {
            ProductProps p = new ProductProps();
            p.ProductID = this.ProductID;
            p.ProductCode = this.ProductCode;
            p.Description = this.Description;
            p.UnitPrice = this.UnitPrice;
            p.OnHandQuantity = this.OnHandQuantity;
            p.ConcurrencyID = this.ConcurrencyID;
            return p;
        }

        public string GetState()
        {
            string jsonString;
            jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

        public void SetState(string jsonString)
        {
            ProductProps p = JsonSerializer.Deserialize<ProductProps>(jsonString);
            this.ProductID = p.ProductID;
            this.ProductCode = p.ProductCode;
            this.Description = p.Description;
            this.UnitPrice = p.UnitPrice;
            this.OnHandQuantity = p.OnHandQuantity;
            this.ConcurrencyID = p.ConcurrencyID;
        }

        public void SetState(MySqlDataReader dr)
        {
            this.ProductID = (int)dr["ProductID"];
            this.ProductCode = (String)dr["ProductCode"];
            this.Description = (String)dr["Description"];
            this.UnitPrice = (decimal)dr["UnitPrice"];
            this.OnHandQuantity = (int)dr["OnHandQuantity"];
            this.ConcurrencyID = (int)dr["ConcurrencyID"];
        }
    }
}
