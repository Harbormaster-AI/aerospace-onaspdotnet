using System.ComponentModel.DataAnnotations.Schema;

namespace aerospaceonaspdotnet.Domain;


    [ComplexType]
    public record MSN(
    string Value
    );

    [ComplexType]
    public record TailNumber(
    string Value,
    string Country
    );

    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

