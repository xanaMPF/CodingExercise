using System;

namespace CodingExercise.Application.Common.Exceptions
{
    public class PhotoRecordsException : Exception
    {
        public PhotoRecordsException()
         : base("Unable to get the Photo records")
        {
        }
    }
}