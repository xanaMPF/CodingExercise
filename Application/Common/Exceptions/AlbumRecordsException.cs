using System;

namespace CodingExercise.Application.Common.Exceptions
{
    public class AlbumRecordsException : Exception
    {
        public AlbumRecordsException()
         : base("Unable to get the Album records")
        {
        }
    }
}