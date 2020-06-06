using restlessmedia.Module.Address;
using System;
using System.Linq;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiPropertyEntity : PropertyEntity
  {
    public ApiPropertyEntity(ApiProperty apiProperty, IProperty property = null)
    {
      _apiProperty = apiProperty ?? throw new ArgumentNullException(nameof(apiProperty));

      Automated = true;

      if (property != null)
      {
        PropertyId = property.PropertyId;
        DevelopmentId = property.DevelopmentId;
      }
    }

    public override int? PropertyId { get; set; }

    public override bool Featured
    {
      get
      {
        return _apiProperty.FeaturedProperty == 1;
      }
    }

    public override bool IsCommercial
    {
      get
      {
        const string commercial = "commercial";
        return string.Equals(_apiProperty.Department, commercial, StringComparison.OrdinalIgnoreCase);
      }
    }

    public override bool IsStudio
    {
      get
      {
        return _apiProperty.PropertyStyle == PropertyStyle.Studio;
      }
    }

    public override string LongDescription
    {
      get
      {
        if (_apiProperty.FullDescription == null || string.Equals(StringHelper.StripXmlTags(_apiProperty.FullDescription), "description", StringComparison.OrdinalIgnoreCase))
        {
          return null;
        }

        return _apiProperty.FullDescription.Trim();
      }
    }

    public override string ShortDescription
    {
      get
      {
        if (string.IsNullOrWhiteSpace(_apiProperty.MainSummary) || string.Equals(StringHelper.StripXmlTags(_apiProperty.MainSummary), "description", StringComparison.OrdinalIgnoreCase))
        {
          return null;
        }

        return _apiProperty.MainSummary.Trim();
      }
    }

    public override OwnershipType Ownership
    {
      get
      {
        if (!string.IsNullOrEmpty(_apiProperty.PropertyTenure))
        {
          if (IsCommercial)
          {
            const string freehold = "freehold";
            const string longLeasehold = "long leasehold";

            switch (_apiProperty.PropertyTenure.ToLower())
            {
              case freehold:
                return OwnershipType.Freehold;
              case longLeasehold:
                return OwnershipType.LongLeasehold;
            }
          }
          else if (ListingType == ListingType.Sale)
          {
            PropertyTenure tenure;

            if (Enum.TryParse(_apiProperty.PropertyTenure, out tenure))
            {
              switch (tenure)
              {
                case PropertyTenure.Commonhold:
                  return OwnershipType.Commonhold;
                case PropertyTenure.FlyingFreehold:
                  return OwnershipType.FlyingFreehold;
                case PropertyTenure.Freehold:
                  return OwnershipType.Freehold;
                case PropertyTenure.Leasehold:
                  return OwnershipType.Leasehold;
                case PropertyTenure.ShareOfFreehold:
                  return OwnershipType.ShareOfFreehold;
                case PropertyTenure.ShareTransfer:
                  return OwnershipType.ShareTransfer;
              }
            }
          }
        }

        return OwnershipType.None;
      }
    }

    public override PropertyType? PropertyType
    {
      get
      {
        switch (_apiProperty.PropertyStyle)
        {
          case PropertyStyle.Apartment:
            return Property.PropertyType.Apartment;
          case PropertyStyle.BarnConversion:
            return Property.PropertyType.BarnConversion;
          case PropertyStyle.Bedsit:
            return Property.PropertyType.Bedsit;
          case PropertyStyle.BuildingPlotOrLand:
            return Property.PropertyType.BuildingPlotOrLand;
          case PropertyStyle.Chalet:
            return Property.PropertyType.Chalet;
          case PropertyStyle.Cottage:
            return Property.PropertyType.Cottage;
          case PropertyStyle.DetachedBungalow:
          case PropertyStyle.EndTerracedBungalow:
          case PropertyStyle.MidTerracedBungalow:
            return Property.PropertyType.Bungalow;
          case PropertyStyle.DetachedHouse:
          case PropertyStyle.EndTerracedHouse:
          case PropertyStyle.MidTerracedHouse:
            return Property.PropertyType.House;
          case PropertyStyle.Equestrian:
            return Property.PropertyType.Equestrian;
          case PropertyStyle.FarmHouse:
            return Property.PropertyType.FarmHouse;
          case PropertyStyle.Flat:
            return Property.PropertyType.Flat;
          case PropertyStyle.Garage:
            return Property.PropertyType.Garage;
          case PropertyStyle.GroundFloorFlat:
            return Property.PropertyType.GroundFloorFlat;
          case PropertyStyle.GroundFloorMaisonette:
            return Property.PropertyType.GroundFloorMaisonette;
          case PropertyStyle.HouseBoat:
            return Property.PropertyType.HouseBoat;
          case PropertyStyle.Maisonette:
            return Property.PropertyType.Maisonette;
          case PropertyStyle.ManorHouse:
            return Property.PropertyType.ManorHouse;
          case PropertyStyle.Mews:
            return Property.PropertyType.Mews;
          case PropertyStyle.MobileHome:
            return Property.PropertyType.MobileHome;
          case PropertyStyle.Parking:
            return Property.PropertyType.Parking;
          case PropertyStyle.Penthouse:
            return Property.PropertyType.Penthouse;
          case PropertyStyle.SharedFlat:
            return Property.PropertyType.SharedFlat;
          case PropertyStyle.SharedHouse:
            return Property.PropertyType.SharedHouse;
          case PropertyStyle.ShelteredHousing:
            return Property.PropertyType.ShelteredHousing;
          case PropertyStyle.Studio:
            return Property.PropertyType.Studio;
          case PropertyStyle.TownHouse:
            return Property.PropertyType.TownHouse;
          case PropertyStyle.UnconvertedBarn:
            return Property.PropertyType.UnconvertedBarn;
          case PropertyStyle.Villa:
            return Property.PropertyType.Villa;
          default:
            return Property.PropertyType.Villa;
        }
      }
    }

    public override PropertyPosition Position
    {
      get
      {
        switch (_apiProperty.PropertyStyle)
        {
          case PropertyStyle.DetachedBungalow:
          case PropertyStyle.DetachedHouse:
            return PropertyPosition.Detached;
          case PropertyStyle.SemiDetachedBungalow:
          case PropertyStyle.SemiDetachedHouse:
            return PropertyPosition.SemiDetached;
          case PropertyStyle.MidTerracedHouse:
          case PropertyStyle.MidTerracedBungalow:
            return PropertyPosition.MidTerraced;
          case PropertyStyle.EndTerracedBungalow:
          case PropertyStyle.EndTerracedHouse:
            return PropertyPosition.EndOfTerrace;
          case PropertyStyle.LinkDetached:
            return PropertyPosition.LinkDetached;
        }

        return PropertyPosition.None;
      }
    }

    public override byte ReceptionCount
    {
      get
      {
        return _apiProperty.PropertyReceptionRooms;
      }
    }

    public override double? SquareFootage
    {
      get
      {
        if (_apiProperty.FloorAreaUnits == FloorAreaUnits.None)
        {
          return null;
        }

        double floorArea;

        if (_apiProperty.FloorAreaFrom > 0)
        {
          floorArea = _apiProperty.FloorAreaFrom * _apiProperty.FloorAreaTo;
        }
        else
        {
          floorArea = _apiProperty.FloorArea;
        }

        double conversion;
        const double acres = 43560;
        const double hectares = 107639;
        const double sqM = 3.28084;
        const double equal = 1;

        switch (_apiProperty.FloorAreaUnits)
        {
          case FloorAreaUnits.Acres:
            conversion = acres;
            break;
          case FloorAreaUnits.Hectares:
            conversion = hectares;
            break;
          case FloorAreaUnits.SqM:
            conversion = sqM;
            break;
          default:
            conversion = equal;
            break;
        }

        return floorArea * conversion;
      }
    }

    public override DateTime AddedDate
    {
      get
      {
        return _apiProperty.DateLastModified;
      }
    }

    public override IAddress Address
    {
      get
      {
        if (_address == null)
        {
          _address = new AddressEntity
          {
            NameNumber = _apiProperty.AddressName ?? _apiProperty.AddressNumber,
            Address01 = _apiProperty.AddressStreet,
            Address02 = _apiProperty.Address2,
            PostCode = _apiProperty.AddressPostcode
          };
          _address.Marker.Latitude = _apiProperty.Latitude;
          _address.Marker.Longitude = _apiProperty.Longitude;
        }

        return _address;
      }
    }

    public override byte BathroomCount
    {
      get
      {
        return _apiProperty.PropertyBathrooms;
      }
    }

    public override byte BedroomCount
    {
      get
      {
        return _apiProperty.PropertyBedrooms;
      }
    }

    public override IPropertyBranch Branch
    {
      get
      {
        if (_branch == null)
        {
          _branch = new PropertyBranch
          {
            Name = _apiProperty.BranchName,
            BranchId = _apiProperty.BranchId,
            ListingType = ListingType
          };
        }

        return _branch;
      }
    }

    public override int BranchGuid
    {
      get
      {
        return Branch.BranchGuid;
      }
    }

    public override decimal Cost
    {
      get
      {
        if (IsCommercial)
        {
          const string pa = "pa";
          const string pcm = "pcm";

          if (!string.IsNullOrWhiteSpace(_apiProperty.RentFrequency))
          {
            switch (ListingType)
            {
              case ListingType.Letting:
                switch (_apiProperty.RentFrequency.ToLower())
                {
                  case pa:
                    return RentFromAnumToWeek;
                  case pcm:
                    return RentFromMonthToWeek;
                }
                break;
              case ListingType.Sale:
                switch (_apiProperty.RentFrequency.ToLower())
                {
                  case pa:
                    return PriceFromAnumToWeek;
                  case pcm:
                    return PriceFromMonthToWeek;
                }
                break;
            }
          }

          switch (ListingType)
          {
            case ListingType.Letting:
              return Rent;
            case ListingType.Sale:
              return Price;
          }
        }
        else if (ListingType == ListingType.Letting)
        {
          switch (RentFrequency)
          {
            case RentFrequency.pcm:
              // at the moment, all rent is being supplied as per week
              return Rent /*RentFromMonthToWeek*/;
            case RentFrequency.pa:
              return RentFromAnumToWeek;
            default:
              return Rent;
          }
        }
        else if (ListingType == ListingType.Sale)
        {
          return Price;
        }

        return 0;
      }
    }

    public override PropertyStatus Status
    {
      get
      {
        if (IsCommercial)
        {
          switch (CommercialAvailability)
          {
            case CommercialAvailability.ToLet:
            case CommercialAvailability.ForSale:
            case CommercialAvailability.ForSaleOrToLet:
              {
                return PropertyStatus.Available;
              }
            case CommercialAvailability.UnderOffer:
              {
                return PropertyStatus.UnderOffer;
              }
            default:
              {
                return PropertyStatus.Unavailable;
              }
          }
        }
        else if (ListingType == ListingType.Sale)
        {
          switch (SalesAvailability)
          {
            case SalesAvailability.ForSale:
              {
                return PropertyStatus.Available;
              }
            case SalesAvailability.UnderOffer:
              {
                return PropertyStatus.UnderOffer;
              }
            default:
              {
                return PropertyStatus.Unavailable;
              }
          }
        }
        else if (ListingType == ListingType.Letting)
        {
          switch (LettingsAvailability)
          {
            case LettingsAvailability.ToLet:
              {
                return PropertyStatus.Available;
              }
            default:
              {
                return PropertyStatus.Unavailable;
              }
          }
        }

        return PropertyStatus.Available;
      }
    }

    public override string Title
    {
      get
      {
        const char postCodeSeparator = ' ';
        const string separator = ", ";
        string postCodeArea = string.IsNullOrWhiteSpace(_apiProperty.AddressPostcode) ? null : _apiProperty.AddressPostcode.Split(new char[] { postCodeSeparator }).First();
        return string.Join(separator, new string[3] { _apiProperty.AddressStreet, _apiProperty.Address2, postCodeArea }.Where(p => !string.IsNullOrEmpty(p)));
      }
    }

    public override int? DevelopmentId { get; set; }

    private RentFrequency RentFrequency
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(_apiProperty.RentFrequency))
        {
          int value;
          RentFrequency frequency;

          if (IsCommercial && Enum.TryParse(_apiProperty.RentFrequency, out frequency))
          {
            return frequency;
          }

          if (int.TryParse(_apiProperty.RentFrequency, out value))
          {
            return (RentFrequency)value;
          }
        }

        return RentFrequency.None;
      }
    }

    private ListingType ListingType
    {
      get
      {
        if (IsCommercial)
        {
          switch (CommercialAvailability)
          {
            case CommercialAvailability.ForSale:
            case CommercialAvailability.UnderOffer:
            case CommercialAvailability.SoldSTC:
            case CommercialAvailability.Exchanged:
            case CommercialAvailability.Completed:
            case CommercialAvailability.ForSaleOrToLet:
              return ListingType.Sale;
            case CommercialAvailability.ToLet:
            case CommercialAvailability.Let:
            case CommercialAvailability.LetAgreed:
              return ListingType.Letting;
          }
        }
        else if (!string.IsNullOrEmpty(_apiProperty.Department))
        {
          const string lettings = "lettings";
          const string sales = "sales";
          const string agricultural = "agricultural";

          switch (_apiProperty.Department.ToLower())
          {
            case lettings:
              return ListingType.Letting;
            case sales:
            case agricultural:
              return ListingType.Sale;
          }
        }

        return ListingType.None;
      }
    }

    private CommercialAvailability CommercialAvailability
    {
      get
      {
        return (CommercialAvailability)_apiProperty.Availability;
      }
    }

    private LettingsAvailability LettingsAvailability
    {
      get
      {
        return (LettingsAvailability)_apiProperty.Availability;
      }
    }

    private SalesAvailability SalesAvailability
    {
      get
      {
        return (SalesAvailability)_apiProperty.Availability;
      }
    }

    private decimal PriceFromMonthToWeek
    {
      get
      {
        return Math.Round(decimal.Divide(decimal.Multiply(Price, _monthsInYear), _weeksInYear), _costPrecision);
      }
    }

    private decimal PriceFromAnumToWeek
    {
      get
      {
        return Math.Round(decimal.Divide(Price, _weeksInYear), _costPrecision);
      }
    }

    private decimal RentFromMonthToWeek
    {
      get
      {
        return Math.Round(decimal.Divide(decimal.Multiply(Rent, _monthsInYear), _weeksInYear), _costPrecision);
      }
    }

    private decimal RentFromAnumToWeek
    {
      get
      {
        return Math.Round(decimal.Divide(Rent, _weeksInYear), _costPrecision);
      }
    }

    private decimal Price
    {
      get
      {
        if (IsCommercial)
        {
          return _apiProperty.PriceTo;
        }

        return _apiProperty.Price;
      }
    }

    private decimal Rent
    {
      get
      {
        if (IsCommercial)
        {
          return _apiProperty.RentTo;
        }

        return _apiProperty.Rent;
      }
    }

    private readonly ApiProperty _apiProperty;

    private IAddress _address;

    private IPropertyBranch _branch;

    private const int _weeksInYear = 52;

    private const int _monthsInYear = 12;

    private const int _costPrecision = 2;
  }
}