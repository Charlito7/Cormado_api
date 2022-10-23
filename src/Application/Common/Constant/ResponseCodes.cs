using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Constant;

public enum ResponseCodes
{
    Success = 200,
    ValidUser = 201,
    EmptyFields = 400,
    PasswordMismatch,
    WrongPassword,
    AgeRequirement,
    ServerFailure,
    InvalidEmail,
    InvalidOTPAttempt,
    UserSuspended,
    EmailVerificationRequired,
    InvalidUser,
    InvalidAttempt,
}
