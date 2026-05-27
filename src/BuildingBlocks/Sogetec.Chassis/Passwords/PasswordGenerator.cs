namespace Sogetec.Chassis.Passwords;

public static class PasswordGenerator
{
    public static string GeneratePassword(bool includeLowercase, bool includeUppercase, bool includeNumeric, bool includeSpecial, bool includeSpaces, int lengthOfPassword)
    {
        const int maximumIdenticalConsecutiveChars = 2;
        const string lowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        const string uppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numericCharacters = "0123456789";
        const string specialCharacters = @"!#$%&*@\";
        const string spaceCharacter = " ";
        const int passwordLengthMin = 8;
        const int passwordLengthMax = 128;

        if (lengthOfPassword is < passwordLengthMin or > passwordLengthMax)
        {
            throw new ApplicationException("Password length must be between 8 and 128.");
        }

        var characterSet = "";

        if (includeLowercase)
        {
            characterSet += lowercaseCharacters;
        }

        if (includeUppercase)
        {
            characterSet += uppercaseCharacters;
        }

        if (includeNumeric)
        {
            characterSet += numericCharacters;
        }

        if (includeSpecial)
        {
            characterSet += specialCharacters;
        }

        if (includeSpaces)
        {
            characterSet += spaceCharacter;
        }

        var password = new char[lengthOfPassword];
        var characterSetLength = characterSet.Length;

        var random = new Random();
        for (var characterPosition = 0; characterPosition < lengthOfPassword; characterPosition++)
        {
            password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

            var moreThanTwoIdenticalInARow = characterPosition > maximumIdenticalConsecutiveChars &&
                                             password[characterPosition] == password[characterPosition - 1] &&
                                             password[characterPosition - 1] == password[characterPosition - 2];

            if (moreThanTwoIdenticalInARow)
            {
                characterPosition--;
            }
        }

        return string.Join(null, password);
    }
}
