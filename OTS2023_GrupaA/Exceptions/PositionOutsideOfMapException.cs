using System;

namespace OTS2026_GrupaA.Exceptions
{
    public class PositionOutsideOfMapException: Exception
    {
        public PositionOutsideOfMapException() 
        {

        }

        public PositionOutsideOfMapException(string message) : base(message)
        {

        }
    }
}
