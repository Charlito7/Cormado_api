using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers.Security;

public class HashPassword
{
    public HashSalt HashText(string plainText)
    {
        try
        {
            byte[] salt = new byte[128 / 8]; // makes the salt as complicated as possible (high bit): 128-bit
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //add the salt to the password before you hash it. 
            // you do not hash then add the salt. 

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainText,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            return new HashSalt { Hash = hashedPassword, Salt = salt };
        }
        catch
        {
            throw;
        }
    }


    //get the salt
    // get the hashed password
    //hash the entered text using the same salt
    //compare the hashed entered text to the hashed password
    public bool VerifyText(string enteredText, byte[] salt, string storedHashedPassword)
    {
        try
        {
            string hashedUserInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredText,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
            return hashedUserInput == storedHashedPassword;
        }
        catch
        {
            throw;
        }
    }


}
