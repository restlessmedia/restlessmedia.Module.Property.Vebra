using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiProperty
  {
    #region Xml fields

    [XmlElement("propertyID", Type = typeof(long))]
    public long PropertyId { get; set; }

    [XmlElement("branchID", Type = typeof(int))]
    public int BranchId { get; set; }

    [XmlElement("clientName")]
    public string ClientName { get; set; }

    [XmlElement("branchName")]
    public string BranchName { get; set; }

    [XmlElement("department")]
    public string Department { get; set; }

    [XmlElement("referenceNumber")]
    public string ReferenceNumber { get; set; }

    [XmlElement("addressName")]
    public string AddressName { get; set; }

    [XmlElement("addressNumber")]
    public string AddressNumber { get; set; }

    [XmlElement("addressStreet")]
    public string AddressStreet { get; set; }

    [XmlElement("address2")]
    public string Address2 { get; set; }

    [XmlElement("address3")]
    public string Address3 { get; set; }

    [XmlElement("address4")]
    public string Address4 { get; set; }

    [XmlElement("addressPostcode")]
    public string AddressPostcode { get; set; }
    
    [XmlElement("country")]
    public string Country { get; set; }

    [XmlElement("displayAddress")]
    public string DisplayAddress { get; set; }

    [XmlElement("propertyBedrooms", Type = typeof(byte))]
    public byte PropertyBedrooms { get; set; }

    [XmlElement("propertyBathrooms", Type = typeof(byte))]
    public byte PropertyBathrooms { get; set; }

    [XmlElement("propertyEnsuites", Type = typeof(byte))]
    public byte PropertyEnsuites { get; set; }

    [XmlElement("propertyReceptionRooms", Type = typeof(byte))]
    public byte PropertyReceptionRooms { get; set; }

    [XmlElement("propertyKitchens", Type = typeof(byte))]
    public byte PropertyKitchens { get; set; }

    [XmlElement("displayPropertyType")]
    public string DisplayPropertyType { get; set; }

    [XmlElement("propertyType", Type = typeof(int))]
    public int PropertyType { get; set; }

    [XmlElement("propertyStyle", Type = typeof(PropertyStyle))]
    public PropertyStyle PropertyStyle { get; set; }

    [XmlElement("propertyAge", Type = typeof(int))]
    public int PropertyAge { get; set; }

    [XmlElement("floorArea", Type = typeof(double))]
    public double FloorArea { get; set; }

    [XmlElement("floorAreaFrom", Type = typeof(double))]
    public double FloorAreaFrom { get; set; }

    [XmlElement("floorAreaTo", Type = typeof(double))]
    public double FloorAreaTo { get; set; }

    [XmlElement("floorAreaUnits", Type = typeof(FloorAreaUnits))]
    public FloorAreaUnits FloorAreaUnits { get; set; }

    [XmlElement("propertyFeature1")]
    public string PropertyFeature1 { get; set; }

    [XmlElement("propertyFeature2")]
    public string PropertyFeature2 { get; set; }

    [XmlElement("propertyFeature3")]
    public string PropertyFeature3 { get; set; }

    [XmlElement("propertyFeature4")]
    public string PropertyFeature4 { get; set; }

    [XmlElement("propertyFeature5")]
    public string PropertyFeature5 { get; set; }

    [XmlElement("propertyFeature6")]
    public string PropertyFeature6 { get; set; }

    [XmlElement("propertyFeature7")]
    public string PropertyFeature7 { get; set; }

    [XmlElement("propertyFeature8")]
    public string PropertyFeature8 { get; set; }

    [XmlElement("propertyFeature9")]
    public string PropertyFeature9 { get; set; }

    [XmlElement("propertyFeature10")]
    public string PropertyFeature10 { get; set; }

    [XmlElement("price", Type = typeof(decimal))]
    public decimal Price { get; set; }

    [XmlElement("forSale", Type = typeof(byte))]
    public byte ForSale { get; set; }

    [XmlElement("forSalePOA")]
    public string ForSalePOA { get; set; }

    [XmlElement("priceQualifier")]
    public string PriceQualifier { get; set; }

    [XmlElement("rentFrequency")]
    public string RentFrequency { get; set; }

    /// <summary>
    /// If commercial, this is a string Freehold or Long Leasehold.  For sales and lettings, this will correspond to a value in the PropertyTenure enum.
    /// </summary>
    [XmlElement("propertyTenure", Type = typeof(string))]
    public string PropertyTenure { get; set; }

    // not guarenteed to have a value
    //[XmlElement("saleBy", Type = typeof(int))]
    //public int SaleBy { get; set; }

    [XmlElement("developmentOpportunity", Type = typeof(int))]
    public int DevelopmentOpportunity { get; set; }

    [XmlElement("investmentOpportunity", Type = typeof(int))]
    public int InvestmentOpportunity { get; set; }

    [XmlElement("estimatedRentalIncome", Type = typeof(decimal))]
    public decimal EstimatedRentalIncome { get; set; }

    [XmlElement("availability", Type = typeof(byte))]
    public byte Availability { get; set; }

    [XmlElement("mainSummary")]
    public string MainSummary { get; set; }

    [XmlElement("fullDescription")]
    public string FullDescription { get; set; }

    [XmlElement("dateLastModified", Type = typeof(DateTime))]
    public DateTime DateLastModified { get; set; }

    [XmlElement("featuredProperty", Type = typeof(int))]
    public int FeaturedProperty { get; set; }

    [XmlElement("regionID", Type = typeof(int))]
    public int RegionID { get; set; }

    [XmlElement("latitude")]
    public string LatitudeValue { get; set; }

    public double? Latitude
    {
      get
      {
        double value;
        return double.TryParse(LatitudeValue, out value) ? value : (double?)null;
      }
    }

    [XmlElement("longitude")]
    public string LongitudeValue { get; set; }

    public double? Longitude
    {
      get
      {
        double value;
        return double.TryParse(LongitudeValue, out value) ? value : (double?)null;
      }
    }

    [XmlElement("flags")]
    public Flag[] Flags { get; set; }

    [XmlArray("images")]
    [XmlArrayItem("image", Type = typeof(Resource))]
    public Resource[] Images { get; set; }

    [XmlArray("floorplans")]
    [XmlArrayItem("floorplan", Type = typeof(Floorplan))]
    public Floorplan[] Floorplans { get; set; }

    [XmlArray("epcGraphs")]
    [XmlArrayItem("epcGraph", Type = typeof(Resource))]
    public Resource[] EpcGraphs { get; set; }

    [XmlArray("epcFrontPages")]
    [XmlArrayItem("epcFrontPage", Type = typeof(Resource))]
    public Resource[] EpcFrontPages { get; set; }

    [XmlArray("brochures")]
    [XmlArrayItem("brochure", Type = typeof(Resource))]
    public Resource[] Brochures { get; set; }

    [XmlArray("virtualTours")]
    [XmlArrayItem("virtualTour", Type = typeof(Resource))]
    public Resource[] VirtualTours { get; set; }

    [XmlElement("externalLinks")]
    public List<ExternalLink> ExternalLinks { get; set; }

    #region Lettings

    [XmlElement("rent")]
    public decimal Rent { get; set; }

    // not guarenteed to have a value
    // [XmlElement("toLetPOA", Type = typeof(byte))]
    // public byte ToLetPOA { get; set; }

    public byte? StudentProperty
    {
      get
      {
        if (string.IsNullOrWhiteSpace(StudentPropertyString))
        {
          return null;
        }

        return byte.Parse(StudentPropertyString);
      }
    }

    [XmlElement("studentProperty")]
    public string StudentPropertyString { get; set; }

    #endregion

    #region Sale

    #endregion

    #region Commercial

    /// <summary>
    /// The price of the property in pounds. EG 150000 to 450000
    /// </summary>
    [XmlElement("priceTo", Type = typeof(decimal))]
    public decimal PriceTo { get; set; }

    /// <summary>
    /// The price from of the property in pounds EG 150000 to 450000. Note for properties that do not have a range priceFrom will be 0 and only priceTo will be specified.
    /// </summary>
    [XmlElement("priceFrom", Type = typeof(decimal))]
    public decimal priceFrom { get; set; }

    /// <summary>
    /// The rent of the property in pounds EG 12000 to 22000 pa
    /// </summary>
    [XmlElement("rentTo", Type = typeof(decimal))]
    public decimal RentTo { get; set; }

    /// <summary>
    /// The rent from of the property in pounds EG 12000 to 22000 pa. Note for properties that do not have a range rentFrom will be 0 and only rentTo will be specified.
    /// </summary>
    [XmlElement("rentFrom", Type = typeof(decimal))]
    public decimal RentFrom { get; set; }

    #endregion

    #endregion
  }
}