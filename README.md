## What is the Quiron.Extensions?

Package that compiles several resources repeated many times in projects.

## Give a Star! ⭐

If you find this project useful, please give it a star! It helps us grow and improve the community.

## Namespaces and Dependencies

- ✅ Quiron.Extensions
- ✅ Newtonsoft.Json
- ✅ System.Drawing

## Extensions

- ✅ DateExtension
- ✅ EmailExtension
- ✅ EnumExtension
- ✅ ExceptionExtension
- ✅ HttpResponseMessageExtension
- ✅ ListExtension
- ✅ StreamExtension
- ✅ StringExtension

## Some Basic Examples

### DateExtension
```csharp
var otherDate = DateTime.Parse("2025-01-01");
bool isToday = otherDate.EqualToday();
bool isTodayOrMinus = otherDate.EqualTodayOrMinus();
bool isTodayOrBigger = otherDate.EqualTodayOrBigger();
bool isBigger = otherDate.IsDateBigger();
bool isMinus = otherDate.IsDateMinus();
int weekly = otherDate.Weekly();
```

### StringExtension
```csharp
var text = "Package that compiles several resources repeated many times in projects";
string part = text.ToStr(5);
Console.WhiteLine("Packa");
```

### EmailExtension
```csharp
var email1 = "test@c";
bool invalid = email1.IsValid();
Console.WhiteLine("false");

var email2 = "test@quiron.com";
bool valid = email2.IsValid();
Console.WhiteLine("true");
```

Supports:

- ✅ .NET Standard 2.1  
- ✅ .NET 9 through 9 (including latest versions)  
- ⚠️ Legacy support for .NET Core 3.1 and older (with limitations)
  
## About
Quiron.Extensions was developed by [EliasRMJ](https://www.linkedin.com/in/elias-medeiros-98232066/) under the [MIT license](LICENSE).
