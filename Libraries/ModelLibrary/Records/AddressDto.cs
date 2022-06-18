using Domain.Enums;
using FluentValidation;

namespace ModelLibrary.Records;

public class AddressDto
{
    public string FullAddress
    {
        get
        {
            var address = "";
            if (UnitNumber != "") address += UnitNumber + " - ";
            if (BuildingNumber != "") address += BuildingNumber + " ";
            if (StreetName != "") address += StreetName + ", ";
            if (City != "") address += City + ", ";
            if (Province != ProvinceEnum.None) address += Province + ", ";
            if (PostalCode != "") address += PostalCode;

            return address;
        }
    }
    public ProvinceEnum Province { get; set; } = ProvinceEnum.None;
    private string postalCode = string.Empty;
    public string PostalCode
    {
        get => postalCode;
        set => postalCode = value.Trim();
    }
    private string city = string.Empty;
    public string City
    {
        get => city;
        set => city = value.Trim();
    }
    private string streetName = string.Empty;
    public string StreetName
    {
        get => streetName;
        set => streetName = value.Trim();
    }
    private string buildingNumber = string.Empty;
    public string BuildingNumber
    {
        get => buildingNumber;
        set => buildingNumber = value.Trim();
    }
    private string unitNumber = string.Empty;
    public string UnitNumber
    {
        get => unitNumber;
        set => unitNumber = value.Trim();
    }
}

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator(bool searchModel = false)
    {
        if (!searchModel)
        {
            RuleFor(x => x.Province)
                .Must((model, province) => (province == ProvinceEnum.None &&
                        model.FullAddress != "") ? false : true)
                .WithMessage("Province not selected");
            RuleFor(x => x.UnitNumber)
                .Must((number) =>
                    number == "" ? true : RegExp.AddressNumber.IsMatch(number))
                .WithMessage("Incorrect Unit Number format");
        }

        RuleFor(x => x.BuildingNumber)
            .Must((model, number) =>
                number == "" && model.FullAddress != "" ? false : true)
            .WithMessage("Building Number cannot be empty")
            .Must((number) =>
                number == "" ? true : RegExp.AddressNumber.IsMatch(number))
            .WithMessage("Incorrect Building Number format");

        RuleFor(x => x.StreetName)
            .Cascade(CascadeMode.Stop)
            .Must((model, street) =>
                street == "" && model.FullAddress != "" ? false : true)
            .WithMessage("Street Name cannot be empty")
            .MaximumLength(50)
            .WithMessage("Street Nane max length must be 50 characters")
            .Must((model, address) =>
                model.FullAddress == "" ? true : RegExp.StreetName
                .IsMatch(address))
            .WithMessage("Incorrect Street Name format");

        if (!searchModel)
        {
            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .Must((model, city) =>
                    city == "" && model.FullAddress != "" ? false : true)
                .WithMessage("City Name cannot be empty")
                .MaximumLength(20)
                .WithMessage("City Name max length must be 20 characters")
                .Must((city) => city == "" ? true : RegExp.City.IsMatch(city))
                .WithMessage("Incorrect City Name format");
        }

        RuleFor(x => x.PostalCode)
            .Cascade(CascadeMode.Stop)
            .Must((model, code) => (code == ""
                && model.FullAddress != "") ? false : true)
            .WithMessage("Postal Code cannot be empty")
            .Must((code) => code == "" ? true : RegExp.PostalCode
                .IsMatch(code))
            .WithMessage("Incorrect Postal Code format");
    }
}
