using System;

namespace Helpers
{
    public class PasswordPolicy
    {
        public int LowerLimit  { get; set; }
        public int UpperLimit { get; set; }
        public char RequiredCharacter { get; set; }
        public string password { get; set; }
        public bool IsValid { get; set; }

        public void ComputeIsValid()
        {
            var isValid = false;
            var done = false;
            if ((password.Contains(RequiredCharacter)==false) || password.Length < LowerLimit)
            {
                isValid = false;
                done = true;
            }
            

            if(done == false)
            {
                var cArr = password.ToCharArray();
                var reqCount = 0;
                foreach (var c in cArr)
                {
                    if (c.Equals(RequiredCharacter)) reqCount++;
                }

                //if((LowerLimit <= reqCount) && ( reqCount <= UpperLimit)){
                //    done = true;
                //    isValid = true;
                //}

                try { 
                    if (cArr[LowerLimit-1].Equals(RequiredCharacter) ^ cArr[UpperLimit-1].Equals(RequiredCharacter)) {
                        done = true;
                        isValid = true;
                    }
                } catch(Exception e)
                {

                }
            }

            IsValid = isValid;
        }
    }
}
