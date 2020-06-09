using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.Property.Vebra.Tests
{
  public class ApiPropertyEntityTests
  {
    public ApiPropertyEntityTests() { }

    [Fact]
    public void SquareFootage_converts_when_FloorArea_is_in_metres()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FloorArea = 1,
        FloorAreaUnits = FloorAreaUnits.SqM
      }).SquareFootage.MustBe(3.28084);
    }

    [Fact]
    public void SquareFootage_converts_when_FloorArea_is_in_acres()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FloorArea = 1,
        FloorAreaUnits = FloorAreaUnits.Acres
      }).SquareFootage.MustBe(43560);
    }

    [Fact]
    public void SquareFootage_converts_when_FloorArea_is_in_hectares()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FloorArea = 1,
        FloorAreaUnits = FloorAreaUnits.Hectares
      }).SquareFootage.MustBe(107639);
    }

    [Fact]
    public void SquareFootage_converts_when_FloorArea_is_in_feet()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FloorArea = 999,
        FloorAreaUnits = FloorAreaUnits.SqFt
      }).SquareFootage.MustBe(999);
    }

    [Fact]
    public void SquareFootage_calculates_range_when_FloorAreaFrom_is_valid()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FloorAreaFrom = 2,
        FloorAreaTo = 4,
        FloorAreaUnits = FloorAreaUnits.SqFt
      }).SquareFootage.MustBe(8);
    }

    [Fact]
    public void SquareFootage_does_not_calculates_range_when_FloorAreaFrom_is_invalid()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FloorAreaFrom = 0, // to is ignored if from is zero
        FloorAreaTo = 4,
        FloorArea = 10,
        FloorAreaUnits = FloorAreaUnits.SqFt
      }).SquareFootage.MustBe(10);
    }

    [Fact]
    public void IsCommercial_returns_true_when_department_is_commercial()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial"
      }).IsCommercial.MustBeTrue();

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "commercial"
      }).IsCommercial.MustBeTrue();

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "COMMERCIAL"
      }).IsCommercial.MustBeTrue();
    }

    [Fact]
    public void IsStudio_returns_true_when_propertystyle_is_studio()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.Studio
      }).IsStudio.MustBeTrue();
    }

    [Fact]
    public void LongDescription_uses_FullDescription()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FullDescription = "this is full",
        MainSummary = "this is main"
      }).LongDescription.MustBe("this is full");
    }

    [Fact]
    public void ShortDescription_uses_ShortDescription()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        FullDescription = "this is full",
        MainSummary = "this is main"
      }).ShortDescription.MustBe("this is main");
    }

    [Fact]
    public void Ownership_converts_propertytenure_when_property_is_commercial()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Freehold"
      }).Ownership.MustBe(OwnershipType.Freehold);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Long LeaseHold"
      }).Ownership.MustBe(OwnershipType.LongLeasehold);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "NotAValidOption"
      }).Ownership.MustBe(OwnershipType.None);
    }

    [Fact]
    public void Ownership_converts_propertytenure_when_property_is_commercial_for_sale()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Freehold"
      }).Ownership.MustBe(OwnershipType.Freehold);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        PropertyTenure = "Long Leasehold"
      }).Ownership.MustBe(OwnershipType.LongLeasehold);
    }

    [Fact]
    public void Ownership_returns_none_when_property_is_to_let()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Lettings",
        PropertyTenure = "6"
      }).Ownership.MustBe(OwnershipType.None);
    }

    [Fact]
    public void ListingType_returns_listingtype_when_property_is_commercial()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 2
      }).Branch.ListingType.MustBe(ListingType.Sale);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 5
      }).Branch.ListingType.MustBe(ListingType.Sale);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 6
      }).Branch.ListingType.MustBe(ListingType.Sale);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 7
      }).Branch.ListingType.MustBe(ListingType.Sale);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 8
      }).Branch.ListingType.MustBe(ListingType.Sale);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 3
      }).Branch.ListingType.MustBe(ListingType.Letting);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 9
      }).Branch.ListingType.MustBe(ListingType.Letting);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 10
      }).Branch.ListingType.MustBe(ListingType.Letting);
    }

    [Fact]
    public void ListingType_returns_listingtype_when_property_is_not_commercial()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Sales"
      }).Branch.ListingType.MustBe(ListingType.Sale);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Lettings"
      }).Branch.ListingType.MustBe(ListingType.Letting);
    }

    [Fact]
    public void Cost_returns_value_when_property_is_commercial()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        PriceTo = 123456,
        Availability = 2, // for sale
      }).Cost.MustBe(123456);
    }

    [Fact]
    public void Cost_returns_value_when_property_is_sale()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Sales",
        Price = 98798
      }).Cost.MustBe(98798);
    }

    [Fact]
    public void Cost_is_converted_to_price_per_week_when_letting()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Lettings",
        Rent = 200,
        RentFrequency = "1" // pcm
      }).Cost.MustBe(200);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Lettings",
        Rent = 1500,
        RentFrequency = "3" // pa
      }).Cost.MustBe(28.85M);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Lettings",
        Rent = 400,
        RentFrequency = "2" // pw
      }).Cost.MustBe(400M);
    }

    [Fact]
    public void Cost_is_converted_to_price_per_week_when_commercial_letting()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 3, // to let
        RentTo = 1000,
        RentFrequency = "pa"
      }).Cost.MustBe(19.23M);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 3, // to let
        RentTo = 500,
        RentFrequency = "pcm"
      }).Cost.MustBe(115.38M);

      new ApiPropertyEntity(new ApiProperty
      {
        Department = "Commercial",
        Availability = 3, // to let
        RentTo = 500,
        RentFrequency = "fooBar" // not valid
      }).Cost.MustBe(500M);
    }

    [Fact]
    public void Position_returns_value_based_on_propertystyle()
    {
      // detached

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.DetachedBungalow
      }).Position.MustBe(PropertyPosition.Detached);

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.DetachedHouse
      }).Position.MustBe(PropertyPosition.Detached);

      // eo terrace

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.EndTerracedBungalow
      }).Position.MustBe(PropertyPosition.EndOfTerrace);

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.EndTerracedHouse
      }).Position.MustBe(PropertyPosition.EndOfTerrace);

      // mid terraced

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.MidTerracedBungalow
      }).Position.MustBe(PropertyPosition.MidTerraced);

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.MidTerracedHouse
      }).Position.MustBe(PropertyPosition.MidTerraced);

      // link detached

      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.LinkDetached
      }).Position.MustBe(PropertyPosition.LinkDetached);
    }

    [Fact]
    public void PropertyType_returns_value_based_on_propertystyle()
    {
      new ApiPropertyEntity(new ApiProperty
      {
        PropertyStyle = PropertyStyle.Apartment
      }).PropertyType.MustBe(PropertyType.Apartment);
    }
  }
}