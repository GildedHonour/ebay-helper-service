﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace Ebay1
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class EbayDb : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new EbayDb object using the connection string found in the 'EbayDb' section of the application configuration file.
        /// </summary>
        public EbayDb() : base("name=EbayDb", "EbayDb")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EbayDb object.
        /// </summary>
        public EbayDb(string connectionString) : base(connectionString, "EbayDb")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EbayDb object.
        /// </summary>
        public EbayDb(EntityConnection connection) : base(connection, "EbayDb")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Listings> Listings
        {
            get
            {
                if ((_Listings == null))
                {
                    _Listings = base.CreateObjectSet<Listings>("Listings");
                }
                return _Listings;
            }
        }
        private ObjectSet<Listings> _Listings;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Listings EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToListings(Listings listings)
        {
            base.AddObject("Listings", listings);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EbayModel", Name="Listings")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Listings : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Listings object.
        /// </summary>
        /// <param name="listingID">Initial value of the ListingID property.</param>
        /// <param name="mPN">Initial value of the MPN property.</param>
        /// <param name="quantity">Initial value of the Quantity property.</param>
        /// <param name="sellingPrice">Initial value of the SellingPrice property.</param>
        /// <param name="isUsed">Initial value of the IsUsed property.</param>
        public static Listings CreateListings(global::System.String listingID, global::System.String mPN, global::System.Int32 quantity, global::System.Decimal sellingPrice, global::System.Boolean isUsed)
        {
            Listings listings = new Listings();
            listings.ListingID = listingID;
            listings.MPN = mPN;
            listings.Quantity = quantity;
            listings.SellingPrice = sellingPrice;
            listings.IsUsed = isUsed;
            return listings;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ListingID
        {
            get
            {
                return _ListingID;
            }
            set
            {
                if (_ListingID != value)
                {
                    OnListingIDChanging(value);
                    ReportPropertyChanging("ListingID");
                    _ListingID = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("ListingID");
                    OnListingIDChanged();
                }
            }
        }
        private global::System.String _ListingID;
        partial void OnListingIDChanging(global::System.String value);
        partial void OnListingIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String MPN
        {
            get
            {
                return _MPN;
            }
            set
            {
                if (_MPN != value)
                {
                    OnMPNChanging(value);
                    ReportPropertyChanging("MPN");
                    _MPN = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("MPN");
                    OnMPNChanged();
                }
            }
        }
        private global::System.String _MPN;
        partial void OnMPNChanging(global::System.String value);
        partial void OnMPNChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                OnQuantityChanging(value);
                ReportPropertyChanging("Quantity");
                _Quantity = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Quantity");
                OnQuantityChanged();
            }
        }
        private global::System.Int32 _Quantity;
        partial void OnQuantityChanging(global::System.Int32 value);
        partial void OnQuantityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal SellingPrice
        {
            get
            {
                return _SellingPrice;
            }
            set
            {
                OnSellingPriceChanging(value);
                ReportPropertyChanging("SellingPrice");
                _SellingPrice = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SellingPrice");
                OnSellingPriceChanged();
            }
        }
        private global::System.Decimal _SellingPrice;
        partial void OnSellingPriceChanging(global::System.Decimal value);
        partial void OnSellingPriceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsUsed
        {
            get
            {
                return _IsUsed;
            }
            set
            {
                OnIsUsedChanging(value);
                ReportPropertyChanging("IsUsed");
                _IsUsed = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsUsed");
                OnIsUsedChanged();
            }
        }
        private global::System.Boolean _IsUsed;
        partial void OnIsUsedChanging(global::System.Boolean value);
        partial void OnIsUsedChanged();

        #endregion
    
    }

    #endregion
    
}
