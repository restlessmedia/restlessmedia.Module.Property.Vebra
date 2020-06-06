namespace restlessmedia.UnitTest.Abstractions.Provider.ApiProperty
{
  [TestClass]
  public class ApiPropertyEntityTests
  {
    public ApiPropertyEntityTests() { }

    [TestMethod]
    public void SquareFootage_converts_when_FloorArea_is_in_metres()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FloorArea = 1,
        FloorAreaUnits = FloorAreaUnits.SqM
      }).SquareFootage.ShouldEqual(3.28084);
    }

    [TestMethod]
    public void SquareFootage_converts_when_FloorArea_is_in_acres()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FloorArea = 1,
        FloorAreaUnits = FloorAreaUnits.Acres
      }).SquareFootage.ShouldEqual(43560);
    }

    [TestMethod]
    public void SquareFootage_converts_when_FloorArea_is_in_hectares()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FloorArea = 1,
        FloorAreaUnits = FloorAreaUnits.Hectares
      }).SquareFootage.ShouldEqual(107639);
    }

    [TestMethod]
    public void SquareFootage_converts_when_FloorArea_is_in_feet()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FloorArea = 999,
        FloorAreaUnits = FloorAreaUnits.SqFt
      }).SquareFootage.ShouldEqual(999);
    }

    [TestMethod]
    public void SquareFootage_calculates_range_when_FloorAreaFrom_is_valid()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FloorAreaFrom = 2,
        FloorAreaTo = 4,
        FloorAreaUnits = FloorAreaUnits.SqFt
      }).SquareFootage.ShouldEqual(8);
    }

    [TestMethod]
    public void SquareFootage_does_not_calculates_range_when_FloorAreaFrom_is_invalid()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FloorAreaFrom = 0, // to is ignored if from is zero
        FloorAreaTo = 4,
        FloorArea = 10,
        FloorAreaUnits = FloorAreaUnits.SqFt
      }).SquareFootage.ShouldEqual(10);
    }

    [TestMethod]
    public void IsCommercial_returns_true_when_department_is_commercial()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial"
      }).IsCommercial.ShouldBeTrue();

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "commercial"
      }).IsCommercial.ShouldBeTrue();

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "COMMERCIAL"
      }).IsCommercial.ShouldBeTrue();
    }

    [TestMethod]
    public void IsStudio_returns_true_when_propertystyle_is_studio()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.Studio
      }).IsStudio.ShouldBeTrue();
    }

    [TestMethod]
    public void LongDescription_uses_FullDescription()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FullDescription = "this is full",
        MainSummary = "this is main"
      }).LongDescription.ShouldEqual("this is full");
    }

    [TestMethod]
    public void ShortDescription_uses_ShortDescription()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        FullDescription = "this is full",
        MainSummary = "this is main"
      }).ShortDescription.ShouldEqual("this is main");
    }

    [TestMethod]
    public void Ownership_converts_propertytenure_when_property_is_commercial()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Freehold"
      }).Ownership.ShouldEqual(OwnershipType.Freehold);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Long LeaseHold"
      }).Ownership.ShouldEqual(OwnershipType.LongLeasehold);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "NotAValidOption"
      }).Ownership.ShouldEqual(OwnershipType.None);
    }

    [TestMethod]
    public void Ownership_converts_propertytenure_when_property_is_commercial_for_sale()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Freehold"
      }).Ownership.ShouldEqual(OwnershipType.Freehold);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Long Leasehold"
      }).Ownership.ShouldEqual(OwnershipType.LongLeasehold);
    }

    [TestMethod]
    public void Ownership_returns_none_when_property_is_to_let()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Lettings",
        PropertyTenure = "6"
      }).Ownership.ShouldEqual(OwnershipType.None);
    }

    [TestMethod]
    public void ListingType_returns_listingtype_when_property_is_commercial()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 2
      }).Branch.ListingType.ShouldEqual(ListingType.Sale);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 5
      }).Branch.ListingType.ShouldEqual(ListingType.Sale);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 6
      }).Branch.ListingType.ShouldEqual(ListingType.Sale);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 7
      }).Branch.ListingType.ShouldEqual(ListingType.Sale);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 8
      }).Branch.ListingType.ShouldEqual(ListingType.Sale);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 3
      }).Branch.ListingType.ShouldEqual(ListingType.Letting);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 9
      }).Branch.ListingType.ShouldEqual(ListingType.Letting);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 10
      }).Branch.ListingType.ShouldEqual(ListingType.Letting);
    }

    [TestMethod]
    public void ListingType_returns_listingtype_when_property_is_not_commercial()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Sales"
      }).Branch.ListingType.ShouldEqual(ListingType.Sale);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Lettings"
      }).Branch.ListingType.ShouldEqual(ListingType.Letting);
    }

    [TestMethod]
    public void Cost_returns_value_when_property_is_commercial()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        PriceTo = 123456,
        Availability = 2, // for sale
      }).Cost.ShouldEqual(123456);
    }

    [TestMethod]
    public void Cost_returns_value_when_property_is_sale()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Sales",
        Price = 98798
      }).Cost.ShouldEqual(98798);
    }

    [TestMethod]
    public void Cost_is_converted_to_price_per_week_when_letting()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Lettings",
        Rent = 200,
        RentFrequency = "1" // pcm
      }).Cost.ShouldEqual(46.15M);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Lettings",
        Rent = 1500,
        RentFrequency = "3" // pa
      }).Cost.ShouldEqual(28.85M);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Lettings",
        Rent = 400,
        RentFrequency = "2" // pw
      }).Cost.ShouldEqual(400M);
    }

    [TestMethod]
    public void Cost_is_converted_to_price_per_week_when_commercial_letting()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 3, // to let
        RentTo = 1000,
        RentFrequency = "pa"
      }).Cost.ShouldEqual(19.23M);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 3, // to let
        RentTo = 500,
        RentFrequency = "pcm"
      }).Cost.ShouldEqual(115.38M);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        Department = "Commercial",
        Availability = 3, // to let
        RentTo = 500,
        RentFrequency = "fooBar" // not valid
      }).Cost.ShouldEqual(500M);
    }

    [TestMethod]
    public void Position_returns_value_based_on_propertystyle()
    {
      // detached

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.DetachedBungalow
      }).Position.ShouldEqual(PropertyPosition.Detached);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.DetachedHouse
      }).Position.ShouldEqual(PropertyPosition.Detached);

      // eo terrace

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.EndTerracedBungalow
      }).Position.ShouldEqual(PropertyPosition.EndOfTerrace);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.EndTerracedHouse
      }).Position.ShouldEqual(PropertyPosition.EndOfTerrace);

      // mid terraced

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.MidTerracedBungalow
      }).Position.ShouldEqual(PropertyPosition.MidTerraced);

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.MidTerracedHouse
      }).Position.ShouldEqual(PropertyPosition.MidTerraced);

      // link detached

      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.LinkDetached
      }).Position.ShouldEqual(PropertyPosition.LinkDetached);
    }

    [TestMethod]
    public void PropertyType_returns_value_based_on_propertystyle()
    {
      new ApiPropertyEntity(new restlessmedia.Abstractions.Provider.ApiProperty.ApiProperty
      {
        PropertyStyle = PropertyStyle.Apartment
      }).PropertyType.ShouldEqual(PropertyType.Apartment);
    }
  }
}