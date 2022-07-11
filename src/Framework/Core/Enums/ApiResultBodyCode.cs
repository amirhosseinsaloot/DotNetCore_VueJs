using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum ApiResultBodyCode : byte
{
    [Display(Name = "Operation done successfully")]
    Success = 1,

    [Display(Name = "Server error occurred")]
    ServerError = 2,

    [Display(Name = "Invalid arguments")]
    BadRequest = 3,

    [Display(Name = "Not found")]
    NotFound = 4,

    [Display(Name = "Authentication error")]
    UnAuthorized = 5,

    [Display(Name = "ExpiredSecurityToken error")]
    ExpiredSecurityToken = 6,

    [Display(Name = "Forbidden error (User does not have permission)")]
    Forbidden = 7,

    [Display(Name = "Duplication error")]
    Duplication = 8
}
